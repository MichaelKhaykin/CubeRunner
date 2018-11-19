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

        public Vector2 Velocity;
        public float Mass;
        public float FrictionCoefficient;
        
        public PhysicsObject(RectangleF hitbox, Vector2 velocity, float mass, float frictionCoefficient)
        {
            Hitbox = hitbox;
            Velocity = velocity;
            Mass = mass;
            FrictionCoefficient = frictionCoefficient;
        }

        public virtual void Update()
        {
            Hitbox.Location = new PointF(Hitbox.X + Velocity.X, Hitbox.Y + Velocity.Y);
            if (Mass != float.PositiveInfinity)
            {
                Velocity.Y -= PhysicsConstants.Gravity;
            }
        }

        public virtual void UpdateRelative(ref PhysicsObject other)
        {
            if (!Hitbox.IntersectsWith(other.Hitbox))
                return;
            if (Mass == float.PositiveInfinity && other.Mass == float.PositiveInfinity)
                return;
            Vector2 difference = other.Center - Center;
            float angle = (float)Math.Atan2(Math.Abs(difference.Y), Math.Abs(difference.X));
            Vector2 impulse = Vector2.Transform(new Vector2(0, Vector2.Distance(other.Center, Center)), Matrix.CreateRotationZ(angle));
            if (Mass == float.PositiveInfinity)
            {
                other.Velocity = -other.Velocity;
            }
            else if (other.Mass == float.PositiveInfinity)
            {
                Velocity = -Velocity;
            }
            else
            {
                float massRatio = Mass / other.Mass;
                Position += impulse / massRatio;
                other.Position -= impulse * other.Mass;
            }
        }

        public virtual bool Intersects(PhysicsObject other)
        {
            return Hitbox.IntersectsWith(other.Hitbox);
        }
    }
}