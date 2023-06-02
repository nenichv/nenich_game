using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace superagent
{
    public class EndOfGame
    {
        public static Texture2D Background;
        public static SpriteFont TextEnd;
        Rectangle BackSize;

        public EndOfGame(string path)
        {
            TextEnd = GeneralVariable.Content.Load<SpriteFont>("Fonts\\End");
            Background = GeneralVariable.Content.Load<Texture2D>(path);
            BackSize = new Rectangle(0, 35, 800, 600);
        }

        public void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.Escape)) Main.CloseGame = true;
        }

        public void Draw(World world)
        {
            var positionScore = new Vector2(300, 300);
            var positionConclusion = new Vector2(350, 250);
            GeneralVariable.SpriteBatch.Draw(Background, BackSize, null,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            GeneralVariable.SpriteBatch.DrawString(TextEnd, "Ты набрал " + world.Hero.Score + " очков!", positionScore, Color.WhiteSmoke);
            if (world.Hero.Score >= 40) GeneralVariable.SpriteBatch.DrawString(TextEnd, "Поздравляем!", positionConclusion, Color.WhiteSmoke);
            else GeneralVariable.SpriteBatch.DrawString(TextEnd, "Ты проиграл! Нажми Escape, чтобы выйти из игры.", positionConclusion, Color.WhiteSmoke);
        }
    }
}
