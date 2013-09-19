using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Physics_Game
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Player player1;

        public PlayerManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            player1 = new Player(new Vector2(StaticVar.ScreenWidth / 2, StaticVar.ScreenHeight - 50), new Vector2(0.01f, 0), new Vector2(50, 10), 50);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            player1.LoadContent(StaticVar.texture, GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            player1.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            player1.Draw(GraphicsDevice, StaticVar.spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
