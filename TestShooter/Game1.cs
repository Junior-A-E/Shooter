using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TestShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D targetSprite;
        Texture2D skySprite;
        Texture2D crosshairSprite;
        SpriteFont spriteFont;
        Vector2 targetPosition = new Vector2(100, 200);
        Vector2 skyPosition = new Vector2(0, 0);
        Vector2 crosshairsPosition = new Vector2(200, 200);
        int score = 0;
        MouseState mouseState;
        bool mouseRelease = true;
        int radius;
        int mouseradius;
        Random random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            targetSprite = Content.Load<Texture2D>("target");
            skySprite = Content.Load<Texture2D>("sky");
            crosshairSprite = Content.Load<Texture2D>("crosshairs");
            spriteFont = Content.Load<SpriteFont>("galleryFont");
            radius = targetSprite.Width / 2;
            mouseradius = crosshairSprite.Width / 2;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();

            float mouseTargetDistance = Vector2.Distance(targetPosition, mouseState.Position.ToVector2());
            if (mouseState.LeftButton == ButtonState.Pressed && mouseRelease)
            {
                if (mouseTargetDistance <= radius)
                {
                    score++;
                    targetPosition = new Vector2(random.Next(radius, _graphics.PreferredBackBufferWidth - radius), random.Next(radius, _graphics.PreferredBackBufferHeight - radius));
                    mouseRelease = false;
                }
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                mouseRelease = true;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(skySprite, skyPosition, Color.White);          
            _spriteBatch.Draw(targetSprite, targetPosition - new Vector2(radius, radius), Color.White);
            _spriteBatch.Draw(crosshairSprite, new Vector2(mouseState.X - mouseradius, mouseState.Y - mouseradius), Color.White);
            _spriteBatch.DrawString(spriteFont, "Score : " + score.ToString() , new Vector2(10, 10), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}