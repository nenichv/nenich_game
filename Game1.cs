using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace my_game
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D goodHero;
        Texture2D bandit;
        Texture2D chest;
        Vector2 heroSpritePosition;
        Vector2 banditSpritePosition;
        Vector2 chestSpritePosition;
        Point heroSpriteSize;
        Point banditSpriteSize;
        Point chestSpriteSize;
        float goodSpriteSpeed = 3f;
        float evilSpriteSpeed = 2f;
        bool Pause = false;
        Song music;
        Song songFight;
        Color color = Color.AntiqueWhite;
        private Matrix screenXform;
        private readonly Rectangle screenBounds;

        enum GameState
        {
            Menu,
            GamePlay,
            Pause,
            EndOfGame,
        }

        GameState state;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            chestSpritePosition = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            heroSpritePosition = Vector2.Zero;
            banditSpritePosition = new Vector2(0, Window.ClientBounds.Height / 5);
            var screenScale = graphics.PreferredBackBufferHeight / 1080.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);

        }

        protected override void Initialize()
        {
            TouchPanel.DisplayWidth = screenBounds.Width;
            TouchPanel.DisplayHeight = screenBounds.Height;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            goodHero = Content.Load<Texture2D>("герой");
            bandit = Content.Load<Texture2D>("враг");
            chest = Content.Load<Texture2D>("сундук");
            heroSpriteSize = new Point(goodHero.Width / 10, goodHero.Height / 10);
            banditSpriteSize = new Point(bandit.Width / 10, bandit.Height / 10);
            chestSpriteSize = new Point(chest.Width / 10, chest.Height / 10);

            music = Content.Load<Song>("material-fonovogo-zvuka-igrovoy-stsenyi-39470");
            songFight = Content.Load<Song>("удар");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.X)) Exit();

            if (keyboardState.IsKeyDown(Keys.E))
            {

            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                switch (Pause)
                {
                    case false:
                        Pause = true;
                        break;
                    case true:
                        Pause = false;
                        break;
                }
            }

            if (Pause == false)
            {
                banditSpritePosition.X += evilSpriteSpeed;
                if (banditSpritePosition.X > Window.ClientBounds.Width - bandit.Width * 0.1f || banditSpritePosition.X < 0)
                    evilSpriteSpeed *= -1;

                if (keyboardState.IsKeyDown(Keys.A))
                    heroSpritePosition.X -= goodSpriteSpeed;
                if (keyboardState.IsKeyDown(Keys.D))
                    heroSpritePosition.X += goodSpriteSpeed;
                if (keyboardState.IsKeyDown(Keys.W))
                    heroSpritePosition.Y -= goodSpriteSpeed;
                if (keyboardState.IsKeyDown(Keys.S))
                    heroSpritePosition.Y += goodSpriteSpeed;

                if (heroSpritePosition.X < 0) heroSpritePosition.X = 0;
                if (heroSpritePosition.Y < 0) heroSpritePosition.Y = 0;
                if (heroSpritePosition.X > Window.ClientBounds.Width - heroSpriteSize.X)
                    heroSpritePosition.X = Window.ClientBounds.Width - heroSpriteSize.X;
                if (heroSpritePosition.Y > Window.ClientBounds.Height - heroSpriteSize.Y)
                    heroSpritePosition.Y = Window.ClientBounds.Height - heroSpriteSize.Y;

                if (Collide())
                {
                    color = Color.Red;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songFight);
                    MediaPlayer.Play(music);
                }
                else color = Color.AntiqueWhite;

                base.Update(gameTime);
                
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.AntiqueWhite);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);

            spriteBatch.Draw(chest, chestSpritePosition, null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(goodHero, heroSpritePosition, null, color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            

            spriteBatch.End();

            base.Draw(gameTime);
            switch (state)
            {
                case GameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
            }
        }

        protected bool Collide()
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditSpritePosition.X,
                (int)banditSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        void UpdateMenu(GameTime gameTime, KeyboardState keyboardState)
        {
            // Обрабатывает действия игрока в экране меню
            if (keyboardState.IsKeyDown(Keys.E))
                state = GameState.GamePlay;
        }

        void UpdateGameplay(GameTime gameTime, KeyboardState keyboardState)
        {
            // Обновляет состояние игровых объектов, действия игрока.
            if (keyboardState.IsKeyDown(Keys.X))
            {
                switch (Pause)
                {
                    case false:
                        Pause = true;
                        break;
                    case true:
                        Pause = false;
                        break;
                }
                state = GameState.EndOfGame;
                if (keyboardState.IsKeyDown(Keys.Y)) Exit();
            }
        }

        void UpdateEndOfGame(GameTime gameTime, KeyboardState keyboardState)
        {
            // Обрабатывает действия игрока, сохраняет результаты
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                switch (Pause)
                {
                    case false:
                        Pause = true;
                        break;
                    case true:
                        Pause = false;
                        break;
                }
                state = GameState.Menu;
            }
            //else if (pushedRestartLevelButton)
            //{
                //ResetLevel();
                //state = GameState.Gameplay;
            //}
        }

        void DrawMenu(GameTime gameTime)
        {
            // Отрисовка меню, кнопок и т.д.
        }

        void DrawGameplay(GameTime gameTime)
        {
            // Отрисовка игровых объектов, счета и т.д.
        }

        void DrawEndOfGame(GameTime deltaTime)
        {
            // Отрисовка результатов, кнопок и т.д.
        }
    }
}