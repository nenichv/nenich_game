using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSpace
{
    public class Hero
    {
        public static Texture2D GoodHero { get; set; }
        public static Color Color;
        public static Vector2 HeroSpritePosition;
        public static float GoodSpriteSpeed = 3f;
        public static Point HeroSpriteSize;
        public static int HP = 100;
        public static double Score = 0;


        public static void Update(KeyboardState keyboardState, GameWindow Window, Vector2 banditOneSpritePosition, Vector2 banditTwoSpritePosition, Point banditSpriteSize, Song songFight, Song music)
        {
            if (keyboardState.IsKeyDown(Keys.A))
                HeroSpritePosition.X -= GoodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.D))
                HeroSpritePosition.X += GoodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.W))
                HeroSpritePosition.Y -= GoodSpriteSpeed;
            if (keyboardState.IsKeyDown(Keys.S))
                HeroSpritePosition.Y += GoodSpriteSpeed;

            if (HeroSpritePosition.X < 0) HeroSpritePosition.X = 0;
            if (HeroSpritePosition.Y < 0) HeroSpritePosition.Y = 0;
            if (HeroSpritePosition.X > Window.ClientBounds.Width * 2.1f - HeroSpriteSize.X)
                HeroSpritePosition.X = Window.ClientBounds.Width * 2.1f - HeroSpriteSize.X;
            if (HeroSpritePosition.Y > Window.ClientBounds.Height * 2.1f - HeroSpriteSize.Y)
                HeroSpritePosition.Y = Window.ClientBounds.Height * 2.1f - HeroSpriteSize.Y;

            if (CollideOne(banditOneSpritePosition, banditSpriteSize))
            {
                Color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else Color = Color.AntiqueWhite;

            if (CollideTwo(banditTwoSpritePosition, banditSpriteSize))
            {
                Color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else Color = Color.AntiqueWhite;
        }

        public static bool CollideOne(Vector2 banditOneSpritePosition, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)HeroSpritePosition.X, (int)HeroSpritePosition.Y, HeroSpriteSize.X, HeroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditOneSpritePosition.X, (int)banditOneSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static bool CollideTwo(Vector2 banditTwoSpritePosition, Point banditSpriteSize)
        {
            Rectangle goodSpriteRect = new Rectangle((int)HeroSpritePosition.X, (int)HeroSpritePosition.Y, HeroSpriteSize.X, HeroSpriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)banditTwoSpritePosition.X, (int)banditTwoSpritePosition.Y, banditSpriteSize.X, banditSpriteSize.Y);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }

        public static double CollectScore()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) & (
                (HeroSpritePosition.X > 400 & HeroSpritePosition.X < 500) || (HeroSpritePosition.Y > 400 & HeroSpritePosition.Y < 500)
                || (HeroSpritePosition.X > 400 & HeroSpritePosition.X < 500) || (HeroSpritePosition.Y > 750 & HeroSpritePosition.Y < 850)
                || (HeroSpritePosition.X > 1230 & HeroSpritePosition.X < 1290) || (HeroSpritePosition.Y > 400 & HeroSpritePosition.Y < 500)
                || (HeroSpritePosition.X > 1230 & HeroSpritePosition.X < 1290) || (HeroSpritePosition.Y > 750 & HeroSpritePosition.Y < 850)))
            {
                Score += 0.1;
            }
            return Score;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GoodHero, HeroSpritePosition, null, Color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
        }
    }
}
