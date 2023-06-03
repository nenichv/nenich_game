using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace superagent
{
    public class Basic2D
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 SizeTexture; 

        public Basic2D(string pathToFile, Vector2 position, Vector2 size)
        {
            Texture = GeneralVariable.Content.Load<Texture2D>(pathToFile);
            Position = position; SizeTexture = size;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(Vector2 offset)
        {
            GeneralVariable.SpriteBatch.Draw(Texture, new Rectangle((int)(Position.X + offset.X), (int)(Position.Y + offset.Y), (int)SizeTexture.X, (int)SizeTexture.Y), null, 
                Color.White, 0.0f, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public virtual void Draw(Vector2 offset, Color color)
        {
            GeneralVariable.SpriteBatch.Draw(Texture, new Rectangle((int)(Position.X + offset.X), (int)(Position.Y + offset.Y), (int)SizeTexture.X, (int)SizeTexture.Y), null,
                color , 0.0f, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
    }
}
