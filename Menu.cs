using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using superagent;

namespace MenuSpace
{
    public class Menu
    {
        public static void Update(Game1 game, GameTime gameTime, KeyboardState keyboardState, GameState GameState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape)) game.Exit();
            
        }

        public static void Draw(GameTime gameTime)
        {

        }

    }
}
