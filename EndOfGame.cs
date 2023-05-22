using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using superagent;

namespace EndOfGameSpace
{
    public class EndOfGame
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState state)
        {

        }

        public static void Draw(SpriteBatch spriteBatch, SpriteFont textEnd, int Score, Texture2D backGameover)
        {
            var positionScore = new Vector2(500, 500);
            var positionConclusion = new Vector2(80, 700);
            spriteBatch.Draw(backGameover, new Rectangle(0, 0, 1800, 1400), Color.White);
            spriteBatch.DrawString(textEnd, "You gained " + Score + " points!", positionScore, Color.WhiteSmoke);
            if (Score >= 40) spriteBatch.DrawString(textEnd, "Congratulations! The first level is passed! ", positionConclusion, Color.WhiteSmoke);
            else spriteBatch.DrawString(textEnd, "You lose! Press the X to exit!", positionConclusion, Color.WhiteSmoke);

        }
    }
}
