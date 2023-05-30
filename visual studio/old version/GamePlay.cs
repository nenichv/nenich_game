using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroSpace;
using GlobalSpace;

namespace GamePlaySpace
{
    public class GamePlay
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont TextScore;
        public static SpriteFont TextCollectChests;
        public static SpriteFont TextHP;

        public static void Update()
        {
            
        }

        public static void Draw()
        {
            Global.spriteBatch.Draw(Background, new Rectangle(0, 90, 1800, 1150), Color.White);
            DrawText();
        }

        public static void DrawText()
        {
            Global.spriteBatch.DrawString(TextScore, "Score:" + Hero.Score, new Vector2(1620, 1300), Color.White);
            Global.spriteBatch.DrawString(TextCollectChests, "Your task: collect chests and find exit!", new Vector2(400, 20), Color.White);
            Global.spriteBatch.DrawString(TextScore, "HP:" + Hero.HP, new Vector2(10, 1300), Color.White);
        }
    }
}
