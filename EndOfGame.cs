using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroSpace;

namespace EndOfGameSpace
{
    public class EndOfGame
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont TextEnd;

        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            var positionScore = new Vector2(500, 500);
            var positionConclusion = new Vector2(80, 700);
            spriteBatch.Draw(Background, new Rectangle(0, 0, 1800, 1400), Color.White);
            spriteBatch.DrawString(TextEnd, "You gained " + Hero.Score + " points!", positionScore, Color.WhiteSmoke);
            if (Hero.Score >= 40) spriteBatch.DrawString(TextEnd, "Congratulations! The first level is passed! ", positionConclusion, Color.WhiteSmoke);
            else spriteBatch.DrawString(TextEnd, "You lose! Press the X to exit!", positionConclusion, Color.WhiteSmoke);

        }
    }
}
