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

		public static void UpdateMultiplier(int killCount)
		{
			if (killCount > 2000)
				multiplier = 10;
			else if (killCount > 1500)
				multiplier = 9;
			else if (killCount > 1200)
				multiplier = 8;
			else if (killCount > 900)
				multiplier = 7;
			else if (killCount > 600)
				multiplier = 6;
			else if (killCount > 300)
				multiplier = 5;
			else if (killCount > 150)
				multiplier = 4;
			else if (killCount > 75)
				multiplier = 3;
			else if (killCount > 25)
				multiplier = 2;
			else
				multiplier = 1;
		}
		#endregion Public Methods
	}
}