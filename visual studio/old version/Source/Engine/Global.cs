using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GlobalSpace;

/* В классе Global мы просто сами создаем объекты и переменные которыми хотим пользоваться. Да, возможно мы будем присваивать им значения, 
 * которые уже где-то хранились и мы могли обратиться напрямую к ним, НО
 * своя реализация удобнее, т.к. здесь все на глазу. Это помогает нам использовать, например, размеры игрового окна в любом файле и методе, и 
 * совсем необязательно передать постоянно GameWindow window в каждый метод. Удобно.
 */
public class Global
{
    public static ContentManager Content;
    public static SpriteBatch spriteBatch;
    public static int WindowWidth, WindowHeight;
}
