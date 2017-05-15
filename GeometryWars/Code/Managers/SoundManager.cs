using SFML.Audio;
using System.Collections.Generic;

namespace GeometryWars.Code.Main
{
	static class SoundManager
	{
		#region Private Fields
		private static Queue<Sound> sounds = new Queue<Sound>(1000);
		private static float volume = 10;
		#endregion Private Fields

		#region Public Methods

		public static void AddSound(SoundBuffer sound)
		{
			Sound newSound = new Sound(sound);
			newSound.Volume = volume;
			newSound.Play();

			sounds.Enqueue(newSound);
		}

		public static void Update()
		{
			if (sounds.Count > 0 && sounds.Peek().Status == SoundStatus.Stopped)
			{
				sounds.Dequeue().Dispose();
			}
		}

		#endregion Public Methods
	}
}