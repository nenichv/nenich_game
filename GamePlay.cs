using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using superagent;
using Microsoft.Xna.Framework.Media;

namespace GamePlaySpace
{
    public class GamePlay
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 heroSpritePosition, int Score, int HP)
        {
            if (keyboardState.IsKeyUp(Keys.E) & (
                (heroSpritePosition.X > 400 & heroSpritePosition.X < 500) || (heroSpritePosition.Y > 400 & heroSpritePosition.Y < 500)
                || (heroSpritePosition.X > 400 & heroSpritePosition.X < 500) || (heroSpritePosition.Y > 750 & heroSpritePosition.Y < 850)
                || (heroSpritePosition.X > 1230 & heroSpritePosition.X < 1290) || (heroSpritePosition.Y > 400 & heroSpritePosition.Y < 500)
                || (heroSpritePosition.X > 1230 & heroSpritePosition.X < 1290) || (heroSpritePosition.Y > 750 & heroSpritePosition.Y < 850)))
            {
                Score += 1;
            }

            if (keyboardState.IsKeyDown(Keys.Escape)) GameState = GameState.Pause;

            if (HP <= 0 || (Keyboard.GetState().IsKeyDown(Keys.Enter) & Score >= 40) && (heroSpritePosition.X > 1600) && (heroSpritePosition.Y > 500 || heroSpritePosition.Y > 600))
            {
                MediaPlayer.Pause();
                GameState = GameState.EndOfGame;
            }
        }

        public static void Draw(GameTime gameTime, Texture2D background, SpriteBatch spriteBatch, Texture2D goodHero,
            Texture2D bandit, Texture2D chest, Vector2 heroSpritePosition,
            Vector2 banditOneSpritePosition, Vector2 banditTwoSpritePosition, Vector2 chestSpritePosition, Color color,
            SpriteFont textScore, SpriteFont textCollectChests, SpriteFont textHP, int Score, int HP, Matrix screenXform)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            DrawBackground(spriteBatch, background);
            DrawSprite(spriteBatch, goodHero, bandit, chest,
            heroSpritePosition, banditOneSpritePosition, banditTwoSpritePosition, chestSpritePosition, color);
            DrawPlayText(spriteBatch, textScore, textCollectChests, textHP, Color.Black, Score, HP);
        }

        public static void DrawBackground(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 90, 1800, 1150), Color.White);
        }

        public static void DrawSprite(SpriteBatch spriteBatch, Texture2D goodHero,
            Texture2D bandit, Texture2D chest, Vector2 heroSpritePosition,
            Vector2 banditOneSpritePosition, Vector2 banditTwoSpritePosition, Vector2 chestSpritePosition, Color color)
        {

            spriteBatch.Draw(chest, new Vector2(450, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(450, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 450), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(1280, 800), null, Color.White, 0, Vector2.Zero, 0.07f, SpriteEffects.None, 0);

            spriteBatch.Draw(goodHero, heroSpritePosition, null, color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditOneSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            spriteBatch.Draw(bandit, banditTwoSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);

        }

        public static void DrawPlayText(SpriteBatch spriteBatch,
            SpriteFont textScore, SpriteFont textCollectChests, SpriteFont textHP, Color color, int Score, int HP)
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
