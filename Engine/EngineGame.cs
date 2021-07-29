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

namespace CrownEngine
{
    public class EngineGame : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public static EngineGame instance;

        public virtual int windowWidth => 160;
        public virtual int windowHeight => 200;
        public virtual int windowScale => 2;

        public Stage activeStage = new Stage();
        public List<Stage> stages = new List<Stage>();

        public Texture2D MissingTexture;

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            activeStage.Update();

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
