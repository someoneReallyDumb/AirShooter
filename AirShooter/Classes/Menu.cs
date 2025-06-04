using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace AirShooter.Classes
{
    public abstract class Menu
    {
        protected List<Label> _buttonList = new List<Label>();
        protected int _selected;
        protected int _widthScreen;
        protected int _heightScreen;
        protected KeyboardState _keyboardState;
        protected KeyboardState _prevKeyboardState;
        public Menu(int widthScreen, int heightScreen)
        {
            _selected = 0;
            _widthScreen = widthScreen;
            _heightScreen = heightScreen;
        }
        public void Update()
        {
            _keyboardState = Keyboard.GetState();
            if (_prevKeyboardState.IsKeyUp(Keys.S) &&
                _keyboardState.IsKeyDown(Keys.S))
            {
                _selected++;
                if (_selected >= _buttonList.Count)
                {
                    _selected = 0;
                }
            }
            if (_prevKeyboardState.IsKeyUp(Keys.W) &&
                _keyboardState.IsKeyDown(Keys.W))
            {
                _selected--;
                if (_selected < 0)
                {
                    _selected = 2;
                }
            }
            if (_prevKeyboardState.IsKeyUp(Keys.Enter) &&
                _keyboardState.IsKeyDown(Keys.Enter))
            {
                PressEnter();
            }
            _prevKeyboardState = _keyboardState;
        }
        public void LoadContent(ContentManager content)
        {
            int width = -100000;
            int height = 0;
            int offset = 0;
            foreach (Label button in _buttonList)
            {
                button.LoadContent(content);
                if (button.SizeText.X > width)
                {
                    width = (int)button.SizeText.X;
                }
                height = height + (int)button.SizeText.Y;
            }
            height = height + 20 * (_buttonList.Count - 1);
            int x = _widthScreen / 2 - width / 2;
            int y = _heightScreen / 2 - height / 2;
            for (int i = 0; i < _buttonList.Count; i++)
            {
                _buttonList[i].Position = new Vector2(
                    x + (width - _buttonList[i].SizeText.X) / 2,
                    y + offset);
                offset += (int)_buttonList[i].SizeText.Y + 20;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _buttonList.Count; i++)
            {
                Color colorButton;
                if (i == _selected)
                {
                    colorButton = Color.Yellow;
                }
                else
                {
                    colorButton = Color.White;
                }
                _buttonList[i].Color = colorButton;
                _buttonList[i].Draw(spriteBatch);
            }
        }
        public abstract void PressEnter();
    }
}
