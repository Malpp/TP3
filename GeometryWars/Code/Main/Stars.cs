using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace GeometryWars.Code.Main
{
	static class Stars
	{
		#region Public Fields
		public const int maxStarCount = 2000;
		public const float starSize = 10f;
		public static readonly Vector2f MaxX = new Vector2f(0 - Camera.MaxCameraMovement.X, Game.GAME_X_LIMIT + Camera.MaxCameraMovement.X);
		public static readonly Vector2f MaxY = new Vector2f(0 - Camera.MaxCameraMovement.Y, Game.GAME_Y_LIMIT + Camera.MaxCameraMovement.Y);
		#endregion Public Fields

		#region Private Fields
		private static float angle;
		private static float shipSpeed;
		private static Vertex[] stars = new Vertex[maxStarCount];
		private static float[] starSpeeds = new float[maxStarCount];
		private static Texture starTexture = new Texture("Assets/Textures/star.png");
		#endregion Private Fields

		#region Public Constructors

		static Stars()
		{
			for (int i = 0; i < maxStarCount; i++)
			{
				float starSpeed = Game.rnd.Next(5, 200);
				float xPos = Game.rnd.Next((int)Stars.MaxX.X, (int)Stars.MaxX.Y);
				float yPos = Game.rnd.Next((int)Stars.MaxY.X, (int)Stars.MaxY.Y);
				Vector2f randomPos = new Vector2f(xPos, yPos);
				stars[i] = new Vertex(randomPos, new Color(255, 255, 255, (byte)((55 + starSpeed) / 200 * 255)));
				starSpeeds[i] = starSpeed;
			}
		}

		#endregion Public Constructors

		#region Public Methods

		public static void Draw(RenderTarget window)
		{
			window.Draw(stars, PrimitiveType.Points);
		}

		public static void Update(float timeDelta)
		{
			Hero hero = Hero.GetInstance();
			if (hero.Pos != hero.LastPos)
			{
				angle = Common.AngleBetweenTwoPoints(
					new Vector2f(),
					hero.Pos - hero.LastPos
				);
				shipSpeed = Common.DistanceBetweenTwoPoints(
								hero.Pos,
								hero.LastPos
							) / (Hero.Speed * timeDelta);

				Vector2f newPos = Common.MovePointByAngle(shipSpeed, angle + 180) * timeDelta;

				Parallel.For(0, maxStarCount, i =>
				{
					stars[i].Position += newPos * starSpeeds[i];

					if (stars[i].Position.X < MaxX.X)
					{
						stars[i].Position.X = MaxX.Y;
					}
					else if (stars[i].Position.X > MaxX.Y)
					{
						stars[i].Position.X = MaxX.X;
					}
					else if (stars[i].Position.Y < MaxY.X)
					{
						stars[i].Position.Y = MaxY.Y;
					}
					else if (stars[i].Position.Y > MaxY.Y)
					{
						stars[i].Position.Y = MaxY.X;
					}
				});
			}
		}

		#endregion Public Methods
	}
}