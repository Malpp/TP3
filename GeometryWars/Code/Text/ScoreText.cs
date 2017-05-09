using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using SFML.System;

namespace GeometryWars.Code.Text
{
    class ScoreText : ScreenText
    {

        public ScoreText()
            : base(new Vector2f(), "Score", 40)
        {
            
        }

        protected override bool ShouldUpdate()
        {
            return false;
        }

        protected override string UpdateText()
        {
            return "Score";
        }
    }
}
