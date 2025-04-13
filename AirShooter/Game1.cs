using AirShooter.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monogame.Classes;
using System;
using System.Collections.Generic;

namespace AirShooter
{
    public class Game1 : Game
    {
        private const int COUNT_MINES = 10;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //поля
        private Player _player;
        private Background _background;
        private Mine _mine;
        private List<Mine> _mines;
        private List<Explosion> _explosions;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();
            _background = new Background();
            _mines = new List<Mine>();
            _explosions = new List<Explosion>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _player.LoadContent(Content);
            _background.LoadContent(Content);
            for (int i = 0; i < 10; i++)
            {
                Mine mine = new Mine();
                mine.LoadContent(Content);
                Random random = new Random();
                int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                       - mine.Width);
                int y = random.Next(0, _graphics.PreferredBackBufferHeight);
                mine.Position = new Vector2(x, -y);
                _mines.Add(mine);
            }
        }

        protected override void Update(GameTime gameTime)
        {
             if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _player.Update(
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight, Content);
            _background.Update();
            MinesUpdate();
            CheckCollision();
            UpdExplosions(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here 
            _spriteBatch.Begin();
            {
                _background.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                foreach (Mine mine in _mines)
                {
                    mine.Draw(_spriteBatch);
                }
                foreach (Explosion explosion in _explosions)
                {
                    explosion.Draw(_spriteBatch);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void MinesUpdate()
        {
            for (int i = 0; i < _mines.Count; i++)
            {
                Mine mine = _mines[i];
                mine.Update();
                if (mine.Position.Y > _graphics.PreferredBackBufferHeight)
                {
                    Random random = new Random();
                    int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                       - mine.Width);
                    int y = random.Next(0, _graphics.PreferredBackBufferHeight);
                    mine.Position = new Vector2(x, -y);
                }
                if (!mine.IsAlive)
                {
                    _mines.Remove(mine);
                    i--;
                }
            }
            if (_mines.Count < COUNT_MINES)
            {
                LoadMine();
            }
        }
        private void LoadMine()
        {
            Mine mine = new Mine();
            mine.LoadContent(Content);
            Random random = new Random();
            int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                   - mine.Width);
            int y = random.Next(0, _graphics.PreferredBackBufferHeight);
            mine.Position = new Vector2(x, -y);
            _mines.Add(mine);
        }
        private void CheckCollision()
        {
            foreach (Mine mine in _mines)
            {
                if (mine.Collision.Intersects(_player.Collision))
                {
                    mine.IsAlive = false;
                    Explosion explosion = new Explosion(mine.Position);
                    explosion.LoadContent(Content);
                    _explosions.Add(explosion);
                }
                foreach (Bullet bullet in _player.Bullets)
                {
                    if (mine.Collision.Intersects(bullet.Collision))
                    {
                        bullet.IsAlive = false;
                        mine.IsAlive = false;
                        Explosion explosion = new Explosion(mine.Position);
                        explosion.LoadContent(Content);
                        _explosions.Add(explosion);
                    }
                }
            }
        }
        private void UpdExplosions(GameTime gameTime)
        {
            for (int i = 0; i < _explosions.Count; i++)
            {
                _explosions[i].Update(gameTime);
                if (!_explosions[i].IsAlive)
                {
                    _explosions.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
