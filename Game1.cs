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
                    GamePlay.Update(gameTime, keyboardState, GameState, heroSpritePosition, Score, HP);
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
            switch (GameState)
            {
                case GameState.Menu:
                    Menu.Draw(gameTime);
                    break;
                case GameState.GamePlay:
                    GamePlay.Draw(gameTime, background, spriteBatch, goodHero, bandit, chest, heroSpritePosition, 
                        banditOneSpritePosition, banditTwoSpritePosition, chestSpritePosition, color,
                        textScore, textCollectChests, textHP, Score, HP, screenXform);
                    break;
                case GameState.Pause:
                    Pause.Draw(gameTime);
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Draw(spriteBatch, textEnd, Score, backGameover);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}