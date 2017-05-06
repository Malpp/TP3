using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Main
{
	static class Camera
	{
		private const float cameraSize = 1200;
		static View camara = new View(new Vector2f(), new Vector2f(cameraSize, cameraSize));
		static Vector2f center = Game.GAME_SIZE * 0.5f;

		public static void Update(RenderTarget window, Vector2f pos)
		{

			camara.Center = center - (center - pos) * 0.2f;
			window.SetView(camara);

		}

	}
}
