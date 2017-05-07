using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Main
{
	class Star : Drawable
	{

		public const int maxStarCount = 500;
		private static Texture starTexture = new Texture("Assets/Textures/star.png");
		private Vector2f pos;
		private float starSpeed;

		public static Vector2f MaxX
		{
			get { return new Vector2f(0 - Camera.MaxCameraMovement.X, Game.GAME_X_LIMIT + Camera.MaxCameraMovement.X); }
		}

		public static Vector2f MaxY
		{
			get { return new Vector2f(0 - Camera.MaxCameraMovement.Y, Game.GAME_Y_LIMIT + Camera.MaxCameraMovement.Y); }
		}

		public Star(Vector2f pos)
			: base(pos, 0, starTexture)
		{
			this.pos = pos;
			starSpeed = Game.rnd.Next(5, 200);
			sprite.Color -= new Color(0,0,0, (byte)((200 - starSpeed) / 200 * 255));
		}

		public void Update(float timeDelta)
		{

			Hero hero = Hero.GetInstance();

			float angle = Common.AngleBetweenTwoPoints(
				new Vector2f(),
				hero.Pos - hero.LastPos
			);
			float shipSpeed = Common.DistanceBetweenTwoPoints(
				                  hero.Pos,
				                  hero.LastPos
			                  ) / (Hero.Speed * timeDelta);
						

			pos += Common.MovePointByAngle(starSpeed * shipSpeed, angle + 180) * timeDelta;

			if (pos.X < MaxX.X)
			{
				pos.X = MaxX.Y;
			}
			else if (pos.X > MaxX.Y)
			{
				pos.X = MaxX.X;
			}
			else if (pos.Y < MaxY.X)
			{
				pos.Y = MaxY.Y;
			}
			else if (pos.Y > MaxY.Y)
			{
				pos.Y = MaxY.X;
			}

			sprite.Position = pos;

		}

	}
}
