using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Enemies
{
	class Spinner : FollowerEnemy
	{

		private static Texture spinnerTexture = new Texture("Assets/Textures/spinner.png");
		private const float spinnerSpeed = 400f;
		private const float spinnerSpeedSpeedUpgrade = 50f;
		private const float spinnerAngleSpeed = 400f;
		private float spinnerRotation;
		private const float spinnerRotationSpeed = 400f;
		private Sprite spinnerSprite;
		private static Color color = new Color(229, 0, 38);
	    private const int pointsWorth = 10;

		public Spinner(Vector2f pos, float initAngle)
			: base(pos, initAngle, spinnerSpeed, spinnerAngleSpeed, spinnerTexture, color)
		{
			
			sprite.Color = Color.Transparent;
			spinnerSprite = new Sprite(spinnerTexture);
			spinnerSprite.Origin = (Vector2f)spinnerTexture.Size * 0.5f;
			spinnerSprite.Position = Pos;

		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

			MoveSpeed += spinnerSpeedSpeedUpgrade * timeDelta;

			spinnerRotation += spinnerRotationSpeed * timeDelta;

			spinnerSprite.Position = Pos;
			spinnerSprite.Rotation = spinnerRotation;

			base.Update(timeDelta, entities);

		}

		public override void Draw(RenderTarget window)
		{

			window.Draw(spinnerSprite);

			base.Draw(window);
		}

	    protected override int AddScore()
	    {
	        return pointsWorth;
	    }
	}
}
