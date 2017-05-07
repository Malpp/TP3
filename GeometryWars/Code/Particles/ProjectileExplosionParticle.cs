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
	class ProjectileExplosionParticle : Particle
	{
		private const float particleSpeed = 500f;
		private const float particleDuration = 0.1f;

		public ProjectileExplosionParticle(Vector2f pos)
			: base(Time.FromSeconds(particleDuration))
		{

			float angle = Game.rnd.Next(0, 360);

			Rotation = angle;
			Position = pos;
			Color = Color.Cyan;
			Velocity = Common.MovePointByAngle(particleSpeed, angle);

		}

	}
}
