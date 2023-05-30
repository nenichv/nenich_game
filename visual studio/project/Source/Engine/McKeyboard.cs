using Microsoft.Xna.Framework.Input;

namespace superagent
{
    /* Простая реализация обновления клавиатуры, чтобы не путаться в переменных. В Main.Update не придется создавать локальную переменную, state клавиатуры будет храниться здесь
     * Это все также удобно в использовании и реализации. При запоминании предыдущей клавиатуры удобно проверить как долго человек удерживает клавишу.
     * Если и в прошлом и в нынешнем состоянии клавиша нажата -- он ее удерживает и так далее. Подобная схема позволит избежать 7 ударов за долю секунды если мы просто нажали клавишу за
     * семь апдейтов. и так далее. 
     */
    public class McKeyboard
    {
        public KeyboardState State;
        public KeyboardState OldState;

        public McKeyboard()
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
