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
    class Player : GameObject
    {
        public Player(Vector2 initial_position, Vector2 initial_velocity, Vector2 initial_size, float _mass)
        {
            position = initial_position;
            startPosition = initial_position;
            velocity = initial_velocity;
            size = initial_size;

            rotationAngle = 0;
            origin = new Vector2(size.X / 2, size.Y / 2);

            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public void LoadContent(GraphicsDevice graphicsDevice, string texture_path)
        {
            using (FileStream fileStream = new FileStream(@"Content/whitepx.jpg", FileMode.Open))
            {
                texture = Texture2D.FromStream(graphicsDevice, fileStream);
            }
        }

        public void Update()
        {
            getInput();            
           
            position += velocity; 
            
            getBounds();
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle r = getBounds();
            r = new Rectangle(r.X - (int)origin.X, r.Y - (int)origin.Y, r.Width, r.Height); 
            spriteBatch.Draw(texture, r, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);

        }

       

        

        public Rectangle getBounds()
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            return bounds;
        }

        public void getInput()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (velocity.X < 0)
                {
                    velocity += new Vector2(0.5f, 0f);

                }
                else
                {
                    velocity += new Vector2(0.1f, 0f);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (velocity.X > 0)
                {
                    velocity += new Vector2(-0.5f, 0f);

                }
                else
                {
                    velocity += new Vector2(-0.1f, 0f);
                }

            }

            if (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                if (velocity.X < 0.1 && velocity.X > -0.1)
                {
                    velocity = Vector2.Zero;
                }
                else if (velocity.X > 0.1)
                {
                    velocity += new Vector2(-0.3f, 0f);
                }
                else if (velocity.X < -0.1)
                {
                    velocity += new Vector2(0.3f, 0f);
                }
                
            }

        }


        
        
    }
}
