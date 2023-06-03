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

        //public GridLocation(Vector2 position, float cost, bool full, float fscore)
        //{
        //    Cost = cost;
        //    Full = full;
        //    UnPathable = false;
        //    Position = position;
        //    fScore = fscore;
        //}

        //public void SetNode(Vector2 parent, float fscore, float current)
        //{
        //    Parent = parent;
        //    fScore = fscore;
        //    CurrentDist = current;
        //}

        //public virtual void SetToFull(bool impassible)
        //{
        //    Full = true;
        //    Impassible = impassible;
        //}
    }
}
