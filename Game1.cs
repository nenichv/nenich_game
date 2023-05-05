using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace superagent
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D backGameover;
        Texture2D goodHero;
        Texture2D bandit;
        Texture2D chest;
        Vector2 heroSpritePosition;
        Vector2 banditOneSpritePosition;
        Vector2 banditTwoSpritePosition;
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
        SpriteFont textHP;
        SpriteFont textEnd;
        int Score = 0;
        int HP = 100;

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
            heroSpritePosition = new Vector2(230, 260);
            banditOneSpritePosition = new Vector2(300, 600);
            banditTwoSpritePosition = new Vector2(850, 300);
            var screenScale = graphics.PreferredBackBufferHeight / 1080.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);

        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("бэк");
            backGameover = Content.Load<Texture2D>("gameover");
            goodHero = Content.Load<Texture2D>("герой");
            bandit = Content.Load<Texture2D>("враг");
            chest = Content.Load<Texture2D>("сундук");
            heroSpriteSize = new Point(goodHero.Width / 10, goodHero.Height / 10);
            banditSpriteSize = new Point(bandit.Width / 10, bandit.Height / 10);
            chestSpriteSize = new Point(chest.Width, chest.Height);
            music = Content.Load<Song>("material-fonovogo-zvuka-igrovoy-stsenyi-39470");
            songFight = Content.Load<Song>("удар");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
            textScore = Content.Load<SpriteFont>("Score");
            textCollectChests = Content.Load<SpriteFont>("CollectChests");
            textHP = Content.Load<SpriteFont>("HP");
            textEnd = Content.Load<SpriteFont>("End");
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
                Score += 1;
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
                banditOneSpritePosition.X += evilSpriteSpeed;
                if (banditOneSpritePosition.X > 1840 || banditOneSpritePosition.X < 50)
                    evilSpriteSpeed *= -1;

                banditTwoSpritePosition.Y += evilSpriteSpeed;
                if (banditTwoSpritePosition.Y > 1000 || banditTwoSpritePosition.Y < 300)
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

                if (CollideOne())
                {
                    color = Color.Red;
                    HP -= 1;
                    MediaPlayer.Play(songFight);
                    MediaPlayer.Play(music);
                }
                else color = Color.AntiqueWhite;

                if (CollideTwo())
                {
                    color = Color.Red;
                    HP -= 1;
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
            Drawing.DrawBackground(spriteBatch, background);
            Drawing.DrawSprite(spriteBatch, goodHero, bandit, chest,
            heroSpritePosition, banditOneSpritePosition, banditTwoSpritePosition, chestSpritePosition, color);
            Drawing.DrawText(spriteBatch, textScore, textCollectChests, textHP, Color.Black, Score, HP);

            if (HP <= 0)
            {
                Pause = true;
                MediaPlayer.Pause();
                spriteBatch.Draw(backGameover, new Rectangle(0, 90, 1800, 1150), Color.White);
                Drawing.DrawEnd(spriteBatch, textEnd);
            }

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

        protected bool CollideOne()
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditOneSpritePosition.X,
                (int)banditOneSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        protected bool CollideTwo()
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditTwoSpritePosition.X,
                (int)banditTwoSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }
    }

    public class Drawing
    {
        public static void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 90, 1800, 1150), Color.White);
        }

        public static void DrawSprite(SpriteBatch spriteBatch, Texture2D goodHero, 
            Texture2D bandit, Texture2D chest, Vector2 heroSpritePosition, 
            Vector2 banditOneSpritePosition, Vector2 banditTwoSpritePosition, Vector2 chestSpritePosition, Color color)
        {
            
            spriteBatch.Draw(chest, new Vector2(450, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(450, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);

            spriteBatch.Draw(goodHero, heroSpritePosition, null, color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditOneSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditTwoSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);

        }

        public static void DrawEnd(SpriteBatch spriteBatch,
            SpriteFont textEnd)
        {
            var positionEnd = new Vector2(130, 1150);
            spriteBatch.DrawString(textEnd, "Game Over! Press the button X and exit.", positionEnd, Color.WhiteSmoke);
        }

        public static void DrawText(SpriteBatch spriteBatch, 
            SpriteFont textScore, SpriteFont textCollectChests, SpriteFont textHP, Color color, int Score, int HP)
        {
            var positionScore = new Vector2(1620, 1300);
            var positionCollect = new Vector2(400, 20);
            var positionHP = new Vector2(10, 1300);
            spriteBatch.DrawString(textScore, "Score:" + Score, positionScore, color);
            spriteBatch.DrawString(textCollectChests, 
                "Your task: collect chests and find exit!", positionCollect, color);
            spriteBatch.DrawString(textScore, "HP:" + HP, positionHP, color);
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