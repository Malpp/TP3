using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.System;

namespace GeometryWars.Code.Text
{
    class ScoreText : ScreenText
    {
        private int cScore = -1;
        private int cMul = 0;

        public ScoreText()
            : base(new Vector2f(10,10), "Score", 30)
        {
            
        }

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
            return "Score:" + cScore + " " + cMul + "x";
        }
    }
}
