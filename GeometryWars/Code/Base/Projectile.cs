using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Emmiters;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	abstract class Projectile : Movable
	{

		private static float projectileSpeed = 1000f;
		private Color color;
		static SoundBuffer hitWallSound = new SoundBuffer("Assets/SFX/Projectile_hit_wall.wav");

		public static float Speed
		{
			get { return projectileSpeed; }
		}

		public Projectile(Vector2f pos, float angle, Texture texture, Color color)
			: base(pos, angle, texture)
		{
			this.color = color;
		}

		protected override Vector2f GetNextMove(float timeDelta)
		{
			return Common.MovePointByAngle(projectileSpeed, Angle);
		}

		protected override void HandleEdge()
		{

			Game.PlaySound(hitWallSound);

			Delete();

		}

		public override void Delete()
		{

			EntityManager.AddEmitter(new ProjectileExplosionEmitter(Pos, 1, color));

			base.Delete();
		}
	}
}
