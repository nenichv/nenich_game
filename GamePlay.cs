using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using HeroSpace;
using EnemySpace;

namespace GamePlaySpace
{
    public class GamePlay
    {
        public static void Update()
        {
            
        }

        public static void Draw(Texture2D backGamePlay, SpriteBatch spriteBatch, Texture2D chest, 
            SpriteFont textScore, SpriteFont textCollectChests, SpriteFont textHP, double Score, int HP)
        {
            DrawBackground(spriteBatch, backGamePlay);
            DrawSprite(spriteBatch, chest);
            DrawPlayText(spriteBatch, textScore, textCollectChests, textHP, Color.White, Score, HP);
        }

        public static void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 90, 1800, 1150), Color.White);
        }

        public static void DrawSprite(SpriteBatch spriteBatch, Texture2D chest)
        {

            spriteBatch.Draw(chest, new Vector2(450, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(450, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
        }

        public static void DrawPlayText(SpriteBatch spriteBatch,
            SpriteFont textScore, SpriteFont textCollectChests, SpriteFont textHP, Color color, double Score, int HP)
        {
            var positionScore = new Vector2(1620, 1300);
            var positionCollect = new Vector2(400, 20);
            var positionHP = new Vector2(10, 1300);
            spriteBatch.DrawString(textScore, "Score:" + Score, positionScore, color);
            spriteBatch.DrawString(textCollectChests,
                "Your task: collect chests and find exit!", positionCollect, color);
            spriteBatch.DrawString(textScore, "HP:" + HP, positionHP, color);
        }
    }
}
