namespace GeometryWars.Code.Main
{
	static class ScoreManager
	{
		#region Private Fields
		private static int multiplier = 1;
		private static int score = 0;
		#endregion Private Fields

		#region Public Properties

		public static int Multi
		{
			get { return multiplier; }
		}

		public static int Score
		{
			get { return score; }
		}

		#endregion Public Properties

		#region Public Methods

		public static void AddScore(int scoreToAdd)
		{
			score += scoreToAdd * multiplier;
		}

		public static void Reset()
		{
			multiplier = 1;
			score = 0;
		}

		public static void ResetMulti()
		{
			multiplier = 1;
		}

		#endregion Public Methods
	}
}