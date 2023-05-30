using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace superagent
{
    public class McGameState
    {
        public Menu Menu;
        public Task Task;
        public GamePlay GamePlay;
        public Searching Searching;
        public Pause Pause;
        public EndOfGame EndOfGame;

        public static GameState state = GameState.Menu;

        public McGameState()
        {
            Menu = new Menu("2D\\Backgrounds\\menu");
            Task = new Task("2D\\Backgrounds\\task");
            GamePlay = new GamePlay("2D\\Backgrounds\\backGame");
            Searching = new Searching("2D\\Backgrounds\\search", new string[] {"notebook", "redKey", "verevka"});
            Pause = new Pause("2D\\Backgrounds\\pause");
            EndOfGame = new EndOfGame("2D\\Backgrounds\\gameover");
        }

        public virtual void Update(World world)
        {
            switch (state)
            {
                case GameState.Menu:
                    Menu.Update();
                    break;
                case GameState.Task:
                    Task.Update();
                    break;
                case GameState.GamePlay:
                    GamePlay.Update(world);
                    break;
                case GameState.Searching:
                    Searching.Update();
                    break;
                case GameState.Pause:
                    Pause.Update();
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Update();
                    break;
            }
        }

        public virtual void Draw(World world)
        {
            switch (state)
            {
                case GameState.Menu:
                    Menu.Draw();
                    break;
                case GameState.Task:
                    Task.Draw();
                    break;
                case GameState.GamePlay:
                    GamePlay.Draw(world);
                    break;
                case GameState.Searching:
                    Searching.Draw();
                    break;
                case GameState.Pause:
                    Pause.Draw();
                    break;
                case GameState.EndOfGame:
                    EndOfGame.Draw(world);
                    break;
            }
        }
    }

    public enum GameState
    {
        Menu,
        Task,
        GamePlay,
        Searching,
        Pause,
        EndOfGame,
    }

}
