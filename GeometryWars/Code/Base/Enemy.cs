using GeometryWars.Code.Emmiters;
using GeometryWars.Code.Main;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Enemy : Movable
	{
		#region Private Fields
		private float angleSpeed;
		private Color color;
		private SoundBuffer dieingSound = new SoundBuffer("Assets/SFX/Enemy_explode.wav");
		private float moveSpeed;
		#endregion Private Fields

		#region Protected Constructors

		protected Enemy(Vector2f pos, float initAngle, float moveSpeed, float angleSpeed, Texture texture, Color mainColor)
			: base(pos, initAngle, texture)
		{
			this.moveSpeed = moveSpeed;
			this.angleSpeed = angleSpeed;
			color = mainColor;
		}

		#endregion Protected Constructors

		#region Public Properties

		public float AngleSpeed
		{
			get { return angleSpeed; }
			protected set { angleSpeed = value; }
		}

		public float MoveSpeed
		{
			get { return moveSpeed; }
			protected set { moveSpeed = value; }
		}

		#endregion Public Properties

		#region Public Methods

		public override void Delete()
		{
			if (Bomb.CanEnemiesSpawn)
				ScoreManager.AddScore(AddScore());

			EntityManager.AddEmitter(new EnemyExplosionEmiter(Pos, color));

			Game.PlaySound(dieingSound);

			base.Delete();
		}

		#endregion Public Methods

		#region Protected Methods

		protected abstract int AddScore();

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return new Vector2f();
		}

		//public bool CanShoot
		//{
		//	get { return }
		//}
		protected override void HandleCollision(Drawable entity)
		{
			Hero.GetInstance().TakeDamage(5);
		}

		#endregion Protected Methods
	}
}