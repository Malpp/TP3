using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Base
{
	abstract class ScreenText
	{
		#region Private Fields
		private static Vector2f adjustPos = new Vector2f(30, 0);
		private static Font font = new Font("Assets/Fonts/emulogic.ttf");
		private Vector2f pos;
		private SFML.Graphics.Text text;
		#endregion Private Fields

		#region Public Constructors

		public ScreenText(Vector2f pos, string initString, int size)
		{
			text = new SFML.Graphics.Text(initString, font, (uint)size);
			text.Position = pos;
			this.pos = pos;
		}

		#endregion Public Constructors

		#region Public Properties

		public Vector2f Pos
		{
			get { return pos; }
			protected set { pos = value; }
		}

		#endregion Public Properties

		#region Public Methods

		public virtual void Draw(RenderTarget window)
		{
			window.Draw(text);
		}

		public virtual void ForceUpdate()
		{
			text.DisplayedString = UpdateText();
		}

		public virtual void Update()
		{
			//text.Position = pos + Camera.Center * 0.2f - Camera.Pos - adjustPos;
			if (ShouldUpdate())
			{
				text.DisplayedString = UpdateText();
			}
		}

		#endregion Public Methods

		#region Protected Methods

		protected abstract bool ShouldUpdate();

		protected abstract string UpdateText();

		#endregion Protected Methods
	}
}