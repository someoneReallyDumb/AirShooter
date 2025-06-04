using AirShooter.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes;
using System;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Text.Json;
using AirShooter.Classes.SaveData;

namespace AirShooter
{
    public class Game1 : Game
    {
        private const int COUNT_MINES = 10;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //поля
        private Player _player;
        private Background _background;
        private Mine _mine;
        private List<Mine> _mines;
        private List<Explosion> _explosions;
        public static GameMode gameMode = GameMode.Menu;
        private MainMenu _mainMenu;
        private PauseMenu _pauseMenu;
        private HUD _hud;
        private GameOver _gameOver;
        private Song _gameSong;
        private Song _menuSong;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();
            _background = new Background();
            _mines = new List<Mine>();
            _explosions = new List<Explosion>();
            _mainMenu = new MainMenu(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);
            _pauseMenu = new PauseMenu(_graphics.PreferredBackBufferWidth,
               _graphics.PreferredBackBufferHeight);
            _gameOver = new GameOver(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);
            _hud = new HUD();
            _player.TakeDamage += _hud.OnPlayerTakeDamage;
            _player.UpdateScore += _hud.OnScoreUpdated;
            _mainMenu.OnPlayingStarted += OnPlayingStarted;
            _pauseMenu.OnPlayingResume += OnPlayingResumed;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _player.LoadContent(Content);
            _background.LoadContent(Content);
            _hud.LoadContent(GraphicsDevice, Content);
            _gameOver.LoadContent(Content);
            _mainMenu.LoadContent(Content);
            _pauseMenu.LoadContent(Content);
            _gameSong = Content.Load<Song>("gameMusic");
            _menuSong = Content.Load<Song>("menuMusic");
            MediaPlayer.Volume = 0.09f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_menuSong);
            for (int i = 0; i < 10; i++)
            {
                Mine mine = new Mine();
                mine.LoadContent(Content);
                Random random = new Random();
                int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                       - mine.Width);
                int y = random.Next(0, _graphics.PreferredBackBufferHeight);
                mine.Position = new Vector2(x, -y);
                _mines.Add(mine);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            switch (gameMode)
            {
                case GameMode.Menu:
                    _background.speed1 = 0.5f;
                    _background.speed2 = 1;
                    _background.speed3 = 2;
                    _background.Update();
                    _mainMenu.Update();
                    break;
                case GameMode.Pause:
                    _background.speed1 = 0;
                    _background.speed2 = 0;
                    _background.speed3 = 0;
                    _background.Update();
                    _pauseMenu.Update();
                    break;
                case GameMode.Playing:
                    _background.speed1 = 1;
                    _background.speed2 = 2;
                    _background.speed3 = 4;
                    _player.Update(
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight, Content);
                    _background.Update();
                    MinesUpdate();
                    CheckCollision();
                    UpdExplosions(gameTime);
                    if (_player.Health <= 0)
                    {
                        gameMode = GameMode.GameOver;
                        _gameOver.SetScore(_player.Score);
                        MediaPlayer.Play(_menuSong);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameMode = GameMode.Pause;
                        MediaPlayer.Play(_menuSong);
                    }
                    break;
                case GameMode.GameOver:
                    _background.speed1 = 0.5f;
                    _background.speed2 = 1;
                    _background.speed3 = 2;
                    _background.Update();
                    _gameOver.Update();
                    break;
                case GameMode.Exit:
                    Exit();
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            {
                switch (gameMode)
                {
                    case GameMode.Menu:
                        _background.Draw(_spriteBatch);
                        _mainMenu.Draw(_spriteBatch);
                        break;
                    case GameMode.Pause:
                        _background.Draw(_spriteBatch);
                        _pauseMenu.Draw(_spriteBatch);
                        break;
                    case GameMode.Playing:
                        _background.Draw(_spriteBatch);
                        _player.Draw(_spriteBatch);
                        foreach (Mine mine in _mines)
                        {
                            mine.Draw(_spriteBatch);
                        }
                        foreach (Explosion explosion in _explosions)
                        {
                            explosion.Draw(_spriteBatch);
                        }
                        _hud.Draw(_spriteBatch);
                        break;
                    case GameMode.GameOver:
                        _background.Draw(_spriteBatch);
                        _gameOver.Draw(_spriteBatch);
                        break;
                    case GameMode.Exit:
                        break;
                    default:
                        break;
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void MinesUpdate()
        {
            for (int i = 0; i < _mines.Count; i++)
            {
                Mine mine = _mines[i];
                mine.Update();
                if (mine.Position.Y > _graphics.PreferredBackBufferHeight)
                {
                    Random random = new Random();
                    int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                       - mine.Width);
                    int y = random.Next(0, _graphics.PreferredBackBufferHeight);
                    mine.Position = new Vector2(x, -y);
                }
                if (!mine.IsAlive)
                {
                    _mines.Remove(mine);
                    i--;
                }
            }
            if (_mines.Count < COUNT_MINES)
            {
                LoadMine();
            }
        }
        private void LoadMine()
        {
            Mine mine = new Mine();
            mine.LoadContent(Content);
            Random random = new Random();
            int x = random.Next(0, _graphics.PreferredBackBufferWidth
                                   - mine.Width);
            int y = random.Next(0, _graphics.PreferredBackBufferHeight);
            mine.Position = new Vector2(x, -y);
            _mines.Add(mine);
        }
        private void CheckCollision()
        {
            foreach (Mine mine in _mines)
            {
                if (mine.Collision.Intersects(_player.Collision))
                {
                    mine.IsAlive = false;
                    CreateExplosion(mine.Position, mine.Width, mine.Height);
                    _player.Damage();
                }
                foreach (Bullet bullet in _player.Bullets)
                {
                    if (mine.Collision.Intersects(bullet.Collision))
                    {
                        bullet.IsAlive = false;
                        mine.IsAlive = false;
                        CreateExplosion(mine.Position, mine.Width, mine.Height);
                        _player.AddScore();
                    }
                }
            }
        }
        private void CreateExplosion(Vector2 spawnPosition, int width, int height)
        {
            Explosion explosion = new Explosion(spawnPosition);
            Vector2 position = spawnPosition;
            position = new Vector2(
                position.X - explosion.Width / 2,
                position.Y - explosion.Height / 2);
            position = new Vector2(position.X + width / 2,
                position.Y + height / 2);
            explosion.Position = position;
            explosion.LoadContent(Content);
            _explosions.Add(explosion);
            explosion.PlaySoundEffect();
        }
        private void UpdExplosions(GameTime gameTime)
        {
            for (int i = 0; i < _explosions.Count; i++)
            {
                _explosions[i].Update(gameTime);
                if (!_explosions[i].IsAlive)
                {
                    _explosions.RemoveAt(i);
                    i--;
                }
            }
        }
        private void OnPlayingStarted()
        {
            gameMode = GameMode.Playing;
            MediaPlayer.Play(_gameSong);
            Reset();
        }
        private void OnPlayingResumed()
        {
            gameMode = GameMode.Playing;
            MediaPlayer.Play(_gameSong);
        }
        private void Reset()
        {
            _player.Reset();
            _hud.Reset();
            _explosions.Clear();
            _mines.Clear();

        }
        private void SaveGame()
        {
            PlayerData playerData = (PlayerData)_player.SaveData();
            string stringData = JsonSerializer.Serialize(playerData);
        }
    }
}
