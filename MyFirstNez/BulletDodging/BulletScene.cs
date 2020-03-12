using Microsoft.Xna.Framework;
using Nez;
using Nez.PhysicsShapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class BulletScene : Scene
    {
        public BulletScene() : base()
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            ClearColor = Color.Black;

            AddRenderer(new DefaultRenderer());
            Screen.SetSize(1280, 720);

            var polyPoints = new Box(32, 32).Points; //Polygon.BuildSymmetricalPolygon(4, 32);
            //Polygon.RotatePolygonVerts((float)(45*Math.PI/180), polyPoints, polyPoints);
            var playerSquare = CreateEntity("player");
            playerSquare.AddComponent(new BasicMovementComponent());
            playerSquare.SetPosition(640, 360);
            playerSquare.AddComponent(new PolygonMesh(polyPoints).SetColor(Color.CornflowerBlue));
            playerSquare.AddComponent(new PolygonCollider(polyPoints));

            var bulletSpawnNW = CreateEntity("spawner");
            bulletSpawnNW.SetPosition(160, -50);
            bulletSpawnNW.AddComponent(new BulletSpawnerComponent(new Vector2(0, 1)));

            var bulletSpawnNE = CreateEntity("spawner");
            bulletSpawnNE.SetPosition(800, -50);
            bulletSpawnNE.AddComponent(new BulletSpawnerComponent(new Vector2(0, 1)));

            var bulletSpawnSW = CreateEntity("spawner");
            bulletSpawnSW.SetPosition(480, 770);
            bulletSpawnSW.AddComponent(new BulletSpawnerComponent(new Vector2(0, -1)));

            var bulletSpawnSE = CreateEntity("spawner");
            bulletSpawnSE.SetPosition(1120, 770);
            bulletSpawnSE.AddComponent(new BulletSpawnerComponent(new Vector2(0, -1)));

            AddEntityProcessor(new BulletColliderSystem(new Matcher().All(typeof(BasicMovementComponent))));
            AddEntityProcessor(new BulletSpawnerSystem(new Matcher().All(typeof(BulletSpawnerComponent))));
            
        }
    }
}
