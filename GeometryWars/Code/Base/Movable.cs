using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GeometryWars.Code
{
	abstract class Movable : Drawable
	{
		#region Private Fields
		private float angle;
		private Vector2f nextPos;
		private Vector2f pos;
		#endregion Private Fields

		#region Protected Constructors

		protected Movable(Vector2f pos, float initAngle, Texture texture)
			: base(pos, initAngle, texture)
		{
			angle = initAngle;
			this.pos = pos;
		}

		#endregion Protected Constructors

		#region Public Properties

		public float Angle
		{
			get { return angle; }
			protected set { angle = value % 360; }
		}

		public Vector2f LastPos
		{
			get { return pos - nextPos; }
		}

		public Vector2f Pos
		{
			get { return pos; }
			protected set { pos = value; }
		}

		#endregion Public Properties

		#region Public Methods

		public virtual void DoCollisions(IEnumerable<Drawable> entities)
		{
			foreach (Drawable entity in entities)
			{
				if (!entity.ToDelete && sprite.GetGlobalBounds().Intersects(entity.GlobalBounds))
				{
					HandleCollision(entity);
				}
			}
		}

		public virtual void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			nextPos = GetNextMove(timeDelta) * timeDelta;
			Pos += nextPos;

			if (entities != null)
			{
				DoCollisions(entities);
			}

			if (IsAtEdge())
				HandleEdge();

			sprite.Position = Pos;
			sprite.Rotation = Angle;
		}

		#endregion Public Methods

		#region Protected Methods

		protected float CorrectAngle(float _angle)
		{
			float correctedAngle = 360 - Math.Abs(_angle);

			if (_angle < 0)
				correctedAngle = 360 - correctedAngle;

			if (correctedAngle < 180)
				correctedAngle = -correctedAngle;
			else
				correctedAngle = 360 - correctedAngle;

			return correctedAngle;
		}

		protected abstract Vector2f GetNextMove(float timeDelta);

		protected abstract void HandleCollision(Drawable entity);

		protected virtual void HandleEdge()
		{
			Pos = new Vector2f(

				Math.Max(Game.BORDER_SIZE + TextureSize.X * 0.5f,
					Math.Min(Game.GAME_X_LIMIT - Game.BORDER_SIZE - TextureSize.X * 0.5f, sprite.Position.X)),

				Math.Max(Game.BORDER_SIZE + TextureSize.Y * 0.5f,
					Math.Min(Game.GAME_Y_LIMIT - Game.BORDER_SIZE - TextureSize.Y * 0.5f, sprite.Position.Y)));
		}

		#endregion Protected Methods

		#region Private Methods

		private bool IsAtEdge()
		{
			if (sprite.Position.X > Game.GAME_X_LIMIT - Game.BORDER_SIZE - TextureSize.X * 0.5f ||
				sprite.Position.X < Game.BORDER_SIZE + TextureSize.X * 0.5f ||
				sprite.Position.Y > Game.GAME_Y_LIMIT - Game.BORDER_SIZE - TextureSize.Y * 0.5f ||
				sprite.Position.Y < Game.BORDER_SIZE + TextureSize.Y * 0.5f)
				return true;

			return false;
		}

		#endregion Private Methods
	}
}