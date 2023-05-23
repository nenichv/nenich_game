using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PauseSpace
{
    public class Pause
    {
        public static Texture2D Background { get; set; }

        public static void Update()
        {
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);
        }
    }
}
