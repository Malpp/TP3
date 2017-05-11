using GeometryWars.Code;
using GeometryWars.Code.Effects;
using GeometryWars.Code.Enemies;
using GeometryWars.Code.Main;
using GeometryWars.Code.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace GeometryWars
{
	class Game
	{
		#region Public Fields
		public const int BORDER_SIZE = 5;
		public const int GAME_HEIGHT = 720;
		public const int GAME_WIDTH = 1240;
		public static readonly Texture ParticleTexture = new Texture("Assets/Particles/particle.png");
		public static Random rnd = new Random();
		#endregion Public Fields

		#region Private Fields
		private const float SpawnRadius = 200f;
		private static Texture borderTexture = new Texture("Assets/Textures/border.png");
		private Sprite borderSprite = new Sprite(borderTexture);
		private Clock clock = new Clock();
		private float currentMaxEnemies;
		private RenderTexture currentRenderTexture = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
		private Sprite drawSprite = new Sprite();
		private int fps = 0;
		private Time gameTime = new Time();
		private float glow = 2f;
		private HorizontalPass horizontal = new HorizontalPass();
		private Music music = new Music("Assets/Music/theme.ogg");
		private RenderTexture secondPass = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
		private float sigma = 3.5f;
		private float timeElapsed = 0;
		private VerticalPass vertical = new VerticalPass();
		private RenderWindow window;
		#endregion Private Fields

		#region Public Constructors

		/// <summary>
		/// Constructor of the window
		/// </summary>
		/// <param name="windowHeight">Height of the window</param>
		/// <param name="windowWidth">Width of the window</param>
		/// <param name="title">Title of the window</param>
		/// <param name="style">Style of the window</param>
		public Game(uint windowHeight = GAME_HEIGHT, uint windowWidth = GAME_WIDTH, string title = "SFML APP", Styles style = Styles.None)
		{
			window = new RenderWindow(new VideoMode(windowWidth, windowHeight), title, style);

			window.SetVerticalSyncEnabled(true);

			//window.SetFramerateLimit(30);
			window.SetMouseCursorVisible(false);

			//Add the Closed function to the window
			window.Closed += window_Closed;
		}

		#endregion Public Constructors

		#region Public Properties

		/// <summary>
		/// Gets the size of the game.
		/// </summary>
		/// <value>
		/// The size of the game.
		/// </value>
		public static Vector2f GAME_SIZE
		{
			get { return (Vector2f)borderTexture.Size; }
		}

		/// <summary>
		/// Gets the game x limit.
		/// </summary>
		/// <value>
		/// The game x limit.
		/// </value>
		public static float GAME_X_LIMIT
		{
			get { return borderTexture.Size.X; }
		}

		/// <summary>
		/// Gets the game y limit.
		/// </summary>
		/// <value>
		/// The game y limit.
		/// </value>
		public static float GAME_Y_LIMIT
		{
			get { return borderTexture.Size.Y; }
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Plays the sound.
		/// </summary>
		/// <param name="sound">The sound.</param>
		public static void PlaySound(SoundBuffer sound)
		{
			SoundManager.AddSound(sound);
		}

		/// <summary>
		/// Main loop of the program
		/// </summary>
		public void Run()
		{
			window.SetVisible(true);

			window.SetActive(true);

			InitGame();

			clock.Restart();

			//Thread thread = new Thread(() => Draw(window));
			//thread.Start();

			while (window.IsOpen && IsGameClosing())
			{
				//Call the Events
				window.DispatchEvents();

				float step = clock.Restart().AsSeconds();

				//Update the game
				Update(step);

				Draw();
			}
		}

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Draw code of the program
		/// </summary>
		private void Draw()
		{
			//totalTime += drawClock.Restart().AsSeconds();
			//if (totalTime > 0.016f)
			//{
			//    totalTime = 0;

			window.Clear();
			currentRenderTexture.Clear();
			currentRenderTexture.Draw(borderSprite);
			EntityManager.Draw(currentRenderTexture);
			currentRenderTexture.Display();
			drawSprite.Texture = currentRenderTexture.Texture;
			window.Draw(drawSprite);
			horizontal.Update(currentRenderTexture.Texture, sigma, glow);

			secondPass.Clear();
			secondPass.Draw(horizontal);
			secondPass.Display();

			vertical.Update(secondPass.Texture, sigma, glow);

			window.Draw(vertical);

			EntityManager.DrawText(window);

			window.Display();

			//}
		}

		/// <summary>
		/// Sets up global vars to the program
		/// </summary>
		private void InitGame()
		{
			music.Loop = true;
			music.Play();

			music.Volume = 20;

			currentMaxEnemies = 5;

			EntityManager.AddText(new ScoreText());
			EntityManager.AddText(new LifeText());
			EntityManager.AddText(new BombText());
			StringTable.GetInstance().LoadFromTextFile("Assets/Text/Language.txt");
		}

		/// <summary>
		/// Determines whether [the game is closing].
		/// </summary>
		/// <returns>
		///   <c>true</c> if [the game is closing]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsGameClosing()
		{
			if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
			{
				window.Close();
				return false;
			}

			return true;
		}

		/// <summary>
		/// Update code of the program
		/// </summary>
		private void Update(float step)
		{
			//gameTime = clock.Restart();
			//float step = 1f / 60f;

			#region FPS

			timeElapsed += step;

			if (timeElapsed > 1)
			{
				Console.WriteLine("FPS: {0}", fps);

				fps = 0;
				timeElapsed = 0;
			}

			fps++;

			#endregion FPS

			if (Keyboard.IsKeyPressed(Keyboard.Key.E))
			{
				for (int i = 0; i < 500; i++)
				{
					//EntityManager.AddEnemy(new Shooter(new Vector2f(100 + i, 100), -90));
				}
			}

			if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
				sigma += 0.01f;

			if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
				sigma -= 0.01f;

			if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
				glow -= 0.01f;

			if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
				glow += 0.01f;

			EntityManager.Update(step);

			if (!Bomb.CanEnemiesSpawn)
			{
				EntityManager.DeleteAllEnemies();
			}

			if (EntityManager.EnemyCount < currentMaxEnemies && Bomb.CanEnemiesSpawn)
			{
				Vector2f spawnPos;

				Vector2f heroPos = Hero.GetInstance().Pos;

				do
				{
					spawnPos = new Vector2f(rnd.Next(50, (int)GAME_X_LIMIT - 50), rnd.Next(50, (int)GAME_Y_LIMIT - 50));
				} while (Math.Abs(Common.DistanceBetweenTwoPoints(spawnPos, heroPos)) < SpawnRadius);

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
		/// Called when the window "X" is clicked
		/// </summary>
		private void window_Closed(object sender, EventArgs e)
		{
			window.Close();
		}

		#endregion Private Methods
	}
}