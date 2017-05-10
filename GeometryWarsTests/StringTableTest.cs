using System;
using System.IO;
using GeometryWars.Code.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometryWarsTests
{
	[TestClass]
	public class StringTableTest
	{
		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void LoadFromFileFail()
		{

			StringTable.GetInstance().LoadFromTextFile("PATH");

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmptyString()
		{

			const string stringToTest = "";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Junk()
		{

			const string stringToTest = "asdwafeafwe\nwefrawefefsefrawerawefasefsea\nafefeafesfaef34123412312=a/;p3,41i412n4ui120-if890ajg8iasug8a9";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void MissingKey()
		{

			const string stringToTest = 
			"scoreScore:~~~Pointages:\n" +
			"lifeLife:~~~Vies:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void WrongKey()
		{

			const string stringToTest =
				"score|Score:~~~Pointages:\n" +
				"life-)Life:~~~Vies:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void MissingValueSplitter()
		{

			const string stringToTest =
				"score=>Score:Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void WrongValueSplitter()
		{

			const string stringToTest =
				"score=>Score:~~Pointages:\n" +
				"life=>Life:~|~Vies:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void TooLittleLocales()
		{

			const string stringToTest =
				"score=>Score:\n" +
				"life=>Life:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void TooManyLocales()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:~~~übereinstimmen:\n" +
				"life=>Life:~~~Vies:~~~Leben:";

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void Correct()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			Assert.AreEqual("en", StringTable.GetInstance().Locale);
			Assert.AreEqual("Score:",StringTable.GetInstance().GetWord("score"));
			Assert.AreEqual("Life:", StringTable.GetInstance().GetWord("life"));

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void SwitchLocale()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			Assert.AreEqual("en", StringTable.GetInstance().Locale);
			StringTable.GetInstance().NextLocale();
			Assert.AreEqual("fr", StringTable.GetInstance().Locale);
			StringTable.GetInstance().NextLocale();
			Assert.AreEqual("en", StringTable.GetInstance().Locale);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void NextLocaleText()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			StringTable.GetInstance().NextLocale();
			Assert.AreEqual("fr", StringTable.GetInstance().Locale);
			Assert.AreEqual("Pointages:", StringTable.GetInstance().GetWord("score"));
			Assert.AreEqual("Vies:", StringTable.GetInstance().GetWord("life"));
			StringTable.GetInstance().NextLocale();
			Assert.AreEqual("en", StringTable.GetInstance().Locale);

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void GetInvalidKey1()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			Assert.AreEqual("", StringTable.GetInstance().GetWord("sdwfwfa"));
			Assert.AreEqual("", StringTable.GetInstance().GetWord(""));

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void GetInvalidKey2()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			Assert.AreEqual("", StringTable.GetInstance().GetWord(""));

			StringTable.GetInstance().ClearWords();

		}

		[TestMethod]
		public void GetInvalidKey3()
		{

			const string stringToTest =
				"score=>Score:~~~Pointages:\n" +
				"life=>Life:~~~Vies:";

			StringTable.GetInstance().ClearWords();

			StringTable.GetInstance().LoadFromString(stringToTest);

			Assert.AreEqual("", StringTable.GetInstance().GetWord(stringToTest));

			StringTable.GetInstance().ClearWords();

		}

	}
}
