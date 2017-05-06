using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace GeometryWars.Code.Main
{

	enum ControllerType
	{
		GameCube = 48813,
		XboxWireless = 673
	}

	static class Controller
	{

		public const int deadzone = 10;

		private static uint selectedController = 1000;
		private static int controllerId = -1;

		public static bool IsConnected
		{
			get { return selectedController != 1000; }
		}

		public static bool MoveIsNotCentered
		{
			get
			{

				Vector2f axis = GetMoveAxis();

				if (IsConnected &&
					axis.X > deadzone ||
					axis.X < -deadzone ||
					axis.Y > deadzone ||
					axis.Y < -deadzone)
				{
					return true;
				}
				return false;
			}
		}

		public static bool FireIsNotCentered
		{
			get
			{

				Vector2f axis = GetShootAxis();

				if (IsConnected &&
					axis.X > deadzone ||
					axis.X < -deadzone ||
					axis.Y > deadzone ||
					axis.Y < -deadzone)
				{
					return true;
				}
				return false;
			}
		}

		public static void SetSelectedController()
		{

			if (selectedController == 1000)
			{

				for (uint i = 0; i < Joystick.Count; i++)
				{

					if (Joystick.IsConnected(i))
					{
						for (uint j = 0; j < Joystick.GetButtonCount(i); j++)
						{
							if (Joystick.IsButtonPressed(i, j) &&
								Joystick.HasAxis(i, Joystick.Axis.X) &&
								Joystick.HasAxis(i, Joystick.Axis.Y))
							{
								selectedController = i;
								controllerId = (int)Joystick.GetIdentification(i).ProductId;
								Console.WriteLine(controllerId);
							}
						}
					}

				}

			}

		}

		public static uint GetSelectedController()
		{

			return selectedController;

		}

		public static Vector2f GetMoveAxis()
		{

			if (selectedController != 1000)
			{

				float xAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.X), 2);
				float yAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.Y), 2);

				Vector2f movePos = new Vector2f(xAxis, yAxis);

				if (controllerId == (uint)ControllerType.GameCube)
				{
					movePos = movePos * 100 / 70;
					movePos = new Vector2f(Math.Min(100, Math.Max(-100, movePos.X)), Math.Min(100, Math.Max(-100, movePos.Y)));
				}

				return movePos;

			}

			return new Vector2f();

		}

		public static Vector2f GetShootAxis()
		{

			if (selectedController != 1000)
			{

				float xAxis;
				float yAxis;

				if (controllerId == (uint)ControllerType.GameCube)
				{
					xAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.V), 2);
					yAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.U), 2);
				}
				else
				{
					xAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.U), 2);
					yAxis = (float)Math.Round(Joystick.GetAxisPosition(selectedController, Joystick.Axis.R), 2);
				}


				return new Vector2f(xAxis, yAxis);

			}

			return new Vector2f();

		}

		public static void Update()
		{

			Joystick.Update();

			if (Keyboard.IsKeyPressed(Keyboard.Key.P))
			{

				selectedController = 1000;
				controllerId = -1;

			}

			if (!IsConnected)
			{
				SetSelectedController();
			}

		}

	}
}
