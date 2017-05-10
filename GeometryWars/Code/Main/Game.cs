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
using System.Threading;
using GeometryWars.Code;
using GeometryWars.Code.Effects;
using GeometryWars.Code.Emiters;
using GeometryWars.Code.Enemies;
using GeometryWars.Code.Main;
using GeometryWars.Code.Text;
using NetEXT.MathFunctions;
using NetEXT.Particles;
using SFML.Audio;

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
        public const int GAME_WIDTH = 1920;
        public const int GAME_HEIGHT = 1080;

        #endregion

        public static Random rnd = new Random();

        private static Texture borderTexture = new Texture("Assets/Textures/border.png");
        public static readonly Texture ParticleTexture = new Texture("Assets/Particles/particle.png");
        private static Sprite borderSprite = new Sprite(borderTexture);
        RenderTexture renderTexture = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
        Sprite renderTextureSprite = new Sprite();
        Shader horiShader = new Shader(null, "Assets/Shaders/hori.frag");
        Shader myShader;
        private static Sound soundBuffer = new Sound();
        private Pixelate pixelate = new Pixelate();
        RenderTexture secondPass = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
        private float sigma = 3.5f;
        private float glow = 2f;
        Clock drawClock = new Clock();
        float totalTime = 0;
        HorizontalPass horizontal = new HorizontalPass();
        VerticalPass vertical = new VerticalPass();
        RenderTexture renderA = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
        RenderTexture renderB = new RenderTexture(GAME_WIDTH, GAME_HEIGHT);
        private bool isRenderAReady = false;
        private Thread drawThread;

        private Music music = new Music("Assets/Music/theme.ogg");

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
        public Game(uint windowHeight = GAME_HEIGHT, uint windowWidth = GAME_WIDTH, string title = "SFML APP", Styles style = Styles.None)
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), title, style);

            window.SetVerticalSyncEnabled(false);

            //window.SetFramerateLimit(60);
            window.SetMouseCursorVisible(false);

            //Add the Closed function to the window
            window.Closed += window_Closed;
        }


        /// <summary>
        /// Main loop of the program
        /// </summary>
        public void Run()
        {

            window.SetVisible(true);

            window.SetActive(false);

            InitGame();

            clock.Restart();

            //Thread thread = new Thread(() => Draw(window));
            //thread.Start();

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

            music.Loop = true;
            music.Play();

            music.Volume = 20;

            currentMaxEnemies = 5;

            EntityManager.AddText(new ScoreText());
			EntityManager.AddText(new LifeText());
			StringTable.GetInstance().LoadFromTextFile("Assets/Text/Language.txt");

            //EntityManager.AddEmitter(new HeroTrailEmitter());

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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                sigma += 0.01f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                sigma -= 0.01f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                glow -= 0.01f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                glow += 0.01f;


            EntityManager.Update(gameTime.AsSeconds());

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
        /// Draw code of the program
        /// </summary>
        private void Draw()
        {

            totalTime += drawClock.Restart().AsSeconds();
            if (totalTime > 0.016f)
            {
                totalTime = 0;

                window.Clear();

                renderTexture.Clear();
                renderTexture.Draw(borderSprite);
                EntityManager.Draw(renderTexture);
                renderTexture.Display();
                renderTextureSprite.Texture = renderTexture.Texture;
                window.Draw(renderTextureSprite, new RenderStates(BlendMode.Add));

                horizontal.Update(renderTextureSprite.Texture, sigma, glow);

                secondPass.Clear();
                secondPass.Draw(horizontal, new RenderStates(BlendMode.Add));
                secondPass.Display();

                vertical.Update(secondPass.Texture, sigma, glow);

                window.Draw(vertical, new RenderStates(BlendMode.Add));

                EntityManager.DrawText(window);

                window.Display();
            }


        }

        public static void PlaySound(SoundBuffer sound)
        {
            SoundManager.AddSound(sound);
        }
    }
}
