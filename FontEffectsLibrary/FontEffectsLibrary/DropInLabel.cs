using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{

    public class DropInLabel : TextLabel
    {
        enum DropInLabelStates
        {
            GoDown,
            Compress,
            ExpandBack,
            Done
        }

        DropInLabelStates dropInLabelStates = DropInLabelStates.GoDown;

        private Vector2 GoalPosition;
        private Vector2 StartPosition;

        private float lerpAmount;
        private float positionTravelPercentage;

        private float bounceLerpAmount;

        private Vector2 compressAmount;
        private Vector2 InitScale;

        public DropInLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, Vector2 scale, Vector2 goalPosition, float lerpAmount, float bounceLerpAmount) 
            : base(font, position, colors, rateOfChange, text, scale)
        {
            Init(goalPosition, lerpAmount, bounceLerpAmount);
        }

        public DropInLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, Vector2 goalPosition, float lerpAmount, float bounceLerpAmount)
            : base(font, position, color, text, scale)
        {
            Init(goalPosition, lerpAmount, bounceLerpAmount);
        }

        private void Init(Vector2 goalPosition, float lerpAmount, float bounceLerpAmount)
        {
            StartPosition = Position;
            GoalPosition = goalPosition;
            this.lerpAmount = lerpAmount;
            this.bounceLerpAmount = bounceLerpAmount;

            compressAmount = new Vector2(Scale.X * 1.1f, Scale.Y * 0.9f);
            InitScale = Scale;
        }

        public override void Update(GameTime gameTime)
        {
            switch (dropInLabelStates)
            {
                case DropInLabelStates.GoDown:
                    Position = Vector2.Lerp(StartPosition, GoalPosition, positionTravelPercentage);
                    positionTravelPercentage += lerpAmount;

                    if(positionTravelPercentage >= 1)
                    {
                        positionTravelPercentage = 0f;
                        dropInLabelStates = DropInLabelStates.Compress;
                    }

                    break;

                case DropInLabelStates.Compress:

                    Scale = Vector2.Lerp(InitScale, compressAmount, positionTravelPercentage);
                    positionTravelPercentage += bounceLerpAmount;

                    if(positionTravelPercentage >= 1)
                    {
                        positionTravelPercentage = 0f;
                        dropInLabelStates = DropInLabelStates.ExpandBack;
                    }

                    break;

                case DropInLabelStates.ExpandBack:

                    Scale = Vector2.Lerp(compressAmount, InitScale, positionTravelPercentage);
                    positionTravelPercentage += bounceLerpAmount;

                    if(positionTravelPercentage >= 1)
                    {
                        positionTravelPercentage = 0f;
                        dropInLabelStates = DropInLabelStates.Done;
                    }

                    break;

                case DropInLabelStates.Done:
                    break;

            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
