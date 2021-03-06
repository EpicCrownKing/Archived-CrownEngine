using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Content;
using CrownEngine.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace CrownEngine
{
    public class EngineGame : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public static EngineGame instance;

        public int windowWidth = 160;
        public int windowHeight = 200;
        public int windowScale = 2;

        public Stage activeStage = new Stage();
        public List<Stage> stages = new List<Stage>();

        public Texture2D MissingTexture;

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();

        public Random random;

        public KeyboardState oldKeyboardState;
        public KeyboardState keyboardState;

        public EngineGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            instance = this; 

            scene = new RenderTarget2D(GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            base.Initialize();

            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(file));
            }

            MissingTexture = Textures["MissingTexture.png"];

            /*foreach (string file in Directory.EnumerateFiles("Content/", "*.wav", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                SoundEffects[Path.GetFileName(fixedPath)] = SoundEffect.FromFile(fixedPath);
            }*/

            random = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            InitializeStages();
        }

        public virtual void InitializeStages()
        {
            stages.Add(new Facility());
            stages.Add(new Facility());

            activeStage = stages[0];

            activeStage.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            activeStage.Update();

            if(keyboardState.IsKeyDown(Keys.OemPlus) && windowScale < 6)
            {
                windowScale++;
            }
            if (keyboardState.IsKeyDown(Keys.OemMinus) && windowScale > 1)
            {
                windowScale--;
            }

            base.Update(gameTime);
        }

        public void SwitchStages(int newStage)
        {
            activeStage.Unload();

            activeStage = stages[newStage];

            activeStage.Load();
        }

        protected void DrawSceneToTexture(RenderTarget2D renderTarget)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(activeStage.bgColor);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            /*for (int j = 0; j < activeLevel.tilemap.GetLength(0); j++)
            {
                for (int i = 0; i < activeLevel.tilemap.GetLength(1); i++)
                {
                    if (GetTileFrame(i, j) != new Vector2(-1, -1))
                    {
                        _spriteBatch.Draw(tilesheet, new Rectangle(i * 8, j * 8, 8, 8), new Rectangle((int)GetTileFrame(i, j).X, (int)GetTileFrame(i, j).Y, 8, 8), Color.White);
                    }
                }
            }*/

            activeStage.Draw(_spriteBatch);

            //activeLevel.player.Draw(_spriteBatch);

            _spriteBatch.End();

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
        }

        //Drawing the scene
        private RenderTarget2D scene;
        protected override void Draw(GameTime gameTime)
        {
            _graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            _graphics.PreferredBackBufferHeight = windowHeight * windowScale;
            _graphics.ApplyChanges();

            DrawSceneToTexture(scene);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(scene, new Rectangle(0, 0, windowWidth * windowScale, windowHeight * windowScale), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
