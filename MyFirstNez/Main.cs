using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace MyFirstNez
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Core
    {        
        public Main() : base()
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            var scene = new Scene();

            var brick = scene.Content.Load<Texture2D>("Textures/Bricks");
            var entity = scene.CreateEntity("floor");
            entity.AddComponent<SpriteRenderer>(new SpriteRenderer(brick));

            Core.Scene = scene;
        }
    }
}
