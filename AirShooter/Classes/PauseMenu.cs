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
using SharpDX.MediaFoundation;

namespace AirShooter.Classes
{
    public class PauseMenu : Menu
    {
        public event Action OnPlayingResume;
        public PauseMenu(int widthScreen, int heightScreen) : base(widthScreen, heightScreen)
        {
            _buttonList.Add(new Label(new Vector2(0, 0), "Resume", Color.White));
            _buttonList.Add(new Label(new Vector2(0, 40), "SaveGame", Color.White));
            _buttonList.Add(new Label(new Vector2(0, 40), "Exit to menu", Color.White));
        }
        public override void PressEnter()
        {
            if (_selected == 0)
            {
                if (OnPlayingResume != null)
                    OnPlayingResume();
            }
            else if (_selected == 1)
            {
                Game1.gameMode = GameMode.Menu;
            }
            if (_selected == 2)
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
    }
}
