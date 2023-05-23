using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GamePlaySpace;
using EndOfGameSpace;
using MenuSpace;
using PauseSpace;
using HeroSpace;
using EnemySpace;

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
        Texture2D chest;
        Song music;
        Song songFight;
        private Matrix screenXform;
        public readonly Rectangle screenBounds;
        SpriteFont textScore;
        SpriteFont textCollectChests;
        SpriteFont textHP;
        SpriteFont textEnd;
        GameState GameState = GameState.Menu;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Hero.HeroSpritePosition = new Vector2(230, 260);
            Enemy.BanditOneSpritePosition = new Vector2(300, 600);
            Enemy.BanditTwoSpritePosition = new Vector2(850, 300);
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
            Hero.GoodHero = Content.Load<Texture2D>("герой");
            Enemy.Bandit = Content.Load<Texture2D>("враг");
            chest = Content.Load<Texture2D>("сундук");
            Hero.HeroSpriteSize = new Point(Hero.GoodHero.Width / 10, Hero.GoodHero.Height / 10);
            Enemy.BanditSpriteSize = new Point(Enemy.Bandit.Width / 10, Enemy.Bandit.Height / 10);
            music = Content.Load<Song>("material-fonovogo-zvuka-igrovoy-stsenyi-39470");
            songFight = Content.Load<Song>("удар");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
            textScore = Content.Load<SpriteFont>("Score");
            textCollectChests = Content.Load<SpriteFont>("CollectChests");
            textHP = Content.Load<SpriteFont>("HP");
            textEnd = Content.Load<SpriteFont>("End");
            Menu.Background = Content.Load<Texture2D>("menu");
            Pause.Background = Content.Load<Texture2D>("PAUSE");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            switch (GameState)
            {
                case GameState.Menu:
                    {
                        Menu.Update();
                        if (keyboardState.IsKeyDown(Keys.Space))
                            GameState = GameState.GamePlay;
                        if (keyboardState.IsKeyDown(Keys.Escape)) Exit();
                        break;
                    }
                case GameState.GamePlay:
                    {
                        GamePlay.Update();
                        Hero.Update(keyboardState, Window, Enemy.BanditOneSpritePosition, Enemy.BanditTwoSpritePosition, Enemy.BanditSpriteSize, songFight, music);
                        Enemy.Update();

                        if (Hero.HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.Enter) & Hero.Score >= 40) && (Hero.HeroSpritePosition.X > 1600) && (Hero.HeroSpritePosition.Y > 500 || Hero.HeroSpritePosition.Y > 600))
                        {
                            MediaPlayer.Pause();
                            GameState = GameState.EndOfGame;
                        }
                        if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.Pause;
                        break;
                    }
                case GameState.Pause:
                    {
                        Pause.Update();
                        if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.GamePlay;
                        break;
                    }
                case GameState.EndOfGame:
                    EndOfGame.Update();
                    if (keyboardState.IsKeyDown(Keys.X)) Exit();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            switch (GameState)
            {
                case GameState.Menu:
                    Menu.Draw(spriteBatch);
                    break;
                case GameState.GamePlay:
                    GamePlay.Draw(background, spriteBatch, chest, textScore, textCollectChests, textHP, Hero.CollectScore(), Hero.HP);
                    Hero.Draw(spriteBatch);
                    Enemy.Draw(spriteBatch);
                    break;
                case GameState.Pause:
                    Pause.Draw(spriteBatch);
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Draw(spriteBatch, textEnd, Hero.Score, backGameover);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}