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
	class LifeText : ScreenText
	{
		private int cLife;
		private const string key = "life";

		public LifeText()
			: base(new Vector2f(10, 40), "Life:", 30)
		{
			
		}

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
	}
}
