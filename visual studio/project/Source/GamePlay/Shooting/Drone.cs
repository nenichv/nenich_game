using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace superagent
{
    public class Drone : Projectiles
    {
        public Drone(Vector2 position, Unit owner, Vector2 target) : base("2D\\Objects\\drone",position, new Vector2(5, 5), owner, target)
        {

        }

        public override void Update(Vector2 Offset, List<Enemy> units)
        {
            base.Update(Offset, units);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
