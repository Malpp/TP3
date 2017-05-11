using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Drawable
	{
		#region Protected Fields
		protected Sprite sprite;
		#endregion Protected Fields

		#region Private Fields
		private bool toDelete = false;
		#endregion Private Fields

		#region Protected Constructors

		protected Drawable(Vector2f pos, float angle, Texture texture)
		{
			sprite = new Sprite(texture);
			sprite.Origin = (Vector2f)texture.Size * 0.5f;
			sprite.Rotation = angle;
			sprite.Position = pos;
		}

		#endregion Protected Constructors

		#region Public Properties

		public FloatRect GlobalBounds
		{
			get { return sprite.GetGlobalBounds(); }
		}

		public Vector2f TextureSize
		{
			get { return (Vector2f)sprite.Texture.Size; }
		}

		public bool ToDelete
		{
			get { return toDelete; }
		}

		#endregion Public Properties

		#region Public Methods

		public virtual void Delete()
		{
			toDelete = true;
		}

		public virtual void Draw(RenderTarget window)
		{
			window.Draw(sprite);
		}

		#endregion Public Methods
	}
}