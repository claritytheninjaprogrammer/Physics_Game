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
            mass = _mass;

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

            ApplyFriction();
            velocity = Vector2.Add(velocity, acceleration);
            velocity = Vector2.Clamp(velocity, new Vector2(-10f, -10f), new Vector2(10f, 10f));
            acceleration = new Vector2(0, 0);

           
            position += velocity; 
            
            getBounds();
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle r = getBounds();
            r = new Rectangle(r.X - (int)origin.X, r.Y - (int)origin.Y, r.Width, r.Height); 
            spriteBatch.Draw(texture, r, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);

        }

        public void ApplyForce(Vector2 force)
        {
            force = Vector2.Divide(force, mass);
            acceleration = Vector2.Add(acceleration, force);

        }

        public void ApplyFriction()
        {
            //the coefficient of friction
            float c = 0.2f;
            //the normal force (e.g. a road pushing back against a vehicle)
            float normal = 1;
            float frictionMag = c * normal;

            Vector2 friction = velocity;
            friction = Vector2.Multiply(friction, -1.0f);
            friction.Normalize();
            friction = Vector2.Multiply(friction, frictionMag);
            ApplyForce(friction);
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
                ApplyForce(new Vector2(1.0f, 0f));
            }
            

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ApplyForce(new Vector2(-1.0f, 0f));

            }

        }


        
        
    }
}
