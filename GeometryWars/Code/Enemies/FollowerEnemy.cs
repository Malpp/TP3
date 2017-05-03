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

		protected override Vector2f GetMove()
		{

			return Common.MovePointByAngle(SPEED, Angle);

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			if (Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - Angle < 1f)
			{
				angle = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);
			}
			else if (Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) >= 0)
			{
				angle += angleSPEED;
			}
			else
			{
				angle -= angleSPEED;
			}

			base.Update(deltaTime, entities);
		}
	}
}
