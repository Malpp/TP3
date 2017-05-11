using GeometryWars.Code.Base;
using GeometryWars.Code.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Emiters
{
	class BombEmitter : BaseEmitter
	{
		#region Private Fields
		private const int bombEmitterCount = 300;
		private const float bombEmitterDuration = 0.075f;
		#endregion Private Fields

		#region Public Constructors

		public BombEmitter(Vector2f pos, Color color)
			: base(pos, bombEmitterCount, bombEmitterDuration, color)
		{
		}

		#endregion Public Constructors

		#region Public Methods

		public override BaseParticle ParticleToAdd(Vector2f pos, Color color)
		{
			return new BombParticle(pos, color);
		}

		#endregion Public Methods
	}
}