using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace superagent
{
    public class Unit : Basic2D
    {
        public bool unitIsDead;
        public float speed, distanceHit;

        public Unit(string path, Vector2 position, Vector2 size) : base(path, position, size)
        {
            unitIsDead = false;
            distanceHit = 35f;
        }

        public virtual void GetHit()
        {
            unitIsDead = true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
