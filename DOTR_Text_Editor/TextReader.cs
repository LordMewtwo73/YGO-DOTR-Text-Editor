using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DOTR_Text_Editor
{
	public static class TextReader
	{
		public static List<string> VanillaStrings;
		public static Dictionary<int, char> GameChars;

		public static List<int> offsets;
		public static List<ushort> textbytes;

		private static List<ushort> tempbytes;

		private static string SaveTextFilePath;
		private static string PointerFilePath;

		private static List<Pointer> pointers;


		public static void Load(string filepath)
		{
			ReadFromFile(filepath);
			InitGameChars();
			ConvertBytesToStrings();
		}



		private static void ReadFromFile(string filepath)
		{
			using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite))
			{
				offsets = new List<int>();
				stream.Position = Constants.OffsetTable;
				for (int i = 0; i < Constants.TotalStringCount; i++)
				{
					offsets.Add(StreamReadInt32(stream));
				}

				textbytes = new List<ushort>();
				stream.Position = Constants.TextDataTable;
				for (int i = 0; i < Constants.TotalTextLength; i++)
				{
					textbytes.Add(StreamReadInt16(stream));
				}

			}
		}

		private static void InitGameChars()
		{
			GameChars = new Dictionary<int, char> {
				{ 0x00, '\n' }, { 0x01, '\uFFF2' }, //PNAME_CHAR
				{ 0x02, ',' }, { 0x03, '\u25CF' },
				{ 0x1E, '~' }, { 0x1F, '\uFF3B' }, { 0x20, '\uFF3D' }, { 0x3B, '\uFF08' },
				{ 0x3C, '\uFF09' }, { 0x46, '\uFF11' }, { 0x47, '\uFF01' }, { 0x48, '\uFF02' },
				{ 0x49, '\uFF03' }, { 0x4A, '\uFF04' }, { 0x4B, '\uFF05' }, { 0x4C, '\uFF06' },
				{ 0x4D, '\uFF07' }, { 0x4E, '\uFF1D' }, { 0x4F, '\uFF3E' }, { 0x50, '\uFF0D' },
				{ 0x51, '\uFFE5' }, { 0x52, '\uFF0C' }, { 0x53, '\uFF0E' }, { 0x54, '\uFF0F' },
				{ 0x55, '\uFF3F' }, { 0x70, ' ' }, { 0x71, '[' }, { 0x72, ']' }, { 0x8D, '(' },
				{ 0x8E, ')' }, { 0x98, '0' }, { 0x99, '!' }, { 0x9A, '"' }, { 0x9B, '#' },
				{ 0x9C, '$' }, { 0x9D, '%' }, { 0x9E, '&' }, { 0x9F, '\'' }, { 0xA0, '=' },
				{ 0xA1, '^' }, { 0xA2, '-' }, { 0xA3, '\u00A5' }, { 0xA4, '.' }, { 0xA5, '/' },
				{ 0xA6, '_' }, { 0x14F, '\u221E' }, { 0x151, '?' }, { 0x152, ':' },
				{ 0x153, '\u00B7' }, { 0x169, '\u03B1' }, { 0x16A, '<' }, { 0x16B, '>' },
				{ 0x16C, '\uFFF3' }/* III character for Richard III */, { 0x16D, '\u25A1' }, { 0x16E, '\u25B3' }, { 0x16F, '\u00D7' }, { 0x170, ';' }
				};

			for (int i = 0; i < 26; i++)
			{
				GameChars[0x56 + i] = ((char)('A' + i));
				GameChars[0x73 + i] = ((char)('a' + i));
				GameChars[0x04 + i] = ((char)(0xFF21 + i));
				GameChars[0x21 + i] = ((char)(0xFF41 + i));
			}
			for (int i = 0; i < 9; i++)
			{
				GameChars[0x8F + i] = (i + 1).ToString()[0];
				GameChars[0x3D + i] = ((char)(0xFF11 + i));
			}

			for (int i = 0; i < 194; i++)
			{
				if (i == 168 | (i >= 170 & i <= 172))
				{ }
				else
					GameChars[0xA7 + i] = ((char)(0xD0A7 + i));
			}
		}

		private static void ConvertBytesToStrings()
		{
			VanillaStrings = new List<string>();
			
			pointers = new List<Pointer>();


			var strings = new List<List<List<int>>>();
			for (int i = 0; i < Constants.TotalStringCount; i++)
			{
				tempbytes = new List<ushort>();
				strings.Add(ReadString(offsets, textbytes, i));
			}

			//strings = new List<List<List<int>>>();
			List<int> temp = new List<int>();
			for (int i = 0; i < textbytes.Count; i++)
			{
				temp.Add(textbytes[i] & 0x1FFF);
			}
			List<List<int>> temp2 = new List<List<int>>();
			temp2.Add(temp);
			//strings.Add(temp2);

			List<int> unaccounted = new List<int>();
			for (int i = 0; i < strings.Count; i++)
			{
				var realChars = new List<char>();
				foreach (var line in strings[i])
				{
					foreach (var charValue in line)
					{
						if (GameChars.ContainsKey(charValue))
							realChars.Add(GameChars[charValue]);
						else
						{
							realChars.Add('\uFFFD');
							if (!unaccounted.Contains(charValue))
								unaccounted.Add(charValue);
						}
					}
					realChars.Add('\uFFF1');
				}
				//realChars.Remove(realChars[^1]);
				VanillaStrings.Add(string.Join("", realChars));
				VanillaStrings[i] = VanillaStrings[i].Remove(VanillaStrings[i].Length - 1);

				if (VanillaStrings.Count % 50 == 0)
				{
					int dkdkdk = 0;
				}
			}

		}



		private static int StreamReadInt32(FileStream stream)
		{
			byte[] readbytes = new byte[4];
			stream.Read(readbytes, 0, 4);
			return BytesToInt(readbytes);
		}

		private static ushort StreamReadInt16(FileStream stream)
		{
			byte[] readbytes = new byte[2];
			stream.Read(readbytes, 0, 2);
			return (ushort)BytesToInt(readbytes);
		}

		private static int BytesToInt(byte[] bytes)
		{
			int output = 0;

			for (int i = 0; i < bytes.Length; i++)
			{
				output += (int)(Math.Pow(256, i)) * bytes[i];
			}

			return output;
		}



		private static List<List<int>> ReadString(List<int> offsets, List<ushort> blob, int index)
		{
			if (index == 2182)
			{ int stop = 0; }

			int blobIndex = offsets[index];
			List<List<int>> lines = new List<List<int>> { new List<int>() };
			
			while (true)
			{
				tempbytes.Add(blob[blobIndex]);

				if ((blob[blobIndex] & 0x4000) != 0)// & false)
				{
					int subLength = blob[blobIndex] & 0x3F;
					int pointerindex = blob[blobIndex + 1] & 0x3FFF;
					int pointerStart = offsets[pointerindex];
					int pointeroffset = (blob[blobIndex] >> 6) & 0x7F;
					pointerStart += pointeroffset;

					pointers.Add(new Pointer(index, pointerindex, subLength, pointeroffset));

					var subStr = RecursiveRead(offsets, blob, pointerStart, subLength);
					if (subStr.Count > 0)
					{
						lines[lines.Count - 1].AddRange(subStr[0]);
						lines.AddRange(subStr.GetRange(1, subStr.Count - 1));
					}
					blobIndex += 2;
				}
				else if ((blob[blobIndex] & 0x1FFF) != 0)
				{
					lines[lines.Count - 1].Add(blob[blobIndex] & 0x1FFF);
					blobIndex++;
				}
				else
				{
					lines.Add(new List<int>());
					blobIndex++;
				}

				if ((blob[blobIndex - 1] & 0x8000) != 0)
					return lines;
			}
		}
		static List<List<int>> RecursiveRead(List<int> offsets, List<ushort> blob, int blobIndex, int length)
		{
			List<List<int>> lines = new List<List<int>> { new List<int>() };

			while (length > 0)
			{
				tempbytes.Add(blob[blobIndex]);

				if ((blob[blobIndex] & 0x4000) != 0)
				{
					int subLength = blob[blobIndex] & 0x3F;
					int pointerStart = offsets[blob[blobIndex + 1] & 0x3FFF];
					pointerStart += (blob[blobIndex] >> 6) & 0x7F;
					var subStr = RecursiveRead(offsets, blob, pointerStart, subLength);
					if (subStr.Count > 0)
					{
						lines[lines.Count - 1].AddRange(subStr[0]);
						lines.AddRange(subStr.GetRange(1, subStr.Count - 1));
					}
					blobIndex += 2;
					length -= 2;
				}
				else if ((blob[blobIndex] & 0x1FFF) != 0)
				{
					lines[lines.Count - 1].Add(blob[blobIndex] & 0x1FFF);
					blobIndex++;
					length--;
				}
				else
				{
					lines.Add(new List<int>());
					blobIndex++;
					length--;
				}
				if ((blob[blobIndex - 1] & 0x8000) != 0)
					return lines;
			}
			return lines;
		}

	}

	public class Pointer
	{
		public int StringIndex;
		public int PointerIndex;
		public int Length;
		public int Offset;

		public Pointer(int sindex, int pindex, int length, int offset)
		{
			StringIndex = sindex;
			PointerIndex = pindex;
			Length = length;
			Offset = offset;
		}
	}

}
