using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using superagent;

namespace PauseSpace
{
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
}
