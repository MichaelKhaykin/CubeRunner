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

            List<Color> colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Yellow);
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Purple);
            colors.Add(Color.Brown);
            
            textLabel = new TextLabel(Content.Load<SpriteFont>("Font"), new Vector2(800, 400), colors, 0.05f, "Hello", Vector2.One / 2);

            //IMPORTANT NUMBERS 0.01, and 0.09
            dropInlabel = new DropInLabel(Content.Load<SpriteFont>("Font"), new Vector2(800, -200), Color.Orange, "Herro my good sir", Vector2.One, new Vector2(800, 400), 0.01f, 0.09f);

            shadowLabel = new ShadowLabel(Content.Load<SpriteFont>("ShadowFont"), new Vector2(800, 400), Color.Orange, "Great Minds", Vector2.One);

            assemblerLabel = new AssemblerLabel(Content.Load<SpriteFont>("Font"), new Vector2(50, 10), Color.Orange, "Hello, my name is Michael, and I like to eat lots of food, but when the food is cooked badly, it tastes bad.", Vector2.One, GraphicsDevice);

            /*
              "Lorem ipsum dolor sit amet, consectetur adipiscin elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            */
        }   

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            textLabel.Update(gameTime);
            dropInlabel.Update(gameTime);
            shadowLabel.Update(gameTime);
            assemblerLabel.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //textLabel.Draw(spriteBatch);
            //dropInlabel.Draw(spriteBatch);
            //shadowLabel.Draw(spriteBatch);
            assemblerLabel.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
