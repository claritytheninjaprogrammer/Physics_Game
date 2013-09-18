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
    public class GameObject : Game1
    {
        public Rectangle bounds { get; set; }

        public Vector2 position { get; set; }
        public Vector2 startPosition { get; set; }
        public Vector2 size { get; set; }

        public Vector2 origin { get; set; }
        public float rotationAngle { get; set; }

        public Vector2 acceleration { get; set; }
        public float mass { get; set; }
        public Vector2 velocity { get; set; }

        public Texture2D texture { get; set; }
    }
}
