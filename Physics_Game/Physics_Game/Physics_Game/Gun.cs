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
using System.IO;


namespace Physics_Game
{
    public class Bullet : GameObject
    {
        public bool shot { get; set; }

        public Bullet(Vector2 start_position, Vector2 initial_velocity)
        {
            position = start_position;
            startPosition = start_position;
            velocity = velocity;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, string texture_path)
        {
            using (FileStream fileStream = new FileStream(texture_path, FileMode.Open))
            {
                texture = Texture2D.FromStream(graphicsDevice, fileStream);
            }
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;// * gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, r, null, Color.Pink);

        }


    }

    public class Gun : GameObject
    {
        /* SHOOT DIRECTIONS:
         * "UP"
         * "DOWN"
         * "LEFT"
         * "RIGHT" */

        Vector2 drawOffset = new Vector2(0,0);
        GameObject parentObject;
        bool stuckTo;
        string shootDirection = "DOWN"; // default shoot down
        List<Bullet> listOfBullets = new List<Bullet>();

        public Gun(Vector2 draw_position, Vector2 vel, string shoot_direction)
        {
            position = draw_position;
            velocity = vel;
            stuckTo = false;
        }

        public Gun(GameObject stuck_to, Vector2 draw_offset, Vector2 _size, string shoot_direction)
        {
            drawOffset = draw_offset;
            parentObject = stuck_to;
            stuckTo = true;

            shootDirection = shoot_direction;
            size = _size;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, string texture_path)
        {
            using (FileStream fileStream = new FileStream(texture_path, FileMode.Open))
            {
                texture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            for (int i = 0; i < 50; i++)
            {
                Bullet b = new Bullet(new Vector2(0, 0), new Vector2(0, 0));
                b.LoadContent(graphicsDevice, texture_path);
                b.shot = false;
                listOfBullets.Add(b);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (stuckTo)
            {
                position = parentObject.position + drawOffset;
            }
            else
            {
                position += velocity;
            }

            foreach (Bullet b in listOfBullets)
            {
                if (b.shot)
                {
                    b.Update(gameTime);
                }
            }
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, r, null, Color.Red);

        }

        


        public void ShootBullet()
        {

        }
    }
}
