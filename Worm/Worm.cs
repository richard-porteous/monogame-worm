using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Worm
{
    public class Worm : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D background;
        private Texture2D head;
        private Texture2D face;
        private Texture2D body;
        private Texture2D food;

        public Worm()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.PreferredBackBufferWidth = 800;

            // if changing GraphicsDeviceManager properties outside 
            // your game constructor also call:
            // _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("head");
            head = Content.Load<Texture2D>("head");
            face = Content.Load<Texture2D>("face");
            body = Content.Load<Texture2D>("body");
            food = Content.Load<Texture2D>("food");
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
            _spriteBatch.Begin();

            //_spriteBatch.Draw(background, new Rectangle(0, 0, 800, 640), Color.White);
            _spriteBatch.Draw(head, new Vector2(400, 240), Color.White);
            _spriteBatch.Draw(face, new Vector2(420, 260), Color.White);
            _spriteBatch.Draw(body, new Vector2(320, 240), Color.White);
            _spriteBatch.Draw(food, new Vector2(580, 260), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
