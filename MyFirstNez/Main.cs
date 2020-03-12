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
            Core.DebugRenderEnabled = true;
            // TODO: Add your initialization logic here
            base.Initialize();
            Scene = new GridScene();
        }
    }
}
