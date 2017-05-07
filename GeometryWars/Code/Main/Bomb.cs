using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emiters;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Main
{
	static class Bomb
	{

		static Color color = Color.Yellow;
		private const float bombCooldown = 5f;
		private static bool canFire = true;
		private static float cooldownTime;
		private static bool enemiesCanSpawn = true;
		private const float bombTime = 1f;
		private static float enemiesRespawnTimeDelta;

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
			if (!canFire)
			{

				cooldownTime += timeDelta;

				if (cooldownTime > bombCooldown)
				{
					canFire = true;
					cooldownTime = 0;
				}

			}

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

			if (canFire)
			{
				canFire = false;

				enemiesCanSpawn = false;

				//EntityManager.DeleteAllEnemies();

				EntityManager.AddEmitter(new BombEmitter(pos, Color));
			}


		}

	}
}
