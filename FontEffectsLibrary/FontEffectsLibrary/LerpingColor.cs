using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FontEffectsLibrary
{
    public class LerpingColor
    {
        public Queue<Color> colorsToLerpThrough;
        private Color colorToLerpFrom;

        public Color Color { get; set; }

        public float travelPercentage;

        public float rateOfChange;

        public LerpingColor(float rateOfChange, List<Color> colorsToLoopThrough)
        {
            this.rateOfChange = rateOfChange;
            if (colorsToLoopThrough.Count <= 1)
            {
                Color = colorsToLoopThrough[0];
            }
            colorsToLerpThrough = new Queue<Color>(colorsToLoopThrough);
        }

        public void Update(GameTime gameTime)
        {
            if(colorsToLerpThrough.Count > 1)
            {
                Color = Color.Lerp(colorToLerpFrom, colorsToLerpThrough.Peek(), travelPercentage);
                travelPercentage += rateOfChange;

                if (travelPercentage >= 1)
                {
                    travelPercentage = 0f;

                    colorsToLerpThrough.Enqueue(colorToLerpFrom);
                    colorToLerpFrom = colorsToLerpThrough.Dequeue();
                }
            }
        }
    }
}
