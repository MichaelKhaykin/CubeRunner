using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PhysicsLibrary
{
    public static class PointFExtensions
    {
        public static Vector2 ToVector2(this PointF point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static PointF ToPointF(this Vector2 vector)
        {
            return new PointF(vector.X, vector.Y);
        }
    }

    public class PhysicsObject
    {
        public RectangleF Hitbox;
        public Vector2 Velocity;
        public float Mass;
        public float Restitution;

        public Vector2 Position
        {
            get => new Vector2(Hitbox.X, Hitbox.Y);
            set
            {
                Hitbox.X = value.X;
                Hitbox.Y = value.Y;
            }
        }
        public Vector2 Center => Hitbox.Location.ToVector2() + new Vector2(Hitbox.Width / 2f, Hitbox.Height / 2f);
        public Vector2 Momentum => Velocity * Mass;

        public PhysicsObject(RectangleF hitbox, Vector2 velocity, float mass, float restitution)
        {
            Hitbox = hitbox;
            Velocity = velocity;
            Mass = mass;
            Restitution = Math.Min(Math.Max(restitution, 0), 1);
        }

        public virtual void Update()
        {
            Hitbox.Location = new PointF(Hitbox.X + Velocity.X, Hitbox.Y + Velocity.Y);
            if (Mass != float.PositiveInfinity)
            {
                Velocity.Y -= PhysicsConstants.Gravity;
            }
            Velocity *= 1 - PhysicsConstants.Drag;
        }
        
        class Ray
        {
            public PointF Position;
            public Vector2 Direction;

            public Ray(PointF position, Vector2 direction)
            {
                Position = position;
                Direction = Vector2.Normalize(direction);
            }
        }

        float[] rayTrace(RectangleF AABB, Ray ray)
        {
            float t;
            Vector2 dirfrac;
            dirfrac = Vector2.One / ray.Direction;
            if (dirfrac.X.Equals(float.NaN))
            {
                dirfrac.X = float.NegativeInfinity;
            }
            if (dirfrac.Y.Equals(float.NaN))
            {
                dirfrac.Y = float.NegativeInfinity;
            }
            float t1 = (AABB.Location.X - ray.Position.X) * dirfrac.X;
            float t2 = ((AABB.Location + AABB.Size).X - ray.Position.X) * dirfrac.X;
            float t3 = (AABB.Location.Y - ray.Position.Y) * dirfrac.Y;
            float t4 = ((AABB.Location + AABB.Size).Y - ray.Position.Y) * dirfrac.Y;

            float tmin = Math.Max(Math.Min(t1, t2), Math.Min(t3, t4));
            float tmax = Math.Min(Math.Max(t1, t2), Math.Max(t3, t4));

            if (tmax < 0)
            {
                t = tmax;
            }
            
            if (tmin > tmax)
            {
                t = tmax;
            }

            t = tmin;
            return new float[] { t, t1, t2, t3, t4 };
        }
        
        public virtual void UpdateRelative(ref PhysicsObject other)
        {
            if (!Hitbox.IntersectsWith(other.Hitbox))
            {
                return;
            }
            if (Mass == float.PositiveInfinity && other.Mass == float.PositiveInfinity)
            {
                return;
            }

            RectangleF combinedAABB = new RectangleF(Position.X, Position.Y, Hitbox.Width + other.Hitbox.Width, Hitbox.Height + other.Hitbox.Height);
            PointF combinedPoint = new PointF(other.Position.X, other.Position.Y);
            Vector2 combinedVelocity = other.Velocity - Velocity;
            float[] tValues = rayTrace(combinedAABB, new Ray(combinedPoint, combinedVelocity));
            if (tValues.Length == 0)
            {
                //no intersection
                return;
            }
            Vector2 collisionNormal = Vector2.Zero;

            if (tValues[0].Equals(tValues[1]))
            {
                collisionNormal = new Vector2(1, 0);
            }
            else if (tValues[0].Equals(tValues[2]))
            {
                collisionNormal = new Vector2(-1, 0);
            }
            else if (tValues[0].Equals(tValues[3]))
            {
                collisionNormal = new Vector2(0, -1);
            }
            else if (tValues[0].Equals(tValues[4]))
            {
                collisionNormal = new Vector2(0, 1);
            }
            else
            {
                throw new Exception("Collision normal broke");
            }

            float restitution = Math.Min(Restitution, other.Restitution);

            float impulseMagnitude = Vector2.Dot((-(1 + restitution) * combinedVelocity), collisionNormal) / Vector2.Dot(collisionNormal, collisionNormal * (1f / Mass + 1f / other.Mass));

            other.Velocity += impulseMagnitude / other.Mass * collisionNormal;
            Velocity -= impulseMagnitude / Mass * collisionNormal;
        }
        
        public virtual bool Intersects(PhysicsObject other)
        {
            return Hitbox.IntersectsWith(other.Hitbox);
        }
    }
}