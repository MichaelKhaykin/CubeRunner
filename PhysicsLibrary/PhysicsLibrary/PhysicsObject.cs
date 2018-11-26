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
        public Vector2 Momentum => Velocity * Mass;

        public Vector2 Velocity;
        public float Mass;
        public float FrictionCoefficient;
        public float Elasticity;
        Vector2 impulse;

        public PhysicsObject(RectangleF hitbox, Vector2 velocity, float mass, float frictionCoefficient, float elasticity)
        {
            Hitbox = hitbox;
            Velocity = velocity;
            Mass = mass;
            FrictionCoefficient = frictionCoefficient;
            Elasticity = Math.Min(Math.Max(elasticity, 0), 1);
        }

        public virtual void Update()
        {
            Hitbox.Location = new PointF(Hitbox.X + Velocity.X, Hitbox.Y + Velocity.Y);
            if (Mass != float.PositiveInfinity)
            {
                Velocity.Y -= PhysicsConstants.Gravity;
            }
        }

        public virtual void UpdateRelative(ref PhysicsObject other, SpriteBatch spriteBatch, Texture2D pixel)
        {
            if (!Hitbox.IntersectsWith(other.Hitbox))
                return;
            if (Mass == float.PositiveInfinity && other.Mass == float.PositiveInfinity)
                return;

            Vector2 difference = other.Center - Center;

            float velAlongNormal = Vector2.Dot(difference, Velocity);
            float bounce = Math.Min(Elasticity, other.Elasticity);
            float impulseScalar = -(1 + bounce) * velAlongNormal;
            impulseScalar /= (1 / Mass) + (1 / other.Mass);
            impulse = impulseScalar * Velocity;
            Velocity += (1 / Mass) * impulse * 0.01f;
            //other.Velocity += (1 / other.Mass) * impulse * 0.01f;
            
            //differenceAngle = (float)Math.Atan2(difference.Y, -difference.X);
            //impulse = Vector2.Transform(new Vector2(Vector2.Distance(other.Center, Center), 0), Matrix.CreateRotationZ(differenceAngle));


            //Vector2 netMomentum = Momentum + other.Momentum;
            //float angle = (float)Math.Atan2(Velocity.Y, Velocity.X);
            //float otherAngle = (float)Math.Atan2(other.Velocity.Y, other.Velocity.X);
            
            /*if (Mass == float.PositiveInfinity)
            {
                other.Position += impulse;
                other.Velocity = -other.Velocity * Elasticity;
            }
            else if (other.Mass == float.PositiveInfinity)
            {
                Position -= impulse;
                Velocity = -Velocity * other.Elasticity;
            }
            else
            {
                float massRatio = Mass / other.Mass;
                Velocity += impulse * massRatio;
                other.Velocity += impulse * massRatio;
                if (netMomentum != Momentum + other.Momentum)
                {
                    //throw new Exception("Momentum is not conserved.");
                }
            }*/
        }

        public void DrawImpulse(SpriteBatch spriteBatch, Texture2D pixel)
        {
            var angle = (float)(Math.Atan2(impulse.Y, impulse.X));

            spriteBatch.Draw(pixel, Center, null, null, new Vector2(0, 0.5f), angle, new Vector2(impulse.Length(), 1), Microsoft.Xna.Framework.Color.Red, SpriteEffects.None, 0);
        }

        public virtual bool Intersects(PhysicsObject other)
        {
            return Hitbox.IntersectsWith(other.Hitbox);
        }
    }
}