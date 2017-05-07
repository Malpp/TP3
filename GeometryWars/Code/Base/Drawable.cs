using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Drawable
	{
		protected Sprite sprite;
		private bool toDelete = false;

		public bool ToDelete
		{
			get { return toDelete; }
		}

		public FloatRect GlobalBounds
		{
			get { return sprite.GetGlobalBounds(); }
		}

		public Vector2f TextureSize
		{
			get { return (Vector2f)sprite.Texture.Size; }
		}

		protected Drawable(Vector2f pos, float angle, Texture texture)
		{

			sprite = new Sprite(texture);
			sprite.Origin = (Vector2f)texture.Size * 0.5f;
			sprite.Rotation = angle;
			sprite.Position = pos;

		}

		public virtual void Delete()
		{
			toDelete = true;
		}

		public virtual void Draw(RenderTarget window)
		{

			window.Draw(sprite);

		}

	}
}
