using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	class Projectile : Movable
	{

		private static Texture projectileTexture = new Texture("Assets/Textures/projectile.png");
		private const float defaultProjectileSpeed = 1000f;
		private float projectileSpeed;


		public Projectile(Vector2f pos, float angle, float projectileSpeed = defaultProjectileSpeed)
			: base(pos, angle, projectileTexture)
		{

			this.projectileSpeed = projectileSpeed;

		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return Common.MovePointByAngle(projectileSpeed, Angle);
		}

		protected override void HandleEdge()
		{

			Delete();

		}

		protected override void HandleCollision(Drawable entity)
		{

			if (!ToDelete)
			{
				Delete();
				entity.Delete();
			}

		}
	}
}
