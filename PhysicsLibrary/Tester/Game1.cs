using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsLibrary;
using System;

namespace Tester
{
    public class Game1 : Game
    {
        enum Directions
        {
            Left,
            Right,
            Forward
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PhysicsSprite floor;
        PhysicsSprite player;

        Texture2D playerLeft;
        Texture2D playerForward;
        Texture2D playerRight;
        Directions playerDirection;

        KeyboardState keyboard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = new PhysicsSprite(new Vector2(100), Content.Load<Texture2D>("Block"), Color.White, Vector2.Zero, float.PositiveInfinity, 1f, 0f);
            playerLeft = Content.Load<Texture2D>("playerLeft");
            playerForward = Content.Load<Texture2D>("playerForward");
            playerRight = Content.Load<Texture2D>("playerRight");
            player = new PhysicsSprite(new Vector2(100, 84), playerForward, Color.White, Vector2.Zero, 1, 1f, 0.5f);
            playerDirection = Directions.Forward;
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
            if (keyboard.IsKeyDown(Keys.A) && keyboard.IsKeyUp(Keys.D))
            {
                if (playerDirection != Directions.Left)
                {
                    playerDirection = Directions.Left;
                    player.Texture = playerLeft;
                }
                if (player.Velocity.X >= -15)
                {
                    player.Velocity.X -= 0.5f;
                }
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                if (playerDirection != Directions.Right)
                {
                    playerDirection = Directions.Right;
                    player.Texture = playerRight;
                }
                if (player.Velocity.X <= 15)
                {
                    player.Velocity.X += 0.5f;
                }
            }
            else
            {
                playerDirection = Directions.Forward;
                player.Texture = playerForward;
                player.Velocity.X = 0;
            }
            if (keyboard.IsKeyDown(Keys.W) && player.Velocity.Y >= -10)
            {
                player.Velocity.Y -= 0.25f;
            }
            else if (keyboard.IsKeyDown(Keys.S) && player.Velocity.Y <= 10)
            {
                player.Velocity.Y += 0.25f;
            }

            player.Update();
            floor.Update();
            var derp = floor as PhysicsObject;
            player.UpdateRelative(ref derp);
            player.Position = new Vector2(player.Position.X % GraphicsDevice.Viewport.Width, player.Position.Y % GraphicsDevice.Viewport.Height);
            while (player.Position.Y < 0)
            {
                player.Position = new Vector2(player.Position.X, player.Position.Y + GraphicsDevice.Viewport.Height);
            }
            while (player.Position.X < 0)
            {
                player.Position = new Vector2(player.Position.X + GraphicsDevice.Viewport.Width, player.Position.Y);
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            floor.Draw(spriteBatch);
            player.Draw(spriteBatch);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}