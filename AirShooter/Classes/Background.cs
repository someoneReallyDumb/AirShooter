using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AirShooter.Classes
{
    internal class Background
    {
        private Texture2D _texture1;
        private Texture2D _texture2;
        private Texture2D _texture3;
        private Vector2 _position11;
        private Vector2 _position12;
        private Vector2 _position21;
        private Vector2 _position22;
        private Vector2 _position31;
        private Vector2 _position32;
        public float speed1;
        public float speed2;
        public float speed3;
        public Background()
        {
            _texture1 = null;
            _texture2 = null;
            _texture3 = null;
            //_position1 = new Vector2(0, _texture.Height);
            _position12 = Vector2.Zero;
            _position22 = Vector2.Zero;
            _position32 = Vector2.Zero;
            speed1 = 1;
            speed2 = 2;
            speed3 = 4;
        }
        public void LoadContent(ContentManager content)
        {
            _texture1 = content.Load<Texture2D>("mainbackground");
            _texture2 = content.Load<Texture2D>("bgLayer1");
            _texture3 = content.Load<Texture2D>("bgLayer2");
            _position11 = new Vector2(-_texture1.Width, 0);
        }
        public void Update()
        {
            _position11.X += speed1;
            _position12.X += speed1;
            _position21.X += speed2;
            _position22.X += speed2;
            _position31.X += speed3;
            _position32.X += speed3;
            if (_position11.X >= 0)
            {
                _position11.X = -_texture1.Width;
                _position12.X = 0;
            }
            if (_position21.X >= 0)
            {
                _position21.X = -_texture1.Width;
                _position22.X = 0;
            }
            if (_position31.X >= 0)
            {
                _position31.X = -_texture1.Width;
                _position32.X = 0;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture1, _position11, Color.White);
            spriteBatch.Draw(_texture1, _position12, Color.White);
            spriteBatch.Draw(_texture2, _position21, Color.White);
            spriteBatch.Draw(_texture2, _position22, Color.White);
            spriteBatch.Draw(_texture3, _position31, Color.White);
            spriteBatch.Draw(_texture3, _position32, Color.White);
        }
    }
}
