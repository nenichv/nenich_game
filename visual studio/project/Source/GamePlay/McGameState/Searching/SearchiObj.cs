using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class SearchiObj : Basic2D
    {
        public bool Found;
        private int foundFlag;

        public SearchiObj(string name, string folder, Vector2 position, Vector2 dimension) : base(folder+name, position, dimension)
        {
            Found = false; foundFlag = 0;
        }

        public override void Update()
        {
            var positionMouse = Mouse.GetState().Position;
            if (positionMouse.X < Position.X + SizeTexture.X && positionMouse.X > Position.X && positionMouse.Y < Position.Y + SizeTexture.Y && positionMouse.Y > Position.Y && 
                GeneralVariable.Mouse.LeftClick()) 
                Found = true;
        }

        public override void Draw(Vector2 offset)
        {
            if (!Found) base.Draw(offset);
            else
            {
                if (foundFlag < 20)
                {
                    base.Draw(offset, Color.Green);
                    foundFlag++;
                }
            }
        }
    }
}
