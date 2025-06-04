using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.SaveData;
using AirShooter.Classes.SaveData;

namespace AirShooter.Classes.SaveData
{
    public class PlayerData
    {
        public Vector2 Position { get; set; }
        public int Score { get; set; }
        public int Health { get; set; }
        public int Timer { get; set; }
        public List<BulletData> Bullets { get; set; }
    }
}

