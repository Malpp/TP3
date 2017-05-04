using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class Shooter : FollowerEnemy
	{

		private static Texture shooterTexture = new Texture("Assets/Textures/shooter.png");
		private const float shooterSpeed = 300f;
		private const float fireSpeed = 0.3f;
		private float fireDelta = 0;
		private bool canFire = false;
		private bool correctAngle = false;
		private List<EnemyProjectile> enemyProjectiles;

		public Shooter(float x, float y)
			: base(x, y, shooterSpeed, shooterTexture)
		{
			
			enemyProjectiles = new List<EnemyProjectile>();

		}

		protected override Vector2f GetMove(float timeDelta)
		{

			float distance = Common.DistanceBetweenTwoPoints(Hero.GetInstance().Pos, Pos);

			if (distance > 210f)
			{
				
				return Common.MovePointByAngle(shooterSpeed, Angle);

			}
			else if(distance < 200f)
			{

				return Common.MovePointByAngle(-shooterSpeed, Angle);

			}

			return new Vector2f();

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			if (Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - Angle < 1f)
				correctAngle = true;
			else
				correctAngle = false;

			if (canFire && correctAngle)
			{
				canFire = false;
				enemyProjectiles.Add(new EnemyProjectile(Pos + Common.MovePointByAngle(shooterTexture.Size.X * 0.3f, Angle), Angle));
			}

			if (!canFire)
			{

				fireDelta += deltaTime;

				if (fireDelta > fireSpeed)
				{
					fireDelta = 0;
					canFire = true;
				}

			}

			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Update(deltaTime, new []{Hero.GetInstance()});
			}

			enemyProjectiles.RemoveAll(x => x.ToDelete);

			base.Update(deltaTime, entities);
		}

		public override void Draw(RenderTarget window)
		{

			foreach (EnemyProjectile enemyProjectile in enemyProjectiles)
			{
				enemyProjectile.Draw(window);
			}

			base.Draw(window);
		}
	}
}
