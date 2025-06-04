using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirShooter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AirShooter.Classes
{
    public class GameOver
    {
        private Label _label;
        private Label _lblScore;
        private Label _lblInstructions;
        private int _widthScreen;
        private int _heightScreen;
        public GameOver(int widthScreen, int heightScreen)
        {
            _label = new Label(new Vector2(0,0), "GAME OVER", Color.Red);
            _lblScore = new Label(new Vector2(0, 0), "", Color.White);
            _lblInstructions = new Label(new Vector2(0, 0), "Press Enter to continue", Color.Orange);
            _widthScreen = widthScreen;
            _heightScreen = heightScreen;
        }
        public void LoadContent(ContentManager content)
        {
            _label.LoadContent(content);
            _lblScore.LoadContent(content);
            _lblInstructions.LoadContent(content);
            _label.Position = new Vector2(_widthScreen / 2 - _label.SizeText.X / 2,
                _heightScreen / 2 - _label.SizeText.Y / 2 - 40);
            _lblScore.Position = new Vector2(_widthScreen / 2 - _label.SizeText.X / 2,
                _heightScreen / 2 - _label.SizeText.Y / 2);
            _lblInstructions.Position = new Vector2(_widthScreen / 2 - _label.SizeText.X / 2,
                _heightScreen / 2 - _label.SizeText.Y / 2 + 40);
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _label.Draw(spriteBatch);
            _lblScore.Draw(spriteBatch);
            _lblInstructions.Draw(spriteBatch);
        }
        public void SetScore(int score)
        {
            _lblScore.Text = $"Final score: {score}";
            _lblScore.Position = new Vector2(_widthScreen / 2 - _label.SizeText.X / 2,
                _heightScreen / 2 - _label.SizeText.Y / 2);
        }
    }
}
