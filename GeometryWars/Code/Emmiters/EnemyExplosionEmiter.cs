using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Particles;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class EnemyExplosionEmiter: EmitterBase
	{
		private Vector2f pos;
		private int particleCount;
		private Color color;

		public EnemyExplosionEmiter(Vector2f pos, int count, Color color)
		{
			this.pos = pos;
			particleCount = count;
			this.color = color;
		}

		public override void EmitParticles(ParticleSystem ParticleSystem, Time DeltaTime)
		{
			for (int i = 0; i < particleCount; i++)
			{
				EmitParticle(ParticleSystem, new EnemyExplosionParticle(pos, color));
			}

			

		}
	}
}
