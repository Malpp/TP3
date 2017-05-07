using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Particles;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Base
{
	abstract class BaseEmitter : EmitterBase
	{

		private Vector2f pos;
		private int particleCount;
		private float _emitterDuration;
		private Color color;

		public float EmitterDuration
		{
			get { return _emitterDuration; }
		}

		public BaseEmitter(Vector2f pos, int count, float emitterDuration, Color particaleColor)
		{
			this.pos = pos;
			particleCount = count;
			_emitterDuration = emitterDuration;
			color = particaleColor;
		}

		public override void EmitParticles(ParticleSystem ParticleSystem, Time DeltaTime)
		{
			for (int i = 0; i < particleCount; i++)
			{
				EmitParticle(ParticleSystem, ParticleToAdd(pos, color));
			}
		}

		public abstract BaseParticle ParticleToAdd(Vector2f pos, Color color);

	}
}
