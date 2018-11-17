using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsLibrary;

namespace Tester
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PhysicsSprite floor;
        PhysicsSprite player;

        Texture2D playerLeft;
        Texture2D playerForward;
        Texture2D playerRight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            PhysicsConstants.Gravity = 0.125f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = new PhysicsSprite(new Vector2(100), Content.Load<Texture2D>("floor"), Color.White, Vector2.Zero, float.PositiveInfinity / 2f, 1f);
            playerLeft = Content.Load<Texture2D>("playerLeft");
            playerForward = Content.Load<Texture2D>("playerForward");
            playerRight = Content.Load<Texture2D>("playerRight");
            player = new PhysicsSprite(new Vector2(100 - 16), playerForward, Color.White, Vector2.Zero, float.PositiveInfinity, 1f);
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
