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
            BackSize = new Rectangle(0, 35, 800, 600);
        }

        public void Update(World world)
        {
            world.Update();

            if (world.Hero.HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.V) & world.Hero.Score >= 40) && (world.Hero.Position.X > 600) && (world.Hero.Position.Y > 250 || world.Hero.Position.Y < 300))
            {
                MediaPlayer.Pause();
                GameStateControl.state = GameState.EndOfGame;
            }
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Escape)) GameStateControl.state = GameState.Pause;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.T)) GameStateControl.state = GameState.Task;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.E) && world.Hero.ChestCollisionFlag) GameStateControl.state = GameState.Searching;
        }

        public void Draw(World world)
        {
            
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            GeneralVariable.SpriteBatch.DrawString(TextScore, "Score:" + world.Hero.Score, new Vector2(700, 645), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            GeneralVariable.SpriteBatch.DrawString(TextCollectChests, "Collect chests and find exit!", new Vector2(225, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            GeneralVariable.SpriteBatch.DrawString(TextScore, "HP:" + world.Hero.HP, new Vector2(20, 645), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            world.Draw();
        }
    }
}
