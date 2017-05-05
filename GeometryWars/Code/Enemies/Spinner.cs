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
		private const float spinnerSpeed = 200f;
		private const float spinnerAngleSpeed = 200f;
		private float spinnerRotation;
		private const float spinnerRotationSpeed = 400f;
		private Sprite spinnerSprite;

		public Spinner(Vector2f pos, float initAngle)
			: base(pos, initAngle, spinnerSpeed, spinnerAngleSpeed, spinnerTexture)
		{
			
			sprite.Color = Color.Transparent;
			spinnerSprite = new Sprite(spinnerTexture);
			spinnerSprite.Origin = (Vector2f)spinnerTexture.Size * 0.5f;
			spinnerSprite.Position = Pos;

		}

		public override void Update(float timeDelta, IEnumerable<Drawable> entities = null)
		{

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
	}
}
