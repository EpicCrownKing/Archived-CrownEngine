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

namespace CrownEngine
{
    public class EngineGame : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public int windowWidth = 200;
        public int windowHeight = 200;
        public int windowScale = 2;

        public Color defaultColor = Color.Black;

        public Stage activeStage;
        public List<Stage> stages = new List<Stage>();

        public EngineGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            scene = new RenderTarget2D(GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            base.Initialize();
        }

        Texture2D tilesheet;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tilesheet = Content.Load<Texture2D>("Grass");

            InitializeStages(ref stages);

            activeStage = stages[0];
        }

        public void InitializeStages(ref List<Stage> stages)
        {
            stages.Add(new Stage());
        }

        public int updateCount;
        protected override void Update(GameTime gameTime)
        {
            if (!levelHasLoaded) { OnLoadLevel(); }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            updateCount++;
            if(updateCount >= 60)
            {
                updateCount = 0;
            }

            base.Update(gameTime);
        }

        private bool levelHasLoaded = false;
        private void OnLoadLevel()
        {
            levelHasLoaded = true;
        }

        public static EngineGame instance = new EngineGame();

        protected void DrawSceneToTexture(RenderTarget2D renderTarget)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(defaultColor);

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

            activeStage.Draw();

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
