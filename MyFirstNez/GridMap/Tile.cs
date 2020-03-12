using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class Tile : Entity
    {
        struct GridPos
        {
            public int PosX;
            public int PosY;
        }

        private GridPos Location;
        private int tileSize;

        public Tile(int gridX, int gridY, Grid grid)
        {
            Location.PosX = gridX;
            Location.PosY = gridY;
            tileSize = grid.tileSize;
            Transform.SetParent(grid.Transform);
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();
            Transform.SetLocalPosition(new Vector2(Location.PosX * tileSize, Location.PosY * tileSize));
            Debug.DrawHollowBox(Position + new Vector2(tileSize / 2, tileSize / 2), tileSize, Color.CornflowerBlue, 100000);
        }
    }
}
