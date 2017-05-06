using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Main;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GeometryWars.Code
{
	class Hero : Movable
	{
		private static Texture heroTexture = new Texture("Assets/Textures/hero.png");
		private const float heroSpeed = 500f;
		private const float heroAngleSpeed = 200f;
		private const int spraySize = 10;
		private const float fireDelay = 0.01f;
		private float fireDelta = 0;
		private bool canFire = true;
		private static Hero hero;

		public static Hero GetInstance()
		{
			
			if(hero == null)
				hero = new Hero(new Vector2f(Game.GAME_WIDTH * 0.5f, Game.GAME_HEIGHT * 0.5f), 0);
			return hero;

		}

		private Hero(Vector2f pos, float initAngle) 
			: base(pos, initAngle, heroTexture)
		{


		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && canFire)
			{
				canFire = false;
				EntityManager.AddProjectile(new Projectile(Pos + Common.MovePointByAngle(heroTexture.Size.X * 0.3f, Angle), Angle + Game.rnd.Next(-spraySize, spraySize)));
			}
			else if(canFire && Controller.FireIsNotCentered)
			{
				float fireAngle = Common.AngleBetweenTwoPoints(new Vector2f(), Controller.GetShootAxis());
				canFire = false;
				EntityManager.AddProjectile(new Projectile(Pos + Common.MovePointByAngle(heroTexture.Size.X * 0.3f, fireAngle), fireAngle + Game.rnd.Next(-spraySize, spraySize)));
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

			return pos;
		}

		protected override void HandleCollision(Drawable entity)
		{

			entity.Delete();
			//Maybe remove this, not sure yet

		}
	}
}
