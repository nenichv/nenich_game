using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace superagent
{
    public class Chest : Basic2D
    {
        // private Searching searchingLogic; или
        // private static Searching[] searchLevels;
        public Chest(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        /* Что было бы неплохо добавить.
         * Как по мне, чтобы не запариваться с привязкой конкретного уровня поиска к сундуку, можно сделать своей фичей то, что при активации сундука выпадает рандомный из 4.
         * И после этого его убирать. Для этого можно в Сhest добавить статическое поле List<Searching>. Если происходит взаимодействие с сундуком то мы вытаскиваем как-нибудь
         * рандомный Searching и запускаем. После чего удаляем из листа. Как-нибудь поиграть с этим кароче.
         */
    }
}
