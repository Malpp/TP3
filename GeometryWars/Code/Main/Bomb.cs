using GeometryWars.Code.Emiters;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Main
{
	static class Bomb
	{
		#region Private Fields
		private const float bombTime = 1.5f;
		private static readonly Texture BombTexture = new Texture("Assets/Textures/bomb.png");
		private static Vector2f basePos = new Vector2f(50, 80);
		private static SoundBuffer bombSound = new SoundBuffer("Assets/SFX/Gravity_well_die.ogg");
		private static Sprite bombSprite = new Sprite(BombTexture);
		private static Color color = Color.Yellow;
		private static bool enemiesCanSpawn = true;
		private static float enemiesRespawnTimeDelta;
		#endregion Private Fields

		#region Public Properties

		public static float BombDuration
		{
			get { return bombTime; }
		}

		public static bool CanEnemiesSpawn
		{
			get { return enemiesCanSpawn; }
		}

		public static Color Color
		{
			get { return color; }
		}

		#endregion Public Properties

		#region Public Methods

		public static void Draw(RenderTarget window)
		{
			for (int i = 0; i < Hero.GetInstance().BombCount; i++)
			{
				bombSprite.Position = basePos + new Vector2f(40 * i, 0);
				window.Draw(bombSprite);
			}
		}

		public static void Fire(Vector2f pos)
		{
			enemiesCanSpawn = false;

			SoundManager.AddSound(bombSound);

			EntityManager.AddEmitter(new BombEmitter(pos, Color));
		}

		public static void Update(float timeDelta)
		{
			if (!enemiesCanSpawn)
			{
				enemiesRespawnTimeDelta += timeDelta;

				if (enemiesRespawnTimeDelta > BombDuration)
				{
					enemiesCanSpawn = true;
					enemiesRespawnTimeDelta = 0;
				}
			}
		}

		#endregion Public Methods
	}
}