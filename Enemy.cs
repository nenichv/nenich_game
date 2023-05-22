using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using superagent;

namespace EnemySpace
{
    public class Enemy
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 banditOneSpritePosition, float evilSpriteSpeed, Vector2 banditTwoSpritePosition,
            int HP, Color color, Song songFight, Song music)
        {
            banditOneSpritePosition.X += evilSpriteSpeed;
            if (banditOneSpritePosition.X > 1840 || banditOneSpritePosition.X < 50)
                evilSpriteSpeed *= -1;

            banditTwoSpritePosition.Y += evilSpriteSpeed;
            if (banditTwoSpritePosition.Y > 1000 || banditTwoSpritePosition.Y < 300)
                evilSpriteSpeed *= -1;
        }

        public static void Draw(GameTime gameTime)
        {

        }
    }
}
