using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	abstract class FollowerEnemy : Enemy
	{

		public FollowerEnemy(float x, float y, float speed, Texture texture)
			: base(x, y, speed, texture)
		{
			
		}

		public FollowerEnemy(float x, float y, float speed, Texture texture, float angle)
			: base(x, y, speed, texture, angle)
		{

		}

		protected override Vector2f GetMove(float timeDelta)
		{

			return Common.MovePointByAngle(SPEED, Angle);

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			float angleBetweenPoints = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);

			if (Math.Abs(Math.Abs(angleBetweenPoints) - Math.Abs(Angle) % 360) < 1f)
			{
				angle = angleBetweenPoints;
			}
			else if (angleBetweenPoints >= 0)
			{
				angle += angleSPEED * deltaTime;
			}
			else
			{
				angle -= angleSPEED * deltaTime;
			}

			base.Update(deltaTime, entities);
		}
	}
}
