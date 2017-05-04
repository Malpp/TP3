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

			float angle1 = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);


			float angle2 = Common.AngleBetweenTwoPoints(Hero.GetInstance().Pos, Pos) - Angle % 360;
			float distance = Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle % 360), Hero.GetInstance().Pos);
			float newDistance = Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle % 360 + angleSPEED * deltaTime), Hero.GetInstance().Pos);

			//Console.WriteLine("{0} {1}", 
			//	Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle), Hero.GetInstance().Pos),
			//	Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle + angleSPEED), Hero.GetInstance().Pos));

			//if (Math.Abs(Math.Abs(angleBetweenPoints) - Math.Abs(Angle) % 360) < 1f)
			//{
			//	angle = angleBetweenPoints;
			//}
			//else 
			if (newDistance < distance)
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
