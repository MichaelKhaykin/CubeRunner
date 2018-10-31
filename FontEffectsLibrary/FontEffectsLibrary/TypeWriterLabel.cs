using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class TypeWriterLabel : GameFont
    {
        StringBuilder TextBuilder;
        int index = 1;
        string currentText = "";

        TimeSpan timeBetweenNextLetter;
        TimeSpan elapsedTime;

        public TypeWriterLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, TimeSpan timeBetweenLetter) 
            : base(font, text, position, color, scale, Vector2.Zero)
        {
            TextBuilder = new StringBuilder(text);
            timeBetweenNextLetter = timeBetweenLetter;
        }
        
        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, currentText, Position, Color);
        }
    }
}
