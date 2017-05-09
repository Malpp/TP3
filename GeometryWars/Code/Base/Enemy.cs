using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emmiters;
using GeometryWars.Code.Main;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Enemy : Movable
	{
		private float moveSpeed;
		private float angleSpeed;
		private Color color;
		SoundBuffer dieingSound = new SoundBuffer("Assets/SFX/Enemy_explode.wav");

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

		protected Enemy(Vector2f pos, float initAngle, float moveSpeed, float angleSpeed, Texture texture, Color mainColor)
			: base(pos, initAngle, texture)
		{

			this.moveSpeed = moveSpeed;
			this.angleSpeed = angleSpeed;
			color = mainColor;

		}

		protected override void HandleCollision(Drawable entity)
		{
			
		}

	    protected abstract int AddScore();

		public override void Delete()
		{

            ScoreManager.AddScore(AddScore());

			EntityManager.AddEmitter(new EnemyExplosionEmiter(Pos, 5, color));

			Game.PlaySound(dieingSound);

			base.Delete();
		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return new Vector2f();
		}

	}
}
