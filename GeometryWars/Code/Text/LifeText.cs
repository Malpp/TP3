using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.System;

namespace GeometryWars.Code.Text
{
	class LifeText : ScreenText
	{
		#region Private Fields
		private const string key = "life";
		private int cLife;
		#endregion Private Fields

		#region Public Constructors

		public LifeText()
			: base(new Vector2f(10, 40), "Life:", 30)
		{
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override bool ShouldUpdate()
		{
			if (cLife != Hero.GetInstance().Life)
			{
				cLife = Hero.GetInstance().Life;
				return true;
			}
			return false;
		}

		protected override string UpdateText()
		{
			return StringTable.GetInstance().GetWord(key) + Hero.GetInstance().Life;
		}

		#endregion Protected Methods
	}
}