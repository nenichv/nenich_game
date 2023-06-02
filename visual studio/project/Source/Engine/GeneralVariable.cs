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
    }
}
