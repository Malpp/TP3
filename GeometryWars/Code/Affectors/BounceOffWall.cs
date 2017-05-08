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
				(particle.Position.X < Game.ParticleTexture.Size.X + Game.BORDER_SIZE || particle.Position.X > Game.GAME_X_LIMIT + Game.ParticleTexture.Size.X + Game.BORDER_SIZE) ? -particle.Velocity.X : particle.Velocity.X,
				(particle.Position.Y < Game.ParticleTexture.Size.X + Game.BORDER_SIZE || particle.Position.Y > Game.GAME_Y_LIMIT + Game.ParticleTexture.Size.X + Game.BORDER_SIZE) ? -particle.Velocity.Y : particle.Velocity.Y
				);

            particle.Position = new Vector2f(
                Math.Max(
                    Game.ParticleTexture.Size.X + Game.BORDER_SIZE,
                    Math.Min(
                        Game.GAME_X_LIMIT - Game.ParticleTexture.Size.X - Game.BORDER_SIZE,
                        particle.Position.X)),
                Math.Max(
                    Game.ParticleTexture.Size.X + Game.BORDER_SIZE,
                    Math.Min(
                        Game.GAME_Y_LIMIT - Game.ParticleTexture.Size.X - Game.BORDER_SIZE,
                        particle.Position.Y))
                );

			particle.Rotation = Common.AngleBetweenTwoPoints(new Vector2f(), particle.Velocity);

			particle.Color = new Color(particle.Color.R, particle.Color.G, particle.Color.B, (byte)(50f + 206f * particle.RemainingRatio));

		}

	}
}
