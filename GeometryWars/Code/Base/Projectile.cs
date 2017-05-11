using GeometryWars.Code.Emmiters;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GeometryWars.Code
{
	abstract class Projectile : Movable
	{
		#region Private Fields
		private static SoundBuffer hitWallSound = new SoundBuffer("Assets/SFX/Projectile_hit_wall.wav");
		private static float projectileSpeed = 1000f;
		private Color color;
		private float lastTimeDelta;
		#endregion Private Fields

		#region Public Constructors

		public Projectile(Vector2f pos, float angle, Texture texture, Color color)
			: base(pos, angle, texture)
		{
			this.color = color;
		}

		#endregion Public Constructors

		#region Public Properties

		public static float Speed
		{
			get { return projectileSpeed; }
		}

		#endregion Public Properties

		#region Public Methods

		public override void Delete()
		{
			EntityManager.AddEmitter(new ProjectileExplosionEmitter(Pos, color));

			base.Delete();
		}

		public override void DoCollisions(IEnumerable<Drawable> entities)
		{
			foreach (Drawable entity in entities)
			{
				if (!entity.ToDelete && entity.GlobalBounds.Contains(Pos.X, Pos.Y))
				{
					HandleCollision(entity);
				}
			}
		}

		#endregion Public Methods

		#region Protected Methods

		protected override Vector2f GetNextMove(float timeDelta)
		{
			lastTimeDelta = timeDelta;
			return Common.MovePointByAngle(projectileSpeed, Angle);
		}

		protected override void HandleEdge()
		{
			Game.PlaySound(hitWallSound);

			Delete();
		}

		#endregion Protected Methods
	}
}