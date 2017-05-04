using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class Sniper : Enemy
	{

		private static Texture sniperTexture = new Texture("Assets/Textures/sniper.png");
		private static Texture sniperTurreTexture = new Texture("Assets/Textures/sniperTurret.png");
		static Texture sniperChargingTexture = new Texture("Assets/Textures/sniperCharging.png");

		private Sprite sniperTurretSprite;
		private Sprite sniperChargingSprite;

		private const float fireSpeed = 1.5f;
		private float fireDelta;
		private bool canFire;

		private List<EnemyProjectile> projectiles;

		public Sniper(float x, float y)
			: base(x, y, 0, sniperTexture)
		{
			
			sniperTurretSprite = new Sprite(sniperTurreTexture);
			sniperTurretSprite.Origin = new Vector2f(sniperTurreTexture.Size.X * 0.25f, sniperTurreTexture.Size.Y * 0.5f);
			sniperTurretSprite.Position = new Vector2f(x,y);

			sniperChargingSprite = new Sprite(sniperChargingTexture);
			sniperChargingSprite.Origin = (Vector2f)sniperChargingTexture.Size * 0.5f;
			sniperChargingSprite.Position = Pos;

			projectiles = new List<EnemyProjectile>();

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			if (canFire)
			{

				canFire = false;
				sniperChargingSprite.Color -= new Color(0,0,0,0);

				projectiles.Add(new EnemyProjectile(Pos, sniperTurretSprite.Rotation));

			}

			if (!canFire)
			{

				fireDelta += deltaTime;

				sniperChargingSprite.Color = new Color(
					sniperChargingSprite.Color.R, 
					sniperChargingSprite.Color.B, 
					sniperChargingSprite.Color.G, 
					(byte)(fireDelta * 255 / fireSpeed));

				if (fireDelta > fireSpeed)
				{

					fireDelta = 0;
					canFire = true;

				}

			}

			sniperTurretSprite.Rotation = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);

			foreach (EnemyProjectile enemyProjectile in projectiles)
			{
				enemyProjectile.Update(deltaTime, new []{Hero.GetInstance()});
			}

			projectiles.RemoveAll(x => x.ToDelete);

			base.Update(deltaTime, entities);
		}

		public override void Draw(RenderTarget window)
		{
			base.Draw(window);

			window.Draw(sniperChargingSprite);

			window.Draw(sniperTurretSprite);

			foreach (EnemyProjectile enemyProjectile in projectiles)
			{
				enemyProjectile.Draw(window);
			}

		}
	}
}
