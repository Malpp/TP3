using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emiters;
using GeometryWars.Code.Main;
using GeometryWars.Code.Projectiles;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GeometryWars.Code
{
	class Hero : Movable
	{
		static SoundBuffer fireSound = new SoundBuffer("Assets/SFX/Fire_homing.ogg");
		private static Texture heroTexture = new Texture("Assets/Textures/hero.png");
		private const float heroSpeed = 500f;
		private const float heroAngleSpeed = 200f;
		private const int spraySize = 10;
		private const float fireDelay = 0.05f;
		private float fireDelta = 0;
		private bool canFire = true;
		private static Hero hero;
		Vector2f direction = new Vector2f();
		private int life;
		private const float bombCooldown = 5f;
		private static bool canFireBomb = true;
		private static float bombDelta;
		private int lastLife;
		private int multiplierResetStep = 5;

		public static float Speed
		{
			get { return heroSpeed; }
		}

		public int Life
		{
			get { return life; }
		}

		public Vector2f Direction
		{
			get { return direction; }
		}

		public static Hero GetInstance()
		{
			
			if(hero == null)
				hero = new Hero(new Vector2f(Game.GAME_X_LIMIT * 0.5f, Game.GAME_Y_LIMIT * 0.5f), 0);
			return hero;

		}

		private Hero(Vector2f pos, float initAngle) 
			: base(pos, initAngle, heroTexture)
		{

			life = 100;
			lastLife = life - multiplierResetStep;

		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			if (life < 1)
			{
				life = 100;
				lastLife = life - multiplierResetStep;
				Bomb.Fire(Pos);
				ScoreManager.Reset();
			}

			if ((Keyboard.IsKeyPressed(Keyboard.Key.Return) || Controller.GetBombKey()) && canFireBomb)
			{
				canFireBomb = false;
				Bomb.Fire(Pos);
			}

			if (!canFireBomb)
			{
				bombDelta += timeDelta;

				if (bombDelta > bombCooldown)
				{
					bombDelta = 0;
					canFireBomb = true;
				}
			}

			if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && canFire)
			{
				canFire = false;
				EntityManager.AddProjectile(new HeroProjectile(Pos + Common.MovePointByAngle(heroTexture.Size.X * 0.3f, Angle), Angle + Game.rnd.Next(-spraySize, spraySize)));
				Game.PlaySound(fireSound);
			}
			else if(canFire && Controller.FireIsNotCentered)
			{
				float fireAngle = Common.AngleBetweenTwoPoints(new Vector2f(), Controller.GetShootAxis());
				canFire = false;
				EntityManager.AddProjectile(new HeroProjectile(Pos + Common.MovePointByAngle(heroTexture.Size.X * 0.3f, fireAngle), fireAngle + Game.rnd.Next(-spraySize, spraySize)));
				Game.PlaySound(fireSound);
			}

			if(!canFire)
				fireDelta += timeDelta;

			if (fireDelta > fireDelay)
			{
				fireDelta = 0;
				canFire = true;
			}

			base.Update(timeDelta, entities);
		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			Vector2f pos = new Vector2f();

			if (Keyboard.IsKeyPressed(Keyboard.Key.W))
				pos += Common.MovePointByAngle(heroSpeed, Angle);

			if (Keyboard.IsKeyPressed(Keyboard.Key.A))
				Angle -= heroAngleSpeed * timeDelta;

			if (Keyboard.IsKeyPressed(Keyboard.Key.S))
				pos -= Common.MovePointByAngle(heroSpeed, Angle);

			if (Keyboard.IsKeyPressed(Keyboard.Key.D))
				Angle += heroAngleSpeed * timeDelta;

			if (Controller.IsConnected && pos == new Vector2f())
			{

				if(Controller.MoveIsNotCentered)
					Angle = Common.AngleBetweenTwoPoints(new Vector2f(), Controller.GetMoveAxis());

				pos += Common.MovePointByAngle(heroSpeed, Angle) *
						Common.DistanceBetweenTwoPoints(new Vector2f(), Controller.GetMoveAxis() / 100);

			}

			if(pos != new Vector2f())
				direction = pos;

			return pos;
		}

		protected override void HandleCollision(Drawable entity)
		{

			entity.Delete();
			//Maybe remove this, not sure yet

		}

		public void TakeDamage(int damage)
		{
			life -= damage;
			if (life < lastLife)
			{
				ScoreManager.ResetMulti();
				lastLife -= multiplierResetStep;
			}
		}

	}
}
