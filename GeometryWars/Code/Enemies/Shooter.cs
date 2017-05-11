using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GeometryWars.Code.Enemies
{
	class Shooter : FollowerEnemy
	{
		#region Private Fields
		private const float fireSpeed = 1f;
		private const int pointsWorth = 15;
		private const float shooterAngleSpeed = 125f;
		private const float shooterSpeed = 300f;
		private static Color color = new Color(7, 0, 234);
		private static Texture shooterTexture = new Texture("Assets/Textures/shooter.png");
		private bool canFire = false;
		private bool correctAngle = false;
		private float fireDelta = 0;
		#endregion Private Fields

		#region Public Constructors

		public Shooter(Vector2f pos, float initAngle)
			: base(pos, initAngle, shooterSpeed, shooterAngleSpeed, shooterTexture, color)
		{
		}

		#endregion Public Constructors

		#region Public Methods

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			if (Math.Abs(Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - CorrectAngle(Angle)) < 1f)
				correctAngle = true;
			else
				correctAngle = false;

			if (canFire && correctAngle)
			{
				canFire = false;
				EntityManager.AddEnemyProjectile(new EnemyProjectile(Pos + Common.MovePointByAngle(shooterTexture.Size.X * 0.3f, Angle), Angle));
			}

			if (!canFire)
			{
				fireDelta += timeDelta;

				if (fireDelta > fireSpeed)
				{
					fireDelta = 0;
					canFire = true;
				}
			}

			base.Update(timeDelta, entities);
		}

		#endregion Public Methods

		#region Protected Methods

		protected override int AddScore()
		{
			return pointsWorth;
		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			float distance = Common.DistanceBetweenTwoPoints(Hero.GetInstance().Pos, Pos);

			if (distance > 210f)
			{
				return Common.MovePointByAngle(shooterSpeed, Angle);
			}
			else if (distance < 200f)
			{
				return Common.MovePointByAngle(-shooterSpeed, Angle);
			}

			return new Vector2f();
		}

		#endregion Protected Methods
	}
}