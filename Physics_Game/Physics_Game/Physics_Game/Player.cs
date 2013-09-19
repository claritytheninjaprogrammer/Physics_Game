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
        bool gunsActive = false;
        List<Gun> listOfGuns = new List<Gun>();

        GraphicsDevice graphicsDevice;

        public Player(Vector2 initial_position, Vector2 initial_velocity, Vector2 initial_size, float _mass)
        {
            position = initial_position;
            startPosition = initial_position;
            velocity = initial_velocity;
            size = initial_size;

            rotationAngle = 0;
            origin = new Vector2(size.X / 2, size.Y / 2);

        }

        public void LoadContent(Texture2D _tex, GraphicsDevice gDevice)
        {

            texture = _tex;
            
            graphicsDevice = gDevice;
        }

        public void Update(GameTime gameTime)
        {
            getInput();
            CheckCollisionScreenBounds();
           
            position += velocity;

            if (gunsActive)
            {
                foreach (Gun g in listOfGuns)
                {
                    g.Update(gameTime);
                }
            }
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle r = getBounds();
            //r = new Rectangle((int)position.X, (int)position.Y, r.Width, r.Height);
            spriteBatch.Draw(texture, r, null, Color.White);

            if (gunsActive)
            {
                foreach (Gun g in listOfGuns)
                {
                    g.Draw(graphicsDevice, spriteBatch, gameTime);
                }
            }
        }

        public bool CheckCollisionScreenBounds()
        {
            // Position colliding left
            if (getBounds().Left <= 0)
            {
                position = new Vector2(1 + origin.X, position.Y);
                velocity = new Vector2(0, velocity.Y);
                return true;
            }
            // Position + width of sprite colliding right
            else if (getBounds().Right >= StaticVar.ScreenWidth)
            {
                position = new Vector2((StaticVar.ScreenWidth - getBounds().Width - 1) + origin.X, position.Y);
                velocity = new Vector2(0, velocity.Y);
                return true;
            }
            return false;
        }

        public Rectangle getBounds()
        {
            bounds = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, (int)size.X, (int)size.Y);
            return bounds;
        }

        bool space_pressed = false;
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
                if (velocity.X < 0.3 && velocity.X > -0.3)
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

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                initialiseGuns();
            }

            if (space_pressed == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    foreach (Gun g in listOfGuns)
                    {
                        g.ShootBullet(new Vector2(0, -3), new Vector2(3, 6));
                    }
                    space_pressed = true;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                space_pressed = false;
            }

        }

        public void initialiseGuns()
        {
            Vector2 gun_offset = new Vector2(-(origin.X - 3), -(origin.Y +3));
            Vector2 gun_size = new Vector2(3, getBounds().Height+6);
            Gun gun1 = new Gun(this, gun_offset, gun_size, "UP");
            gun1.LoadContent(graphicsDevice, StaticVar.texture);
            listOfGuns.Add(gun1);

            gun_offset = new Vector2((origin.X - 6), -(origin.Y + 3));
            gun_size = new Vector2(3, getBounds().Height + 6);
            Gun gun2 = new Gun(this, gun_offset, gun_size, "UP");
            gun2.LoadContent(graphicsDevice, StaticVar.texture);
            listOfGuns.Add(gun2);

            gunsActive = true;
        }
        
        
    }
}
