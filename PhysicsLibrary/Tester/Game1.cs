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

        PhysicsObject leftBounds;
        PhysicsObject topBounds;
        PhysicsObject rightBounds;
        PhysicsObject bottomBounds;

        Texture2D playerLeft;
        Texture2D playerForward;
        Texture2D playerRight;
        Directions playerDirection;

        KeyboardState keyboard;

        Texture2D pixel;

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
            floor = new PhysicsSprite(new Vector2(100), Content.Load<Texture2D>("Block"), Color.White, Vector2.Zero, float.PositiveInfinity, 1f);
            playerLeft = Content.Load<Texture2D>("playerLeft");
            playerForward = Content.Load<Texture2D>("playerForward");
            playerRight = Content.Load<Texture2D>("playerRight");
            player = new PhysicsSprite(new Vector2(100.125f, 84), playerForward, Color.White, Vector2.Zero, 1, 0.01f);
            playerDirection = Directions.Forward;
            pixel = Content.Load<Texture2D>("pixel");

            leftBounds = new PhysicsObject(new System.Drawing.RectangleF(-20, -20, 20, GraphicsDevice.Viewport.Height + 20), Vector2.Zero, float.PositiveInfinity, 1f);
            topBounds = new PhysicsObject(new System.Drawing.RectangleF(0, -20, GraphicsDevice.Viewport.Width + 20, 20), Vector2.Zero, float.PositiveInfinity, 1f);
            rightBounds = new PhysicsObject(new System.Drawing.RectangleF(GraphicsDevice.Viewport.Width, 0, 20, GraphicsDevice.Viewport.Height + 20), Vector2.Zero, float.PositiveInfinity, 1f);
            bottomBounds = new PhysicsObject(new System.Drawing.RectangleF(-20, GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width + 20, 20), Vector2.Zero, float.PositiveInfinity, 1f);
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
            if (keyboard.IsKeyDown(Keys.R))
            {
                player.Position = new Vector2(100.125f, 84);
                player.Velocity = Vector2.Zero;
                floor.Position = new Vector2(100, 100);
                floor.Velocity = Vector2.Zero;
            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                player.Velocity = Vector2.Zero;
            }
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
            else if(playerDirection != Directions.Forward)
            {
                playerDirection = Directions.Forward;
                player.Texture = playerForward;
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
            var playerObject = player as PhysicsObject;
            var floorObject = floor as PhysicsObject;
            floor.UpdateRelative(ref playerObject);
            
            leftBounds.UpdateRelative(ref playerObject);
            leftBounds.UpdateRelative(ref floorObject);
            topBounds.UpdateRelative(ref playerObject);
            topBounds.UpdateRelative(ref floorObject);
            rightBounds.UpdateRelative(ref playerObject);
            rightBounds.UpdateRelative(ref floorObject);
            bottomBounds.UpdateRelative(ref playerObject);
            bottomBounds.UpdateRelative(ref floorObject);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            floor.Draw(spriteBatch, pixel);
            player.Draw(spriteBatch, pixel);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}