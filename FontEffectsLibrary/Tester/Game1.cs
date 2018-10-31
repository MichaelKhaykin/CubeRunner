using FontEffectsLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tester
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TextLabel textLabel;
        DropInLabel dropInlabel;
        ShadowLabel shadowLabel;
        AssemblerLabel assemblerLabel;
        TypeWriterLabel typeWriterLabel;
        ShakingLabel shakeyLabel;
        WaveLabel waveLabel;
        //wave and particle 

        GameFont gameFont;

        public static Random Random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Random = new Random();

            SpriteFont spriteFont = Content.Load<SpriteFont>("Font");          

            gameFont = new GameFont(spriteFont, "Herro my good sir", new Vector2(800, -200), Color.Red, Vector2.One, Vector2.Zero);
            gameFont.Commands.Enqueue(new List<Command>()
            {
                new Command(CommandState.Create<DropInLabelStates>())
                {
                    Action = FontFunctions.DropInLabel,
                    Parameters = new object[]
                    {
                        new Vector2(800, -200),
                        new Vector2(800, 500),
                        new Vector2(1.1f, 0.9f),
                        0.09f
                    },
                    Rate = 0.01f
                }
            });
            gameFont.Commands.Enqueue(new List<Command>()
            {
                new Command(CommandState.Create<AssemblerStates>())
                {
                    Action = FontFunctions.AssembleLabel,
                    Parameters = new object[]
                    {
                        new Random(),
                        GraphicsDevice,
                        new Vector2(100, 100),
                        0.03f
                    }
                }
            });

           // FontFunctions.AssembleLabel()


            typeWriterLabel = new TypeWriterLabel(spriteFont, new Vector2(50, 10), Color.White, "Am typewriter, hello", Vector2.One, TimeSpan.FromMilliseconds(250));

          //  gameFont = typeWriterLabel;

            List<Color> colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Yellow);
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Purple);
            colors.Add(Color.Brown);

            var font = Content.Load<SpriteFont>("Font");
            var shadowFont = Content.Load<SpriteFont>("ShadowFont");

            textLabel = new TextLabel(font, new Vector2(800, 400), colors, 0.05f, "Hello", Vector2.One / 2);

            dropInlabel = new DropInLabel(font, new Vector2(800, -200), Color.Orange, "Herro my good sir", Vector2.One, new Vector2(800, 400), 0.01f, 0.09f);

            shadowLabel = new ShadowLabel(shadowFont, new Vector2(800, 400), Color.Orange, "Great Minds", Vector2.One);

            assemblerLabel = new AssemblerLabel(font, new Vector2(50, 10), Color.Black, "The weather is nice. You should go outside. It's 77 degrees outside. I really wish it was raining though. But at least it's not too cold. I really want it to snow.", Vector2.One, GraphicsDevice, 0.04f);

            typeWriterLabel = new TypeWriterLabel(font, new Vector2(50, 10), Color.White, "Am typewriter, hello", Vector2.One, TimeSpan.FromMilliseconds(250));

            shakeyLabel = new ShakingLabel(font, new Vector2(100, 100), Color.White, "Bouncy", Vector2.One / 2);

            waveLabel = new WaveLabel(font, new Vector2(100, 100), Color.White, "wave", Vector2.One, 50);
            
        }   

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            gameFont.Update(gameFont, gameTime);

            textLabel.Update(gameTime);
            dropInlabel.Update(gameTime);
            shadowLabel.Update(gameTime);
            assemblerLabel.Update(gameTime);
            //typeWriterLabel.Update(gameTime);
            shakeyLabel.Update(gameTime);
            waveLabel.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //textLabel.Draw(spriteBatch);
            // dropInlabel.Draw(spriteBatch);
            // shadowLabel.Draw(spriteBatch);
            //   assemblerLabel.Draw(spriteBatch);
            //   typeWriterLabel.Draw(spriteBatch);
            //shakeyLabel.Draw(spriteBatch);
            //    waveLabel.Draw(spriteBatch);

            gameFont.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
