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
	    private static float xMin = Game.ParticleTexture.Size.X + Game.BORDER_SIZE;
	    private static float yMin = Game.ParticleTexture.Size.X + Game.BORDER_SIZE;

	    private static float xMax = Game.GAME_X_LIMIT - Game.BORDER_SIZE;
	    private static float yMax = Game.GAME_Y_LIMIT - Game.BORDER_SIZE;

        public void ApplyAffector(Particle particle, Time timeDelta)
		{

            bool modX = false;
            bool modY = false;

            if (particle.Position.X < xMin ||
                particle.Position.X > xMax)
            {
                modX = true;
            }
            if (particle.Position.Y <  yMin ||
                particle.Position.Y > yMax)
            {
                modY = true;
            }

            if (modY || modX)
            {
                particle.Velocity = new Vector2f(modX ? -particle.Velocity.X : particle.Velocity.X, modY ? -particle.Velocity.Y : particle.Velocity.Y);
                particle.Position = new Vector2f(
                Math.Max(
                    xMin,
                    Math.Min(
                        xMax,
                        particle.Position.X)),
                Math.Max(
                    yMin,
                    Math.Min(
                        yMax,
                        particle.Position.Y))
                );
                particle.Rotation = Common.AngleBetweenTwoPoints(new Vector2f(), particle.Velocity);
            }

		    particle.Velocity *= 0.985f;

			particle.Color = new Color(particle.Color.R, particle.Color.G, particle.Color.B, (byte)(50f + 206f * particle.RemainingRatio));

		}

	}
}
