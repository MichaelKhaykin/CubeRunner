using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class GameFont : GameObject
    {
        public Vector2 InitScale;

        public SpriteFont Font { get; set; }
        public StringBuilder Text { get; set; }
        public List<Letter> Letters { get; set; }

        public bool UseText { get; set; } = true;

        public GameFont(SpriteFont font, string text, Vector2 position, Color color, Vector2 scale, Vector2 origin) 
            : base(position, color, scale, Vector2.Zero)
        {
            Font = font;
            Text = new StringBuilder(text);

            Letters = new List<Letter>();
            
            Vector2 prev = Vector2.Zero;

            Origin = origin * font.MeasureString(text);

            for(int i = 0; i < text.Length; i++)
            {
                Letters.Add(new Letter(Font, text[i], new Vector2(Position.X + prev.X, Position.Y), Color, origin, scale));
                prev += Font.MeasureString(Letters[i].ToString());
            }

            InitScale = scale;
        }

        public override void Update(GameObject objectToOperateOn, GameTime gameTime)
        {
            float prev = 0;

            foreach (var letter in Letters)
            {
                letter.Position = new Vector2(Position.X + prev, Position.Y);
                prev += Font.MeasureString(letter.Value.ToString()).X;
            }

            base.Update(objectToOperateOn, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (UseText)
            {
                spriteBatch.DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
            }
            else
            {
                foreach (var letter in Letters)
                {
                    letter.Draw(spriteBatch);
                }
            }
        }
    }
}
