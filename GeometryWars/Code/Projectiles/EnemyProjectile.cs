using GeometryWars.Code.Main;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code
{
	class EnemyProjectile : Projectile
	{
		#region Private Fields
		private static Color color = new Color(83, 172, 255);
		private static int damage = 1;
		private static SoundBuffer hitSound = new SoundBuffer("Assets/SFX/Player_shielded_hit_enemy.wav");
		private static Texture projectileTexture = new Texture("Assets/Textures/enemyProjectile.png");
		#endregion Private Fields

		#region Public Constructors

		public EnemyProjectile(Vector2f pos, float angle)
			: base(pos, angle, projectileTexture, color)
		{
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override void HandleCollision(Drawable entity)
		{
			Delete();
			Game.PlaySound(hitSound);
			//Do this l8ter or something
			Hero.GetInstance().TakeDamage(1);
			ScoreManager.ResetMulti();
		}

		#endregion Protected Methods
	}
}