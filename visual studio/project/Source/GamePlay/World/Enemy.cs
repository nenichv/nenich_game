using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace superagent
{
    public class Enemy : Basic2D
    {
        private Func<Vector2, float, Vector2> movementUpdate;
        public float Speed;

        public Enemy(string path, Vector2 pos, Vector2 dims, Func<Vector2, float, Vector2> movementLogic) : base(path, pos, dims)
        {
            movementUpdate = movementLogic;
            Speed = 7f;
        }

        public override void Update()
        {
            Position = movementUpdate(Position, Speed);
            if (Position.Y > 560 || Position.Y < 100 || Position.X > 760 || Position.X < 100) Speed *= -1;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
