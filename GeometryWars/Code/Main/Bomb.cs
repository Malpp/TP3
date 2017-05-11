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
		private static SoundBuffer bombSound = new SoundBuffer("Assets/SFX/Gravity_well_die.ogg");
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