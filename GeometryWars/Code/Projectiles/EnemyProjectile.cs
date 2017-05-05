using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace GeometryWars.Code
{
	class EnemyProjectile : Projectile
	{

		public EnemyProjectile(Vector2f pos, float angle)
			: base(pos, angle)
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
