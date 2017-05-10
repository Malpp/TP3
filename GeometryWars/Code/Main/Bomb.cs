using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emiters;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Main
{
	static class Bomb
	{

		static Color color = Color.Yellow;
		private static bool enemiesCanSpawn = true;
		private const float bombTime = 1.5f;
		private static float enemiesRespawnTimeDelta;
		static SoundBuffer bombSound = new SoundBuffer("Assets/SFX/Gravity_well_die.ogg");

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

		public static void Fire(Vector2f pos)
		{

			enemiesCanSpawn = false;

			SoundManager.AddSound(bombSound);

			EntityManager.AddEmitter(new BombEmitter(pos, Color));

		}

	}
}
