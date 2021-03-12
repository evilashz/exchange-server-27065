using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000181 RID: 385
	internal static class ParseSupport
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00078F25 File Offset: 0x00077125
		public static int CharToDecimal(char ch)
		{
			return (int)(ch - '0');
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00078F2B File Offset: 0x0007712B
		public static int CharToHex(char ch)
		{
			return (int)ParseSupport.charToHexTable[(int)(ch & '\u001f')];
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00078F37 File Offset: 0x00077137
		public static int BitCount(byte v)
		{
			return (int)(ParseSupport.octetBitsCount[(int)(v & 15)] + ParseSupport.octetBitsCount[v >> 4 & 15]);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00078F50 File Offset: 0x00077150
		public static int BitCount(short v)
		{
			return ParseSupport.BitCount((byte)v) + ParseSupport.BitCount((byte)(v >> 8));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00078F63 File Offset: 0x00077163
		public static int BitCount(int v)
		{
			return ParseSupport.BitCount((short)v) + ParseSupport.BitCount((short)(v >> 16));
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00078F77 File Offset: 0x00077177
		public static char HighSurrogateCharFromUcs4(int ich)
		{
			return (char)(55296 + (ich - 65536 >> 10));
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00078F8A File Offset: 0x0007718A
		public static char LowSurrogateCharFromUcs4(int ich)
		{
			return (char)(56320 + (ich & 1023));
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00078F9A File Offset: 0x0007719A
		public static int Ucs4FromSurrogatePair(char low, char high)
		{
			return (int)((low & 'Ͽ') + ((int)(high & 'Ͽ') << 10)) + 65536;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00078FB4 File Offset: 0x000771B4
		public static bool IsHighSurrogate(char ch)
		{
			return ch >= '\ud800' && ch < '\udc00';
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00078FC8 File Offset: 0x000771C8
		public static bool IsLowSurrogate(char ch)
		{
			return ch >= '\udc00' && ch < '\ude00';
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00078FDC File Offset: 0x000771DC
		public static bool IsCharClassOneOf(CharClass charClass, CharClass charClassSet)
		{
			return (charClass & charClassSet) != CharClass.Invalid;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00078FE7 File Offset: 0x000771E7
		public static bool InvalidUnicodeCharacter(CharClass charClass)
		{
			return (charClass & CharClass.UniqueMask) == CharClass.Invalid;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00078FF3 File Offset: 0x000771F3
		public static bool HtmlTextCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlNonWhitespaceText) != CharClass.Invalid;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00079002 File Offset: 0x00077202
		public static bool TextCharacter(CharClass charClass)
		{
			return (charClass & CharClass.NonWhitespaceText) != CharClass.Invalid;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00079011 File Offset: 0x00077211
		public static bool TextNonUriCharacter(CharClass charClass)
		{
			return (charClass & CharClass.NonWhitespaceNonUri) != CharClass.Invalid;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00079020 File Offset: 0x00077220
		public static bool TextUriCharacter(CharClass charClass)
		{
			return (charClass & CharClass.NonWhitespaceUri) != CharClass.Invalid;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0007902F File Offset: 0x0007722F
		public static bool NonControlTextCharacter(CharClass charClass)
		{
			return (charClass & CharClass.NonWhitespaceNonControlText) != CharClass.Invalid;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0007903E File Offset: 0x0007723E
		public static bool ControlCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Control) != CharClass.Invalid;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00079049 File Offset: 0x00077249
		public static bool WhitespaceCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Whitespace) != CharClass.Invalid;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00079054 File Offset: 0x00077254
		public static bool WhitespaceCharacter(char ch)
		{
			return (ParseSupport.GetCharClass(ch) & CharClass.Whitespace) != CharClass.Invalid;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00079064 File Offset: 0x00077264
		public static bool NbspCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Nbsp) != CharClass.Invalid;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00079073 File Offset: 0x00077273
		public static bool AlphaCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Alpha) != CharClass.Invalid;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0007907E File Offset: 0x0007727E
		public static bool AlphaCharacter(char ch)
		{
			return (ParseSupport.GetCharClass(ch) & CharClass.Alpha) != CharClass.Invalid;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0007908E File Offset: 0x0007728E
		public static bool QuoteCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Quote) != CharClass.Invalid;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0007909D File Offset: 0x0007729D
		public static bool HtmlTagNamePrefixCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlTagNamePrefix) != CharClass.Invalid;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000790AC File Offset: 0x000772AC
		public static bool HtmlTagNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlTagName) != CharClass.Invalid;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000790BB File Offset: 0x000772BB
		public static bool HtmlAttrNamePrefixCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlAttrNamePrefix) != CharClass.Invalid;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000790CA File Offset: 0x000772CA
		public static bool HtmlAttrNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlAttrName) != CharClass.Invalid;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000790D9 File Offset: 0x000772D9
		public static bool HtmlAttrValueCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlAttrValue) != CharClass.Invalid;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000790E8 File Offset: 0x000772E8
		public static bool HtmlScanQuoteSensitiveCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlScanQuoteSensitive) != CharClass.Invalid;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000790F7 File Offset: 0x000772F7
		public static bool HtmlSimpleTagNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlSimpleTagName) != CharClass.Invalid;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00079106 File Offset: 0x00077306
		public static bool HtmlEndTagNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlEndTagName) != CharClass.Invalid;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00079115 File Offset: 0x00077315
		public static bool HtmlSimpleAttrNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlSimpleAttrName) != CharClass.Invalid;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00079124 File Offset: 0x00077324
		public static bool HtmlEndAttrNameCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlEndAttrName) != CharClass.Invalid;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00079133 File Offset: 0x00077333
		public static bool HtmlSimpleAttrQuotedValueCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlSimpleAttrQuotedValue) != CharClass.Invalid;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00079142 File Offset: 0x00077342
		public static bool HtmlSimpleAttrUnquotedValueCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlAttrValue) != CharClass.Invalid;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00079151 File Offset: 0x00077351
		public static bool HtmlEndAttrUnquotedValueCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlEndAttrUnquotedValue) != CharClass.Invalid;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00079160 File Offset: 0x00077360
		public static bool NumericCharacter(CharClass charClass)
		{
			return (charClass & CharClass.Numeric) != CharClass.Invalid;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0007916C File Offset: 0x0007736C
		public static bool NumericCharacter(char ch)
		{
			return (ParseSupport.GetCharClass(ch) & CharClass.Numeric) != CharClass.Invalid;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0007917D File Offset: 0x0007737D
		public static bool HexCharacter(CharClass charClass)
		{
			return (charClass & (CharClass)2147483664U) != CharClass.Invalid;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0007918C File Offset: 0x0007738C
		public static bool HtmlEntityCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlEntity) != CharClass.Invalid;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00079198 File Offset: 0x00077398
		public static bool HtmlSuffixCharacter(CharClass charClass)
		{
			return (charClass & CharClass.HtmlSuffix) != CharClass.Invalid;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000791A7 File Offset: 0x000773A7
		public static bool RtfInterestingCharacter(CharClass charClass)
		{
			return (charClass & CharClass.RtfInteresting) != CharClass.Invalid;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000791B6 File Offset: 0x000773B6
		public static CharClass GetCharClass(byte ch)
		{
			return ParseSupport.lowCharClass[(int)ch];
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000791BF File Offset: 0x000773BF
		public static CharClass GetCharClass(char ch)
		{
			if (ch > 'ÿ')
			{
				return ParseSupport.GetHighCharClass(ch);
			}
			return ParseSupport.lowCharClass[(int)ch];
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000791D7 File Offset: 0x000773D7
		public static CharClass GetHighCharClass(char ch)
		{
			if (ch < '﷐')
			{
				return CharClass.NotInterestingText;
			}
			if (('￹' <= ch && ch <= '�') || ('ﷰ' <= ch && ch <= '￯'))
			{
				return CharClass.NotInterestingText;
			}
			return CharClass.Invalid;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00079206 File Offset: 0x00077406
		public static bool IsLeadByte(byte bt, DbcsLeadBits codePageMask)
		{
			return codePageMask != (DbcsLeadBits)0 && ParseSupport.IsLeadByteEx(bt, codePageMask);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00079214 File Offset: 0x00077414
		private static bool IsLeadByteEx(byte bt, DbcsLeadBits codePageMask)
		{
			return bt >= 128 && (byte)(ParseSupport.dbcsLeadTable[(int)(bt - 128)] & codePageMask) != 0;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00079238 File Offset: 0x00077438
		public static DbcsLeadBits GetCodePageLeadMask(int codePage)
		{
			DbcsLeadBits result = (DbcsLeadBits)0;
			if (codePage >= 1361)
			{
				if (codePage == 1361)
				{
					result = DbcsLeadBits.Lead1361;
				}
				else if (codePage == 10001)
				{
					result = DbcsLeadBits.Lead10001;
				}
				else if (codePage == 10002)
				{
					result = DbcsLeadBits.Lead10002;
				}
				else if (codePage == 10003)
				{
					result = DbcsLeadBits.Lead10003;
				}
				else if (codePage == 10008)
				{
					result = DbcsLeadBits.Lead10008;
				}
			}
			else if (codePage <= 950)
			{
				if (codePage == 950 || codePage == 949 || codePage == 936)
				{
					result = DbcsLeadBits.Lead9XX;
				}
				else if (codePage == 932)
				{
					result = DbcsLeadBits.Lead932;
				}
			}
			return result;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000792BD File Offset: 0x000774BD
		public static bool IsUpperCase(char ch)
		{
			return ch - 'A' <= '\u0019';
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000792CA File Offset: 0x000774CA
		public static bool IsLowerCase(char ch)
		{
			return ch - 'a' <= '\u0019';
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000792D7 File Offset: 0x000774D7
		public static char ToLowerCase(char ch)
		{
			if (!ParseSupport.IsUpperCase(ch))
			{
				return ch;
			}
			return ch + ' ';
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000792E8 File Offset: 0x000774E8
		public static int Latin1MappingInUnicodeControlArea(int value)
		{
			return (int)ParseSupport.latin1MappingInUnicodeControlArea[value - 128];
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000792F7 File Offset: 0x000774F7
		public static bool TwoFarEastNonHanguelChars(char ch1, char ch2)
		{
			return ch1 >= '\u3000' && ch2 >= '\u3000' && !ParseSupport.HanguelRange(ch1) && !ParseSupport.HanguelRange(ch2);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0007931E File Offset: 0x0007751E
		public static bool FarEastNonHanguelChar(char ch)
		{
			return ch >= '\u3000' && !ParseSupport.HanguelRange(ch);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00079333 File Offset: 0x00077533
		private static bool HanguelRange(char ch)
		{
			return ('㄰' <= ch && ch <= '㆏') || ('가' <= ch && ch <= '힣') || ('ﾡ' <= ch && ch <= 'ￜ');
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0007936C File Offset: 0x0007756C
		public static string TrimWhitespace(string value)
		{
			string result = value;
			if (!string.IsNullOrEmpty(value))
			{
				int num = 0;
				int num2 = value.Length;
				if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(value[0])))
				{
					num = 1;
					while (num < num2 && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(value[num])))
					{
						num++;
					}
				}
				if (num != num2)
				{
					if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(value[num2 - 1])))
					{
						num2--;
						while (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(value[num2 - 1])))
						{
							num2--;
						}
					}
					if (num2 - num != value.Length)
					{
						result = value.Substring(num, num2 - num);
					}
				}
				else
				{
					result = string.Empty;
				}
			}
			return result;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00079488 File Offset: 0x00077688
		// Note: this type is marked as 'beforefieldinit'.
		static ParseSupport()
		{
			DbcsLeadBits[] array = new DbcsLeadBits[128];
			array[1] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[2] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[3] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[4] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[5] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[6] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[7] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[8] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[9] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[10] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[11] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[12] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[13] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[14] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[15] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[16] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[17] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[18] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[19] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[20] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[21] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[22] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[23] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[24] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[25] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[26] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[27] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[28] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[29] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[30] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[31] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[32] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead9XX);
			array[33] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[34] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[35] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[36] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[37] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[38] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[39] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[40] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[41] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[42] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead9XX);
			array[43] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead9XX);
			array[44] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead9XX);
			array[45] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead9XX);
			array[46] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead9XX);
			array[47] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead9XX);
			array[48] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[49] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[50] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[51] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[52] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[53] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[54] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[55] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[56] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[57] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[58] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[59] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[60] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[61] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[62] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[63] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[64] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[65] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[66] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[67] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[68] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[69] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[70] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[71] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[72] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[73] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[74] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[75] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[76] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[77] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[78] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[79] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[80] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[81] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[82] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[83] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[84] = (DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[85] = (DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[86] = (DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[87] = (DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[88] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[89] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[90] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[91] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[92] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[93] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[94] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[95] = (DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead9XX);
			array[96] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[97] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[98] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[99] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[100] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[101] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[102] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[103] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[104] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[105] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[106] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[107] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[108] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[109] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[110] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[111] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[112] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[113] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[114] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[115] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[116] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[117] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[118] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[119] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead10008 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[120] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[121] = (DbcsLeadBits.Lead1361 | DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[122] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[123] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[124] = (DbcsLeadBits.Lead10001 | DbcsLeadBits.Lead10002 | DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead932 | DbcsLeadBits.Lead9XX);
			array[125] = (DbcsLeadBits.Lead10003 | DbcsLeadBits.Lead9XX);
			array[126] = DbcsLeadBits.Lead9XX;
			ParseSupport.dbcsLeadTable = array;
			ParseSupport.charToHexTable = new byte[]
			{
				byte.MaxValue,
				10,
				11,
				12,
				13,
				14,
				15,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};
			ParseSupport.octetBitsCount = new byte[]
			{
				0,
				1,
				1,
				2,
				1,
				2,
				2,
				3,
				1,
				2,
				2,
				3,
				2,
				3,
				3,
				4
			};
			ParseSupport.lowCharClass = new CharClass[]
			{
				CharClass.RtfInteresting,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Whitespace | CharClass.RtfInteresting,
				CharClass.Whitespace | CharClass.RtfInteresting,
				CharClass.Whitespace,
				CharClass.Whitespace,
				CharClass.Whitespace | CharClass.RtfInteresting,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Control,
				CharClass.Whitespace,
				CharClass.NotInterestingText,
				CharClass.DoubleQuote,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText | CharClass.HtmlSuffix,
				CharClass.Ampersand,
				CharClass.SingleQuote,
				CharClass.Parentheses,
				CharClass.Parentheses,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.Comma,
				CharClass.NotInterestingText | CharClass.HtmlSuffix,
				CharClass.NotInterestingText,
				CharClass.Solidus,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Numeric,
				CharClass.Colon,
				CharClass.NotInterestingText,
				CharClass.LessThan,
				CharClass.Equals,
				CharClass.GreaterThan | CharClass.HtmlSuffix,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.SquareBrackets,
				CharClass.Backslash | CharClass.RtfInteresting,
				CharClass.SquareBrackets | CharClass.HtmlSuffix,
				CharClass.Circumflex,
				CharClass.NotInterestingText,
				CharClass.GraveAccent,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				(CharClass)2147483656U,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.Alpha,
				CharClass.CurlyBrackets | CharClass.RtfInteresting,
				CharClass.VerticalLine,
				CharClass.CurlyBrackets | CharClass.RtfInteresting,
				CharClass.Tilde,
				CharClass.Control,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.Nbsp,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText,
				CharClass.NotInterestingText
			};
		}

		// Token: 0x0400114F RID: 4431
		private static readonly char[] latin1MappingInUnicodeControlArea = new char[]
		{
			'€',
			'\u0081',
			'‚',
			'ƒ',
			'„',
			'…',
			'†',
			'‡',
			'ˆ',
			'‰',
			'Š',
			'‹',
			'Œ',
			'\u008d',
			'Ž',
			'\u008f',
			'\u0090',
			'‘',
			'’',
			'“',
			'”',
			'•',
			'–',
			'—',
			'˜',
			'™',
			'š',
			'›',
			'œ',
			'\u009d',
			'ž',
			'Ÿ'
		};

		// Token: 0x04001150 RID: 4432
		private static readonly DbcsLeadBits[] dbcsLeadTable;

		// Token: 0x04001151 RID: 4433
		private static readonly byte[] charToHexTable;

		// Token: 0x04001152 RID: 4434
		private static readonly byte[] octetBitsCount;

		// Token: 0x04001153 RID: 4435
		private static readonly CharClass[] lowCharClass;
	}
}
