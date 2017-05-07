using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Projectile : Movable
	{

		private static Texture projectileTexture = new Texture("Assets/Textures/projectile.png");
		private static float projectileSpeed = 1000f;

		public static float Speed
		{
			get { return projectileSpeed; }
		}

		public Projectile(Vector2f pos, float angle)
			: base(pos, angle, projectileTexture)
		{

		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return Common.MovePointByAngle(projectileSpeed, Angle);
		}

		protected override void HandleEdge()
		{

			Delete();

		}

	}
}
