using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class MouseControl
    {
        public Vector2 newMousePosition, oldMousePosition;
        public MouseState newMouse, oldMouse;

        public MouseControl()
        {
            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            newMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            GetMouseAndAdjust();
        }

        public MouseState New
        {
            get { return newMouse; }
        }

        public MouseState Old
        {
            get { return oldMouse; }
        }

        public void Update()
        {
            GetMouseAndAdjust();
        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePosition = GetScreenPos(oldMouse);
        }

        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePosition = GetScreenPos(newMouse);
        }

        public Vector2 GetScreenPos(MouseState mouse)
        {
            Vector2 positionMouse = new Vector2(mouse.Position.X, mouse.Position.Y);
            return positionMouse;
        }

        public virtual bool LeftClick()
        {
            if (newMouse.LeftButton == ButtonState.Pressed
                && oldMouse.LeftButton != ButtonState.Pressed 
                && newMouse.Position.X >= 0 
                && newMouse.Position.X <= GeneralVariable.WindowWidth 
                && newMouse.Position.Y >= 0 
                && newMouse.Position.Y <= GeneralVariable.WindowHeight)
                return true;

            return false;
        }

        public virtual bool RightClick()
        {
            if (newMouse.RightButton == ButtonState.Pressed 
                && oldMouse.RightButton != ButtonState.Pressed 
                && newMouse.Position.X >= 0 
                && newMouse.Position.X <= GeneralVariable.WindowWidth 
                && newMouse.Position.Y >= 0 
                && newMouse.Position.Y <= GeneralVariable.WindowHeight)
                return true;

            return false;
        }
    }
}
