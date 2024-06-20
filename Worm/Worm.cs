using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Worm
{
    public class Worm : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Create a new render target
        RenderTarget2D background;
        //Sprites
        private Texture2D head;
        private Texture2D face;
        private Texture2D body;
        private Texture2D food;
        private int tileWidth = 40;
        private Point head_pos;
        private int head_px_x = 0;
        private int head_px_y = 0;

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
            _graphics.PreferredBackBufferWidth = 20 * tileWidth;
            _graphics.PreferredBackBufferHeight = 16 * tileWidth;

            // if changing GraphicsDeviceManager properties outside 
            // your game constructor also call:
            // _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = CreateBackroundTexture();

            // TODO: use this.Content to load your game content here
            head = Content.Load<Texture2D>("head");
            face = Content.Load<Texture2D>("face");
            body = Content.Load<Texture2D>("body");
            food = Content.Load<Texture2D>("food");

            head_pos = pointFromTile(10, 6, tileWidth, tileWidth);
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float deltaTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            Debug.WriteLine("delta = " + delta + "    deltaTime = " + deltaTime);
            
            int diff = (int)(40 * 3 * delta);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                head_pos.X -= diff;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                head_pos.X += diff;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                head_pos.Y -= diff;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                head_pos.Y += diff;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(head, new Rectangle(head_pos, new Point(tileWidth, tileWidth)), Color.White);
            _spriteBatch.Draw(face, new Rectangle(pointFromTile(10, 6, tileWidth, tileWidth/2), new Point((int)tileWidth/2, (int)tileWidth /2)), Color.White);
            _spriteBatch.Draw(body, new Rectangle(pointFromTile(9, 6, tileWidth, tileWidth), new Point(tileWidth, tileWidth)), Color.White);
            _spriteBatch.Draw(food, new Rectangle(pointFromTile(12, 6, tileWidth, tileWidth/2), new Point((int)tileWidth/2, (int)tileWidth/2)), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the entire scene in the given render target.
        /// </summary>
        /// <returns>A texture2D with the scene drawn in it.</returns>
        protected RenderTarget2D CreateBackroundTexture()
        {
            RenderTarget2D renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
            // Set the render target
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            // Draw the scene
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            int[] max = { GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight };
            for (int x = 0; x <= max[0]; x += tileWidth)
                DrawLineBetween(_spriteBatch, new Vector2((float)x,0f), new Vector2((float)x, (float)max[1]), 1, Color.Black);
            for (int y = 0; y <= max[1]; y += tileWidth)
                DrawLineBetween(_spriteBatch, new Vector2(0f, (float)y), new Vector2((float)max[0], (float)y), 1, Color.Black);
            _spriteBatch.End();

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
            return renderTarget;
        }

        public static void DrawLineBetween(
            SpriteBatch spriteBatch,
            Vector2 startPos,
            Vector2 endPos,
            int thickness,
            Color color)
        {
            // Create a texture as wide as the distance between two points and as high as
            // the desired thickness of the line.
            var distance = (int)Vector2.Distance(startPos, endPos);
            var texture = new Texture2D(spriteBatch.GraphicsDevice, distance, thickness);

            // Fill texture with given color.
            var data = new Color[distance * thickness];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }
            texture.SetData(data);

            // Rotate about the beginning middle of the line.
            var rotation = (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
            var origin = new Vector2(0, thickness / 2);

            spriteBatch.Draw(
                texture,
                startPos,
                null,
                Color.White,
                rotation,
                origin,
                1.0f,
                SpriteEffects.None,
                1.0f);
        }

        private Point pointFromTile(int x, int y, int tileSize, int imgSize)
        {
            int diff = tileSize - imgSize;
            int halfDiff = (int)(diff / 2);
            return new Point((int)(x * tileSize) + halfDiff, (int)(y * tileSize) + halfDiff);
        }
    }
}
