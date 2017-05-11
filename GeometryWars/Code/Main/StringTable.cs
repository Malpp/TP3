using System;
using System.Collections.Generic;
using System.IO;
using Keyboard = SFML.Window.Keyboard;

namespace GeometryWars.Code.Main
{
	public class StringTable
	{
		#region Public Fields
		/// <summary>
		/// The number of locales
		/// </summary>
		public const int NoOfLocales = 2;
		#endregion Public Fields

		#region Private Fields
		private static StringTable stringTable;
		/// <summary>
		/// The current locale
		/// </summary>
		private int currentLocal = 0;
		/// <summary>
		/// The locales
		/// </summary>
		private string[] locales = new[] { "en", "fr" };
		/// <summary>
		/// The words
		/// </summary>
		private Dictionary<string, string[]> words = new Dictionary<string, string[]>();
		#endregion Private Fields

		#region Private Constructors

		private StringTable()
		{
		}

		#endregion Private Constructors

		#region Public Properties

		/// <summary>
		/// Gets the locale.
		/// </summary>
		/// <value>
		/// The locale.
		/// </value>
		public string Locale
		{
			get { return locales[currentLocal]; }
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns></returns>
		public static StringTable GetInstance()
		{
			if (stringTable == null)
				stringTable = new StringTable();
			return stringTable;
		}

		/// <summary>
		/// Clears the words.
		/// </summary>
		public void ClearWords()
		{
			words.Clear();
		}

		/// <summary>
		/// Gets the word.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public string GetWord(string key)
		{
			if (words.ContainsKey(key))
			{
				return words[key][currentLocal];
			}
			return "";
		}

		/// <summary>
		/// Loads from string.
		/// </summary>
		/// <param name="loadString">The load string.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		/// <exception cref="System.IndexOutOfRangeException">
		/// Can't find keys and values
		/// or
		/// invalid number of values
		/// </exception>
		public void LoadFromString(string loadString)
		{
			if (loadString.Length == 0)
				throw new ArgumentOutOfRangeException();

			string[] lines = loadString.Split('\n');

			for (int i = 0; i < lines.Length; i++)
			{
				string[] dict = lines[i].Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
				if (dict.Length != 2)
					throw new IndexOutOfRangeException("Can't find keys and values");

				string[] values = dict[1].Split(new[] { "~~~" }, StringSplitOptions.RemoveEmptyEntries);

				if (values.Length != NoOfLocales)
					throw new IndexOutOfRangeException("invalid number of values");

				for (int j = 0; j < values.Length; j++)
				{
					values[j] = values[j].Replace("\r", "");
				}

				words.Add(dict[0], values);
			}
		}

		/// <summary>
		/// Loads from text file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
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

		/// <summary>
		/// Nexts the locale.
		/// </summary>
		public void NextLocale()
		{
			currentLocal++;
			if (currentLocal >= NoOfLocales)
			{
				currentLocal = 0;
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
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

		#endregion Public Methods
	}
}