using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class BasicScene : Scene
    {
        public BasicScene() : base()
        {
            var mapEntity = this.CreateEntity("map", new Vector2(Screen.Width / 2, Screen.Height / 2));
            mapEntity.AddComponent(new Forest());

            var playerEntity = this.CreateEntity("player", new Vector2(Screen.Width / 2, Screen.Height / 2));
            playerEntity.AddComponent(new Player());
            var collider = playerEntity.AddComponent(new CircleCollider(9.0f));

            // we only want to collide with the tilemap, which is on the default layer 0
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);

            Debug.DrawHollowRect(Camera.Bounds, Color.Green, 10000);
            Camera.Entity.AddComponent(new FollowCamera(playerEntity, Camera, FollowCamera.CameraStyle.CameraWindow));
            var followCamera = Camera.Entity.GetComponent<FollowCamera>();
            followCamera.SetCenteredDeadzone(20, 4);
            Debug.DrawHollowRect(followCamera.Deadzone, Color.Red, 10000);
        }
    }
}
