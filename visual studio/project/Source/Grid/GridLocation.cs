using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace superagent
{
    public class GridLocation
    {
        public Vector2 Parent, Position;
        public float fScore, Cost, CurrentDist;
        public bool Full, Impassible, UnPathable;

        public GridLocation(float cost, bool full)
        {
            Cost = cost; Full = full;
            UnPathable = false; Impassible = false;
        }

        public virtual void SetToFull(bool impassible)
        {
            Full = true;
            Impassible = impassible;
        }
    }
}
