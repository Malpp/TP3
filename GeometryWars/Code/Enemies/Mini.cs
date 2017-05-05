using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class Mini : FollowerEnemy
	{

		static Texture miniSniperTexture = new Texture("Assets/Textures/mini.png");
		private const float miniSpeed = 150f;
		private const float miniAngleSpeed = 200f;

		private const float fireSpeed = 2f;
		private float fireDelta;
		private bool canFire;

		public static float Size
		{
			get { return miniSniperTexture.Size.X; }
		}

		public Mini(Vector2f pos, float initAngle)
			: base(pos, initAngle, miniSpeed, miniAngleSpeed, miniSniperTexture)
		{

		}

		protected override void HandleCollision(Drawable entity)
		{
			Delete();
		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			if (canFire)
			{

				if (Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - Angle < 1f)
				{

					canFire = false;
					EntityManager.AddEnemyProjectile(new EnemyProjectile(Pos, Angle));

				}

			}

			if (!canFire)
			{

				fireDelta += timeDelta;

				if (fireDelta > fireSpeed)
				{

					canFire = true;
					fireDelta = 0;

				}

			}

			base.Update(timeDelta, entities);

		}

	}
}
