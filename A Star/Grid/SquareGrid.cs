using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace superagent
{
    public class SquareGrid
    {
        public Basic2D SpriteGrid;
        public bool ShowGrid;
        public Vector2 SlotDims, GridDims, PhysicalStartPos, TotalPhysicalDims, CurrentHoverSlot;
        public List<List<GridLocation>> Slots = new List<List<GridLocation>>();

        public SquareGrid(Vector2 slotsDims, Vector2 startPosition, Vector2 totalDims)
        {
            ShowGrid = false;
            SlotDims = slotsDims;
            PhysicalStartPos = new Vector2((int)startPosition.X, (int)startPosition.Y);
            TotalPhysicalDims = new Vector2((int)totalDims.X, (int)totalDims.Y);
            CurrentHoverSlot = new Vector2(-1, -1);
            SpriteGrid = new Basic2D("2D\\Backgrounds\\grid", SlotDims / 2, new Vector2(SlotDims.X - 2, SlotDims.Y - 2));

            SetBaseGrid();
        }

        public virtual void Update(Vector2 offset)
        {
            CurrentHoverSlot = ...
        }

        public virtual GridLocation GetSlotFromLocation(Vector2 location)
        {
            if (location.X >= 0 && location.Y >= 0 && location.X < Slots.Count && location.Y < Slots[(int)location.X].Count)
                return Slots[(int)location.X][(int)location.Y];

            return null;
        }

        public virtual Vector2 GetSlotFromPixel(Vector2 pixel, Vector2 offset)
        {
            Vector2 adjustedPos = pixel - PhysicalStartPos + offset;
            ...
        }

        public virtual void SetBaseGrid()
        {
            GridDims = new Vector2((int)(TotalPhysicalDims.X / SlotDims.X), (int)(TotalPhysicalDims.Y / SlotDims.Y));
            Slots.Clear();

            for (int pixelX = 0; pixelX < GridDims.X; pixelX++)
            {
                Slots.Add(new List<GridLocation>());

                for (int pixelY = 0; pixelY < GridDims.Y; pixelY++)
                    Slots[pixelX].Add(new GridLocation(1, false));
            }
        }
         
        public virtual void DrawGrid(Vector2 offset)
        {
            if (ShowGrid)
            {
                Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0), Vector2.Zero);
                Vector2 botRight = GetSlotFromPixel(new Vector2(GeneralVariable.WindowWidth, GeneralVariable.WindowHeight), Vector2.Zero);

                for (int x = (int)topLeft.X; x <= botRight.X && x < Slots.Count; x++)
                {
                    for (int y = (int)topLeft.Y; y <= botRight.Y && y < Slots[0].Count; y++)
                        SpriteGrid.Draw(offset + PhysicalStartPos + new Vector2(x * SlotDims.X, y * SlotDims.Y));
                }
            }
        }
    }
}
