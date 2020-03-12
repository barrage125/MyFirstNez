using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class Grid : Entity
    {
        struct GridRange
        {
            public int Minimum;
            public int Maximum;
        }

        private GridRange Width, Height;
        public int tileSize, currentWidth, currentHeight;


        public Grid(int minWidth, int maxWidth, int minHeight, int maxHeight, int tileSize)
        {
            Width.Minimum = minWidth;
            Width.Maximum = maxWidth;
            Height.Minimum = minHeight;
            Height.Maximum = maxHeight;
            this.tileSize = tileSize;
        }

        public Grid(int minimum, int maximum, int tileSize) : this(minimum, maximum, minimum, maximum, tileSize)
        {
        }

        //Sets intial position of grid to screen center
        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            BuildGrid();
        }

        public Vector2 GridPosition(int tileX, int tileY)
        {
            float posX = Position.X + tileX * tileSize;
            float posY = Position.Y + tileY * tileSize;
            return new Vector2(posX, posY);

            //Should edit to throw error if out of bounds
            //return new Vector2(-1, -1);
        }

        //Populates Grid with Tile entities. Public for testing purposes
        public void BuildGrid()
        {
            currentWidth = Nez.Random.NextInt(Width.Maximum - Width.Minimum) + Width.Minimum;
            currentHeight = Nez.Random.NextInt(Height.Maximum - Height.Minimum) + Height.Minimum;

            for (int x = 0; x < currentWidth; x++)
            {
                for (int y = 0; y < currentHeight; y++)
                {
                    Scene.AddEntity(new Tile(x, y, this));
                }
            }

            Transform.SetPosition((Screen.Width - ((currentWidth) * tileSize)) / 2, (Screen.Height - ((currentHeight) * tileSize)) / 2);
            Debug.DrawHollowRect(new Rectangle((int)Position.X, (int)Position.Y, currentWidth * tileSize, currentHeight * tileSize), Color.Green, 100000);
        }
    }
}
