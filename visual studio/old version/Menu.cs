using GlobalSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuSpace
{
    public class Menu
    {
        public static Texture2D Background { get; set; }

        public static void Update()
        {
            
        }

        public static void Draw()
        {
            Global.spriteBatch.Draw(Background, Vector2.Zero, Color.White);
        }

    }
}
