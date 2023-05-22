using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace superagent
{
    public enum GameState
    {
        Menu,
        GamePlay,
        Pause,
        EndOfGame,
    }

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
        GameState GameState = GameState.Menu;
        
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

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            switch (GameState)
            {
                case GameState.Menu:
                    Menu.Update(this, gameTime, keyboardState, GameState);
                    break;
                case GameState.GamePlay:
                    GamePlay.Update(gameTime, keyboardState, GameState, heroSpritePosition, Score);
                    break;
                case GameState.Pause:
                    Pause.Update(gameTime, keyboardState, GameState);
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Update(gameTime, keyboardState, GameState);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            Drawing.DrawBackground(spriteBatch, background);
            Drawing.DrawSprite(spriteBatch, goodHero, bandit, chest,
            heroSpritePosition, banditOneSpritePosition, banditTwoSpritePosition, chestSpritePosition, color);
            Drawing.DrawPlayText(spriteBatch, textScore, textCollectChests, textHP, Color.Black, Score, HP);

            if (HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.Enter) & Score >= 40) && (heroSpritePosition.X > 1600) && (heroSpritePosition.Y > 500 || heroSpritePosition.Y > 600))
            {
                MediaPlayer.Pause();
                GameState = GameState.EndOfGame;
            }

            switch (GameState)
            {
                case GameState.Menu:
                    Menu.Draw(gameTime);
                    break;
                case GameState.GamePlay:
                    GamePlay.Draw(gameTime);
                    break;
                case GameState.Pause:
                    Pause.Draw(gameTime);
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Draw(spriteBatch, textEnd, Score, backGameover);
                    break;
                
            }
            spriteBatch.End();
            base.Draw(gameTime);
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

        public static void DrawPlayText(SpriteBatch spriteBatch, 
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


    }


    public class Menu
    {
        public static void Update(Game1 game, GameTime gameTime, KeyboardState keyboardState, GameState GameState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape)) game.Exit();
            if (keyboardState.IsKeyDown(Keys.Space))
                GameState = GameState.GamePlay;
        }

        public static void Draw(GameTime gameTime)
        {
            
        }

    }

    public class GamePlay
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 heroSpritePosition, int Score)
        {
            if (keyboardState.IsKeyUp(Keys.E) & (
                (heroSpritePosition.X > 400 & heroSpritePosition.X < 500) || (heroSpritePosition.Y > 400 & heroSpritePosition.Y < 500)
                || (heroSpritePosition.X > 400 & heroSpritePosition.X < 500) || (heroSpritePosition.Y > 750 & heroSpritePosition.Y < 850)
                || (heroSpritePosition.X > 1230 & heroSpritePosition.X < 1290) || (heroSpritePosition.Y > 400 & heroSpritePosition.Y < 500)
                || (heroSpritePosition.X > 1230 & heroSpritePosition.X < 1290) || (heroSpritePosition.Y > 750 & heroSpritePosition.Y < 850)))
            {
                Score += 1;
            }

            if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.Pause;
        }

        public static void Draw(GameTime gameTime)
        {

        }
    }

    public class Pause
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.GamePlay;
        }

        public static void Draw(GameTime gameTime)
        {

        }
    }

    public class EndOfGame
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState state)
        {

        }

        public static void Draw(SpriteBatch spriteBatch, SpriteFont textEnd, int Score, Texture2D backGameover)
        {
            var positionScore = new Vector2(500, 500);
            var positionConclusion = new Vector2(80, 700);
            spriteBatch.Draw(backGameover, new Rectangle(0, 0, 1800, 1400), Color.White);
            spriteBatch.DrawString(textEnd, "You gained " + Score + " points!", positionScore, Color.WhiteSmoke);
            if (Score >= 40) spriteBatch.DrawString(textEnd, "Congratulations! The first level is passed! ", positionConclusion, Color.WhiteSmoke);
            else spriteBatch.DrawString(textEnd, "You lose! Press the X to exit!", positionConclusion, Color.WhiteSmoke);

        }
    }

    public class Enemy
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 banditOneSpritePosition, float evilSpriteSpeed, Vector2 banditTwoSpritePosition,
            int HP, Color color,  Song songFight, Song music)
        {
            banditOneSpritePosition.X += evilSpriteSpeed;
            if (banditOneSpritePosition.X > 1840 || banditOneSpritePosition.X < 50)
                evilSpriteSpeed *= -1;

            banditTwoSpritePosition.Y += evilSpriteSpeed;
            if (banditTwoSpritePosition.Y > 1000 || banditTwoSpritePosition.Y < 300)
                evilSpriteSpeed *= -1;
        }

        

        public static void Draw(GameTime gameTime)
        {

        }
    }

    public class Hero
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 heroSpritePosition, float goodSpriteSpeed, Point heroSpriteSize, GameWindow Window,
            Vector2 banditOneSpritePosition, Point banditSpriteSize, int HP, Color color, Song songFight, Song music)
        {
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

            if (CollideOne(heroSpritePosition, banditOneSpritePosition, heroSpriteSize, banditSpriteSize))
            {
                color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else color = Color.AntiqueWhite;

            if (CollideTwo(heroSpritePosition, banditOneSpritePosition, heroSpriteSize, banditSpriteSize))
            {
                color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else color = Color.AntiqueWhite;
        }

        public static  bool CollideOne(Vector2 heroSpritePosition, Vector2 banditOneSpritePosition, Point heroSpriteSize, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditOneSpritePosition.X,
                (int)banditOneSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static bool CollideTwo(Vector2 heroSpritePosition, Vector2 banditTwoSpritePosition, Point heroSpriteSize, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditTwoSpritePosition.X,
                (int)banditTwoSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static void Draw(GameTime gameTime)
        {

        }
    }
}