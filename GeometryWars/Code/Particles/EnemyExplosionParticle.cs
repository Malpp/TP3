using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Particles
{
	class EnemyExplosionParticle : Particle
	{
		private const float particleSpeed = 500f;
		private const float particleDuration = 0.2f;

		public EnemyExplosionParticle(Vector2f pos, Color color)
			: base(Time.FromSeconds(particleDuration))
		{

			float angle = Game.rnd.Next(0, 360);

			Rotation = angle;
			Position = pos;
			Color = color;
			Velocity = Common.MovePointByAngle(particleSpeed, angle);

		}

	}
}
