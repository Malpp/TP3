using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Particles
{
	class BombParticle : BaseParticle
	{

		private const float bombParticleSpeed = 4000f;
	    private const int randomElement = (int)(bombParticleSpeed * 0.2f);

		public BombParticle(Vector2f pos, Color color)
			: base(pos, color, Bomb.BombDuration, bombParticleSpeed + Game.rnd.Next(-randomElement,randomElement))
		{
			
		}

	}
}
