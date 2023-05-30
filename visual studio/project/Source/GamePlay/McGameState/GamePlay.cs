using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace superagent
{
    public class GamePlay
    {
        public static SpriteFont TextScore;
        public static SpriteFont TextCollectChests;
        public static SpriteFont TextHP;
        Texture2D Background;
        Rectangle BackSize;

        public GamePlay(string path)
        {
            TextScore = GeneralVariable.Content.Load<SpriteFont>("Fonts\\Score");
            TextCollectChests = GeneralVariable.Content.Load<SpriteFont>("Fonts\\CollectChests");
            TextHP = GeneralVariable.Content.Load<SpriteFont>("Fonts\\File");
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(0, 90, 800, 600);
        }

        public void Update(World world)
        {
            world.Update();

            if (world.Hero.HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.Enter) & world.Hero.Score >= 40) && (world.Hero.Position.X > 1600) && (world.Hero.Position.Y > 500 || world.Hero.Position.Y > 600))
            {
                MediaPlayer.Pause();
                McGameState.state = GameState.EndOfGame;
            }
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Escape)) McGameState.state = GameState.Pause;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.X)) McGameState.state = GameState.Task;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.E) && world.Hero.ChestCollisionFlag) McGameState.state = GameState.Searching;
        }

        public void Draw(World world)
        {
            
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            GeneralVariable.SpriteBatch.DrawString(TextScore, "Score:" + world.Hero.Score, new Vector2(1620, 1300), Color.White);
            GeneralVariable.SpriteBatch.DrawString(TextCollectChests, "Your task: collect chests and find exit!", new Vector2(400, 20), Color.White);
            GeneralVariable.SpriteBatch.DrawString(TextScore, "HP:" + world.Hero.HP, new Vector2(10, 1300), Color.White);
            world.Draw();
        }
    }
}
