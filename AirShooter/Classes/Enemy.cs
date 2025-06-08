using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AirShooter.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.SaveData;
using SharpDX.Direct3D9;

namespace AirShooter.Classes
{
    public class Enemy
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _collision;
        private float _speed = 2.0f;
        private int _widthScreen;
        private int _timer = 0;
        private int _maxTime = 200;

        private List<Bullet> _bullets;
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        public int Width
        {
            get => _texture.Width;
        }
        public int Height
        {
            get => _texture.Height;
        }
        public Rectangle Collision
        {
            get => _collision;
        }
        public List<Bullet> Bullets
        {
            get => _bullets;
        }
        public bool IsAlive;
        public Enemy(int widthScreen)
        {
            _texture = null;
            IsAlive = true;
            _widthScreen = widthScreen;
            _bullets = new List<Bullet>();
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("enemy");  //простите за текстуру(((
        }
        public void Update(ContentManager content)
        {
            _position.X += _speed;
            _timer++;
            if (_timer >= _maxTime)
            {
                Bullet bullet = new Bullet(new Vector2(4, 0),
                    "enemyBullet", null, _widthScreen);
                bullet.Position = new Vector2(_position.X + _texture.Width / 2 -
                    bullet.Width / 2, _position.Y + bullet.Height / 4);
                bullet.LoadContent(content);
                _bullets.Add(bullet);
                _timer = 0;
            }
            if (_position.X > _widthScreen)
            {
                IsAlive = false;
            }
            _collision = new Rectangle((int)_position.X, (int)_position.Y,
                Width, Height);
            foreach (Bullet bullet in _bullets)
            {
                bullet.Update();
            }
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].IsAlive == false)
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}

