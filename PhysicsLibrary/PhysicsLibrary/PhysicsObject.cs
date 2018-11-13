using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PhysicsLibrary
{
    public class PhysicsObject
    {
        public RectangleF Hitbox;
        public Vector2 Position => new Vector2(Hitbox.X, Hitbox.Y);

        public Vector2 Velocity;
        public float Mass;
        public float FrictionCoefficient;
        
        public PhysicsObject(RectangleF hitbox, Vector2 velocity, float mass, float frictionCoefficient)
        {
            Hitbox = hitbox;
        }

        public virtual void Update()
        {
            Hitbox.Location = new PointF(Hitbox.X + Velocity.X, Hitbox.Y + Velocity.Y);
            Velocity.Y -= PhysicsConstants.Gravity;
        }
    }
}