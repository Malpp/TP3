using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GeometryWars.Code.Enemies
{
	class Spinner : FollowerEnemy
	{
		#region Private Fields
		private const int pointsWorth = 10;
		private const float spinnerAngleSpeed = 400f;
		private const float spinnerRotationSpeed = 400f;
		private const float spinnerSpeed = 400f;
		private const float spinnerSpeedSpeedUpgrade = 50f;
		private static Color color = new Color(229, 0, 38);
		private static Texture spinnerTexture = new Texture("Assets/Textures/spinner.png");
		private float spinnerRotation;
		private Sprite spinnerSprite;
		#endregion Private Fields

		#region Public Constructors

		public Spinner(Vector2f pos, float initAngle)
			: base(pos, initAngle, spinnerSpeed, spinnerAngleSpeed, spinnerTexture, color)
		{
			sprite.Color = Color.Transparent;
			spinnerSprite = new Sprite(spinnerTexture);
			spinnerSprite.Origin = (Vector2f)spinnerTexture.Size * 0.5f;
			spinnerSprite.Position = Pos;
		}

		#endregion Public Constructors

		#region Public Methods

		public override void Draw(RenderTarget window)
		{
			window.Draw(spinnerSprite);

			base.Draw(window);
		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{
			MoveSpeed += spinnerSpeedSpeedUpgrade * timeDelta;

			spinnerRotation += spinnerRotationSpeed * timeDelta;

			spinnerSprite.Position = Pos;
			spinnerSprite.Rotation = spinnerRotation;

			base.Update(timeDelta, entities);
		}

		#endregion Public Methods

		#region Protected Methods

		protected override int AddScore()
		{
			return pointsWorth;
		}

		#endregion Protected Methods
	}
}