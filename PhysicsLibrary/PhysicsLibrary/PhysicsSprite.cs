using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsLibrary
{
    public class PhysicsSprite : PhysicsObject
    {
        public Texture2D Texture;
        public Microsoft.Xna.Framework.Color Tint;

        public PhysicsSprite(Vector2 position, Texture2D texture, Microsoft.Xna.Framework.Color tint, Vector2 velocity, float mass, float elasticity)
            : base(new RectangleF(position.X, position.Y, texture.Width, texture.Height), velocity, mass, elasticity)
        {
            Texture = texture;
            Tint = tint;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Tint);
        }
    }
}