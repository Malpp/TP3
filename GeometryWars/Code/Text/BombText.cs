using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.System;

namespace GeometryWars.Code.Text
{
	class BombText : ScreenText
	{
		#region Private Fields
		private int cBomb;
		#endregion Private Fields

		#region Public Constructors

		public BombText()
			: base(new Vector2f(10, 70), "Bomb:", 30)
		{
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override bool ShouldUpdate()
		{
			if (cBomb != Hero.GetInstance().BombCount)
			{
				cBomb = Hero.GetInstance().BombCount;
				return true;
			}
			return false;
		}

		protected override string UpdateText()
		{
			return StringTable.GetInstance().GetWord("bomb") + cBomb;
		}

		#endregion Protected Methods
	}
}