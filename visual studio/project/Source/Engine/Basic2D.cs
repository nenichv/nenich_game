using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace superagent
{
    /* Итак,Basic2D - наследуемый класс, который мы используем при создании всех других классов-объектов. У всех объектов, таких как сундук, противник, игрок, есть одна и
     * та же структура, а именно их моделька texture, координаты position и размер dimension. Вместо того, чтобы внутри каждого класса писать одни и те же строки, мы реализуем это
     * здесь, а затем заставляем все классы наследовать Basic2D. Внутри классов из папки World постараюсь подробнее пояснить.
     * И, так как все объекты на карте действуют по одному и тому же принципу - Update и Draw, то тут мы их делаем ВИРТУАЛЬНЫМИ. Виртуальные методы могут быть ПЕРЕПИСАНЫ 
     * при использовании ключевого слова override. В чем соль: когда мы переписываем виртуальный метод, можно внизу написать, к примеру, base.Update(). 
     * То есть, если мы по сути каждую эту картинку в любом случае каким-то одинаковым образом отрисовываем, то мы можем сделать это внутри
     * класса Basic2D. В наследующих классах мы просто в конце делаем приписочку base.Draw();
     * Машина - транспортное средство. Грузовик - транспортное средство. Мотоцикл - транспортное средство. У них есть что-то общее, поэтому 
     * это общее мы не обговариваем - они все могут осуществлять транспортировку, причем по поверхности ездят. Но у каждого из них это сделано по-своему.
     * Тут тоже самое. Игрок, враги, сундуки - это все одни и те же модельки в игре. Они все обновляются и все отрисовываются. Поэтому они все наследуют одну и ту же логику
     * из Basic2D, но внутри них самих есть что-то, что их отличает. */
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
