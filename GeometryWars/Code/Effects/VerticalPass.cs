using GeometryWars.Code.Base;
using SFML.Graphics;

namespace GeometryWars.Code.Effects
{
	class VerticalPass : Effect
	{
		#region Private Fields
		private Shader myShader = null;

		private Sprite mySprite = null;

		private Texture myTexture = null;
		#endregion Private Fields

		#region Public Constructors

		public VerticalPass()
		{
			// Load the texture and initialize the sprite
			mySprite = new Sprite();

			// Load the shader
			myShader = new Shader(null, "Assets/Shaders/vert.frag");
			myShader.SetParameter("sourceTexture", Shader.CurrentTexture);
			myShader.SetParameter("sigma", 1f);
			myShader.SetParameter("height", Game.GAME_HEIGHT);
			myShader.SetParameter("glowMultiplier", 1.5f);
		}

		#endregion Public Constructors

		#region Public Properties

		public Texture Texture
		{
			get { return myTexture; }
		}

		#endregion Public Properties

		#region Protected Methods

		protected override void OnDraw(RenderTarget target, RenderStates states)
		{
			states = new RenderStates(states);
			states.Shader = myShader;
			states.BlendMode = BlendMode.Add;
			target.Draw(mySprite, states);
		}

		protected override void OnUpdate(Texture texture, float sigma, float glow)
		{
			mySprite.Texture = texture;
			myShader.SetParameter("sigma", sigma);
			myShader.SetParameter("glowMultiplier", glow);
		}

		#endregion Protected Methods
	}
}