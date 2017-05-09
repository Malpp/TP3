using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class Shooter : FollowerEnemy
	{

		private static Texture shooterTexture = new Texture("Assets/Textures/shooter.png");
		private const float shooterSpeed = 300f;
		private const float shooterAngleSpeed = 125f;
		private const float fireSpeed = 0.3f;
		private float fireDelta = 0;
		private bool canFire = false;
		private bool correctAngle = false;
		private static Color color = new Color(7, 0, 234);
        private const int pointsWorth = 10;

        public Shooter(Vector2f pos, float initAngle)
			: base(pos, initAngle, shooterSpeed, shooterAngleSpeed, shooterTexture, color)
		{

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

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			 
			if (Math.Abs( Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos) - CorrectAngle(Angle)) < 1f)
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

	    protected override int AddScore()
	    {
	        return pointsWorth;
	    }
	}
}
