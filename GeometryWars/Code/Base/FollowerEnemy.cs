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



		protected FollowerEnemy(Vector2f pos, float initAngle, float moveSpeed, float angleSpeed, Texture texture, Color mainColor)
			: base(pos, initAngle, moveSpeed, angleSpeed, texture, mainColor)
		{
			
		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return Common.MovePointByAngle(MoveSpeed, Angle);
		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			float distance = Common.DistanceBetweenTwoPoints(
				Pos + Common.MovePointByAngle(
					MoveSpeed * timeDelta, 
					Angle % 360), 
				Hero.GetInstance().Pos);

			float newDistance = Common.DistanceBetweenTwoPoints(
				Pos + Common.MovePointByAngle(
					MoveSpeed * timeDelta, 
					Angle % 360 + AngleSpeed * timeDelta), 
				Hero.GetInstance().Pos);

			//Console.WriteLine("{0} {1}", 
			//	Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle), Hero.GetInstance().Pos),
			//	Common.DistanceBetweenTwoPoints(Pos + Common.MovePointByAngle(SPEED * deltaTime, Angle + angleSPEED), Hero.GetInstance().Pos));

			//if (Math.Abs(Math.Abs(angleBetweenPoints) - Math.Abs(Angle) % 360) < 1f)
			//{
			//	angle = angleBetweenPoints;
			//}
			//else
		    float angleBetweenHero = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);

            if (Math.Abs(angleBetweenHero - CorrectAngle(Angle)) < AngleSpeed * timeDelta)
		    {
		        Angle = angleBetweenHero;

		    }
			else if (newDistance < distance)
			{
				Angle += AngleSpeed * timeDelta;
			}
			else
			{
				Angle -= AngleSpeed * timeDelta;
			}

			base.Update(timeDelta, entities);
		}
		
	}
}
