using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsLibrary
{
    public interface IGameObject
    {
        Vector2 Position { get; }
    }

    public class PhysicsObject
    {
        public Rectangle Hitbox;

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}