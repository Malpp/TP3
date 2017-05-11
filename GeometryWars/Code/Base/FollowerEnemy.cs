using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GeometryWars.Code.Enemies
{
	abstract class FollowerEnemy : Enemy
	{
		#region Protected Constructors

		protected FollowerEnemy(Vector2f pos, float initAngle, float moveSpeed, float angleSpeed, Texture texture, Color mainColor)
			: base(pos, initAngle, moveSpeed, angleSpeed, texture, mainColor)
		{
		}

		#endregion Protected Constructors

		#region Public Methods

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

		#endregion Public Methods

		#region Protected Methods

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return Common.MovePointByAngle(MoveSpeed, Angle);
		}

		#endregion Protected Methods
	}
}