using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GeometryWars.Code.Enemies
{
	class Mini : FollowerEnemy
	{
		#region Private Fields
		private const float fireSpeed = 5f;
		private const float miniAngleSpeed = 120f;
		private const float miniSpeed = 150f;
		private const int pointsWorth = 5;
		private static Color color = new Color(255, 106, 0);
		private static Texture miniSniperTexture = new Texture("Assets/Textures/mini.png");
		private bool canFire;
		private float fireDelta;
		#endregion Private Fields

		#region Public Constructors

		public Mini(Vector2f pos, float initAngle)
			: base(pos, initAngle, miniSpeed, miniAngleSpeed, miniSniperTexture, color)
		{
		}

		#endregion Public Constructors

		#region Public Properties

		public static float Size
		{
			get { return miniSniperTexture.Size.X; }
		}

		#endregion Public Properties

		#region Public Methods

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			if (canFire)
			{
				if (Math.Abs(Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - CorrectAngle(Angle)) < 1f)
				{
					canFire = false;
					EntityManager.AddEnemyProjectile(new EnemyProjectile(Pos, Angle));
				}
			}

			if (!canFire)
			{
				fireDelta += timeDelta;

				if (fireDelta > fireSpeed)
				{
					canFire = true;
					fireDelta = 0;
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

		protected override void HandleCollision(Drawable entity)
		{
			Delete();
			base.HandleCollision(entity);
		}

		#endregion Protected Methods
	}
}