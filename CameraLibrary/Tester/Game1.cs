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
        ControlledCamera camera;
        Texture2D test;
        KeyboardState keyboard;

        Dictionary<Keys, ControlledCamera.Controls> translate;

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
            camera = new ControlledCamera(Vector3.Zero, GraphicsDevice.Viewport.Bounds, 3f, 1f);
            translate = new Dictionary<Keys, ControlledCamera.Controls>
            {
                [Keys.W] = ControlledCamera.Controls.Up,
                [Keys.A] = ControlledCamera.Controls.Left,
                [Keys.S] = ControlledCamera.Controls.Down,
                [Keys.D] = ControlledCamera.Controls.Right,
                [Keys.Q] = ControlledCamera.Controls.TurnLeft,
                [Keys.E] = ControlledCamera.Controls.TurnRight
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
            List<ControlledCamera.Controls> controls = new List<ControlledCamera.Controls>();
            foreach (Keys key in keyboard.GetPressedKeys())
            {
                if (translate.ContainsKey(key))
                {
                    controls.Add(translate[key]);
                }
            }
            camera.Update(controls.ToArray(), gameTime);
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