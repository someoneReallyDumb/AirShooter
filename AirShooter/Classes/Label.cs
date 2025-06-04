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
    public class Label
    {
        private Vector2 _position;
        private SpriteFont _spriteFont;
        private string _text;
        private Color _color;
        public Color Color
        {
            set { _color = value; }
        }
        public Vector2 SizeText
        {
            get { return _spriteFont.MeasureString(_text); }
        }
        public Vector2 Position
        {
            set { _position = value; }
        }
        public string Text
        {
            get => _text;
            set => _text = value;
        }
        public Label(Vector2 position, string text, Color color)
        {
            _position = position;
            _text = text;
            _color = color;
            _spriteFont = null;
        }
        public void LoadContent(ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>("GameFont");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _text, _position, _color);
        }
    }
}
