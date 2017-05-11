using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Projectiles
{
	class HeroProjectile : Projectile
	{
		#region Private Fields
		private static Color color = new Color(255, 216, 0);
		private static Texture projectileTexture = new Texture("Assets/Textures/heroProjectile.png");
		#endregion Private Fields

		#region Public Constructors

		public HeroProjectile(Vector2f pos, float angle)
			: base(pos, angle, projectileTexture, color)
		{
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override void HandleCollision(Drawable entity)
		{
			if (!ToDelete)
			{
				Delete();
				entity.Delete();
				Hero.GetInstance().AddEnemyKill();
			}
		}

		protected override void HandleEdge()
		{
			base.HandleEdge();
		}

		#endregion Protected Methods
	}
}