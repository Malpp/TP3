﻿using System;
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

		public Sniper(Vector2f pos)
			: base(pos, 0, 0, 0, sniperTexture)
		{
			
			sniperTurretSprite = new Sprite(sniperTurreTexture);
			sniperTurretSprite.Origin = new Vector2f(sniperTurreTexture.Size.X * 0.25f, sniperTurreTexture.Size.Y * 0.5f);
			sniperTurretSprite.Position = Pos;

			sniperChargingSprite = new Sprite(sniperChargingTexture);
			sniperChargingSprite.Origin = (Vector2f)sniperChargingTexture.Size * 0.5f;
			sniperChargingSprite.Position = Pos;

		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			if (canFire)
			{

				canFire = false;
				sniperChargingSprite.Color -= new Color(0,0,0,0);

				EntityManager.AddEnemyProjectile(new EnemyProjectile(Pos, sniperTurretSprite.Rotation));

			}

			if (!canFire)
			{

				fireDelta += timeDelta;

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

			base.Update(timeDelta, entities);
		}

		public override void Draw(RenderTarget window)
		{
			base.Draw(window);

			window.Draw(sniperChargingSprite);

			window.Draw(sniperTurretSprite);

		}
	}
}