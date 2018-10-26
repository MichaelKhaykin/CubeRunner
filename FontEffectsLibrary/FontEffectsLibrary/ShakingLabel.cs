using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class ShakingLabel : TextLabel
    {
        enum ShakingStates
        {
            ShakeUpLeft,
            ShakeDownRight,
        }


        ShakingStates shakeStates = ShakingStates.ShakeUpLeft;
        float travelPercentage = 0f;

        Vector2 startingPosition;
        Vector2 goalUpPosition;
        Vector2 goalDownPosition;

        public ShakingLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale) 
            : base(font, position, color, text, scale)
        {
            startingPosition = position;

            goalUpPosition = new Vector2(position.X - 1, position.Y - 1);
            goalDownPosition = new Vector2(position.X + 1, position.Y + 1);
        }

        public override void Update(GameTime gameTime)
        {
            switch(shakeStates)
            {
                case ShakingStates.ShakeUpLeft:
                    Position = Vector2.Lerp(startingPosition, goalUpPosition, travelPercentage);

                    travelPercentage += 0.35f;

                    if(travelPercentage >= 1)
                    {
                        startingPosition = goalUpPosition;
                        travelPercentage = 0f;
                        shakeStates = ShakingStates.ShakeDownRight;
                    }
                    
                    break;

                case ShakingStates.ShakeDownRight:
                    Position = Vector2.Lerp(startingPosition, goalDownPosition, travelPercentage);

                    travelPercentage += 0.35f;

                    if(travelPercentage >= 1f)
                    {
                        startingPosition = goalDownPosition;
                        travelPercentage = 0f;
                        shakeStates = ShakingStates.ShakeUpLeft;
                    }

                    break;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
