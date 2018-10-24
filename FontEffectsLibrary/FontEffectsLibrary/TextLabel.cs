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
        protected LerpingColor LerpingColor;

        protected SpriteFont Font;

        private Vector2 position;
        public ref Vector2 Position
        {
            get
            {
                return ref position;
            }
        }
     
        protected float Rotation { get; set; }
        
        protected string text;

        protected Vector2 Origin
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

        protected Vector2 Scale { get; set; }
        
        public TextLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, Vector2 scale)
        {
            LerpingColor = new LerpingColor(rateOfChange, colors);

            Init(font, position, text, scale);
         }

        public TextLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale)
        {
            LerpingColor = new LerpingColor(1, new List<Color>() { color });

            Init(font, position, text, scale);
        }

        private void Init(SpriteFont font, Vector2 position, string text, Vector2 scale)
        {
            Font = font;
            Position = position;
            this.text = text;
            Scale = scale;
        }

        public virtual void Update(GameTime gameTime)
        {
            LerpingColor.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, text, Position, LerpingColor.Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
