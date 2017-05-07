﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Diagnostics;
using GeometryWars.Code;
using GeometryWars.Code.Enemies;
using NetEXT.MathFunctions;
using NetEXT.Particles;

namespace GeometryWars
{
	class Game
	{

		#region Global vars

		//The window of application
		private RenderWindow window;

		//Used to time the movement of the objects on screen
		Clock clock = new Clock();
		Time gameTime = new Time();

		//FPS vars
		float timeElapsed = 0;
		int fps = 0;

		//Height and width of the game window
		public const int GAME_HEIGHT = 700;
		public const int GAME_WIDTH = 700;

		#endregion

		public static Random rnd = new Random();

		private static Texture borderTexture = new Texture("Assets/Textures/border.png");
		public static readonly Texture ParticleTexture = new Texture("Assets/Particles/particle.png");
		private static Sprite borderSprite = new Sprite(borderTexture);

		private const float SpawnRadius = 200f;
		private float currentMaxEnemies;

		public const int BORDER_SIZE = 5;

		public static float GAME_X_LIMIT
		{
			get { return borderTexture.Size.X; }
		}

		public static float GAME_Y_LIMIT
		{
			get { return borderTexture.Size.Y; }
		}

		public static Vector2f GAME_SIZE
		{
			get { return (Vector2f)borderTexture.Size; }
		}

		/// <summary>
		/// Constructor of the window
		/// </summary>
		/// <param name="windowHeight">Height of the window</param>
		/// <param name="windowWidth">Width of the window</param>
		/// <param name="title">Title of the window</param>
		/// <param name="style">Style of the window</param>
		public Game(uint windowHeight = GAME_HEIGHT, uint windowWidth = GAME_WIDTH, string title = "SFML APP", Styles style = Styles.Close)
		{
			window = new RenderWindow(new VideoMode(windowWidth, windowHeight), title, style);

			window.SetFramerateLimit(240);

			//Add the Closed function to the window
			window.Closed += window_Closed;

		}


		/// <summary>
		/// Main loop of the program
		/// </summary>
		public void Run()
		{

			window.SetVisible(true);

			window.SetActive();

			InitGame();

			clock.Restart();

			while (window.IsOpen && IsGameClosing())
			{

				//Call the Events
				window.DispatchEvents();

				//Update the game
				Update();

				//Draw the updated app
				Draw();

			}


		}

		bool IsGameClosing()
		{

			if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
			{
				window.Close();
				return false;
			}

			return true;

		}

		/// <summary>
		/// Sets up global vars to the program
		/// </summary>
		void InitGame()
		{

			currentMaxEnemies = 5;
			//EntityManager.AddEnemy(new Sniper(new Vector2f(300f,300f)));

		}

		/// <summary>
		/// Called when the window "X" is clicked
		/// </summary>
		void window_Closed(object sender, EventArgs e)
		{

			window.Close();

		}

		/// <summary>
		/// Update code of the program
		/// </summary>
		private void Update()
		{

			gameTime = clock.Restart();

			#region FPS

			timeElapsed += gameTime.AsSeconds();

			if (timeElapsed > 1)
			{

				Console.WriteLine("FPS: {0}", fps);

				fps = 0;
				timeElapsed = 0;

			}

			fps++;

			#endregion

			if (Keyboard.IsKeyPressed(Keyboard.Key.E))
			{
				for (int i = 0; i < 500; i++)
				{
					//EntityManager.AddEnemy(new Shooter(new Vector2f(100 + i, 100), -90));
				}
			}

			EntityManager.Update(gameTime.AsSeconds());

			if (EntityManager.EnemyCount < currentMaxEnemies)
			{

				float xSpawnPos;
				float ySpawnPos;

				Vector2f heroPos = Hero.GetInstance().Pos;

				do
				{
					xSpawnPos = rnd.Next(0, (int)GAME_X_LIMIT);
				} while (!(xSpawnPos + SpawnRadius < heroPos.X) &&
				         !(xSpawnPos - SpawnRadius > heroPos.X));

				do
				{
					ySpawnPos = rnd.Next(0, (int)GAME_X_LIMIT);
				} while (!(ySpawnPos + SpawnRadius < heroPos.Y) &&
				         !(ySpawnPos - SpawnRadius > heroPos.Y));

				Vector2f spawnPos = new Vector2f(xSpawnPos, ySpawnPos);
				float spawnAngle = rnd.Next(0, 360);

				Array values = Enum.GetValues(typeof(SpawnableEnemies));

				switch ((SpawnableEnemies)values.GetValue(rnd.Next(values.Length)))
				{
					
					case SpawnableEnemies.Shooter:
						EntityManager.AddEnemy(new Shooter(spawnPos, spawnAngle));
						break;
					case SpawnableEnemies.Sniper:
						EntityManager.AddEnemy(new Sniper(spawnPos));
						break;
					case SpawnableEnemies.Spinner:
						EntityManager.AddEnemy(new Spinner(spawnPos, spawnAngle));
						break;

				}

			}

		}

		/// <summary>
		/// Draw code of the program
		/// </summary>
		private void Draw()
		{

			window.Clear();

			window.Draw(borderSprite);

			//Code goes here lul
			EntityManager.Draw(window);

			window.Display();

		}

	}
}
