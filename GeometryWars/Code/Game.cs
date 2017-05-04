using System;
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

		private List<BaseEntity> entities;
		
		private static Texture borderTexture = new Texture("Assets/Textures/border.png");
		private static Sprite borderSprite = new Sprite(borderTexture);

		public const int BORDER_SIZE = 5;

		public static float GAME_X_LIMIT
		{
			get { return borderTexture.Size.X; }
		}

		public static float GAME_Y_LIMIT
		{
			get { return borderTexture.Size.Y; }
		}

		private View camera;

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

			//Add the Closed function to the window
			window.Closed += window_Closed;

		}


		/// <summary>
		/// Main loop of the program
		/// </summary>
		public void Run()
		{

			window.SetVisible(true);

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

			entities = new List<BaseEntity>();

			entities.Add(Hero.GetInstance());

			entities.Add(new Shooter(250,300));

			entities.Add(new Spinner(200,100));

			entities.Add(new Sniper(100, 200));

			//entities.Add(new MiniSniper(100, 100, 180));

			camera = new View(new Vector2f(GAME_WIDTH * 0.5f, GAME_HEIGHT * 0.5f), new Vector2f(1000,1000));

			window.SetView(camera);

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

			if(Keyboard.IsKeyPressed(Keyboard.Key.E))
				entities.Add(new MiniSniper(new Vector2f(100,100), -90));

			foreach (var entity in entities)
			{
				if(entity.GetType() == typeof(Hero))
					entity.Update(gameTime.AsSeconds(), entities.Where(x => x != entity));
				else
					entity.Update(gameTime.AsSeconds());
			}

			List<MiniSniper> miniSnipersTemp = new List<MiniSniper>();

			foreach (var baseEntity in entities)
			{
				if (baseEntity.GetType() == typeof(Sniper) && baseEntity.ToDelete)
				{

					miniSnipersTemp.Add(new MiniSniper(baseEntity.Pos + Common.MovePointByAngle(MiniSniper.Size, 0), 0));
					miniSnipersTemp.Add(new MiniSniper(baseEntity.Pos + Common.MovePointByAngle(MiniSniper.Size, 118), 118));
					miniSnipersTemp.Add(new MiniSniper(baseEntity.Pos + Common.MovePointByAngle(MiniSniper.Size, 236), 236));

				}
			}

			foreach (MiniSniper miniSniper in miniSnipersTemp)
			{
				entities.Add(miniSniper);
			}

			entities.RemoveAll(x => x.ToDelete);

			//Because the player will always be first in the list
			camera.Move(Hero.GetInstance().Pos - camera.Center);

			window.SetView(camera);

		}

		/// <summary>
		/// Draw code of the program
		/// </summary>
		private void Draw()
		{

			window.Clear();

			window.Draw(borderSprite);

			//Code goes here lul
			foreach (var entity in entities)
			{
				entity.Draw(window);
			}


			window.Display();

		}

	}
}
