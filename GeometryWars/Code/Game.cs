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

			while (window.IsOpen && !Keyboard.IsKeyPressed(Keyboard.Key.Escape))
			{

				//Call the Events
				window.DispatchEvents();

				//Update the game
				Update();

				//Draw the updated app
				Draw();

			}


		}

		/// <summary>
		/// Sets up global vars to the program
		/// </summary>
		void InitGame()
		{

			entities = new List<BaseEntity>();

			entities.Add(Hero.GetInstance());

			entities.Add(new Shooter(100,100));

			entities.Add(new Spinner(200,100));

			camera = new View(new Vector2f(GAME_WIDTH * 0.5f, GAME_HEIGHT * 0.5f), new Vector2f(800,800));

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

			foreach (var entity in entities)
			{
				if(entity.GetType() == typeof(Hero))
					entity.Update(gameTime.AsSeconds(), entities.Where(x => x != entity));
				else
					entity.Update(gameTime.AsSeconds());
			}

			entities.RemoveAll(x => x.ToDelete);

			//Because the player will always be first in the list
			camera.Move(entities[0].Pos - camera.Center);

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
