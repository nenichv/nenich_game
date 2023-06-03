using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace superagent
{
    public class Searching
    {
        public List<SearchiObj> Objects;
        readonly Texture2D Background;
        Rectangle BackSize;
        public bool objectIsFind;

        public Searching(string path, string folder, string[] names)
        {
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(0, 35, 800, 600);
            Objects = new List<SearchiObj>();
            
            for (int obj = 0; obj < names.Length; obj++)
                Objects.Add(new SearchiObj(names[obj], folder, RandomizePosition(), RandomizeSize()));
        }

        public static Vector2 RandomizePosition()
        {
            var random = new Random();
            return new Vector2(random.Next(10, 600), random.Next(10, 600));
        }

        public static Vector2 RandomizeSize()
        {
            var random = new Random();
            return new Vector2(random.Next(16, 128), random.Next(16, 128));
        }

        public void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Enter) && objectIsFind) GameStateControl.state = GameState.GamePlay;

            foreach (var obj in Objects)
            {
                obj.Update();

                if (!obj.Found) objectIsFind = false;
                else objectIsFind = true;
            }
        }

        public void Draw(Vector2 offset)
        {
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            foreach (var obj in Objects)
                obj.Draw(offset);
        }
    }
}
