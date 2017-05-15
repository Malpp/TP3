using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class ProjectileExplosionEmitter : BaseEmitter
	{
		#region Private Fields
		private const float _emitterDuration = 0.1f;
		private const int count = 5;
		#endregion Private Fields

		#region Public Constructors

		public ProjectileExplosionEmitter(Vector2f pos, Color color)
			: base(pos, count, _emitterDuration, color)
		{
		}

		#endregion Public Constructors

		#region Public Methods

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new ProjectileExplosionParticle(pos, color);
		}

		#endregion Public Methods
	}
}