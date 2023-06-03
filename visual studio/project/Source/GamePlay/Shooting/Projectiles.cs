using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace superagent
{
    public class Projectiles : Basic2D
    {
        public Unit Owner;
        public TimerControl timer;
        public Vector2 direction;
        public bool droneIsDone;
        public float speed;

        public Projectiles(string pathToFile, Vector2 position, Vector2 size, Unit owner, Vector2 target) : base(pathToFile, position, size)
        {
            droneIsDone = false;
            speed = 6f;
            Owner = owner;
            direction = target - owner.Position;
            direction.Normalize();
            timer = new TimerControl(1200);
        }

        public virtual void Update(Vector2 Offset, List<Enemy> units)
        {
            Position += direction * speed;
            timer.UpdateTimer();
            if (timer.Test()) droneIsDone = true;
            if (HitSomething(units)) droneIsDone = true;
        }

        public virtual bool HitSomething(List<Enemy> units)
        {
            for (var i = 0; i < units.Count; i++)
            {
                if (GeneralVariable.GetDistance(Position, units[i].Position) < units[i].distanceHit)
                {
                    units[i].GetHit();
                    return true;
                }
            }
            return false;
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
