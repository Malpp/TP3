using SFML.Graphics;

namespace GeometryWars.Code.Base
{
	abstract class Effect : SFML.Graphics.Drawable
	{
		#region Public Methods

		public void Draw(RenderTarget target, RenderStates states)
		{
			if (Shader.IsAvailable)
			{
				OnDraw(target, states);
			}
		}

		public void Update(Texture texture, float sigma, float glow)
		{
			if (Shader.IsAvailable)
				OnUpdate(texture, sigma, glow);
		}

		#endregion Public Methods

		#region Protected Methods

		protected abstract void OnDraw(RenderTarget target, RenderStates states);

		protected abstract void OnUpdate(Texture texture, float sigma, float glow);

		#endregion Protected Methods
	}
}