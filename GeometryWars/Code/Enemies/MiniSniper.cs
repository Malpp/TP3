using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class MiniSniper : FollowerEnemy
	{

		static Texture miniSniperTexture = new Texture("Assets/Textures/miniSniper.png");
		private const float miniSniperSpeed = 150f;

		private const float fireSpeed = 2f;
		private float fireDelta;
		private bool canFire;

		private List<EnemyProjectile> projectiles;

		public static float Size
		{
			get { return miniSniperTexture.Size.X; }
		}

		public MiniSniper(Vector2f pos, float angle)
			: base(pos.X, pos.Y, miniSniperSpeed, miniSniperTexture, angle)
		{

			projectiles = new List<EnemyProjectile>();

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			if (canFire)
			{

				if (Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - Angle < 1f)
				{

					canFire = false;
					projectiles.Add(new EnemyProjectile(Pos, Angle));

				}

			}

			if (!canFire)
			{

				fireDelta += deltaTime;

				if (fireDelta > fireSpeed)
				{

					canFire = true;
					fireDelta = 0;

				}

			}

			foreach (EnemyProjectile enemyProjectile in projectiles)
			{
				enemyProjectile.Update(deltaTime, new []{Hero.GetInstance()});
			}

			projectiles.RemoveAll(x => x.ToDelete);

			base.Update(deltaTime, entities);

		}

		public override void Draw(RenderTarget window)
		{
			base.Draw(window);

			foreach (EnemyProjectile enemyProjectile in projectiles)
			{
				enemyProjectile.Draw(window);
			}

		}
	}
}
