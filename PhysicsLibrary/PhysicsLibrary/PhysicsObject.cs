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

        Vector2 impulse;

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


            //calculate contact force
            //calculate friction force
            //apply forces to velocities

        }

        public void DrawImpulse(SpriteBatch spriteBatch, Texture2D pixel)
        {
            //var angle = (float)(Math.Atan2(impulse.Y, impulse.X));

            //spriteBatch.Draw(pixel, Center, null, null, new Vector2(0, 0.5f), angle, new Vector2(impulse.Length(), 1), Microsoft.Xna.Framework.Color.Red, SpriteEffects.None, 0);
        }

        public virtual bool Intersects(PhysicsObject other)
        {
            return Hitbox.IntersectsWith(other.Hitbox);
        }
    }
}