using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GlobalSpace;

namespace HeroSpace
{
    public class Hero
    {
        public static Texture2D TextureHero { get; set; }
        public static Color Color;
        public static Vector2 Position = new Vector2(230, 260);
        public static float Speed = 3f;
        public static Point Size;
        public static int HP = 100;
        public static double Score = 0;


        public static void Update(KeyboardState keyboardState, GameWindow Window, Vector2 firstEnemyPosition, Vector2 secondEnemyPosition, Point enemySize, Song songFight, Song music)
        {
            if (keyboardState.IsKeyDown(Keys.A))
                Position.X -= Speed;
            if (keyboardState.IsKeyDown(Keys.D))
                Position.X += Speed;
            if (keyboardState.IsKeyDown(Keys.W))
                Position.Y -= Speed;
            if (keyboardState.IsKeyDown(Keys.S))
                Position.Y += Speed;

            if (Position.X < 0) Position.X = 0;
            if (Position.Y < 0) Position.Y = 0;
            if (Position.X > Window.ClientBounds.Width * 2.1f - Size.X)
                Position.X = Window.ClientBounds.Width * 2.1f - Size.X;
            if (Position.Y > Window.ClientBounds.Height * 2.1f - Size.Y)
                Position.Y = Window.ClientBounds.Height * 2.1f - Size.Y;

            if (CollideWithEnemy(enemySize, firstEnemyPosition, secondEnemyPosition))
            {
                Color = Color.Red;
                HP -= 1;
                MediaPlayer.Play(songFight);
                MediaPlayer.Play(music);
            }
            else Color = Color.AntiqueWhite;

            Score = CollectScore();
        }

        public static bool CollideWithEnemy(Point enemySpriteSize, params Vector2[] enemyPositions)
        {
            for (int enemy = 0; enemy < enemyPositions.Length ; enemy++)
            {
                Rectangle heroRect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
                Rectangle enemyRect = new Rectangle((int)enemyPositions[enemy].X, (int)enemyPositions[enemy].Y, enemySpriteSize.X, enemySpriteSize.Y);
                if (heroRect.Intersects(enemyRect)) return true;
            }
            return false;
        }

        public static double CollectScore()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.C) & (
                (Position.X > 400 & Position.X < 500) || (Position.Y > 400 & Position.Y < 500)
                || (Position.X > 400 & Position.X < 500) || (Position.Y > 750 & Position.Y < 850)
                || (Position.X > 1230 & Position.X < 1290) || (Position.Y > 400 & Position.Y < 500)
                || (Position.X > 1230 & Position.X < 1290) || (Position.Y > 750 & Position.Y < 850))) Score += 5;
            return Score;
        }

        public static bool GetTrueToCollect()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) & (
                (Position.X > 400 & Position.X < 500) || (Position.Y > 400 & Position.Y < 500)
                || (Position.X > 400 & Position.X < 500) || (Position.Y > 750 & Position.Y < 850)
                || (Position.X > 1230 & Position.X < 1290) || (Position.Y > 400 & Position.Y < 500)
                || (Position.X > 1230 & Position.X < 1290) || (Position.Y > 750 & Position.Y < 850))) return true;

            return false;
        }

        public static void Draw()
        {
            Global.spriteBatch.Draw(TextureHero, Position, null, Color, 0, Vector2.Zero, 0.13f, SpriteEffects.None, 0);
        }
    }
}
