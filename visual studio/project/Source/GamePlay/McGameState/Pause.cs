using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class Pause
    {
        Texture2D Background;
        Rectangle BackSize;

        public Pause(string path)
        {
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(GeneralVariable.WindowWidth / 2, GeneralVariable.WindowHeight / 2, 400, 200);
        }

        public void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Space)) GameStateControl.state = GameState.GamePlay;
        }

        public void Draw()
        {
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, new Vector2(200, 100), SpriteEffects.None, 0);
        }
    }
}
