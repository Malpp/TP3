using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	class Enemy : BaseEntity
	{

		private static Texture enemyTexture = new Texture("Assets/Textures/enemy.png");
		private const float enemySpeed = 300f;

		public Enemy(float x, float y, float speed , Texture texture)
			: base(x, y, speed, texture)
		{
			


		}

		public Enemy(float x, float y, float speed, Texture texture, float angle)
			: base(x, y, speed, texture, angle)
		{



		}

		public Enemy(float x, float y)
			: base(x,y, enemySpeed, enemyTexture)
		{
			


		}

		protected override Vector2f GetMove(float timeDelta)
		{

			return new Vector2f(0,0);

		}

	}
}
