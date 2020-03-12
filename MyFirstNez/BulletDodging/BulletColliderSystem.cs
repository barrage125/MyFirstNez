using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class BulletColliderSystem : EntityProcessingSystem
    {
        public BulletColliderSystem(Matcher matcher) : base(matcher)
        {
        }

        public override void Process(Entity entity)
        {
            var polyCollider = entity.GetComponent<PolygonCollider>();
            var polyBounds = polyCollider.Bounds;
            Debug.DrawHollowRect(polyBounds, Color.White);
            var colliders = Physics.BoxcastBroadphaseExcludingSelf(polyCollider);

            foreach (var coll in colliders)
            {
                CollisionResult collResult;
                if (entity.GetComponent<PolygonCollider>().CollidesWith(coll, out collResult))
                {
                    //TriggerDamage(coll.entity, entity);
                    //entity.GetComponent<PolygonMesh>().SetColor(Color.DarkGreen);
                    //coll.Entity.GetComponent<PolygonMesh>().SetColor(Color.DarkGreen);
                    //collResult.Collider.Entity.GetComponent<PolygonMesh>().SetColor(Color.DarkGreen);
                    //Debug.DrawHollowRect(coll.Entity.GetComponent<PolygonMesh>().Bounds, Color.White);
                    //coll.Entity.Enabled = false;
                    //entity.Destroy();
                    coll.Entity.Destroy();
                }
            }
        }
    }
}
