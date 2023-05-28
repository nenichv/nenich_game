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
using Search;
using ChestSpace;

namespace superagent
{
    public enum GameState
    {
        Menu,
        GamePlay,
        Searching,
        Pause,
        EndOfGame,
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Searching SearchingObjects;
        SpriteBatch spriteBatch;
        Song music;
        Song songFight;
        GameState GameState = GameState.Menu;
        private Matrix screenXform;
        public readonly Rectangle screenBounds;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            var screenScale = graphics.PreferredBackBufferHeight / 1090.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);
            IsMouseVisible = true;
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
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.Content = this.Content;

            GamePlay.Background = Content.Load<Texture2D>("backGame");
            GamePlay.TextScore = Content.Load<SpriteFont>("Score");
            GamePlay.TextCollectChests = Content.Load<SpriteFont>("CollectChests");
            GamePlay.TextHP = Content.Load<SpriteFont>("HP");

            Menu.Background = Content.Load<Texture2D>("menu");
            Pause.Background = Content.Load<Texture2D>("pause");

            EndOfGame.Background = Content.Load<Texture2D>("gameover");
            EndOfGame.TextEnd = Content.Load<SpriteFont>("End");

            Hero.TextureHero = Content.Load<Texture2D>("hero");
            Hero.Size = new Point((int)(Hero.TextureHero.Width * 0.13), (int)(Hero.TextureHero.Height * 0.13));

            Enemy.TextureEnemy = Content.Load<Texture2D>("enemy");
            Enemy.EnemySize = new Point((int)(Enemy.TextureEnemy.Width * 0.09), (int)(Enemy.TextureEnemy.Height * 0.09));

            Chest.TextureChest = Content.Load<Texture2D>("chest");
            
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
            
            music = Content.Load<Song>("music");
            songFight = Content.Load<Song>("удар");

            var background1 = Content.Load<Texture2D>("search");
            var arrayObj = new string[] { "apple", "bike" };
            SearchingObjects = new Searching(background1, "search1\\", arrayObj);
        }

        protected override void UnloadContent()
        {
            
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
                        Hero.Update(keyboardState, Window, Enemy.FirstEnemyPosition, Enemy.SecondEnemyPosition, Enemy.EnemySize, songFight, music);
                        Enemy.Update();
                        Chest.Update();

                        if (Hero.HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.Enter) & Hero.Score >= 40) && (Hero.Position.X > 1600) && (Hero.Position.Y > 500 || Hero.Position.Y > 600))
                        {
                            MediaPlayer.Pause();
                            GameState = GameState.EndOfGame;
                        }
                        if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.Pause;
                        if (Hero.GetTrueToCollect()) GameState = GameState.Searching;
                        break;
                    }
                case GameState.Searching:
                    {
                        SearchingObjects.Update();
                        if (keyboardState.IsKeyDown(Keys.Enter)) GameState = GameState.GamePlay;
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
                    GamePlay.Draw(spriteBatch);
                    Hero.Draw(spriteBatch);
                    Enemy.Draw(spriteBatch);
                    Chest.Draw(spriteBatch);
                    break;
                case GameState.Searching:
                    SearchingObjects.Draw();
                    break;
                case GameState.Pause:
                    Pause.Draw(spriteBatch);
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}