using NetEXT.Particles;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Base
{
	class BaseParticle : Particle
	{
		#region Public Constructors

		public BaseParticle(Vector2f pos, Color color, float duration, float speed)
			: base(Time.FromSeconds(duration))
		{
			float angle = Game.rnd.Next(0, 360);

			Rotation = angle;
			Position = pos;
			Color = color;
			Velocity = Common.MovePointByAngle(speed, angle);
		}

		public BaseParticle(Vector2f pos, Color color, float duration, float speed, float angle)
			: base(Time.FromSeconds(duration))
		{
			Rotation = angle;
			Position = pos;
			Color = color;
			Velocity = Common.MovePointByAngle(speed, angle);
		}

		#endregion Public Constructors
	}
}