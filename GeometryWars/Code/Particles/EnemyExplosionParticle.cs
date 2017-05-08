using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Particles
{
	class EnemyExplosionParticle : BaseParticle
	{
		private const float particleSpeed = 1000f;
		private const float particleDuration = 0.3f;

		public EnemyExplosionParticle(Vector2f pos, Color color)
			: base(pos, color, particleDuration, particleSpeed)
		{

		}

	}
}
