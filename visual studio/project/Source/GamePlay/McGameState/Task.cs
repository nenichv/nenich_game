using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class Task
    {
        Texture2D Background;
        Rectangle BackSize;

        public Task(string path)
        {
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(0, 90, 800, 600);
        }

        public void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.X)) McGameState.state = GameState.GamePlay;
        }

        public void Draw()
        {
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
