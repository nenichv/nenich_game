using GlobalSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemySpace
{
    public class Enemy
    {
        public static Point EnemySize;
        public static Vector2 FirstEnemyPosition = new Vector2(300, 600);
        public static Vector2 SecondEnemyPosition = new Vector2(850, 300);
        public static float EnemySpeed = 9f;
        public static Texture2D TextureEnemy;
        public static Color Color;

        public static void Update()
        {
            FirstEnemyPosition.X += EnemySpeed;
            if (FirstEnemyPosition.X > 1840 || FirstEnemyPosition.X < 50) EnemySpeed *= -1;

            SecondEnemyPosition.Y += EnemySpeed;
            if (SecondEnemyPosition.Y > 1000 || SecondEnemyPosition.Y < 300)  EnemySpeed *= -1;
        }

        public static void Draw()
        {
            Global.spriteBatch.Draw(TextureEnemy, FirstEnemyPosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
            Global.spriteBatch.Draw(TextureEnemy, SecondEnemyPosition, null, Color.White, 0, Vector2.Zero, 0.09f, SpriteEffects.None, 0);
        }
    }
}
