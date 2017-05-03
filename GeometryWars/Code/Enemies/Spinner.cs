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
		private const float spinnerSpeed = 200;
		private float spinnerRotation;
		private const float spinnerRotationSpeed = 0.15f;
		private Sprite spinnerSprite;

		public Spinner(float x, float y)
			: base(x, y, spinnerSpeed, spinnerTexture)
		{
			
			sprite.Color = Color.Transparent;
			spinnerSprite = new Sprite(spinnerTexture);
			spinnerSprite.Origin = (Vector2f)spinnerTexture.Size * 0.5f;
			spinnerSprite.Position = Pos;

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			spinnerRotation += spinnerRotationSpeed;

			spinnerSprite.Position = Pos;
			spinnerSprite.Rotation = spinnerRotation;

			base.Update(deltaTime, entities);

		}

		public override void Draw(RenderTarget window)
		{

			window.Draw(spinnerSprite);

			base.Draw(window);
		}
	}
}
