using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	class Projectile : BaseEntity
	{

		private static Texture projectileTexture = new Texture("Assets/Textures/projectile.png");
		private const int PROJECTILE_SPEED = 1000;


		public Projectile(Vector2f pos, float angle)
			: base(pos.X, pos.Y, PROJECTILE_SPEED, projectileTexture, angle)
		{


		}

		protected override Vector2f GetMove()
		{

			return Common.MovePointByAngle(PROJECTILE_SPEED, Angle);

		}

		protected override void HandleEdge()
		{

			toDelete = true;

		}

		protected override void HandleCollision(BaseEntity entity)
		{

			toDelete = true;
			entity.Delete();

		}
	}
}
