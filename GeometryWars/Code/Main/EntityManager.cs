using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Enemies;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	static class EntityManager
	{
		private static List<Drawable> enemies;
		private static List<Drawable> enemiesBuffer;
		private static List<Projectile> projectiles;
		private static List<EnemyProjectile> enemyProjectiles;
		private static Hero hero;

		static EntityManager()
		{
			
			enemies = new List<Drawable>();
			enemiesBuffer = new List<Drawable>();
			projectiles = new List<Projectile>();
			enemyProjectiles = new List<EnemyProjectile>();
			hero = Hero.GetInstance();

		}

		public static void AddEnemy(Drawable enemy)
		{
			
			enemies.Add(enemy);

		}

		public static void Update(float timeDelta)
		{

			//Update the hero
			hero.Update(timeDelta, enemies);

			//Updates all enemies
			foreach (Drawable enemy in enemies)
			{
				if (enemy is Movable)
				{
					(enemy as Movable).Update(timeDelta, new[] { hero });
				}
			}

			//Add new enemies
			enemiesBuffer.Clear();

			foreach (Drawable enemy in enemies)
			{
				if (enemy is Movable)
				{
					if (!(enemy is Mini) && enemy.ToDelete)
					{
						//enemiesBuffer.Add();
						enemiesBuffer.Add(new Mini((enemy as Movable).Pos + Common.MovePointByAngle(Mini.Size, 0), 0));
						enemiesBuffer.Add(new Mini((enemy as Movable).Pos + Common.MovePointByAngle(Mini.Size, 118), 118));
						enemiesBuffer.Add(new Mini((enemy as Movable).Pos + Common.MovePointByAngle(Mini.Size, 236), 236));
					}
				}
			}

			foreach (Drawable enemy in enemiesBuffer)
			{
				enemies.Add(enemy);
			}

			//Remove all enemies that died
			enemies.RemoveAll(x => x.ToDelete);

			//Update all enemy projectiles
			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Update(timeDelta, new[] { hero });
			}

			//Remove all enemy projectile that died
			enemyProjectiles.RemoveAll(x => x.ToDelete);

			//Update all projectiles
			foreach (Projectile projectile in projectiles)
			{
				projectile.Update(timeDelta, enemies);
			}

			//Remove all projectiles that died
			projectiles.RemoveAll(x => x.ToDelete);

			//Console.WriteLine(enemies.Count);

		}

		public static void Draw(RenderTarget window)
		{

			foreach (Drawable enemy in enemies)
			{
				enemy.Draw(window);
			}

			foreach (EnemyProjectile projectile in enemyProjectiles)
			{
				projectile.Draw(window);
			}

			foreach (Projectile projectile in projectiles)
			{
				projectile.Draw(window);
			}

			hero.Draw(window);

		}

		public static void AddProjectile(Projectile projectile)
		{
			projectiles.Add(projectile);
		}

		public static void AddEnemyProjectile(EnemyProjectile projectile)
		{
			enemyProjectiles.Add(projectile);
		}

	}
}
