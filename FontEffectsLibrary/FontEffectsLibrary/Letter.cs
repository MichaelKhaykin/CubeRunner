using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class Letter
    {
        public char Value;

        public Vector2 Position;

        public Vector2 GoalPosition;

        public float Velocity;

        public Color Color;

        public SpriteFont Font;

        protected Vector2 Origin;

        public Vector2 StartingPosition;

        public float TravelPercentage;

        public TimeSpan UpdateTime = TimeSpan.FromMilliseconds(18);

        public TimeSpan ElapsedTime;

        public float Rotation { get; set; }

        public bool IsVisible { get; set; } = true;

        public Letter(SpriteFont font, char letter, Vector2 position, Color color, Vector2 origin)
        {
            Font = font;
            Value = letter;
            Position = position;
            Color = color;
            StartingPosition = Position;
            TravelPercentage = 0f;
            Origin = origin;
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsVisible)
            {
                sb.DrawString(Font, Value.ToString(), Position, Color, Rotation, Origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
