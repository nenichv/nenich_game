using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GlobalSpace;

namespace Tasks
{
    public class Task
    {
        public static Texture2D Background;

        public static void Update()
        {

        }

        public static void Draw()
        {
            Global.spriteBatch.Draw(Background, Vector2.Zero, Color.White);
        }
    }
}
