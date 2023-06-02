using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class KeyboardControl
    {
        public KeyboardState State;
        public KeyboardState OldState;

        public KeyboardControl()
        {

        }

        public void Update()
        {
            State = Keyboard.GetState();
        }

        public void UpdateOld()
        {
            OldState = State;
        }
    }
}
