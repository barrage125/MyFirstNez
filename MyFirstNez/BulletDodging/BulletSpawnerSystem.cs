using Microsoft.Xna.Framework;
using Nez;
using System;

namespace MyFirstNez
{
    class BulletSpawnerSystem : EntityProcessingSystem
    {
        public BulletSpawnerSystem(Matcher matcher) : base(matcher)
        {
        }

        public override void Process(Entity entity)
        {
            var spawners = entity.GetComponents<BulletSpawnerComponent>();

            foreach (BulletSpawnerComponent spawner in spawners) { 
                spawner.Cooldown -= Time.DeltaTime;
                if (spawner.Cooldown <= 0)
                {
                    spawner.Cooldown = 0.5f;

                    CreateBullet(entity.Position.X, entity.Position.Y, spawner.polyPoints, entity, spawner.direction);
                }
            }
        }

        void CreateBullet(float posX, float posY, Vector2[] polyPoints, Entity entity, Vector2 direction)
        {
            var myBullet = entity.Scene.CreateEntity("bullet");
            myBullet.SetPosition(posX, posY);
            myBullet.AddComponent(new PolygonMesh(polyPoints).SetColor(Color.Crimson));
            myBullet.AddComponent(new PolygonCollider(polyPoints));
            myBullet.AddComponent(new Bullet(direction));
        }
    }

    class Bullet : Component, IUpdatable
    {
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        ProjectileMover _pmover;
        float _moveSpeed = 100f;
        Vector2 moveDir;
        public Bullet(Vector2 direction)
        {
            moveDir = direction;
        }
        public override void OnAddedToEntity()
        {
            _pmover = Entity.AddComponent(new ProjectileMover());
        }
        void IUpdatable.Update()
        {
            if (moveDir != Vector2.Zero)
            {
                //var movement = moveDir * _moveSpeed * Time.DeltaTime;
                var wave = new Vector2((float) (10 * (Mathf.Sin(Time.FrameCount/20) * Math.PI / 180)), moveDir.Y);
                var movement = wave * _moveSpeed * Time.DeltaTime;

                //_pmover.CalculateMovement(ref movement, out var res);
                //_subpixelV2.Update(ref movement);
                _pmover.Move(movement);
            }

            if (Entity.Position.Y > Screen.Height * 1.5 || Entity.Position.Y < Screen.Height * -0.5 || Entity.Position.X > Screen.Width * 1.5 || Entity.Position.X < Screen.Width * -0.5)
                Entity.Destroy();
        }
    }
}