using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class GridScene : Scene
    {
        public GridScene() : base()
        {

        }
        public override void Initialize()
        {
            base.Initialize();

            ClearColor = Color.Black;

            AddRenderer(new DefaultRenderer());
            Screen.SetSize(1280, 720);

            var myGrid = AddEntity(new Grid(10, 20, 32));
            Debug.DrawHollowRect(new Rectangle(624,344,32,32), Color.Red, 100000);
           
        }
    }
}
