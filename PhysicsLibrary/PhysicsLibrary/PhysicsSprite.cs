using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsLibrary
{
    public class PhysicsSprite : PhysicsObject
    {
        public Texture2D Texture;
        public Color Tint;

        public PhysicsSprite(Vector2 position, Texture2D texture, Color tint, Vector2 velocity, float mass, float elasticity)
            : base(new System.Drawing.RectangleF(position.X, position.Y, texture.Width, texture.Height), velocity, mass, elasticity)
        {
            Texture = texture;
            Tint = tint;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            spriteBatch.Draw(Texture, Position, Tint);
            spriteBatch.Draw(pixel, Position, null, new Color(100, 100, 200, 100), 0f, Vector2.Zero, new Vector2(Hitbox.Width, Hitbox.Height), SpriteEffects.None, 0f);
        }
    }
}