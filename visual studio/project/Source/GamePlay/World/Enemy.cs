using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace superagent
{
    public class Enemy : Basic2D
    {
        public float Speed;

        public Enemy(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
        {
            Speed = 1.5f;
        }

        public virtual void Update(Hero Hero)
        {
            Position += GeneralVariable.Movement(Position, Hero.Position, Speed);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
