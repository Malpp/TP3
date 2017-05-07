using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class ProjectileExplosionEmitter : BaseEmitter
	{
		private const float _emitterDuration = 0.1f;

		public ProjectileExplosionEmitter(Vector2f pos, int count, Color color)
			: base(pos, count, _emitterDuration, color)
		{
			
		}

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new ProjectileExplosionParticle(pos, color);
		}
	}
}
