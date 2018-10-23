using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CameraLibrary;
using System;
using System.Collections.Generic;

namespace Tester
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Texture2D test;
        KeyboardState keyboard;

        //Vector2 Transformation;
        Matrix TranslationMatrix;
        //float Rotation;
        Matrix RotationMatrix;

        Dictionary<Keys, Action> Actions;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("TestTexture");
            camera = new Camera(Vector2.Zero, 1, 0, GraphicsDevice.Viewport.Bounds);

            Actions = new Dictionary<Keys, Action>()
            {
                [Keys.W] = () => { }
            };

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboard = Keyboard.GetState();
            Vector2 change = Vector2.Zero;
            if (keyboard.IsKeyDown(Keys.W))
                change.Y -= 3;
            if (keyboard.IsKeyDown(Keys.S))
                change.Y += 3;
            if (keyboard.IsKeyDown(Keys.A))
                change.X -= 3;
            if (keyboard.IsKeyDown(Keys.D))
                change.X += 3;
            TranslationMatrix = Matrix.CreateTranslation(change.X, change.Y, 0);
            float value = 0;
            if (keyboard.IsKeyDown(Keys.Left))
                value += 3;
            if (keyboard.IsKeyDown(Keys.Right))
                value -= 3;
            RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(value));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(camera);
            spriteBatch.Draw(test, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}