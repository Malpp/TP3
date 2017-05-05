using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Enemy : Movable
	{
		private float moveSpeed;
		private float angleSpeed;

		public float MoveSpeed
		{
			get { return moveSpeed; }
			protected set { moveSpeed = value; }
		}

		public float AngleSpeed
		{
			get { return angleSpeed; }
			protected set { angleSpeed = value; }
		}

		//public bool CanShoot
		//{
		//	get { return }
		//}

		protected Enemy(Vector2f pos, float initAngle, float moveSpeed, float angleSpeed, Texture texture)
			: base(pos, initAngle, texture)
		{

			this.moveSpeed = moveSpeed;
			this.angleSpeed = angleSpeed;

		}

		protected override void HandleCollision(Drawable entity)
		{

		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return new Vector2f();
		}

	}
}
