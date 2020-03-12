using Microsoft.Xna.Framework;
using Nez;
using Nez.PhysicsShapes;

namespace MyFirstNez
{
    class BulletSpawnerComponent : Component
    {
        public float Cooldown = -1;
        public Vector2[] polyPoints;
        public Vector2 direction;
        public BulletSpawnerComponent(Vector2 direction)
        {
            this.direction = direction;
            polyPoints = Polygon.BuildSymmetricalPolygon(4, 16);
            //playerSquare.AddComponent(new PolygonMesh(polyPoints).SetColor(Color.CornflowerBlue));
        }
    }
}