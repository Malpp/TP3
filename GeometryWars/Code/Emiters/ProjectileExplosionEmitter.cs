using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class ProjectileExplosionEmitter : BaseEmitter
	{
		private const float _emitterDuration = 0.1f;
		private const int count = 5;

		public ProjectileExplosionEmitter(Vector2f pos, Color color)
			: base(pos, count, _emitterDuration, color)
		{
		}

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new ProjectileExplosionParticle(pos, color);
		}
	}
}