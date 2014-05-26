#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarsProject
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		GraphicsDevice device;
		VertexPositionColor[] vertices;
		Matrix viewMatrix;
		Matrix projectionMatrix;
		BasicEffect basicEffect;
		int[] indices;

		private float angle = 0f;
		private int terrainWidth = 4;
		private int terrainHeight = 3;
		private float[,] heightData;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			graphics.PreferredBackBufferWidth = 500;
			graphics.PreferredBackBufferHeight = 500;
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();
			Window.Title = "DEM Reader";
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			device = graphics.GraphicsDevice;
	
			SetUpCamera();

			Texture2D heightMap = Content.Load<Texture2D> ("heightmap");            
			LoadHeightData(heightMap);

			SetUpVertices();
			SetUpIndices();            
		}

		protected override void UnloadContent()
		{
		}

		private void SetUpVertices()
		{
			vertices = new VertexPositionColor[terrainWidth * terrainHeight];
			for (int x = 0; x < terrainWidth; x++)
			{
				for (int y = 0; y < terrainHeight; y++)
				{
					vertices[x + y * terrainWidth].Position = new Vector3(x, heightData[x, y], -y);
					vertices[x + y * terrainWidth].Color = Color.White;
				}
			}
		}

		private void SetUpIndices()
		{
			indices = new int[(terrainWidth - 1) * (terrainHeight - 1) * 6];
			int counter = 0;
			for (int y = 0; y < terrainHeight - 1; y++)
			{
				for (int x = 0; x < terrainWidth - 1; x++)
				{
					int lowerLeft = x + y * terrainWidth;
					int lowerRight = (x + 1) + y * terrainWidth;
					int topLeft = x + (y + 1) * terrainWidth;
					int topRight = (x + 1) + (y + 1) * terrainWidth;

					indices[counter++] = topLeft;
					indices[counter++] = lowerRight;
					indices[counter++] = lowerLeft;

					indices[counter++] = topLeft;
					indices[counter++] = topRight;
					indices[counter++] = lowerRight;
				}
			}
		}

		private void LoadHeightData(Texture2D heightMap)
		{
			terrainWidth = heightMap.Width;
			terrainHeight = heightMap.Height;

			Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
			heightMap.GetData(heightMapColors);

			heightData = new float[terrainWidth, terrainHeight];
			for (int x = 0; x < terrainWidth; x++)
				for (int y = 0; y < terrainHeight; y++)
					heightData[x, y] = heightMapColors[x + y * terrainWidth].R / 5.0f;
		}

		private void SetUpCamera()
		{
			viewMatrix = Matrix.CreateLookAt(new Vector3(60, 80, -80), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
			projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1.0f, 300.0f);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			angle += 0.005f;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			basicEffect.World = matrixWorld;
			basicEffect.View = view;
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.EnableDefaultLighting();

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.CullClockwiseFace;
			rasterizerState.ScissorTestEnable = true;
			GraphicsDevice.RasterizerState = rasterizerState;

			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColor.VertexDeclaration);
			}

			base.Draw(gameTime);
		}
	}
}

