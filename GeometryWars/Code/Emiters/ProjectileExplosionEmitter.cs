using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Particles;
using NetEXT.Particles;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class ProjectileExplosionEmitter : EmitterBase
	{
		private Vector2f pos;
		private int particleCount;

		public ProjectileExplosionEmitter(Vector2f pos, int count)
		{
			this.pos = pos;
			particleCount = count;
		}

		public override void EmitParticles(ParticleSystem ParticleSystem, Time DeltaTime)
		{
			for (int i = 0; i < particleCount; i++)
			{
				EmitParticle(ParticleSystem, new ProjectileExplosionParticle(pos));
			}

			

		}
	}
}
