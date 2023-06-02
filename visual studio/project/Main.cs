using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace superagent 
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        World world;
        Song music;
        GameStateControl GameStates;
        public static bool CloseGame = false;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 670;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GeneralVariable.SpriteBatch = new SpriteBatch(GraphicsDevice);
            GeneralVariable.Content = Content;
            GeneralVariable.Keyboard = new KeyboardControl();
            GeneralVariable.Mouse = new MouseControl();
            GeneralVariable.WindowWidth = Window.ClientBounds.Width;
            GeneralVariable.WindowHeight = Window.ClientBounds.Height;

            world = new World(new Vector2(200, 250), new Vector2(200, 400), new Vector2(630, 250), new Vector2(630, 400));
            GameStates = new GameStateControl();

            var enemiesPositions = new List<Vector2>() { new Vector2(150, 300), new Vector2(425, 150) };
            var enemiesMovementLogic = new List<Func<Vector2, float, Vector2>>()
            {
                (position, speed) => { position.X += speed; return position; },
                (position, speed) => { position.Y += speed; return position; }
            };
            world.CreateEnemies(enemiesPositions, new Vector2(64, 64), enemiesMovementLogic);

            music = GeneralVariable.Content.Load<Song>("Audio\\music");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            GeneralVariable.Keyboard.Update();
            GeneralVariable.Mouse.Update();
            GameStates.Update(world);
            if (CloseGame) Exit();
            GeneralVariable.Keyboard.UpdateOld();
            GeneralVariable.Mouse.UpdateOld();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GeneralVariable.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
            GameStates.Draw(world);
            GeneralVariable.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class Program
    {
        public static void Main()
        {
            Main game = new();
            game.Run();
        }
    }
}