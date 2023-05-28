using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SearchObject;
using System.Collections.Generic;

namespace Search
{
    public class Searching
    {
        public Texture2D Background;
        public List<SearchiObj> Objects;

        public Searching(Texture2D background, string folderPath, string[] names)
        {
            Background = background;

            for (int i = 0; i < names.Length; i++)
                Objects.Add(new SearchiObj(names[i], folderPath, new Vector2(300, 200)));
        }

        public void Update()
        {
            foreach (var obj in Objects)
                obj.Update();
        }

        public void Draw()
        {
            Global.spriteBatch.Draw(Background, Vector2.Zero, Color.White);

            foreach (var obj in Objects)
                obj.Draw();
        }
    }
}
