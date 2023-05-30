using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace superagent
{
    /* Итак,Basic2D - наследуемый класс, который мы используем при создании всех других классов-объектов. Поясняю: У всех объектов, таких как сундук, противник, игрок, есть одна и
     * та же структура, а именно их моделька texture, координаты position и размер dimension. Вместо того, чтобы внутри каждого класса писать одни и те же строки, мы реализуем это
     * здесь, а затем заставляем все классы наследовать Basic2D. Внутри классов из папки World постараюсь подробнее пояснить.
     * И, так как все объекты на карте действуют по одному и тому же принципу - Update и Draw, то тут мы их делаем ВИРТУАЛЬНЫМИ. Виртуальные методы могут быть ПЕРЕПИСАНЫ 
     * при использовании ключевого слова override. В чем соль: когда мы переписываем виртуальный метод, можно внизу написать, к примеру, base.Update();, что ты часто
     * увидишь в этих методах. То есть, если мы по сути каждую эту картинку в любом случае каким-то одинаковым образом отрисовываем, то мы можем сделать это внутри
     * класса Basic2D. В наследующих классах мы просто в конце делаем приписочку base.Draw();
     * Вспомни лекции про наследование. Машина - транспортное средство. Грузовик - транспортное средство. Мотоцикл - транспортное средство. У них есть что-то общее, поэтому 
     * это общее мы не обговариваем - они все могут осуществлять транспортировку, причем по поверхности ездят. Но у каждого из них это сделано по-своему.
     * Тут тоже самое. Игрок, враги, сундуки - это все одни и те же модельки в игре. Они все обновляются и все отрисовываются. Поэтому они все наследуют одну и ту же логику
     * из Basic2D, но внутри них самих есть что-то, что их отличает. Загляни в Hero для подробностей. */
    public class Basic2D
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 SizeTexture; //вместо уменьшения картинки в 2,3 раза при создании объекта можно указать его размер.
                                  //если dimension будет 64 на 64, то картинка растянется 64 на 64.

        public Basic2D(string pathToFile, Vector2 position, Vector2 size)
        {
            Texture = GeneralVariable.Content.Load<Texture2D>(pathToFile); //вместо того, чтобы подгружать все текстурки в Main, мы просто переносим эту подгрузку
                                                            //внутрь классов и передаем ПУТЬ к файлу

            Position = position; SizeTexture = size;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw() // тут просто отрисовка картинки в нужном нам квадратике. size X и Y - ширина и высота картинки, так и регулируется размер.
        {
            GeneralVariable.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)SizeTexture.X, (int)SizeTexture.Y), null, 
                Color.White, 0.0f, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public virtual void Draw(Color color) //это зовется перегрузкой метода. Одно и то же название, разные принимаемые значения. Тут мы принимаем на вход цвет, 
                                              //и он будет ставить нужный нам цвет. Если цвет не поставим - он выполнит верхний метод, а не этот.
        {
            GeneralVariable.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)SizeTexture.X, (int)SizeTexture.Y), null,
                color , 0.0f, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
    }
}
