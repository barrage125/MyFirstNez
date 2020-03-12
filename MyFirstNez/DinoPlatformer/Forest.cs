using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace MyFirstNez
{
    class Forest : Component
    {
        public override void OnAddedToEntity()
        {
            
            Texture2D[] textures = {
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0000_9"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0001_8"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0002_7"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0003_6"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0004_Lights"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0005_5"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0006_4"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0007_Lights"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0008_3"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0009_2"),
                Entity.Scene.Content.Load<Texture2D>("Textures/Forest/Layer_0010_1")
            };
            Entity.SetScale(new Microsoft.Xna.Framework.Vector2(((float)Screen.Width) / 928, ((float)Screen.Height) / 793));
            for (int i = 0; i < textures.Length; i++)
            {
                var sprite = new SpriteRenderer(textures[i]);
                sprite.SetRenderLayer(-textures.Length + i);
                Entity.AddComponent<SpriteRenderer>(sprite);
            }
            Entity.AddComponent(new BoxCollider(-Screen.Width / 2, Screen.Height / 2 - 28, Screen.Width, 65));
        }
    }
}
