using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emmiters;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Projectiles
{
	class HeroProjectile : Projectile
	{

		private static Texture projectileTexture = new Texture("Assets/Textures/heroProjectile.png");
		private static Color color = new Color(255,216,0);

		public HeroProjectile(Vector2f pos, float angle)
			: base(pos, angle, projectileTexture, color)
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

			base.HandleEdge();
		}
	}
}
