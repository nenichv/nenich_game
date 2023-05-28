using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ChestSpace
{
    public class Chest
    {
        public static Texture2D TextureChest { get; set; }

        public static void Update()
        {
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureChest, new Vector2(400, 450), null, Color.White, 0, Vector2.Zero, 0.15f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureChest, new Vector2(400, 800), null, Color.White, 0, Vector2.Zero, 0.15f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureChest, new Vector2(1230, 450), null, Color.White, 0, Vector2.Zero, 0.15f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureChest, new Vector2(1230, 800), null, Color.White, 0, Vector2.Zero, 0.15f, SpriteEffects.None, 0);
        }
    }
}
