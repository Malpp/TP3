﻿using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GeometryWars.Code.Enemies
{
	class Sniper : Enemy
	{
		#region Private Fields
		private const float fireSpeed = 1.5f;
		private const int pointsWorth = 10;
		private static Color color = new Color(0, 234, 27);
		private static Texture sniperChargingTexture = new Texture("Assets/Textures/sniperCharging.png");
		private static Texture sniperTexture = new Texture("Assets/Textures/sniper.png");
		private static Texture sniperTurreTexture = new Texture("Assets/Textures/sniperTurret.png");
		private bool canFire;
		private float fireDelta;
		private Sprite sniperChargingSprite;
		private Sprite sniperTurretSprite;
		#endregion Private Fields

		#region Public Constructors

		public Sniper(Vector2f pos)
			: base(pos, 0, 0, 0, sniperTexture, color)
		{
			sniperTurretSprite = new Sprite(sniperTurreTexture);
			sniperTurretSprite.Origin = new Vector2f(sniperTurreTexture.Size.X * 0.25f, sniperTurreTexture.Size.Y * 0.5f);
			sniperTurretSprite.Position = Pos;

			sniperChargingSprite = new Sprite(sniperChargingTexture);
			sniperChargingSprite.Origin = (Vector2f)sniperChargingTexture.Size * 0.5f;
			sniperChargingSprite.Position = Pos;
		}

		#endregion Public Constructors

		#region Public Methods

		public override void Draw(RenderTarget window)
		{
			base.Draw(window);

			//window.Draw(sniperChargingSprite);

			window.Draw(sniperTurretSprite);
		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			if (canFire)
			{
				canFire = false;
				sniperChargingSprite.Color -= new Color(0, 0, 0, 0);

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

			//sniperTurretSprite.Rotation = Common.AngleBetweenTwoPoints(Pos, Hero.GetInstance().Pos);

			sniperTurretSprite.Rotation = EstimateShootingAngle(timeDelta);

			base.Update(timeDelta, entities);
		}

		#endregion Public Methods

		#region Protected Methods

		protected override int AddScore()
		{
			return pointsWorth;
		}

		#endregion Protected Methods

		#region Private Methods

		private float EstimateShootingAngle(float timeDelta)
		{
			float framesUntilNormalHit = Common.DistanceBetweenTwoPoints(Pos, Hero.GetInstance().Pos) /
										 (Projectile.Speed * timeDelta);

			float heroDirection = Common.AngleBetweenTwoPoints(Hero.GetInstance().Pos, Hero.GetInstance().LastPos);

			float heroSpeed = Common.DistanceBetweenTwoPoints(Hero.GetInstance().Pos, Hero.GetInstance().LastPos);

			Vector2f estimatedPos = Hero.GetInstance().Pos +
									Common.MovePointByAngle(heroSpeed * framesUntilNormalHit, heroDirection + 180);

			return Common.AngleBetweenTwoPoints(Pos, estimatedPos);
		}

		#endregion Private Methods
	}
}