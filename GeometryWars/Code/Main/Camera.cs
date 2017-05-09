﻿using System;
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
		static View camara = new View(new Vector2f(), new Vector2f(1920, 1080));
		static Vector2f center = Game.GAME_SIZE * 0.5f;
        public static readonly Vector2f MaxCameraMovement = new Vector2f(Math.Abs((center.X - Game.GAME_X_LIMIT) * 0.2f), Math.Abs((center.Y - Game.GAME_Y_LIMIT) * 0.2f));
	    private static Vector2f newPos = center;

	    public static Vector2f Center
	    {
            get { return center; }
	    }

	    public static Vector2f Pos
	    {
            get { return newPos; }
	    }

		public static void Update(RenderTarget window, Vector2f pos)
		{

		    newPos = (center - pos) * 0.2f;
            camara.Center = center - newPos;
			window.SetView(camara);

		}

	}
}
