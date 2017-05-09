using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GeometryWars.Code
{
	abstract class Movable : Drawable
	{

		private float angle;
		private Vector2f pos;
		private Vector2f nextPos;

		public Vector2f Pos
		{
			get { return pos; }
			protected set { pos = value; }
		}

		public float Angle
		{
			get { return angle; }
			protected set { angle = value % 360; }
		}

		public Vector2f LastPos
		{
			get { return Pos - nextPos; }
		}

		protected Movable(Vector2f pos, float initAngle, Texture texture)
			: base(pos, initAngle, texture)
		{

			angle = initAngle;
			this.pos = pos;

		}

		protected virtual void HandleEdge()
		{

			Pos = new Vector2f(

				Math.Max(Game.BORDER_SIZE + TextureSize.X * 0.5f,
					Math.Min(Game.GAME_X_LIMIT - Game.BORDER_SIZE - TextureSize.X * 0.5f, sprite.Position.X)),

				Math.Max(Game.BORDER_SIZE + TextureSize.Y * 0.5f,
					Math.Min(Game.GAME_Y_LIMIT - Game.BORDER_SIZE - TextureSize.Y * 0.5f, sprite.Position.Y)));

		}

		protected abstract void HandleCollision(Drawable entity);

		protected abstract Vector2f GetNextMove(float timeDelta);

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

		private bool IsAtEdge()
		{

			if (sprite.Position.X > Game.GAME_X_LIMIT - Game.BORDER_SIZE - TextureSize.X * 0.5f ||
				sprite.Position.X < Game.BORDER_SIZE + TextureSize.X * 0.5f ||
				sprite.Position.Y > Game.GAME_Y_LIMIT - Game.BORDER_SIZE - TextureSize.Y * 0.5f ||
				sprite.Position.Y < Game.BORDER_SIZE + TextureSize.Y * 0.5f)
				return true;

			return false;

		}

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

	}
}
