using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Affectors;
using GeometryWars.Code.Base;
using GeometryWars.Code.Enemies;
using GeometryWars.Code.Main;
using GeometryWars.Code.Projectiles;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GeometryWars.Code
{
	static class EntityManager
	{
		private static List<Drawable> enemies;
		private static List<Drawable> enemiesBuffer;
		private static List<HeroProjectile> heroProjectiles;
		private static List<EnemyProjectile> enemyProjectiles;
		private static Hero hero;
		private static List<Star> stars;
		private static ParticleSystem system;

		public static int EnemyCount
		{
			get { return enemies.Count(x => !(x is Mini)); }
		}

		static EntityManager()
		{
			
			enemies = new List<Drawable>();
			enemiesBuffer = new List<Drawable>();
			heroProjectiles = new List<HeroProjectile>();
			enemyProjectiles = new List<EnemyProjectile>();
			hero = Hero.GetInstance();
			stars = new List<Star>();
			system = new ParticleSystem(Game.ParticleTexture);

			system.AddAffector(new BounceOffWall());

			GenerateStars();

		}

		private static void GenerateStars()
		{

			for (int i = 0; i < Star.maxStarCount; i++)
			{
				float xPos = Game.rnd.Next((int)Star.MaxX.X, (int)Star.MaxX.Y);
				float yPos = Game.rnd.Next((int)Star.MaxY.X, (int)Star.MaxY.Y);
				Vector2f randomPos = new Vector2f(xPos, yPos);

				stars.Add(new Star(randomPos));

			}

		}

		public static void AddEnemy(Drawable enemy)
		{
			
			enemies.Add(enemy);

		}

		public static void Update(float timeDelta)
		{

			Controller.Update();

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

			//Update all enemy heroProjectiles
			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Update(timeDelta, new[] { hero });
			}

			//Remove all enemy projectile that died
			enemyProjectiles.RemoveAll(x => x.ToDelete);

			//Update all heroProjectiles
			foreach (HeroProjectile projectile in heroProjectiles)
			{
				projectile.Update(timeDelta, enemies);
			}

			//Remove all heroProjectiles that died
			heroProjectiles.RemoveAll(x => x.ToDelete);

			//Console.WriteLine(enemies.Count);

			//Update stars
			if (hero.Pos != hero.LastPos)
			{
				foreach (Star star in stars)
				{
					star.Update(timeDelta);
				}
			}

			Bomb.Update(timeDelta);

			SoundManager.Update();

			system.Update(Time.FromSeconds(timeDelta));

		}

		public static void Draw(RenderTarget window)
		{

			foreach (Star star in stars)
			{
				star.Draw(window);
			}

			foreach (Drawable enemy in enemies)
			{
				enemy.Draw(window);
			}

			foreach (EnemyProjectile projectile in enemyProjectiles)
			{
				projectile.Draw(window);
			}

			foreach (HeroProjectile projectile in heroProjectiles)
			{
				projectile.Draw(window);
			}

			hero.Draw(window);

			system.Draw(window, new RenderStates(BlendMode.Add));

			Camera.Update(window, hero.Pos);

		}

		public static void AddProjectile(HeroProjectile projectile)
		{
			heroProjectiles.Add(projectile);
		}

		public static void AddEnemyProjectile(EnemyProjectile projectile)
		{
			enemyProjectiles.Add(projectile);
		}

		public static void AddEmitter(BaseEmitter emitter)
		{
			system.AddEmitter(emitter, Time.FromSeconds(emitter.EmitterDuration));
		}

		public static void DeleteAllEnemies()
		{
			foreach (Drawable enemy in enemies)
			{
				enemy.Delete();
			}
			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Delete();
			}
		}

	}
}
