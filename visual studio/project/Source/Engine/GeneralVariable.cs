using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace superagent
{
    public class GeneralVariable
    {
        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;
        public static KeyboardControl Keyboard;
        public static int WindowWidth, WindowHeight;
        public static MouseControl Mouse;

        public static float GetDistance(Vector2 position, Vector2 goal)
        {
            return (float)Math.Sqrt(Math.Pow(position.X - goal.X, 2) + Math.Pow(position.Y - goal.Y, 2));
        }

        public static Vector2 Movement(Vector2 position, Vector2 goal, float speed)
        {
            float distance = GetDistance(position, goal);
            if (distance < speed) return goal - position;
            else return (goal - position) * speed / distance;
        }
    }
}
