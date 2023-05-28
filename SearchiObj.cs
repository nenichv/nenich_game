using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GlobalSpace;

namespace SearchObject
{
    public class SearchiObj
    {
        public readonly string Name;
        public Vector2 Position;
        private Texture2D Texture;
        public bool Found;
        private int foundFlag;

        public SearchiObj(string name, string folder, Vector2 position)
        {
            Name = name;
            Position = position;
            Texture = Global.Content.Load<Texture2D>(folder + name);
            Found = false;
            foundFlag = 0;
        }

        public void Update()
        {
            var a = Mouse.GetState().Position;
            if ((a.X < Texture.Width && a.X > Position.X) && (a.Y < Texture.Height && a.Y > Position.Y)) Found = true;

        }

        public void Draw()
        {
            if (!Found) Global.spriteBatch.Draw(Texture, Position, Color.White);
            if (Found)
            {
                if (foundFlag < 5)
                {
                    Global.spriteBatch.Draw(Texture, Position, Color.Yellow);
                    foundFlag++;
                }
            }
        }
    }
}
