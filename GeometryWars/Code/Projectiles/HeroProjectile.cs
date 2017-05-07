using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emmiters;
using SFML.System;

namespace GeometryWars.Code.Projectiles
{
	class HeroProjectile : Projectile
	{

		public HeroProjectile(Vector2f pos, float angle)
			: base(pos, angle)
		{
			


		}

		protected override void HandleCollision(Drawable entity)
		{

			if (!ToDelete)
			{
				Delete();
				entity.Delete();
			}

		}

		protected override void HandleEdge()
		{

			EntityManager.AddEmitter(new ProjectileExplosionEmitter(Pos, 1));

			base.HandleEdge();
		}
	}
}
