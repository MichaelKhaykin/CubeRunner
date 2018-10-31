using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public partial class AssemblerLabel : TextLabel
    {
        List<Letter> letterList;
        
        private Vector2 GoalPosition;

        public AssemblerLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, Vector2 scale, GraphicsDevice graphics, float assemblerSpeed)
            : base(font, position, colors, rateOfChange, text, scale)
        {
            Init(font, text, graphics, assemblerSpeed);
        }

        public AssemblerLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, GraphicsDevice graphics, float assemblerSpeed)
            : base(font, position, color, text, scale)
        {
            Init(font, text, graphics, assemblerSpeed);
        }

        private void Init(SpriteFont font, string text, GraphicsDevice graphics, float assemblerSpeed)
        {
            GoalPosition = Position;

            letterList = new List<Letter>();

            Random rand = new Random();

            //calculate and store the goal position for each letter
            // each goal position is the previous goal position + letter width
            int lineCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                float firstX = rand.Next(-200, 0);
                float firstY = rand.Next(-100, 0);

                float secondX = rand.Next(graphics.Viewport.Width, graphics.Viewport.Width + 150);
                float secondY = rand.Next(graphics.Viewport.Height, graphics.Viewport.Height + 150);

                float x = 0;
                float y = 0;

                //flip coin between x's and y's
                switch (rand.Next(0, 2))
                {
                    case 0:
                        x = firstX;
                        y = secondY;
                        break;

                    case 1:
                        x = secondX;
                        y = firstY;
                        break;
                }

                //fix color later

                letterList.Add(new Letter(font, text[i], new Vector2(x, y), Color.Red, Vector2.Zero, Vector2.One));
                letterList[i].GoalPosition = GoalPosition;
                letterList[i].Velocity = assemblerSpeed;

                //each time we hit a space, calculate if the next word can fit on the screen
                //if it can't, move to the next line
            }

            bool prevStateOfNextLine = false;

            for (int i = 1; i < letterList.Count; i++)
            {
                bool nextLine = false;
                if (letterList[i].Value == ' ') // problem in here that folds first letters
                {
                    int length = 1;
                    bool eof = false;
                    while (letterList[i + length].Value != ' ' && eof == false)
                    {
                        length++;
                        if (i + length >= letterList.Count)
                        {
                            //THIS BREAKS IT
                            eof = true;
                            break;
                        }
                    }

                    // + " " is to add some padding
                    if (GoalPosition.X + letterList[0].Font.MeasureString(letterList.Substring(i, length) + "  ").X > graphics.Viewport.Width)
                    {
                        nextLine = true;
                    }
                }

                if (nextLine)
                {
                    lineCount++;
                    GoalPosition.X = letterList[0].GoalPosition.X;
                    GoalPosition.Y = lineCount * (letterList[0].Font.MeasureString(letterList[0].Value.ToString()).Y) + letterList[0].GoalPosition.Y;
                }
                else
                {
                    if (!prevStateOfNextLine)
                    {
                        GoalPosition.X += letterList[i].Font.MeasureString(letterList[i - 1].Value.ToString()).X;
                    }
                }

                letterList[i].GoalPosition = GoalPosition;

                letterList[i].Velocity = letterList[i - 1].Velocity * 0.95f;

                prevStateOfNextLine = nextLine;
            }    
        }

      

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < letterList.Count; i++)
            {
                letterList[i].Position = Vector2.Lerp(letterList[i].StartingPosition, letterList[i].GoalPosition, letterList[i].TravelPercentage);

                letterList[i].TravelPercentage += letterList[i].Velocity;

                letterList[i].TravelPercentage = MathHelper.Clamp(letterList[i].TravelPercentage, 0, 1);

                if (letterList[i].TravelPercentage == 1 && i + 1 < letterList.Count)
                {
                    letterList[i + 1].Velocity = letterList[i].Velocity;

                    for (int j = i + 2; j < letterList.Count; j++)
                    {
                        //fix 0.5 later this is really the gap
                        //if gap = 0 they would land at same time
                        //gap = gap between each letter land
                        letterList[j].Velocity = letterList[j - 1].Velocity * 0.95f;
                    }
                }
            }
           
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (var letter in letterList)
            {
                letter.Draw(sb);
            }
        }
    }
}
