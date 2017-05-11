using GeometryWars.Code.Base;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Particles
{
	class EnemyExplosionParticle : BaseParticle
	{
		#region Private Fields
		private const float particleDuration = 0.3f;
		private const float particleSpeed = 500f;
		#endregion Private Fields

		#region Public Constructors

		public EnemyExplosionParticle(Vector2f pos, Color color)
			: base(pos, color, particleDuration, particleSpeed)
		{
		}

		#endregion Public Constructors
	}
}