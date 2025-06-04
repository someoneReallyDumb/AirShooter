using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirShooter.Classes.SaveData;
using Microsoft.Xna.Framework;
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
        public Bullet()
        {
            _texture = null;
            IsAlive = true;
            _destinationRectangle = new Rectangle(100, 300, _width, _height);
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("laser");
        }
        public void Update()
        {
            _destinationRectangle.X -= _speed;
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
