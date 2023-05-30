using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace superagent
{
    public class Searching
    {
        public List<SearchiObj> Objects;
        Texture2D Background;
        Rectangle BackSize;

        public Searching(string path, string[] names)
        {
            var folder = "2D\\Interface\\Searching\\First\\";
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(0, 90, 800, 600);
            Objects = new List<SearchiObj>();
            for (int i = 0; i < names.Length; i++)
                Objects.Add(new SearchiObj(names[i], folder, new Vector2(300, 200), new Vector2(64, 64)));
        }

        public void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Enter)) McGameState.state = GameState.GamePlay;
            foreach (var obj in Objects)
                obj.Update();
        }

        public void Draw()
        {
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            foreach (var obj in Objects)
                obj.Draw();
        }
    }
}
