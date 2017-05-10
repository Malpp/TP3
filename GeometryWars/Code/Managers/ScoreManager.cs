using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars.Code.Main
{
    static class ScoreManager
    {
        private static int score = 0;
        private static int multiplier = 1;

        public static int Score
        {
            get { return score; }

        }

        public static int Multi
        {
            get { return multiplier; }
        }

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

    }
}
