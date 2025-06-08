using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.SaveData;
using SharpDX.Direct3D9;

namespace AirShooter.Classes
{
    public class HealBoost
    {
        private const int Speed = 2;
        private const int TimeRespawn = 1000;

        private Texture2D _texture;
        private Vector2 _position;
        private bool _isAlive;
        private bool _isMove;
        private int _timer = 0;
        private int _widthScreen;
        private int _heightScreen;
        private Rectangle _collision;

        public Rectangle Collision
        {
            get => _collision;
        }
        public HealBoost(int widthScreen, int HeightScreen)
        {
            _texture = null;
            _widthScreen = widthScreen;
            _heightScreen = HeightScreen;
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("healBoost");
            Reset();
        }
        public void Update()
        {
            _collision = new Rectangle((int)_position.X, (int)_position.Y,
                _texture.Width, _texture.Height);
            if (_isMove)
            {
                _position.Y += Speed;
            }
            if (_position.Y == _heightScreen)
            {
                Reset();
            }
            if (!_isMove)
            {
                _timer++;
                if (_timer >= TimeRespawn)
                {
                    _isMove = true;
                    _timer = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isMove)
            {
                spriteBatch.Draw(_texture, _position, Color.White);

            }
        }
        public void Reset()
        {
            _isMove = false;
            Random random = new Random();
            _position = new Vector2(random.Next(0, _widthScreen - _texture.Width),
                    0 - _texture.Height);
        }
    }
}
