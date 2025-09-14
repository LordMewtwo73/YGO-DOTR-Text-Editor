using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DOTR_Text_Editor
{
	public static class TextCompressor
	{
		private static List<string> modStrings;
		private static List<Pointer2> PointerList;

		private static Dictionary<char, string> ReplacedStrings = new Dictionary<char, string>();
		private static Dictionary<char, Pointer2> PointerDictionary = new Dictionary<char, Pointer2>();

		private static List<int> Offsets;

		public static int IndexCompleted;
		public static List<byte> OffsetBytes;
		public static List<byte> StringBytes;


		public static void Initialize(List<string> allStrings)
		{
			PointerList = new List<Pointer2>();
			modStrings = new List<string>();

			for (int str = 0; str < allStrings.Count; str++)
			{
				modStrings.Add(allStrings[str]);
			}

		}

		public static void CompressStrings(int index)
		{
			int str = index;

			string thisstring = modStrings[str];
			if (str >= 30)
			{
				thisstring = ReplacePiecesofWords(thisstring, str);
			}
			modStrings[str] = thisstring;

		}

		private static string ReplacePiecesofWords(string instring, int index)
		{
			//List<Substring> comparisons = new List<Substring>();

			for (int i = 30; i < index; i++)
			{
				Substring comparison = LargestStringCompare(i, instring, modStrings[i]);
				//comparisons.Add(comparison);
				if (comparison.SubString.Length >= 3)
				{
					instring = CreatePointer(comparison, index, i, instring);
					i--;
				}
			}

			/*
			while (comparisons.Where(x => x.SubString.Length >= 3).ToList().Count > 0)
			{
				Substring sstr = new Substring("", 0, 0);
				Substring longest = comparisons.Aggregate(sstr, (max, cur) => max.SubString.Length > cur.SubString.Length ? max : cur);
				if (longest.SubString != "")
				{
					int longind = comparisons.IndexOf(longest);
					longest = LargestStringCompare(instring, modStrings[longind]);

					if (longest.SubString.Length >= 3)//LargestStringCompare(instring, modStrings[longind]) == longest)
					{
						instring = CreatePointer(longest, index, longind, instring);
						comparisons[longind] = LargestStringCompare(instring, modStrings[longind]);
					}
					else
						comparisons[longind].SubString = "";
				}
			}*/
			return instring;
		}

		private static string CreatePointer(Substring substring, int index, int pindex, string instring)
		{
			char replacechar = (char)(0xE000 + PointerList.Count);
			ReplacedStrings.Add(replacechar, substring.SubString);

			ReplaceAll(substring.SubString, index, replacechar);
			instring = instring.Replace(substring.SubString, (replacechar).ToString());

			int length = 0;
			int offset = 0;
			for (int i = 0; i < substring.SubString.Length; i++)
			{
				if (substring.SubString[i] >= '\uE000' & substring.SubString[i] < '\uFF00')
					length += 2;
				else
					length++;
				
			}

			for (int i = 0; i < modStrings[substring.Index].Length; i++)
			{
				if (i < substring.Offset)
				{
					if (modStrings[substring.Index][i] >= '\uE000' & modStrings[substring.Index][i] < '\uFF00')
						offset += 2;
					else
						offset++;
				}
			}

			Pointer2 newpoint = new Pointer2(index, pindex, offset, length, substring.SubString);
			PointerDictionary.Add(replacechar, newpoint);
			PointerList.Add(newpoint);

			return instring;
		}

		private static void ReplaceAll(string replacethis, int starthere, char replacechar)
		{
			for (int i = starthere; i < modStrings.Count; i++)
			{
				modStrings[i] = modStrings[i].Replace(replacethis, (replacechar).ToString());
			}
		}

		private static Substring LargestStringCompare(int index, string findthis, string inthis)
		{
			List<int[]> foundchars = new List<int[]>();
			foreach (char c in findthis)
			{
				if (c == '\uFFF1')
				{
					foundchars.Add(new int[0] { });
				}
				else
				{
					List<int> foundIndexes = new List<int>();

					long t1 = DateTime.Now.Ticks;
					for (int i = inthis.IndexOf(c); i > -1; i = inthis.IndexOf(c, i + 1))
					{
						// for loop end when i=-1 ('a' not found)
						foundIndexes.Add(i);
					}
					foundchars.Add(foundIndexes.ToArray());
				}
			}

			int longestlength = 0;
			int longeststart = -1;

			for (int i = 0; i < foundchars.Count; i++)
			{
				for (int j = 0; j < foundchars[i].Length; j++)
				{
					int currcount = 1;
					int currindex = i;
					int currvalue = foundchars[i][j];

					while (true)
					{
						if (currindex + 1 < foundchars.Count)
						{
							if (NextInt(foundchars[currindex + 1], currvalue))
							{
								currcount++;
								currindex++;
								currvalue++;
							}
							else
								break;
						}
						else
							break;
					}

					if (currcount > 1 & currcount > longestlength)
					{
						longestlength = currcount;
						longeststart = foundchars[i][j];
					}
				}
			}

			if (longestlength == 0)
				return new Substring(0, "", 0, 0);
			else
				return new Substring(index, inthis.Substring(longeststart, longestlength), longeststart, longestlength);

		}

		private static bool NextInt(int[] checkthese, int followingthis)
		{
			return (checkthese.Contains(followingthis + 1));
		}



		private static Dictionary<char, byte[]> CharByteDictionary;
		private static void CreateCharByteDictionary()
		{
			DefaultCharByteDictionary();

			List<char> keychars = new List<char>(PointerDictionary.Keys);
			for (int i = 0; i < keychars.Count; i++)
			{
				if (keychars[i] == modStrings[157][0])
				{ }
				Pointer2 point = PointerDictionary[keychars[i]];
				int len = point.Length;
				int off = point.Offset;
				int index = point.PointerIndex;

				// 2 bytes: ABCD EFGH IJKL MNOP
				// offset is D->J
				// length is K->P
				// B high = pointer
				// next two bytes -> C->P is index of pointer
				int b12 = off << 6;
				b12 += len;
				b12 += 0x4000;
				int b34 = index;

				byte[] pbytes = new byte[4] {
					(byte)(b34 / 256), (byte)(b34 % 256),
					(byte)(b12 / 256), (byte)(b12 % 256)
				};

				CharByteDictionary.Add(keychars[i], pbytes);
			}
		}

		private static void DefaultCharByteDictionary()
		{
			CharByteDictionary = new Dictionary<char, byte[]> {
				{ '\n', new byte[2] { 0x00, 0x00 } }, { '\uFFF2', new byte[2] { 0x00, 0x00 } }, { '@', new byte[2] { 0x00, 0x01 } }, { ',', new byte[2] { 0x00, 0x02 } }, { '\u25CF', new byte[2] { 0x00, 0x03 } },
				{ '~', new byte[2] { 0x00, 0x1E } },{ '\uFF3B', new byte[2] { 0x00, 0x1F } },{ '\uFF3D', new byte[2] { 0x00, 0x20 } }, { '\uFF08', new byte[2] { 0x00, 0x3B } },
				{ '\uFF09', new byte[2] { 0x00, 0x3C } }, { '\uFF10', new byte[2] { 0x00, 0x46 } }, { '\uFF01', new byte[2] { 0x00, 0x47 } }, { '\uFF02', new byte[2] { 0x00, 0x48 } },
				{ '\uFF03', new byte[2] { 0x00, 0x49 } }, { '\uFF04', new byte[2] { 0x00, 0x4A } }, { '\uFF05', new byte[2] { 0x00, 0x4B } }, { '\uFF06', new byte[2] { 0x00, 0x4C } },
				{ '\uFF07', new byte[2] { 0x00, 0x4D } }, { '\uFF1D', new byte[2] { 0x00, 0x4E } }, { '\uFF3E', new byte[2] { 0x00, 0x4F } }, { '\uFF0D', new byte[2] { 0x00, 0x50 } },
				{ '\uFFE5', new byte[2] { 0x00, 0x51 } }, { '\uFF0C', new byte[2] { 0x00, 0x52 } }, { '\uFF0E', new byte[2] { 0x00, 0x53 } }, { '\uFF0F', new byte[2] { 0x00, 0x54 } },
				{ '\uFF3F', new byte[2] { 0x00, 0x55 } }, { ' ', new byte[2] { 0x00, 0x70 } }, { '[', new byte[2] { 0x00, 0x71 } }, { ']', new byte[2] { 0x00, 0x72 } }, { '(', new byte[2] { 0x00, 0x8D } },
				{ ')', new byte[2] { 0x00, 0x8E } }, { '0', new byte[2] { 0x00, 0x98 } }, { '!', new byte[2] { 0x00, 0x99 } }, { '"', new byte[2] { 0x00, 0x9A } }, { '#', new byte[2] { 0x00, 0x9B } },
				{ '$', new byte[2] { 0x00, 0x9C } }, { '%', new byte[2] { 0x00, 0x9D } }, { '&', new byte[2] { 0x00, 0x9E } }, { '\'', new byte[2] { 0x00, 0x9F } }, { '=', new byte[2] { 0x00, 0xA0 } },
				{ '^', new byte[2] { 0x00, 0xA1 } }, { '-', new byte[2] { 0x00, 0xA2 } }, { '\u00A5', new byte[2] { 0x00, 0xA3 } }, { '.', new byte[2] { 0x00, 0xA4 } }, { '/', new byte[2] { 0x00, 0xA5 } },
				{ '_', new byte[2] { 0x00, 0xA6 } }, { '\u221E', new byte[2] { 0x01, 0x4F } },{ '?', new byte[2] { 0x01, 0x51 } },{ ':', new byte[2] { 0x01, 0x52 } },
				{ '\u00B7', new byte[2] { 0x01, 0x53 } }, { '\u03B1', new byte[2] { 0x01, 0x69 } },{ '<', new byte[2] { 0x01, 0x6A } },{ '>', new byte[2] { 0x01, 0x6B } },
				{ '\uFFF3', new byte[2] { 0x01, 0x6C } }, { '\u25A1', new byte[2] { 0x01, 0x6D } }, { '\u25B3', new byte[2] { 0x01, 0x6E } }, { '\u00D7', new byte[2] { 0x01, 0x6F } }, { ';', new byte[2] { 0x01, 0x70 } },
				{ '\uFFF1', new byte[2] { 0x00, 0x00 } }
				};

			for (int i = 0; i < 26; i++)
			{
				CharByteDictionary.Add((char)('A' + i), new byte[2] { 0x00, (byte)(0x56 + i) });
				CharByteDictionary.Add((char)('a' + i), new byte[2] { 0x00, (byte)(0x73 + i) });
				CharByteDictionary.Add((char)(0xFF21 + i), new byte[2] { 0x00, (byte)(0x04 + i) });
				CharByteDictionary.Add((char)(0xFF41 + i), new byte[2] { 0x00, (byte)(0x21 + i) });
			}

			for (int i = 0; i < 9; i++)
			{
				CharByteDictionary.Add((char)('1' + i), new byte[2] { 0x00, (byte)(0x8F + i) });
				CharByteDictionary.Add((char)(0xFF11 + i), new byte[2] { 0x00, (byte)(0x3D + i) });
			}

			for (int i = 0; i < 194; i++)
			{
				if (i == 168 | (i >= 170 & i <= 172))
				{ }
				else
					CharByteDictionary.Add((char)(0xD0A7 + i), new byte[2] { 0x00, (byte)(0xA7 + 1) });
			}

		}


		
		public static void ExportToBytes()
		{
			Offsets = new List<int>();
			StringBytes = new List<byte>();

			// start of offset for index 30
			int currentoffset = Constants.FirstEnglishOffset;

			int total = 0;
			CreateCharByteDictionary();

			for (int index = 30; index < modStrings.Count; index++)
			{
				Offsets.Add(currentoffset);

				for (int c = 0; c < modStrings[index].Length; c++)
				{
					if (index == 35)
					{ }

					byte[] savethis = new byte[CharByteDictionary[modStrings[index][c]].Length];
					Array.Copy(CharByteDictionary[modStrings[index][c]], savethis, savethis.Length);

					total += savethis.Length;

					// end of string -> add 0x80 to first byte
					if (c == modStrings[index].Length - 1)
						savethis[0] += 0x80;
					// i don't know what this value indicates, but pretty much each non-pointer has this bit high
					else if (savethis.Length == 2)
						savethis[0] += 0x20;

					Array.Reverse(savethis);

					if (modStrings[index][c] == '\uE000')
					{ }

					foreach (byte b in savethis)
						StringBytes.Add(b);

					currentoffset += (savethis.Length / 2);
				}
			}

			ExportOffsetsToBytes();
		}

		private static void ExportOffsetsToBytes()
		{
			OffsetBytes = new List<byte>();

			for (int i = 0; i < Offsets.Count; i++)
			{
				byte[] savethis = new byte[4];
				int offset = Offsets[i];

				for (int j = 0; j < 4; j++)
				{
					savethis[j] = (byte)(offset / (int)(Math.Pow(256, 3 - j)));
					offset = offset % (int)(Math.Pow(256, 3 - j));
				}

				Array.Reverse(savethis);

				foreach (byte b in savethis)
					OffsetBytes.Add(b);
			}

		}
		
	}

	public class Substring
	{
		public int Index;
		public string SubString;
		public int Offset;
		public int Length;

		public Substring(int index, string str, int offset, int length)
		{
			Index = index;
			SubString = str;
			Offset = offset;
			Length = length;
		}
	}

	public class Pointer2
	{
		public int Index;
		public int PointerIndex;
		public int Offset;
		public int Length;
		public string SubString;

		public Pointer2(int index, int pindex, int offset, int length, string substring)
		{
			Index = index;
			PointerIndex = pindex;
			Offset = offset;
			Length = length;
			SubString = substring;
		}

	}


}
