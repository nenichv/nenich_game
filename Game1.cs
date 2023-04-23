using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace superagent
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
        float evilSpriteSpeed = 9f;
        bool Pause = false;
        Song music;
        Song songFight;
        Color color = Color.AntiqueWhite;
        private Matrix screenXform;
        public readonly Rectangle screenBounds;
        SpriteFont textScore;
        SpriteFont textCollectChests;

        public enum GameState
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
            heroSpritePosition = new Vector2(0, 60);
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

            textScore = Content.Load<SpriteFont>("Score");
            textCollectChests = Content.Load<SpriteFont>("CollectChests");
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
                if (banditSpritePosition.X > Window.ClientBounds.Width * 2.1f || banditSpritePosition.X < 0)
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
                if (heroSpritePosition.X > Window.ClientBounds.Width * 2.1f - heroSpriteSize.X)
                    heroSpritePosition.X = Window.ClientBounds.Width * 2.1f - heroSpriteSize.X;
                if (heroSpritePosition.Y > Window.ClientBounds.Height * 2.1f - heroSpriteSize.Y)
                    heroSpritePosition.Y = Window.ClientBounds.Height * 2.1f - heroSpriteSize.Y;

                if (Collide())
                {
                    color = Color.Red;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songFight);
                    MediaPlayer.Play(music);
                }
                else color = Color.AntiqueWhite;

                base.Update(gameTime);
                if (keyboardState.IsKeyDown(Keys.Y)) Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            Drawing.DrawSprite(spriteBatch, goodHero, bandit, chest,
            heroSpritePosition, banditSpritePosition, chestSpritePosition, color, screenXform);
            Drawing.DrawText(spriteBatch, textScore, textCollectChests, Color.Black, screenBounds);

            switch (state)
            {
                case GameState.Menu:
                    Drawing.DrawMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    Drawing.DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    Drawing.DrawEndOfGame(gameTime);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected bool Collide()
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditSpritePosition.X,
                (int)banditSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }
    }

    public class Drawing
    {
        public static void DrawCard()
        {
            var count = 5;
            for (int i = 0; i < count; i++)
            {

                count++;
            }
        }

        public static void DrawSprite(SpriteBatch spriteBatch, Texture2D goodHero, 
            Texture2D bandit, Texture2D chest, Vector2 heroSpritePosition, 
            Vector2 banditSpritePosition, Vector2 chestSpritePosition, Color color, 
            Matrix screenXform)
        {
            
            spriteBatch.Draw(chest, chestSpritePosition, null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(goodHero, heroSpritePosition, null, color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            
        }

        public static void DrawText(SpriteBatch spriteBatch, 
            SpriteFont textScore, SpriteFont textCollectChests, Color color, Rectangle screenBounds)
        {
            var positionScore = new Vector2(10, 1030);
            var positionCollect = new Vector2(800, 10);
            spriteBatch.DrawString(textScore, "Score: 0", positionScore, color);
            spriteBatch.DrawString(textCollectChests, 
                "Your task: collect chests and find exit!", positionCollect, color);
        }

        public static void DrawMenu(GameTime gameTime)
        {
            // Отрисовка меню, кнопок и т.д.
        }

        public static void DrawGameplay(GameTime gameTime)
        {
            // Отрисовка игровых объектов, счета и т.д.
        }

        public static void DrawEndOfGame(GameTime deltaTime)
        {
            // Отрисовка результатов, кнопок и т.д.
        }
    }

    public class Update
    {
        void UpdateMenu(GameTime gameTime, KeyboardState keyboardState, Game1.GameState state)
        {
            // Обрабатывает действия игрока в экране меню
            if (keyboardState.IsKeyDown(Keys.E))
                state = Game1.GameState.GamePlay;
        }

        void UpdateGameplay(GameTime gameTime, KeyboardState keyboardState, 
            bool Pause, Game1.GameState state)
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
                state = Game1.GameState.EndOfGame;
            }
        }

        void UpdateEndOfGame(GameTime gameTime, KeyboardState keyboardState, 
            bool Pause, Game1.GameState state)
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
                state = Game1.GameState.Menu;
            }
            //else if (pushedRestartLevelButton)
            //{
            //ResetLevel();
            //state = GameState.Gameplay;
            //}
        }
    }
}