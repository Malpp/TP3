using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars
{
	public static class Common
	{

		//Pierre, regarde pas cette class
		//Elle est par defaut dans mon template SFML

		/// <summary>
		/// Calcul de la distance entre deux points
		/// </summary>
		/// <param name="point1">Le point 1</param>
		/// <param name="point2">Le point 2</param>
		/// <returns>
		/// Retourne la distance entre les deux points
		/// </returns>
		public static float DistanceBetweenTwoPoints(Vector2f point1, Vector2f point2)
		{

			return Convert.ToSingle(Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2)));

		}

		public static Vector2f MovePointByAngle(float moveBy, float angle)
		{
			return new Vector2f((float)Math.Cos(Common.DegreeToRadian(angle)) * moveBy, (float)Math.Sin(Common.DegreeToRadian(angle)) * moveBy);
		}

		public static float AngleBetweenTwoPoints(Vector2f p1, Vector2f p2)
		{
			return (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180f / (float)Math.PI;
		}

		/// <summary>
		/// Convertit un angle en radian vers un angle en degré
		/// </summary>
		/// <param name="radian">L'angle en radian</param>
		/// <returns>Retourne l'angle convertit</returns>
		public static float RadianToDegree(float radian)
		{

			return 180.0f * radian / (float)Math.PI;

		}

		/// <summary>
		/// Convertit un angle en degré vers un angle en randian
		/// </summary>
		/// <param name="degree">L'angle en degré</param>
		/// <returns>Retourne l'angle convertit</returns>
		public static float DegreeToRadian(float degree)
		{

			return (float)Math.PI * degree / 180.0f;

		}

	}
}
