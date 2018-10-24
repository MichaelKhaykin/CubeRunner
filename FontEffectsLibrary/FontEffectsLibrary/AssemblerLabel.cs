using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class AssemblerLabel : TextLabel
    {
        class LettetWithPosition
        {
            //add target position here

            public char Letter;

            public Vector2 Position;

            public Vector2 GoalPosition;

            public float Velocity;

            protected Color Color;

            public SpriteFont Font;

            public Vector2 StartingPosition;

            public float TravelPercentage;

            public TimeSpan UpdateTime = TimeSpan.FromMilliseconds(18);

            public TimeSpan ElapsedTime;

            public bool IsVisible { get; set; } = true;

            public LettetWithPosition(SpriteFont font, char letter, Vector2 position, Color color, Vector2 goalPosition, float velocity)
            {
                Font = font;
                Letter = letter;
                Position = position;
                Color = color;
                StartingPosition = Position;
                TravelPercentage = 0f;
                GoalPosition = goalPosition;
                Velocity = velocity;
            }

            public void Draw(SpriteBatch sb)
            {
                if (IsVisible)
                {
                    sb.DrawString(Font, Letter.ToString(), Position, Color);
                }
            }
        }

        List<LettetWithPosition> letterList;

        private int letterListIndex = 0;

        private Vector2 GoalPosition;

        public AssemblerLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, Vector2 scale, GraphicsDevice graphics)
            : base(font, position, colors, rateOfChange, text, scale)
        {
            Init(font, text, graphics);
        }

        public AssemblerLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale, GraphicsDevice graphics)
            : base(font, position, color, text, scale)
        {
            Init(font, text, graphics);
        }

        private void Init(SpriteFont font, string text, GraphicsDevice graphics)
        {

            GoalPosition = Position;

            letterList = new List<LettetWithPosition>();

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

                letterList.Add(new LettetWithPosition(font, text[i], new Vector2(x, y), Color.Red, GoalPosition, 0.03f));

                //each time we hit a space, calculate if the next word can fit on the screen
                //if it can't, move to the next line
            }

            for (int i = 1; i < letterList.Count; i++)
            {

                //fix 0.5 later this is really the gap
                //if gap = 0 they would land at same time
                //gap = gap between each letter land
                bool nextLine = false;
                if (letterList[i].Letter == ' ') // problem in here that folds first letters
                {
                    int length = 1;
                    bool eof = false;
                    while (letterList[i + length].Letter != ' ' && eof == false)
                    {
                        length++;
                        if (i + length >= letterList.Count)
                        {
                            //THIS BREAKS IT
                            eof = true;
                            break;
                        }
                    }

                    if (GoalPosition.X + letterList[0].Font.MeasureString(Substring(i, length) + "  ").X > graphics.Viewport.Width)
                    {
                        nextLine = true;
                    }
                }

                if (nextLine)
                {
                    lineCount++;
                    GoalPosition.X = letterList[0].GoalPosition.X;
                    GoalPosition.Y = lineCount * (letterList[0].Font.MeasureString(letterList[0].Letter.ToString()).Y) + letterList[0].GoalPosition.Y;
                }
                else
                {
                    GoalPosition.X += letterList[i].Font.MeasureString(letterList[i - 1].Letter.ToString()).X;
                }

                letterList[i].GoalPosition = GoalPosition;

                letterList[i].Velocity = letterList[i - 1].Velocity * 0.95f;

                //PRINT LABOROM is not being printed at the end of the string
                //but in fact at the beggining
            }

        }

        public string Substring(int index, int length)
        {
            StringBuilder build = new StringBuilder();
            for (int i = index; i < index + length; i++)
            {
                build.Append(letterList[i].Letter);
            }

            return build.ToString();
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



            /*
            while (letterListIndex < letterList.Count)
            {
                var currLetter = letterList[letterListIndex];
                
                currLetter.ElapsedTime += gameTime.ElapsedGameTime;

                if(currLetter.ElapsedTime < currLetter.UpdateTime)
                {
                    break;
                }

                currLetter.ElapsedTime = TimeSpan.Zero;

                currLetter.Position = Vector2.Lerp(currLetter.StartingPosition, GoalPosition, currLetter.TravelPercentage);
                currLetter.TravelPercentage += 0.02f;

                if(currLetter.TravelPercentage >= 1)
                {
                    letterListIndex++;

                    if (letterListIndex >= letterList.Count)
                    {
                        break;
                    }

                    letterList[letterListIndex].IsVisible = true;

                    var offset = currLetter.Font.MeasureString(currLetter.Letter.ToString()).X;
                    GoalPosition.X += offset;
                }
            }
            */
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (var letter in letterList)
            {
                letter.Draw(sb);
            }


            //       base.Draw(sb);
        }
    }
}
