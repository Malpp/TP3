using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.System;

namespace GeometryWars.Code.Text
{
	class ScoreText : ScreenText
	{
		#region Private Fields
		private const string key = "score";
		private int cMul = 0;
		private int cScore = -1;
		#endregion Private Fields

		#region Public Constructors

		public ScoreText()
			: base(new Vector2f(10, 10), "Score", 30)
		{
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override bool ShouldUpdate()
		{
			if (cScore != ScoreManager.Score || cMul != ScoreManager.Multi)
			{
				cScore = ScoreManager.Score;
				cMul = ScoreManager.Multi;
				return true;
			}
			return false;
		}

		protected override string UpdateText()
		{
			return StringTable.GetInstance().GetWord(key) + ScoreManager.Score + " " + ScoreManager.Multi + "x";
		}

		#endregion Protected Methods
	}
}