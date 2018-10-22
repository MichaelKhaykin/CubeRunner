using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEffectsLibrary
{
    public class TextLabel
    {
        private SpriteFont Font;
        public Vector2 Position { get; set; }

        private Queue<Color> colorsToLerpThrough;
        private Color colorToLerpFrom;

        public Color Color { get; set; }

        public float Rotation { get; set; }

        private float travelPercentage;

        private float rateOfChange;

        private string text;

        public Vector2 Origin
        {
            get
            {
                return new Vector2(Font.MeasureString(text).X / 2, Font.MeasureString(text).Y / 2);
            }
            set
            {
                Origin = value;
            }
        }

        public float Scale { get; set; }
        
        public TextLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, float scale)
        {
            Init(font, position, colors, text, scale);
            this.rateOfChange = rateOfChange;
        }

        public TextLabel(SpriteFont font, Vector2 position, Color color, string text, float scale)
        {
            Init(font, position, new List<Color>() { color }, text, scale);
        }

        private void Init(SpriteFont font, Vector2 position, List<Color> color, string text, float scale)
        {
            Font = font;
            Position = position;
            this.text = text;
            colorsToLerpThrough = new Queue<Color>(color);
            Scale = scale;
        }

        public virtual void Update(GameTime gameTime)
        {
            Color = Color.Lerp(colorToLerpFrom, colorsToLerpThrough.Peek(), travelPercentage);
            travelPercentage += rateOfChange;

            if(travelPercentage >= 1)
            {
                travelPercentage = 0f;

                colorsToLerpThrough.Enqueue(colorToLerpFrom);
                colorToLerpFrom = colorsToLerpThrough.Dequeue();
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, text, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
