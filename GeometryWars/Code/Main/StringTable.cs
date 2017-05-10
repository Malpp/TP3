using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using Keyboard = SFML.Window.Keyboard;

namespace GeometryWars.Code.Main
{
	public class StringTable
	{
		private static StringTable stringTable;
		private string[] locales = new[] {"en", "fr"};
		private int currentLocal = 0;
		public const int NoOfLocales = 2;
		Dictionary<string, string[]> words = new Dictionary<string, string[]>();

		private StringTable()
		{
			
		}

		public static StringTable GetInstance()
		{
			if(stringTable == null)
				stringTable = new StringTable();
			return stringTable;
		}

		public string Locale
		{
			get { return locales[currentLocal]; }
		}

		public void LoadFromTextFile(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException();
			else
			{

				string text = File.ReadAllText(path);
				LoadFromString(text);

			}
		}

		public void LoadFromString(string loadString)
		{
			if(loadString.Length == 0)
				throw new ArgumentOutOfRangeException();

			string[] lines = loadString.Split('\n');

			for (int i = 0; i < lines.Length; i++)
			{
				string[] dict = lines[i].Split(new[] {'=','>'}, StringSplitOptions.RemoveEmptyEntries);
				if(dict.Length != 2)
					throw new IndexOutOfRangeException("Can't find keys and values");

				string[] values = dict[1].Split(new[] { '~', '~', '~' }, StringSplitOptions.RemoveEmptyEntries);

				if (values.Length != NoOfLocales)
					throw new IndexOutOfRangeException("invalid number of values");

				for (int j = 0; j < values.Length; j++)
				{
					values[j] = values[j].Replace("\r", "");
				}

				words.Add(dict[0], values);

			}

		}

		public string GetWord(string key)
		{
			if (words.ContainsKey(key))
			{
				return words[key][currentLocal];
			}
			return "";
		}

		public void ClearWords()
		{
			words.Clear();
		}

		public void NextLocale()
		{
			currentLocal++;
			if (currentLocal >= NoOfLocales)
			{
				currentLocal = 0;
			}
		}

		public void Update()
		{
			if (Keyboard.IsKeyPressed(Keyboard.Key.F4))
			{
				currentLocal = 1;
			}

			if (Keyboard.IsKeyPressed(Keyboard.Key.F5))
			{
				currentLocal = 0;
			}

			EntityManager.ForceUpdateText();

		}

	}
}
