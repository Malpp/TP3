using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Affectors
{
	class BounceOffWall : IAffector
	{

		public void ApplyAffector(Particle particle, Time timeDelta)
		{

			particle.Velocity = new Vector2f(
				(particle.Position.X < 0 || particle.Position.X > Game.GAME_X_LIMIT) ? -particle.Velocity.X : particle.Velocity.X,
				(particle.Position.Y < 0 || particle.Position.Y > Game.GAME_Y_LIMIT) ? -particle.Velocity.Y : particle.Velocity.Y
				);

			particle.Rotation = Common.AngleBetweenTwoPoints(new Vector2f(), particle.Velocity);

			//particle.Color = new Color(particle.Color.R, particle.Color.G, particle.Color.B, (byte)(256f * particle.RemainingRatio));

		}

	}
}
