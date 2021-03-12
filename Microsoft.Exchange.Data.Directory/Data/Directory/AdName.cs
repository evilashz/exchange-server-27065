using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public sealed class AdName : ICloneable
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000FCEE File Offset: 0x0000DEEE
		private AdName(string prefix, string escapedName, string unescapedName)
		{
			this.prefix = AdName.PrefixIntern(prefix);
			this.escapedName = escapedName;
			this.unescapedName = unescapedName;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public AdName(string prefix, string unescapedValue) : this(prefix, unescapedValue, false)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public AdName(string prefix, string value, bool isEscaped)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(DirectoryStrings.ExEmptyStringArgumentException("value"), "value");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (string.IsNullOrEmpty(prefix))
			{
				throw new ArgumentException(DirectoryStrings.ExEmptyStringArgumentException("prefix"), "prefix");
			}
			this.prefix = AdName.PrefixIntern(prefix);
			if (!isEscaped)
			{
				this.escapedName = AdName.Escape(value);
				return;
			}
			this.escapedName = AdName.Format(value);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		internal static string Escape(string input)
		{
			string result;
			if (!string.IsNullOrEmpty(input) && AdName.ConvertTo(AdName.ConvertOption.Escape, input, 0, input.Length, out result))
			{
				return result;
			}
			return input;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		internal static string Unescape(string input)
		{
			string result;
			if (!string.IsNullOrEmpty(input) && AdName.ConvertTo(AdName.ConvertOption.Unescape, input, 0, input.Length, out result))
			{
				return result;
			}
			return input;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000FE10 File Offset: 0x0000E010
		private static string Format(string input)
		{
			string result;
			if (!string.IsNullOrEmpty(input) && AdName.ConvertTo(AdName.ConvertOption.Format, input, 0, input.Length, out result))
			{
				return result;
			}
			return input;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000FE3A File Offset: 0x0000E03A
		internal static bool ConvertTo(AdName.ConvertOption option, string input, int startIndex, int length, out string converted)
		{
			if (!AdName.TryConvertTo(option, input, startIndex, length, out converted))
			{
				throw new FormatException(DirectoryStrings.InvalidDNFormat(input));
			}
			return converted != null;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FE64 File Offset: 0x0000E064
		internal static bool TryConvertTo(AdName.ConvertOption option, string input, int startIndex, int length, out string converted)
		{
			converted = null;
			StringBuilder stringBuilder = null;
			int num = startIndex;
			int num2 = startIndex;
			int num3 = startIndex + length;
			switch (option)
			{
			case AdName.ConvertOption.Escape:
				num2 = AdName.EscapeChar(input, ref num, startIndex + 1, num3, num2, true, ref stringBuilder);
				if (num2 < num3 - 2)
				{
					num2 = AdName.EscapeChar(input, ref num, num3 - 1, num3, ++num2, false, ref stringBuilder);
				}
				if (num2 < num3 - 1)
				{
					AdName.EscapeChar(input, ref num, num3, num3, ++num2, true, ref stringBuilder);
				}
				break;
			case AdName.ConvertOption.Unescape:
				if (!AdName.UnescapeChar(input, ref num, startIndex + 1, num3, ref num2, true, ref stringBuilder))
				{
					return false;
				}
				if (num2 < num3 - 3)
				{
					num2++;
					if (!AdName.UnescapeChar(input, ref num, num3 - 2, num3, ref num2, false, ref stringBuilder))
					{
						return false;
					}
				}
				if (num2 < num3 - 1)
				{
					num2++;
					if (!AdName.UnescapeChar(input, ref num, num3, num3, ref num2, true, ref stringBuilder))
					{
						return false;
					}
				}
				break;
			case AdName.ConvertOption.Format:
				if (input[startIndex] == ' ')
				{
					while (++startIndex < num3 && input[startIndex] == ' ')
					{
					}
					if (startIndex >= num3)
					{
						return false;
					}
					if (!AdName.TryConvertTo(AdName.ConvertOption.Format, input, startIndex, num3 - startIndex, out converted))
					{
						return false;
					}
					if (converted == null)
					{
						converted = input.Substring(startIndex, num3 - startIndex);
					}
					return true;
				}
				else
				{
					if (!AdName.TryFormatChar(input, ref num, startIndex + 1, num3, ref num2, true, true, ref stringBuilder))
					{
						return false;
					}
					if (num2 < num3 - 3)
					{
						num2++;
						if (!AdName.TryFormatChar(input, ref num, num3 - 2, num3, ref num2, false, false, ref stringBuilder))
						{
							return false;
						}
					}
					if (num2 < num3 - 2 && input[num2 + 1] == ' ')
					{
						num2++;
						if (!AdName.TryFormatChar(input, ref num, num3 - 1, num3, ref num2, true, false, ref stringBuilder))
						{
							return false;
						}
					}
					if (num2 < num3 - 1)
					{
						num2++;
						if (!AdName.TryFormatChar(input, ref num, num3, num3, ref num2, true, true, ref stringBuilder))
						{
							return false;
						}
					}
				}
				break;
			}
			if (stringBuilder == null)
			{
				return true;
			}
			if (num < num3)
			{
				stringBuilder.Append(input, num, num3 - num);
			}
			converted = stringBuilder.ToString();
			if (converted.Equals(input))
			{
				converted = input;
			}
			return true;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00010034 File Offset: 0x0000E234
		private static int EscapeChar(string input, ref int lastSavedIndex, int loopEndIndex, int rdnEndIndex, int currentIndex, bool needEscapeSpace, ref StringBuilder sbConversion)
		{
			for (;;)
			{
				char c = input[currentIndex];
				string value;
				if (c == '+')
				{
					value = "\\+";
					goto IL_AD;
				}
				if (c == '=')
				{
					value = "\\=";
					goto IL_AD;
				}
				if (c == ',')
				{
					value = "\\,";
					goto IL_AD;
				}
				if (c == ';')
				{
					value = "\\;";
					goto IL_AD;
				}
				if (c == '<')
				{
					value = "\\<";
					goto IL_AD;
				}
				if (c == '>')
				{
					value = "\\>";
					goto IL_AD;
				}
				if (c == '#')
				{
					value = "\\#";
					goto IL_AD;
				}
				if (c == '"')
				{
					value = "\\\"";
					goto IL_AD;
				}
				if (c == '\\')
				{
					value = "\\\\";
					goto IL_AD;
				}
				if (c == '\r')
				{
					value = "\\0D";
					goto IL_AD;
				}
				if (c == '\n')
				{
					value = "\\0A";
					goto IL_AD;
				}
				if (needEscapeSpace && c == ' ')
				{
					value = "\\ ";
					goto IL_AD;
				}
				IL_EB:
				if (++currentIndex >= loopEndIndex)
				{
					break;
				}
				continue;
				IL_AD:
				if (sbConversion == null)
				{
					sbConversion = new StringBuilder(input, lastSavedIndex, currentIndex - lastSavedIndex, (rdnEndIndex - lastSavedIndex) * 3);
				}
				else
				{
					sbConversion.Append(input, lastSavedIndex, currentIndex - lastSavedIndex);
				}
				sbConversion.Append(value);
				lastSavedIndex = currentIndex + 1;
				goto IL_EB;
			}
			return --currentIndex;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00010140 File Offset: 0x0000E340
		private static bool UnescapeChar(string input, ref int lastSavedIndex, int loopEndIndex, int rdnEndIndex, ref int currentIndex, bool acceptEscapedSpace, ref StringBuilder sbConversion)
		{
			for (;;)
			{
				if (input[currentIndex] == '\\')
				{
					int num = currentIndex;
					char c = input[++currentIndex];
					char value;
					if (AdName.IsEscapedChar(c))
					{
						value = c;
					}
					else
					{
						char hexChar = input[++currentIndex];
						int num2;
						if (!AdName.TryConvertHexPairToNumber(c, hexChar, out num2))
						{
							break;
						}
						value = (char)num2;
					}
					if (sbConversion == null)
					{
						sbConversion = new StringBuilder(input, lastSavedIndex, num - lastSavedIndex, rdnEndIndex - lastSavedIndex);
					}
					else
					{
						sbConversion.Append(input, lastSavedIndex, num - lastSavedIndex);
					}
					sbConversion.Append(value);
					lastSavedIndex = currentIndex + 1;
				}
				if (++currentIndex >= loopEndIndex)
				{
					goto Block_4;
				}
			}
			return false;
			Block_4:
			currentIndex--;
			return true;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000101FC File Offset: 0x0000E3FC
		private static bool TryFormatChar(string input, ref int lastSavedIndex, int loopEndIndex, int rdnEndIndex, ref int currentIndex, bool acceptEscapedSpace, bool needEscapeSpace, ref StringBuilder sbConversion)
		{
			for (;;)
			{
				int num = currentIndex;
				bool flag = false;
				char c2;
				if (input[currentIndex] == '\\')
				{
					if (currentIndex + 1 >= rdnEndIndex)
					{
						break;
					}
					char c = input[++currentIndex];
					if (!AdName.IsFormattedChar(c))
					{
						if (c == '\r' || c == '\n')
						{
							c2 = c;
							goto IL_EC;
						}
						if (c == ' ')
						{
							if (!acceptEscapedSpace)
							{
								return false;
							}
							if (!needEscapeSpace)
							{
								c2 = c;
								goto IL_EC;
							}
						}
						else
						{
							if (currentIndex + 1 >= rdnEndIndex)
							{
								return false;
							}
							char hexChar = input[++currentIndex];
							int num2;
							if (!AdName.TryConvertHexPairToNumber(c, hexChar, out num2))
							{
								return false;
							}
							if (num2 != 10 && num2 != 13)
							{
								c2 = (char)num2;
								flag = (AdName.IsFormattedChar(c2) || (c2 == ' ' && needEscapeSpace));
								goto IL_EC;
							}
						}
					}
				}
				else if (AdName.IsEscapedChar(input[currentIndex]))
				{
					if (input[currentIndex] != ' ')
					{
						return false;
					}
					if (needEscapeSpace)
					{
						flag = true;
						c2 = ' ';
						goto IL_EC;
					}
				}
				IL_138:
				if (++currentIndex >= loopEndIndex)
				{
					goto Block_18;
				}
				continue;
				IL_EC:
				if (sbConversion == null)
				{
					sbConversion = new StringBuilder(input, lastSavedIndex, num - lastSavedIndex, rdnEndIndex - lastSavedIndex);
				}
				else
				{
					sbConversion.Append(input, lastSavedIndex, num - lastSavedIndex);
				}
				if (flag)
				{
					sbConversion.Append("\\");
				}
				sbConversion.Append(c2);
				lastSavedIndex = currentIndex + 1;
				goto IL_138;
			}
			return false;
			Block_18:
			currentIndex--;
			return true;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001035B File Offset: 0x0000E55B
		private static bool IsEscapedChar(char ch)
		{
			return ch == ' ' || ch == '+' || ch == '=' || ch == ',' || ch == ';' || ch == '<' || ch == '>' || ch == '#' || ch == '"' || ch == '\\' || ch == '\r' || ch == '\n';
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001039B File Offset: 0x0000E59B
		private static bool IsFormattedChar(char ch)
		{
			return ch == '+' || ch == '=' || ch == ',' || ch == ';' || ch == '<' || ch == '>' || ch == '#' || ch == '"' || ch == '\\';
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000103CC File Offset: 0x0000E5CC
		private static bool TryConvertHexPairToNumber(char hexChar1, char hexChar2, out int converted)
		{
			converted = -1;
			int num;
			if (!AdName.TryConvertHexCharToNumber(hexChar1, out num))
			{
				return false;
			}
			int num2;
			if (!AdName.TryConvertHexCharToNumber(hexChar2, out num2))
			{
				return false;
			}
			converted = (num << 4 | num2);
			return true;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000103FC File Offset: 0x0000E5FC
		private static bool TryConvertHexCharToNumber(char hexChar, out int convertedNumber)
		{
			if ('0' <= hexChar && hexChar <= '9')
			{
				convertedNumber = (int)(hexChar - '0');
			}
			else if ('a' <= hexChar && hexChar <= 'f')
			{
				convertedNumber = (int)(hexChar - 'a' + '\n');
			}
			else if ('A' <= hexChar && hexChar <= 'F')
			{
				convertedNumber = (int)(hexChar - 'A' + '\n');
			}
			else
			{
				convertedNumber = -1;
			}
			return convertedNumber >= 0;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00010450 File Offset: 0x0000E650
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00010458 File Offset: 0x0000E658
		public string EscapedName
		{
			get
			{
				return this.escapedName;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00010460 File Offset: 0x0000E660
		public string UnescapedName
		{
			get
			{
				if (this.unescapedName == null)
				{
					this.unescapedName = AdName.Unescape(this.escapedName);
				}
				return this.unescapedName;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00010484 File Offset: 0x0000E684
		public static AdName ParseEscapedString(string escapedString)
		{
			if (escapedString == null)
			{
				throw new ArgumentNullException(DirectoryStrings.CannotParse("<null>").ToString());
			}
			return AdName.ParseRdn(escapedString, 0, escapedString.Length, false);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000104C0 File Offset: 0x0000E6C0
		internal static AdName ParseRdn(string escapedDNString, int startIndex, int count, bool isFormatted)
		{
			int num = escapedDNString.IndexOf('=', startIndex, count) - startIndex;
			if (num < 1 || num >= count - 1)
			{
				throw new FormatException(DirectoryStrings.InvalidDNFormat(escapedDNString));
			}
			string text = escapedDNString.Substring(startIndex, num);
			string value = escapedDNString.Substring(startIndex + num + 1, count - (num + 1));
			if (!isFormatted)
			{
				return new AdName(text, value, true);
			}
			return new AdName(text, value, null);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00010523 File Offset: 0x0000E723
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AdName);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00010531 File Offset: 0x0000E731
		public override int GetHashCode()
		{
			return this.escapedName.GetHashCode();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001053E File Offset: 0x0000E73E
		public bool Equals(AdName obj)
		{
			return !(obj == null) && this.EscapedName.Equals(obj.EscapedName, StringComparison.OrdinalIgnoreCase) && this.Prefix.Equals(obj.Prefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00010573 File Offset: 0x0000E773
		public static bool operator ==(AdName a, AdName b)
		{
			return (a == null && b == null) || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001058C File Offset: 0x0000E78C
		public static bool operator !=(AdName a, AdName b)
		{
			return !(a == b);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00010598 File Offset: 0x0000E798
		public int CompareTo(object obj)
		{
			if (!(obj is AdName))
			{
				throw new ArgumentException(DirectoryStrings.ExInvalidTypeArgumentException("obj", obj.GetType(), typeof(AdName)), "obj");
			}
			return string.Compare(this.EscapedName, (obj == null) ? null : ((AdName)obj).EscapedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.stringRepresentation))
			{
				this.stringRepresentation = this.prefix + "=" + this.escapedName;
			}
			return this.stringRepresentation;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010625 File Offset: 0x0000E825
		public object Clone()
		{
			return new AdName(this.prefix, this.escapedName, this.unescapedName);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010640 File Offset: 0x0000E840
		private static string PrefixIntern(string prefix)
		{
			if (prefix != null)
			{
				if (prefix == "CN")
				{
					return "CN";
				}
				if (prefix == "DC")
				{
					return "DC";
				}
				if (prefix == "OU")
				{
					return "OU";
				}
			}
			return prefix;
		}

		// Token: 0x040000C7 RID: 199
		private string prefix;

		// Token: 0x040000C8 RID: 200
		private string escapedName;

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		private string unescapedName;

		// Token: 0x040000CA RID: 202
		private string stringRepresentation;

		// Token: 0x02000030 RID: 48
		internal enum ConvertOption
		{
			// Token: 0x040000CC RID: 204
			Escape,
			// Token: 0x040000CD RID: 205
			Unescape,
			// Token: 0x040000CE RID: 206
			Format
		}
	}
}
