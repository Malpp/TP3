using GeometryWars.Code.Base;
using SFML.Graphics;

namespace GeometryWars.Code.Effects
{
	class Pixelate : Effect
	{
		#region Private Fields
		private Shader myShader = null;

		private Sprite mySprite = null;

		private Texture myTexture = null;
		#endregion Private Fields

		#region Public Constructors

		public Pixelate()
		{
			// Load the texture and initialize the sprite
			mySprite = new Sprite();

			// Load the shader
			myShader = new Shader(null, "Assets/Shaders/pixelate.frag");
			myShader.SetParameter("texture", Shader.CurrentTexture);
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override void OnDraw(RenderTarget target, RenderStates states)
		{
			states = new RenderStates(states);
			states.Shader = myShader;
			target.Draw(mySprite, states);
		}

		protected override void OnUpdate(Texture texture, float sigma, float glow)
		{
			mySprite.Texture = texture;
		}

		#endregion Protected Methods
	}
}