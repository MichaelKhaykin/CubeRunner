using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CameraLibrary;
using System;

namespace Tester
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Texture2D test;
        KeyboardState keyboard;

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
            if (keyboard.IsKeyDown(Keys.W))
            {
                camera.Position = new Vector2(camera.Position.X - 3 * (float)Math.Sin(camera.Rotation), camera.Position.Y - 3 * (float)Math.Cos(camera.Rotation));
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                camera.Position = new Vector2(camera.Position.X + 3 * (float)Math.Sin(camera.Rotation), camera.Position.Y + 3 * (float)Math.Cos(camera.Rotation));
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                camera.Position = new Vector2(camera.Position.X - 3 * (float)Math.Cos(camera.Rotation), camera.Position.Y + 3 * (float)Math.Sin(camera.Rotation));
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                camera.Position = new Vector2(camera.Position.X + 3 * (float)Math.Cos(camera.Rotation), camera.Position.Y - 3 * (float)Math.Sin(camera.Rotation));
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                camera.Rotation += MathHelper.ToRadians(3);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                camera.Rotation -= MathHelper.ToRadians(3);
            }
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