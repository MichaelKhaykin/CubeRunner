using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class TypeWriterLabel : TextLabel
    {
        StringBuilder TextBuilder;
        int index = 1;
        string currentText = "";

        TimeSpan timeBetweenNextLetter;
        TimeSpan elapsedTime;

        public TypeWriterLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, TimeSpan timeBetweenLetter) 
            : base(font, position, color, text, scale)
        {
            TextBuilder = new StringBuilder(text);
            timeBetweenNextLetter = timeBetweenLetter;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if(elapsedTime >= timeBetweenNextLetter && index < TextBuilder.Length + 1)
            {
                currentText = TextBuilder.Slice(0, index);
                elapsedTime = TimeSpan.Zero;
                index++;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, currentText, Position, LerpingColor.Color);
        }
    }
}
