using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEffectsLibrary
{
    public static class FontFunctions
    {
        /// <summary>
        /// This will drop a label into the screen and bounce it
        /// </summary>
        /// <param name="gameObject">This is the gameFont to operate on</param>
        /// <param name="command">This stores the command information</param>
        /// <param name="parameters">P0 = StartPosition, P1 = GoalPosition, P2 = CompressRate, P3 = BounceRate</param>
        /// <returns></returns>
        public static bool DropInLabel(GameObject gameObject, Command command, object[] parameters)
        {
            var gameFont = (GameFont)gameObject;
           
            var StartPosition = (Vector2)parameters[0];
            var GoalPosition = (Vector2)parameters[1];
            var compressAmount = (Vector2)parameters[2];
            var bounceLerpAmount = (float)parameters[3];

            var commandState = command.State as CommandState<DropInLabelStates>;

            switch (commandState.State)
            {
                case DropInLabelStates.GoDown:
                    gameFont.Position = Vector2.Lerp(StartPosition, GoalPosition, command.PercentComplete);
                    command.PercentComplete += command.Rate;

                    if (command.PercentComplete >= 1)
                    {
                        command.PercentComplete = 0f;
                        commandState.State = DropInLabelStates.Compress;
                    }

                    break;

                case DropInLabelStates.Compress:

                  
                    gameFont.Scale = Vector2.Lerp(gameFont.InitScale, compressAmount, command.PercentComplete);
                    
                    command.PercentComplete += bounceLerpAmount;

                    if (command.PercentComplete >= 1)
                    {
                        command.PercentComplete = 0f;
                        commandState.State = DropInLabelStates.ExpandBack;
                    }

                    break;

                case DropInLabelStates.ExpandBack:
                    
                    gameFont.Scale = Vector2.Lerp(compressAmount, gameFont.InitScale, command.PercentComplete);
                    
                    command.PercentComplete += bounceLerpAmount;

                    return command.PercentComplete >= 1f;
            }

            return false;
        }
        
        /// <summary>
        /// This functions assembles text
        /// </summary>
        /// <param name="gameObject">This is the gamefont to operate on</param>
        /// <param name="command">Stores command information</param>
        /// <param name="parameters">P1 = RandomGen, P2 = Text, P3 = GraphicsDevice, P4 = GoalPosition, P5 = assemblerSpeed </param>
        /// <returns></returns>
        public static bool AssembleLabel(GameObject gameObject, Command command, object[] parameters)
        {
            var commandState = command.State as CommandState<AssemblerStates>;

            var gamefont = (GameFont)gameObject;
            gamefont.UseText = false;

            switch (commandState.State)
            {
                case AssemblerStates.Init:

                    #region Parameters
                    Random rand = (Random)parameters[0];
                    GraphicsDevice graphics = (GraphicsDevice)parameters[1];
                    Vector2 GoalPosition = (Vector2)parameters[2];
                    float assemblerSpeed = (float)parameters[3];
                    #endregion

                    int lineCount = 0;

                    foreach (var letter in gamefont.Letters)
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

                        letter.Position = new Vector2(x, y);
                        letter.GoalPosition = GoalPosition;
                        letter.Velocity = assemblerSpeed;

                        //each time we hit a space, calculate if the next word can fit on the screen
                        //if it can't, move to the next line
                    }

                
                    bool prevStateOfNextLine = false;

                    for (int i = 1; i < gamefont.Letters.Count; i++)
                    {
                        bool nextLine = false;
                        if (gamefont.Letters[i].Value == ' ') // problem in here that folds first letters
                        {
                            int length = 1;
                            bool eof = false;
                            while (gamefont.Letters[i + length].Value != ' ' && eof == false)
                            {
                                length++;
                                if (i + length >= gamefont.Letters.Count)
                                {
                                    //THIS BREAKS IT
                                    eof = true;
                                    break;
                                }
                            }

                            // + " " is to add some padding
                            if (GoalPosition.X + gamefont.Letters[0].Font.MeasureString(gamefont.Letters.Substring(i, length) + "  ").X > graphics.Viewport.Width)
                            {
                                nextLine = true;
                            }
                        }

                        if (nextLine)
                        {
                            lineCount++;
                            GoalPosition.X = gamefont.Letters[0].GoalPosition.X;
                            GoalPosition.Y = lineCount * (gamefont.Letters[0].Font.MeasureString(gamefont.Letters[0].Value.ToString()).Y) + gamefont.Letters[0].GoalPosition.Y;
                        }
                        else
                        {
                            if (!prevStateOfNextLine)
                            {
                                GoalPosition.X += gamefont.Letters[i].Font.MeasureString(gamefont.Letters[i - 1].Value.ToString()).X;
                            }
                        }

                        gamefont.Letters[i].GoalPosition = GoalPosition;

                        gamefont.Letters[i].Velocity = gamefont.Letters[i - 1].Velocity * 0.95f;

                        prevStateOfNextLine = nextLine;
                    }

                    commandState.State = AssemblerStates.Assemble;

                    break;

                case AssemblerStates.Assemble:

                    int count = 0;

                    for (int i = 0; i < gamefont.Letters.Count; i++)
                    {
                        gamefont.Letters[i].Position = Vector2.Lerp(gamefont.Letters[i].StartingPosition, gamefont.Letters[i].GoalPosition - gamefont.Letters[i].Origin, gamefont.Letters[i].TravelPercentage);

                        gamefont.Letters[i].TravelPercentage += gamefont.Letters[i].Velocity;

                        gamefont.Letters[i].TravelPercentage = MathHelper.Clamp(gamefont.Letters[i].TravelPercentage, 0, 1);

                        if(gamefont.Letters[i].TravelPercentage >= 1f)
                        {
                            count++;
                        }

                        if (gamefont.Letters[i].TravelPercentage == 1 && i + 1 < gamefont.Letters.Count)
                        {
                            gamefont.Letters[i + 1].Velocity = gamefont.Letters[i].Velocity;

                            for (int j = i + 2; j < gamefont.Letters.Count; j++)
                            {
                                //fix 0.5 later this is really the gap
                                //if gap = 0 they would land at same time
                                //gap = gap between each letter land
                                gamefont.Letters[j].Velocity = gamefont.Letters[j - 1].Velocity * 0.95f;
                            }
                        }
                    }

                    if(count == gamefont.Letters.Count)
                    {
                        gamefont.UseText = true;
                        gamefont.Position = gamefont.Letters[0].Position;
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
