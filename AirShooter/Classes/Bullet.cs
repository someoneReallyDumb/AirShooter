using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirShooter.Classes.SaveData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AirShooter.Classes
{
    public class Bullet
    {
        private Texture2D _texture;
        private int _width = 20;
        private int _height = 20;
        private int _speed = 5;
        private Rectangle _destinationRectangle;
        private bool _isAlive;
        private Vector2 _velocity;
        private string _bulletTexture;
        private int _widthScreen;

        private SoundEffect _soundEffect;
        private string _soundEffectName;
        public Vector2 Position
        {
            get
            {
                return new Vector2(_destinationRectangle.X, _destinationRectangle.Y);
            }
            set
            {
                _destinationRectangle.X = (int)value.X;
                _destinationRectangle.Y = (int)value.Y;
            }
        }
        public Rectangle Collision
        {
            get { return _destinationRectangle;  }
        }
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
        }
        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }
        public Bullet(Vector2 velocity, string bulletTexture, string soundEffect, int widthScreen)
        {
            _texture = null;
            IsAlive = true;
            _destinationRectangle = new Rectangle(100, 300, _width, _height);
            _velocity = velocity;
            _widthScreen = widthScreen;
            _bulletTexture = bulletTexture;
            _soundEffectName = soundEffect;
        }
        public void LoadContent(ContentManager content)
        {
            if (_bulletTexture == "laser")
            {
                _texture = content.Load<Texture2D>("laser");
            }
            else
            {
                _texture = content.Load<Texture2D>("enemyBullet");
            }
            if (_soundEffectName != null)
            {
                _soundEffect = content.Load<SoundEffect>(_soundEffectName);
            }
        }
        public void Update()
        {
            _destinationRectangle.Y += (int)_velocity.Y;
            _destinationRectangle.X += (int)_velocity.X;
            if (_destinationRectangle.Y <= 0 - _height)
            {
                _isAlive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRectangle, Color.White);
        }
        public object SaveData()
        {
            BulletData data = new BulletData();
            {
                Position = this.Position;
                IsAlive = _isAlive;
            };
            return data;
        }

        public void LoadData(object data, ContentManager content)
        {
            if (!(data is BulletData))
            {
                return;
            }
            BulletData bulletData = (BulletData)data;
            Position = bulletData.Position;
            _isAlive = bulletData.IsAlive;
        }
    }
}
