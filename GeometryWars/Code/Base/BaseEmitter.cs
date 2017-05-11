using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Base
{
	abstract class BaseEmitter : EmitterBase
	{
		#region Private Fields
		private float _emitterDuration;
		private Color color;
		private int particleCount;
		private Vector2f pos;
		#endregion Private Fields

		#region Public Constructors

		public BaseEmitter(Vector2f pos, int count, float emitterDuration, Color particaleColor)
		{
			this.pos = pos;
			particleCount = count;
			if (emitterDuration > 0)
				_emitterDuration = emitterDuration;
			color = particaleColor;
		}

		#endregion Public Constructors

		#region Public Properties

		public float EmitterDuration
		{
			get { return _emitterDuration; }
		}

		#endregion Public Properties

		#region Public Methods

		public override void EmitParticles(ParticleSystem ParticleSystem, Time DeltaTime)
		{
			PreUpdate(DeltaTime.AsSeconds());

			for (int i = 0; i < particleCount; i++)
			{
				EmitParticle(ParticleSystem, ParticleToAdd(pos, color));
			}
		}

		public abstract BaseParticle ParticleToAdd(Vector2f pos, Color color);

		#endregion Public Methods

		#region Protected Methods

		protected virtual void PreUpdate(float timeDelta)
		{
		}

		#endregion Protected Methods
	}
}