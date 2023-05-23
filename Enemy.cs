using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemySpace
{
    public class Enemy
    {
        public static Point BanditSpriteSize;
        public static Vector2 BanditOneSpritePosition;
        public static float EvilSpriteSpeed = 9f;
        public static Vector2 BanditTwoSpritePosition;
        public static Texture2D Bandit;
        public static Color Color;

        public static void Update()
        {
            BanditOneSpritePosition.X += EvilSpriteSpeed;
            if (BanditOneSpritePosition.X > 1840 || BanditOneSpritePosition.X < 50) EvilSpriteSpeed *= -1;

            BanditTwoSpritePosition.Y += EvilSpriteSpeed;
            if (BanditTwoSpritePosition.Y > 1000 || BanditTwoSpritePosition.Y < 300)  EvilSpriteSpeed *= -1;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bandit, BanditOneSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            spriteBatch.Draw(Bandit, BanditTwoSpritePosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
        }
    }
}
