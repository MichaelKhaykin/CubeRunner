using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class WaveLabel : TextLabel
    {
        List<Letter> Letters;
    
        protected float AmountToGoUpBy;
        
        enum WaveState
        {
            GoDown,
            GoUp
        }

        WaveState waveState = WaveState.GoDown;

        float currentPositionInString;
        float travelPercent = 0f;
        float wavelength = 2f;

        float rate = 0.01f;

        public WaveLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, float amountToMoveUpBy)
            : base(font, position, color, text, scale)
        {
            Letters = new List<Letter>();

            AmountToGoUpBy = amountToMoveUpBy;

            Vector2 oldPos = Vector2.Zero;

            for (int i = 0; i < text.Length; i++)
            {
                Letters.Add(new Letter(font, text[i], Position + oldPos, color, Vector2.Zero, Vector2.One));
                Letters[i].GoalPosition = new Vector2(Letters[i].Position.X, Position.Y - AmountToGoUpBy);

                oldPos.X += font.MeasureString(text[i].ToString()).X;
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            currentPositionInString = MathHelper.Lerp(0, Letters.Count + 3, travelPercent);
            travelPercent += rate;
            if(travelPercent >= 1)
            {
                rate *= -1;
            }
            if(travelPercent <= -1)
            {
                rate *= -1;
            }

            for(int i = 0; i < Letters.Count; i++)
            {
                float distance = Math.Abs(currentPositionInString - i);
                var percentage = Math.Max(wavelength * (1 - (distance / Letters.Count)), 0);
                Letters[i].Position = Vector2.Lerp(Letters[i].StartingPosition, Letters[i].GoalPosition, percentage);
            }
        }


        public override void Draw(SpriteBatch sb)
        {
            foreach (var letter in Letters)
            {
                letter.Draw(sb);
            }
        }
    }
}
