using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	class EnemyProjectile : Projectile
	{

		private static Texture projectileTexture = new Texture("Assets/Textures/enemyProjectile.png");
		static Color color = new Color(83,172,255);

		public EnemyProjectile(Vector2f pos, float angle)
			: base(pos, angle, projectileTexture, color)
		{
			


		}

		protected override void HandleCollision(Drawable entity)
		{

			Delete();
			//Do this l8ter or something
			//Hero.GetInstance().TakeDamage();

		}
	}
}
