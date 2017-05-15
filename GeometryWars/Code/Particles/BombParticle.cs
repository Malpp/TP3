using GeometryWars.Code.Base;
using GeometryWars.Code.Main;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Particles
{
	class BombParticle : BaseParticle
	{
		#region Private Fields
		private const float bombParticleSpeed = 4000f;
		private const int randomElement = (int)(bombParticleSpeed * 0.2f);
		#endregion Private Fields

		#region Public Constructors

		public BombParticle(Vector2f pos, Color color)
			: base(pos, color, Bomb.BombDuration, bombParticleSpeed + Game.rnd.Next(-randomElement, randomElement))
		{
		}

		#endregion Public Constructors
	}
}