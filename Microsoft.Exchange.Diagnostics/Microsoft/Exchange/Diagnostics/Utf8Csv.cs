using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C9 RID: 457
	internal static class Utf8Csv
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002EFD6 File Offset: 0x0002D1D6
		public static void WriteBom(Stream output)
		{
			Utf8Csv.WriteBytes(output, Utf8Csv.Bom);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
		public static void WriteHeaderRow(Stream output, string[] fields)
		{
			int num = fields.Length - 1;
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i] != null)
				{
					Utf8Csv.EncodeEscapeAndWrite(output, fields[i]);
				}
				if (i == num)
				{
					Utf8Csv.WriteBytes(output, Utf8Csv.NewLine);
				}
				else
				{
					Utf8Csv.WriteByte(output, 44);
				}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002F02C File Offset: 0x0002D22C
		public static void WriteRawRow(Stream output, byte[][] fields)
		{
			int num = fields.Length - 1;
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i] != null)
				{
					Utf8Csv.WriteBytes(output, fields[i]);
				}
				if (i == num)
				{
					Utf8Csv.WriteBytes(output, Utf8Csv.NewLine);
				}
				else
				{
					Utf8Csv.WriteByte(output, 44);
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002F074 File Offset: 0x0002D274
		public static void WriteByte(Stream output, byte data)
		{
			output.WriteByte(data);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002F07D File Offset: 0x0002D27D
		public static void WriteBytes(Stream output, byte[] data)
		{
			output.Write(data, 0, data.Length);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002F08A File Offset: 0x0002D28A
		public static void EncodeAndWrite(Stream output, string s)
		{
			Utf8Csv.WriteBytes(output, Utf8Csv.Encode(s));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002F098 File Offset: 0x0002D298
		public static void EncodeEscapeAndWrite(Stream output, string s)
		{
			Utf8Csv.WriteBytes(output, Utf8Csv.EncodeAndEscape(s));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002F0A6 File Offset: 0x0002D2A6
		public static void EncodeAndWriteLine(Stream output, string s)
		{
			Utf8Csv.WriteBytes(output, Utf8Csv.Encode(s));
			Utf8Csv.WriteBytes(output, Utf8Csv.NewLine);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002F0BF File Offset: 0x0002D2BF
		public static void EncodeEscapeAndWriteLine(Stream output, string s)
		{
			Utf8Csv.WriteBytes(output, Utf8Csv.EncodeAndEscape(s));
			Utf8Csv.WriteBytes(output, Utf8Csv.NewLine);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002F0D8 File Offset: 0x0002D2D8
		internal static byte[] Encode(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F0E5 File Offset: 0x0002D2E5
		internal static byte[] Escape(byte[] data)
		{
			return Utf8Csv.Escape(data, false);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002F0F0 File Offset: 0x0002D2F0
		internal static byte[] Escape(byte[] data, bool escapeLineBreaks)
		{
			bool flag = false;
			bool flag2 = false;
			List<int> list = null;
			for (int i = 0; i < data.Length; i++)
			{
				byte b = data[i];
				if (b <= 13)
				{
					if (b == 10 || b == 13)
					{
						if (escapeLineBreaks)
						{
							flag2 = true;
							list = (list ?? new List<int>());
							list.Add(i);
						}
						else
						{
							flag = true;
						}
					}
				}
				else if (b != 34)
				{
					if (b == 44)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
					list = (list ?? new List<int>());
					list.Add(i);
				}
			}
			if (!flag && !flag2)
			{
				return data;
			}
			int num = 0;
			byte[] array;
			int num2;
			if (flag)
			{
				array = new byte[data.Length + ((list == null) ? 0 : list.Count) + 2];
				array[0] = 34;
				array[array.Length - 1] = 34;
				num2 = 1;
			}
			else
			{
				array = new byte[data.Length + list.Count];
				num2 = 0;
			}
			if (list != null)
			{
				foreach (int num3 in list)
				{
					int num4 = num3 - num;
					Buffer.BlockCopy(data, num, array, num2, num4);
					int num5 = num2 + num4;
					byte b2 = data[num3];
					if (b2 != 10)
					{
						if (b2 != 13)
						{
							if (b2 != 34)
							{
								throw new InvalidOperationException(string.Format("Cannot escape char '0x{0:x2}'.", data[num3]));
							}
							array[num5] = 34;
							array[num5 + 1] = 34;
						}
						else
						{
							array[num5] = 92;
							array[num5 + 1] = 114;
						}
					}
					else
					{
						array[num5] = 92;
						array[num5 + 1] = 110;
					}
					num = num3 + 1;
					num2 += num4 + 2;
				}
			}
			Buffer.BlockCopy(data, num, array, num2, data.Length - num);
			return array;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F2AC File Offset: 0x0002D4AC
		internal static byte[] EncodeAndEscape(string s)
		{
			return Utf8Csv.EncodeAndEscape(s, false);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002F2B5 File Offset: 0x0002D4B5
		internal static byte[] EncodeAndEscape(string s, bool escapeLineBreaks)
		{
			return Utf8Csv.Escape(Encoding.UTF8.GetBytes(s), escapeLineBreaks);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		internal static void AppendCollectionMember(StringBuilder buffer, string s)
		{
			buffer.Append(s);
			buffer.Append(';');
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		internal static void EscapeAndAppendCollectionMember(StringBuilder buffer, string sourceString, bool escapeLineBreaks)
		{
			bool flag = false;
			bool flag2 = false;
			List<int> list = null;
			for (int i = 0; i < sourceString.Length; i++)
			{
				char c = sourceString[i];
				if (c <= '\r')
				{
					if (c == '\n' || c == '\r')
					{
						if (escapeLineBreaks)
						{
							list = (list ?? new List<int>());
							list.Add(i);
							flag2 = true;
						}
						else
						{
							flag = true;
						}
					}
				}
				else if (c != '\'')
				{
					if (c == ';')
					{
						flag = true;
					}
				}
				else
				{
					list = (list ?? new List<int>());
					list.Add(i);
					flag = true;
				}
			}
			if (!flag && !flag2)
			{
				buffer.Append(sourceString);
				buffer.Append(';');
				return;
			}
			if (flag)
			{
				buffer.Append('\'');
			}
			int num = 0;
			if (list != null)
			{
				foreach (int num2 in list)
				{
					buffer.Append(sourceString, num, num2 - num);
					char c2 = sourceString[num2];
					if (c2 != '\n')
					{
						if (c2 != '\r')
						{
							if (c2 != '\'')
							{
								throw new InvalidOperationException(string.Format("Cannot escape char '0x{0:x2}'.", (byte)sourceString[num2]));
							}
							buffer.Append('\'', 2);
						}
						else
						{
							buffer.Append("\\r");
						}
					}
					else
					{
						buffer.Append("\\n");
					}
					num = num2 + 1;
				}
			}
			buffer.Append(sourceString, num, sourceString.Length - num);
			if (flag)
			{
				buffer.Append('\'');
			}
			buffer.Append(';');
		}

		// Token: 0x04000975 RID: 2421
		private const byte CommaSeparator = 44;

		// Token: 0x04000976 RID: 2422
		private const byte DoubleQuoteEscape = 34;

		// Token: 0x04000977 RID: 2423
		private const char SemicolonSeparator = ';';

		// Token: 0x04000978 RID: 2424
		private const char SingleQuoteEscape = '\'';

		// Token: 0x04000979 RID: 2425
		private const byte CR = 13;

		// Token: 0x0400097A RID: 2426
		private const byte LF = 10;

		// Token: 0x0400097B RID: 2427
		private const int Backslash = 92;

		// Token: 0x0400097C RID: 2428
		private const int N = 110;

		// Token: 0x0400097D RID: 2429
		private const int R = 114;

		// Token: 0x0400097E RID: 2430
		private static readonly byte[] Bom = new byte[]
		{
			239,
			187,
			191
		};

		// Token: 0x0400097F RID: 2431
		private static readonly byte[] NewLine = new byte[]
		{
			13,
			10
		};
	}
}
