using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Physics_Game
{
    public static class StaticVar
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static int ScreenWidth;
        public static int ScreenHeight;

        public static PlayerManager playerManager;

        // Global textures to reduce runtime load content time
        public static Texture2D texture;
    }
}
