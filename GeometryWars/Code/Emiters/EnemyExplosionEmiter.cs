using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emmiters
{
	class EnemyExplosionEmiter : BaseEmitter
	{
		#region Private Fields
		private const float _emitterDuration = 0.075f;
		private const int count = 50;
		#endregion Private Fields

		#region Public Constructors

		public EnemyExplosionEmiter(Vector2f pos, Color color)
			: base(pos, count, _emitterDuration, color)
		{
		}

		#endregion Public Constructors

		#region Public Methods

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new EnemyExplosionParticle(pos, color);
		}

		#endregion Public Methods
	}
}