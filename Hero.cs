using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using superagent;

namespace HeroSpace
{
    public class Hero
    {
        public static void Update(GameTime gameTime, KeyboardState keyboardState, GameState GameState, Vector2 heroSpritePosition, float goodSpriteSpeed, Point heroSpriteSize, GameWindow Window,
            Vector2 banditOneSpritePosition, Point banditSpriteSize, int HP, Color color, Song songFight, Song music)
        {
            if (keyboardState.IsKeyDown(Keys.A))
                heroSpritePosition.X -= goodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.D))
                heroSpritePosition.X += goodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.W))
                heroSpritePosition.Y -= goodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.S))
                heroSpritePosition.Y += goodSpriteSpeed;

            if (heroSpritePosition.X < 0) heroSpritePosition.X = 0;
            if (heroSpritePosition.Y < 0) heroSpritePosition.Y = 0;
            if (heroSpritePosition.X > Window.ClientBounds.Width * 2.1f - heroSpriteSize.X)
                heroSpritePosition.X = Window.ClientBounds.Width * 2.1f - heroSpriteSize.X;
            if (heroSpritePosition.Y > Window.ClientBounds.Height * 2.1f - heroSpriteSize.Y)
                heroSpritePosition.Y = Window.ClientBounds.Height * 2.1f - heroSpriteSize.Y;

            if (CollideOne(heroSpritePosition, banditOneSpritePosition, heroSpriteSize, banditSpriteSize))
            {
                color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else color = Color.AntiqueWhite;

            if (CollideTwo(heroSpritePosition, banditOneSpritePosition, heroSpriteSize, banditSpriteSize))
            {
                color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else color = Color.AntiqueWhite;
        }

        public static bool CollideOne(Vector2 heroSpritePosition, Vector2 banditOneSpritePosition, Point heroSpriteSize, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditOneSpritePosition.X,
                (int)banditOneSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static bool CollideTwo(Vector2 heroSpritePosition, Vector2 banditTwoSpritePosition, Point heroSpriteSize, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)heroSpritePosition.X,
                (int)heroSpritePosition.Y, heroSpriteSize.X, heroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditTwoSpritePosition.X,
                (int)banditTwoSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static void Draw(GameTime gameTime)
        {

        }
    }
}
