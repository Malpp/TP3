using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emiters
{
	class BombEmitter : BaseEmitter
	{
		private const float bombEmitterDuration = 0.2f;
		private const int bombEmitterCount = 100;

		public BombEmitter(Vector2f pos, Color color)
			: base(pos, bombEmitterCount, bombEmitterDuration, color)
		{
			
		}

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new BombParticle(pos, color);
		}
	}
}
