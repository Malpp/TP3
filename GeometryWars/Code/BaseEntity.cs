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
	class BaseEntity
	{

		protected float SPEED;
		protected float angleSPEED = 200f;

		private static Texture defaultTexture = new Texture("Assets/Textures/base.png");
		protected Sprite sprite;
		private Vector2f textureSize;
		protected float angle;
		protected bool toDelete = false;
		private Vector2f newPos;

		public bool ToDelete
		{
			get { return toDelete; }
		} 
			
		public float Angle
		{
			get { return angle; }
		} 
		public Vector2f Pos 
		{
			get { return sprite.Position; }
		}
		public FloatRect GlobalBounds
		{
			get { return sprite.GetGlobalBounds(); }
		} 

		public BaseEntity(float x, float y, float speed, Texture texture = null, float angle = 0)
		{

			SPEED = speed;

			if (texture == null)
				texture = defaultTexture;

			textureSize = (Vector2f)texture.Size;

			this.angle = angle;

			sprite = new Sprite(texture);
			sprite.Origin = new Vector2f(texture.Size.X * 0.5f, texture.Size.Y * 0.5f);
			sprite.Position = new Vector2f(x,y);
			sprite.Rotation = angle;

		}

		protected virtual Vector2f GetMove(float timeDelta)
		{
			
			Vector2f pos = new Vector2f();

			if(Keyboard.IsKeyPressed(Keyboard.Key.W))
				pos += Common.MovePointByAngle(SPEED, angle);

			if (Keyboard.IsKeyPressed(Keyboard.Key.A))
				angle -= angleSPEED * timeDelta;
				//pos += new Vector2f(-1, 0);

			if (Keyboard.IsKeyPressed(Keyboard.Key.S))
				pos -= Common.MovePointByAngle(SPEED, angle);

			if (Keyboard.IsKeyPressed(Keyboard.Key.D))
				angle += angleSPEED * timeDelta;
				//pos += new Vector2f(1, 0);

			return pos;

		}

		public BaseEntity Collides(IEnumerable<BaseEntity> entity)
		{

			foreach (BaseEntity baseEntity in entity)
			{

				if (sprite.GetGlobalBounds().Intersects(baseEntity.GlobalBounds))
				{
					HandleCollision(baseEntity);
				}

			}

			return null;

		}

		private bool IsAtEdge()
		{

			if (sprite.Position.X > Game.GAME_X_LIMIT - Game.BORDER_SIZE - textureSize.X * 0.5f ||
				sprite.Position.X < Game.BORDER_SIZE + textureSize.X * 0.5f ||
				sprite.Position.Y > Game.GAME_Y_LIMIT - Game.BORDER_SIZE - textureSize.Y * 0.5f ||
				sprite.Position.Y < Game.BORDER_SIZE + textureSize.Y * 0.5f)
				return true;

			return false;

		}

		public void Delete()
		{
			toDelete = true;
		}

		protected virtual void HandleEdge()
		{

			sprite.Position = new Vector2f(

				Math.Max(Game.BORDER_SIZE + textureSize.X * 0.5f,
				Math.Min(Game.GAME_X_LIMIT - Game.BORDER_SIZE - textureSize.X * 0.5f, sprite.Position.X)),

				Math.Max(Game.BORDER_SIZE + textureSize.Y * 0.5f,
				Math.Min(Game.GAME_Y_LIMIT - Game.BORDER_SIZE - textureSize.Y * 0.5f, sprite.Position.Y)));

		}

		protected virtual void HandleCollision(BaseEntity entity)
		{

			sprite.Position -= newPos;

		}

		public virtual void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			newPos = GetMove(deltaTime) * deltaTime;

			sprite.Position += newPos;

			if (entities != null)
			{
				Collides(entities);
			}

			if(IsAtEdge())
				HandleEdge();

			sprite.Rotation = angle;


		}

		public virtual void Draw(RenderTarget window)
		{
			
			window.Draw(sprite);

		}

	}
}
