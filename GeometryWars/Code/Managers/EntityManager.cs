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
		private static List<Movable> enemies;
		private static List<Movable> enemiesBuffer;
		private static List<HeroProjectile> heroProjectiles;
		private static List<EnemyProjectile> enemyProjectiles;
		private static Hero hero;
		private static ParticleSystem system;
	    private static List<ScreenText> screenTexts;

		public static int EnemyCount
		{
			get
			{
				int count = 0;
				foreach (Movable movable in enemies)
				{
					if (!(movable is Mini))
						count++;
				}
				return count;
			}
		}

		static EntityManager()
		{

			enemies = new List<Movable>();
			enemiesBuffer = new List<Movable>();
			heroProjectiles = new List<HeroProjectile>();
			enemyProjectiles = new List<EnemyProjectile>();
			hero = Hero.GetInstance();
			system = new ParticleSystem(Game.ParticleTexture);
            screenTexts = new List<ScreenText>();

			system.AddAffector(new BounceOffWall());

		}

		#region PUBLIC FUNCTIONS

		public static void Update(float timeDelta)
		{
			Controller.Update();
			hero.Update(timeDelta, enemies);
			UpdateEnemies(timeDelta);
			UpdateEnemyProjectiles(timeDelta);
			UpdateHeroProjectiles(timeDelta);
			Stars.Update(timeDelta);
			Bomb.Update(timeDelta);
			SoundManager.Update();
			//Update particles
			system.Update(Time.FromSeconds(timeDelta));
			StringTable.GetInstance().Update();
            UpdateText();
		}

		public static void Draw(RenderTarget window)
		{
			Stars.Draw(window);
			DrawEnemies(window);
		    DrawEnemyProjectiles(window);
            DrawHeroProjectiles(window);
			hero.Draw(window);
			system.Draw(window, RenderStates.Default);
			Camera.Update(window, hero.Pos);
            //DrawText(window);
		}
        
		#region ADD FUNCTIONS

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

		public static void AddEnemy(Movable enemy)
		{
			enemies.Add(enemy);
		}

	    public static void AddText(ScreenText text)
	    {
	        screenTexts.Add(text);
	    }

		#endregion

		public static void DeleteAllEnemies()
		{
			foreach (Movable enemy in enemies)
			{
				enemy.Delete();
			}
			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Delete();
			}
		}

		public static void ForceUpdateText()
		{
			foreach (ScreenText screenText in screenTexts)
			{
				screenText.ForceUpdate();
			}
		}

		#endregion

		#region PRIVATE FUNCTIONS

		#region DRAW FUNCTIONS

		private static void DrawEnemies(RenderTarget window)
		{
			foreach (Movable enemy in enemies)
			{
				enemy.Draw(window);
			}
		}

		private static void DrawEnemyProjectiles(RenderTarget window)
		{
			foreach (EnemyProjectile projectile in enemyProjectiles)
			{
				projectile.Draw(window);
			}
		}

		private static void DrawHeroProjectiles(RenderTarget window)
		{
			foreach (HeroProjectile projectile in heroProjectiles)
			{
				projectile.Draw(window);
			}
		}

	    public static void DrawText(RenderTarget window)
	    {
	        foreach (ScreenText screenText in screenTexts)
	        {
	            screenText.Draw(window);
	        }
	    }

		#endregion

		#region UPDATE FUNCTIONS

	    private static void UpdateText()
	    {
	        foreach (ScreenText screenText in screenTexts)
	        {
	            screenText.Update();
	        }
	    }

		private static void UpdateEnemies(float timeDelta)
		{
			foreach (Movable enemy in enemies)
			{
				enemy.Update(timeDelta, new[] { hero });
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

			foreach (Movable enemy in enemiesBuffer)
			{
				enemies.Add(enemy);
			}

			//Remove all enemies that died
			enemies.RemoveAll(x => x.ToDelete);
		}

		private static void UpdateEnemyProjectiles(float timeDelta)
		{
			IEnumerable<Drawable> heroList = new[] {hero};
			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Update(timeDelta, heroList);
			}

			//Remove all enemy projectile that died
			enemyProjectiles.RemoveAll(x => x.ToDelete);
		}

		private static void UpdateHeroProjectiles(float timeDelta)
		{
			foreach (HeroProjectile projectile in heroProjectiles)
			{
				projectile.Update(timeDelta, enemies);
			}

			//Remove all heroProjectiles that died
			heroProjectiles.RemoveAll(x => x.ToDelete);
		}

		#endregion

		#endregion

	}
}
