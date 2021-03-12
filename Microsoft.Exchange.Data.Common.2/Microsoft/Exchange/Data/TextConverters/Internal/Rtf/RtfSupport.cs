using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200025A RID: 602
	internal static class RtfSupport
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x000C7A0D File Offset: 0x000C5C0D
		public static ushort CodePageFromCharRep(RtfSupport.CharRep charRep)
		{
			if ((int)charRep >= RtfSupport.CodePage.Length)
			{
				return 0;
			}
			return RtfSupport.CodePage[(int)charRep];
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000C7A24 File Offset: 0x000C5C24
		public static RtfSupport.CharRep CharRepFromLanguage(int langid)
		{
			short num = (short)(langid & 1023);
			if (num >= 26)
			{
				if (langid == 3098 || langid == 2092 || langid == 2115 || langid == 1104)
				{
					return RtfSupport.CharRep.RUSSIAN_INDEX;
				}
				if ((int)num >= RtfSupport.CharRepFromLID.Length)
				{
					return RtfSupport.CharRep.ANSI_INDEX;
				}
			}
			RtfSupport.CharRep charRep = RtfSupport.CharRepFromLID[(int)num];
			if (!RtfSupport.IsFECharRep(charRep))
			{
				return charRep;
			}
			if (charRep == RtfSupport.CharRep.GB2312_INDEX && langid != 2052 && langid != 4100)
			{
				charRep = RtfSupport.CharRep.BIG5_INDEX;
			}
			return charRep;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000C7A98 File Offset: 0x000C5C98
		public static RtfSupport.CharRep CharRepFromCharSet(int charset)
		{
			byte b = 0;
			while ((int)b < RtfSupport.CharSet.Length)
			{
				if ((int)RtfSupport.CharSet[(int)b] == charset)
				{
					return (RtfSupport.CharRep)b;
				}
				b += 1;
			}
			return RtfSupport.CharRep.UNDEFINED;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public static RtfSupport.CharRep CharRepFromCodePage(ushort codePage)
		{
			byte b = 0;
			while ((int)b < RtfSupport.CodePage.Length)
			{
				if (RtfSupport.CodePage[(int)b] == codePage)
				{
					return (RtfSupport.CharRep)b;
				}
				b += 1;
			}
			if (codePage == 54936)
			{
				return RtfSupport.CharRep.GB18030_INDEX;
			}
			return RtfSupport.CharRep.UNDEFINED;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000C7B08 File Offset: 0x000C5D08
		public static int CharSetFromCodePage(ushort codePage)
		{
			byte b = 0;
			while ((int)b < RtfSupport.CodePage.Length)
			{
				if (RtfSupport.CodePage[(int)b] == codePage)
				{
					return (int)RtfSupport.CharSet[(int)b];
				}
				b += 1;
			}
			return 1;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000C7B3B File Offset: 0x000C5D3B
		public static bool IsBiDiCharRep(RtfSupport.CharRep charRep)
		{
			return (RtfSupport.CharRep.HEBREW_INDEX <= charRep && charRep <= RtfSupport.CharRep.ARABIC_INDEX) || (RtfSupport.CharRep.SYRIAC_INDEX <= charRep && charRep <= RtfSupport.CharRep.THAANA_INDEX);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000C7B56 File Offset: 0x000C5D56
		public static bool IsRtlCharSet(int charset)
		{
			return 177 <= charset && charset <= 178;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000C7B70 File Offset: 0x000C5D70
		public static bool IsBiDiLanguage(int langid)
		{
			short num = (short)(langid & 1023);
			return num == 1 || num == 13 || num == 32 || num == 41 || num == 90 || num == 101;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000C7BA8 File Offset: 0x000C5DA8
		public static bool IsHebrewLanguage(int langid)
		{
			short num = (short)(langid & 1023);
			return num == 13;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000C7BC4 File Offset: 0x000C5DC4
		public static bool IsArabicLanguage(int langid)
		{
			short num = (short)(langid & 1023);
			return num == 1 || num == 32 || num == 41 || num == 90 || num == 101;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000C7BF4 File Offset: 0x000C5DF4
		public static bool IsThaiLanguage(int langid)
		{
			short num = (short)(langid & 1023);
			return num == 30;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000C7C0F File Offset: 0x000C5E0F
		public static bool IsFECharRep(RtfSupport.CharRep charRep)
		{
			return RtfSupport.CharRep.SHIFTJIS_INDEX <= charRep && charRep <= RtfSupport.CharRep.BIG5_INDEX;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000C7C20 File Offset: 0x000C5E20
		public static int RGB(int red, int green, int blue)
		{
			return (int)((byte)blue) << 16 | (int)((byte)green) << 8 | (int)((byte)red);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000C7C2F File Offset: 0x000C5E2F
		public static int Unescape(byte b1, byte b2)
		{
			if (ParseSupport.HexCharacter(ParseSupport.GetCharClass((char)b1)) && ParseSupport.HexCharacter(ParseSupport.GetCharClass((char)b2)))
			{
				return ParseSupport.CharToHex((char)b1) << 4 | ParseSupport.CharToHex((char)b2);
			}
			return 256;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000C7C60 File Offset: 0x000C5E60
		public static void Escape(char ch, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)"0123456789ABCDEF"[(int)(ch >> 4 & '\u000f')];
			buffer[offset + 1] = (byte)"0123456789ABCDEF"[(int)(ch & '\u000f')];
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000C7C8C File Offset: 0x000C5E8C
		public static bool IsHyperlinkField(ref ScratchBuffer scratch, out bool local, out BufferString linkUrl)
		{
			int num = 0;
			int length = scratch.Length;
			while (num != scratch.Length && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(scratch[num])))
			{
				num++;
			}
			if (scratch.Length - num > 10 && scratch[num] == 'H' && scratch[num + 1] == 'Y' && scratch[num + 2] == 'P' && scratch[num + 3] == 'E' && scratch[num + 4] == 'R' && scratch[num + 5] == 'L' && scratch[num + 6] == 'I' && scratch[num + 7] == 'N' && scratch[num + 8] == 'K' && scratch[num + 9] == ' ')
			{
				num += 10;
				int num2;
				int num3;
				int num4;
				int num5;
				int fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
				if (num3 == 2 && scratch[num2] == '\\' && scratch[num2 + 1] == 'l')
				{
					local = true;
					num += fieldArgument;
					fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
				}
				else
				{
					local = false;
				}
				if (num5 != 0)
				{
					if (local)
					{
						num4--;
						num5++;
						scratch[num4] = '#';
					}
					linkUrl = scratch.SubString(num4, num5);
					return true;
				}
			}
			local = false;
			linkUrl = BufferString.Null;
			return false;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000C7DFC File Offset: 0x000C5FFC
		public static bool IsIncludePictureField(ref ScratchBuffer scratch, out BufferString linkUrl)
		{
			int num = 0;
			int length = scratch.Length;
			while (num != scratch.Length && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(scratch[num])))
			{
				num++;
			}
			if (scratch.Length - num > 15 && scratch[num] == 'I' && scratch[num + 1] == 'N' && scratch[num + 2] == 'C' && scratch[num + 3] == 'L' && scratch[num + 4] == 'U' && scratch[num + 5] == 'D' && scratch[num + 6] == 'E' && scratch[num + 7] == 'P' && scratch[num + 8] == 'I' && scratch[num + 9] == 'C' && scratch[num + 10] == 'T' && scratch[num + 11] == 'U' && scratch[num + 12] == 'R' && scratch[num + 13] == 'E' && scratch[num + 14] == ' ')
			{
				num += 15;
				int offset;
				int num2;
				int offset2;
				int num3;
				int fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out offset, out num2, out offset2, out num3);
				while (num2 == 2 && scratch[offset] == '\\')
				{
					num += fieldArgument;
					fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out offset, out num2, out offset2, out num3);
				}
				if (num3 > 2)
				{
					linkUrl = scratch.SubString(offset2, num3);
					return true;
				}
			}
			linkUrl = BufferString.Null;
			return false;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000C7F8C File Offset: 0x000C618C
		public static bool IsSymbolField(ref ScratchBuffer scratch, out TextMapping textMapping, out char symbol, out short points)
		{
			textMapping = TextMapping.Unicode;
			symbol = '\0';
			points = 0;
			int num = 0;
			int length = scratch.Length;
			while (num != scratch.Length && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(scratch[num])))
			{
				num++;
			}
			if (scratch.Length - num <= 7 || scratch[num] != 'S' || scratch[num + 1] != 'Y' || scratch[num + 2] != 'M' || scratch[num + 3] != 'B' || scratch[num + 4] != 'O' || scratch[num + 5] != 'L' || scratch[num + 6] != ' ')
			{
				return false;
			}
			num += 7;
			int num2;
			int num3;
			int num4;
			int num5;
			int fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
			int num6;
			char c;
			if (num3 > 2 && scratch.Buffer[num2] == '0' && (ushort)(scratch.Buffer[num2 + 1] | ' ') == 120)
			{
				num6 = 2;
				while (num6 < num3 && (c = scratch.Buffer[num2 + num6]) <= 'f')
				{
					if (('0' > c || c > '9') && ('a' > c || c > 'f'))
					{
						if ('A' > c)
						{
							break;
						}
						if (c > 'F')
						{
							break;
						}
					}
					symbol = (symbol << 4) + ((c <= '9') ? (c - '0') : ((c & 'O') - 'A' + '\n'));
					num6++;
				}
			}
			else
			{
				num6 = 0;
				while (num6 < num3 && (c = scratch.Buffer[num2 + num6]) <= '9' && '0' <= c)
				{
					symbol = '\n' * symbol + (c - '0');
					num6++;
				}
			}
			num += fieldArgument;
			fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
			if (num3 != 2 || scratch[num2] != '\\' || scratch[num2 + 1] != 'f')
			{
				return false;
			}
			num += fieldArgument;
			fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
			if (num5 == 0)
			{
				return false;
			}
			RecognizeInterestingFontName recognizeInterestingFontName = default(RecognizeInterestingFontName);
			num6 = 0;
			while (num6 < num5 && !recognizeInterestingFontName.IsRejected)
			{
				recognizeInterestingFontName.AddCharacter(scratch.Buffer[num4 + num6]);
				num6++;
			}
			textMapping = recognizeInterestingFontName.TextMapping;
			if (textMapping == TextMapping.Unicode)
			{
				textMapping = TextMapping.OtherSymbol;
			}
			num += fieldArgument;
			fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
			if (num3 != 2 || scratch[num2] != '\\' || scratch[num2 + 1] != 's')
			{
				return true;
			}
			num += fieldArgument;
			fieldArgument = RtfSupport.GetFieldArgument(ref scratch, num, out num2, out num3, out num4, out num5);
			num6 = 0;
			while (num6 < num3 && (c = scratch.Buffer[num2 + num6]) <= '9' && '0' <= c)
			{
				points = 10 * points + (short)(c - '0');
				num6++;
			}
			return true;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000C8238 File Offset: 0x000C6438
		private static int GetFieldArgument(ref ScratchBuffer scratch, int offset, out int rawResultOffset, out int rawResultLength, out int unescapedResultOffset, out int unescapedResultLength)
		{
			int length = scratch.Length;
			bool flag = false;
			int num = 0;
			while (offset < length && scratch[offset] == ' ')
			{
				offset++;
				num++;
			}
			if (offset < length && scratch[offset] == '"')
			{
				flag = true;
				offset++;
				num++;
			}
			rawResultOffset = offset;
			rawResultLength = 0;
			unescapedResultOffset = length;
			unescapedResultLength = 0;
			while (offset < length)
			{
				char c = scratch[offset];
				if ((c == '"' && flag) || (c == ' ' && !flag))
				{
					num++;
					break;
				}
				if (c == '\\')
				{
					offset++;
					num++;
					rawResultLength++;
					if (offset == length)
					{
						break;
					}
					c = scratch[offset];
				}
				if (scratch.Append(c, 5120) != 0)
				{
					unescapedResultLength++;
				}
				rawResultLength++;
				offset++;
				num++;
			}
			scratch.Length = length;
			return num;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000C8304 File Offset: 0x000C6504
		public static string StringFontNameFromScratch(ScratchBuffer scratch)
		{
			int num = 0;
			int num2;
			for (num2 = scratch.Length; num2 != 0; num2--)
			{
				if (scratch[num2 - 1] != ';' && !ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(scratch[num2 - 1])))
				{
					break;
				}
			}
			while (num2 != 0 && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(scratch[num])))
			{
				num++;
				num2--;
			}
			if (num2 != 0 && scratch[num] != '?')
			{
				return scratch.ToString(num, num2);
			}
			return null;
		}

		// Token: 0x04001D56 RID: 7510
		public const int RtfNestingLimit = 4096;

		// Token: 0x04001D57 RID: 7511
		public const int MaxBookmarkNameLength = 4096;

		// Token: 0x04001D58 RID: 7512
		public const int MaxFieldInstructionLength = 4096;

		// Token: 0x04001D59 RID: 7513
		public const int MaxFontNameLength = 256;

		// Token: 0x04001D5A RID: 7514
		public const int MaxUrlLength = 1024;

		// Token: 0x04001D5B RID: 7515
		public const int MaxShapePropertyName = 128;

		// Token: 0x04001D5C RID: 7516
		public const int MaxShapePropertyValue = 4096;

		// Token: 0x04001D5D RID: 7517
		public const byte LANG_NEUTRAL = 0;

		// Token: 0x04001D5E RID: 7518
		public const byte LANG_AFRIKAANS = 54;

		// Token: 0x04001D5F RID: 7519
		public const byte LANG_ALBANIAN = 28;

		// Token: 0x04001D60 RID: 7520
		public const byte LANG_ARABIC = 1;

		// Token: 0x04001D61 RID: 7521
		public const byte LANG_BASQUE = 45;

		// Token: 0x04001D62 RID: 7522
		public const byte LANG_BELARUSIAN = 35;

		// Token: 0x04001D63 RID: 7523
		public const byte LANG_BULGARIAN = 2;

		// Token: 0x04001D64 RID: 7524
		public const byte LANG_CATALAN = 3;

		// Token: 0x04001D65 RID: 7525
		public const byte LANG_CHINESE = 4;

		// Token: 0x04001D66 RID: 7526
		public const byte LANG_CROATIAN = 26;

		// Token: 0x04001D67 RID: 7527
		public const byte LANG_CZECH = 5;

		// Token: 0x04001D68 RID: 7528
		public const byte LANG_DANISH = 6;

		// Token: 0x04001D69 RID: 7529
		public const byte LANG_DUTCH = 19;

		// Token: 0x04001D6A RID: 7530
		public const byte LANG_ENGLISH = 9;

		// Token: 0x04001D6B RID: 7531
		public const byte LANG_ESTONIAN = 37;

		// Token: 0x04001D6C RID: 7532
		public const byte LANG_FAEROESE = 56;

		// Token: 0x04001D6D RID: 7533
		public const byte LANG_FARSI = 41;

		// Token: 0x04001D6E RID: 7534
		public const byte LANG_FINNISH = 11;

		// Token: 0x04001D6F RID: 7535
		public const byte LANG_FRENCH = 12;

		// Token: 0x04001D70 RID: 7536
		public const byte LANG_GERMAN = 7;

		// Token: 0x04001D71 RID: 7537
		public const byte LANG_GREEK = 8;

		// Token: 0x04001D72 RID: 7538
		public const byte LANG_HEBREW = 13;

		// Token: 0x04001D73 RID: 7539
		public const byte LANG_HUNGARIAN = 14;

		// Token: 0x04001D74 RID: 7540
		public const byte LANG_ICELANDIC = 15;

		// Token: 0x04001D75 RID: 7541
		public const byte LANG_INDONESIAN = 33;

		// Token: 0x04001D76 RID: 7542
		public const byte LANG_ITALIAN = 16;

		// Token: 0x04001D77 RID: 7543
		public const byte LANG_JAPANESE = 17;

		// Token: 0x04001D78 RID: 7544
		public const byte LANG_KOREAN = 18;

		// Token: 0x04001D79 RID: 7545
		public const byte LANG_LATVIAN = 38;

		// Token: 0x04001D7A RID: 7546
		public const byte LANG_LITHUANIAN = 39;

		// Token: 0x04001D7B RID: 7547
		public const byte LANG_NORWEGIAN = 20;

		// Token: 0x04001D7C RID: 7548
		public const byte LANG_POLISH = 21;

		// Token: 0x04001D7D RID: 7549
		public const byte LANG_PORTUGUESE = 22;

		// Token: 0x04001D7E RID: 7550
		public const byte LANG_ROMANIAN = 24;

		// Token: 0x04001D7F RID: 7551
		public const byte LANG_RUSSIAN = 25;

		// Token: 0x04001D80 RID: 7552
		public const byte LANG_SERBIAN = 26;

		// Token: 0x04001D81 RID: 7553
		public const byte LANG_SLOVAK = 27;

		// Token: 0x04001D82 RID: 7554
		public const byte LANG_SLOVENIAN = 36;

		// Token: 0x04001D83 RID: 7555
		public const byte LANG_SPANISH = 10;

		// Token: 0x04001D84 RID: 7556
		public const byte LANG_SWEDISH = 29;

		// Token: 0x04001D85 RID: 7557
		public const byte LANG_THAI = 30;

		// Token: 0x04001D86 RID: 7558
		public const byte LANG_TURKISH = 31;

		// Token: 0x04001D87 RID: 7559
		public const byte LANG_UKRAINIAN = 34;

		// Token: 0x04001D88 RID: 7560
		public const byte LANG_VIETNAMESE = 42;

		// Token: 0x04001D89 RID: 7561
		public const byte LANG_URDU = 32;

		// Token: 0x04001D8A RID: 7562
		public const byte LANG_SYRIAC = 90;

		// Token: 0x04001D8B RID: 7563
		public const byte LANG_DIVEHI = 101;

		// Token: 0x04001D8C RID: 7564
		public const short LID_SERBIAN_CYRILLIC = 3098;

		// Token: 0x04001D8D RID: 7565
		public const short LID_AZERI_CYRILLIC = 2092;

		// Token: 0x04001D8E RID: 7566
		public const short LID_UZBEK_CYRILLIC = 2115;

		// Token: 0x04001D8F RID: 7567
		public const short LID_MONGOLIAN_CYRILLIC = 1104;

		// Token: 0x04001D90 RID: 7568
		public const short LID_PRC = 2052;

		// Token: 0x04001D91 RID: 7569
		public const short LID_SINGAPORE = 4100;

		// Token: 0x04001D92 RID: 7570
		public const byte ANSI_CHARSET = 0;

		// Token: 0x04001D93 RID: 7571
		public const byte DEFAULT_CHARSET = 1;

		// Token: 0x04001D94 RID: 7572
		public const byte SYMBOL_CHARSET = 2;

		// Token: 0x04001D95 RID: 7573
		public const byte SHIFTJIS_CHARSET = 128;

		// Token: 0x04001D96 RID: 7574
		public const byte HANGEUL_CHARSET = 129;

		// Token: 0x04001D97 RID: 7575
		public const byte HANGUL_CHARSET = 129;

		// Token: 0x04001D98 RID: 7576
		public const byte GB2312_CHARSET = 134;

		// Token: 0x04001D99 RID: 7577
		public const byte CHINESEBIG5_CHARSET = 136;

		// Token: 0x04001D9A RID: 7578
		public const byte OEM_CHARSET = 255;

		// Token: 0x04001D9B RID: 7579
		public const byte JOHAB_CHARSET = 130;

		// Token: 0x04001D9C RID: 7580
		public const byte HEBREW_CHARSET = 177;

		// Token: 0x04001D9D RID: 7581
		public const byte ARABIC_CHARSET = 178;

		// Token: 0x04001D9E RID: 7582
		public const byte ARABIC1_CHARSET = 180;

		// Token: 0x04001D9F RID: 7583
		public const byte GREEK_CHARSET = 161;

		// Token: 0x04001DA0 RID: 7584
		public const byte TURKISH_CHARSET = 162;

		// Token: 0x04001DA1 RID: 7585
		public const byte VIETNAMESE_CHARSET = 163;

		// Token: 0x04001DA2 RID: 7586
		public const byte THAI_CHARSET = 222;

		// Token: 0x04001DA3 RID: 7587
		public const byte EASTEUROPE_CHARSET = 238;

		// Token: 0x04001DA4 RID: 7588
		public const byte RUSSIAN_CHARSET = 204;

		// Token: 0x04001DA5 RID: 7589
		public const byte MAC_CHARSET = 77;

		// Token: 0x04001DA6 RID: 7590
		public const byte BALTIC_CHARSET = 186;

		// Token: 0x04001DA7 RID: 7591
		public const byte PC437_CHARSET = 254;

		// Token: 0x04001DA8 RID: 7592
		public const ushort CP_SYMBOL = 42;

		// Token: 0x04001DA9 RID: 7593
		private const string HexCharacters = "0123456789ABCDEF";

		// Token: 0x04001DAA RID: 7594
		private static readonly RtfSupport.CharRep[] CharRepFromLID = new RtfSupport.CharRep[]
		{
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.ARABIC_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.GB2312_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.GREEK_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.HEBREW_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.SHIFTJIS_INDEX,
			RtfSupport.CharRep.HANGUL_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.THAI_INDEX,
			RtfSupport.CharRep.TURKISH_INDEX,
			RtfSupport.CharRep.ARABIC_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.EASTEUROPE_INDEX,
			RtfSupport.CharRep.BALTIC_INDEX,
			RtfSupport.CharRep.BALTIC_INDEX,
			RtfSupport.CharRep.BALTIC_INDEX,
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.ARABIC_INDEX,
			RtfSupport.CharRep.VIET_INDEX,
			RtfSupport.CharRep.NCHARSETS,
			RtfSupport.CharRep.TURKISH_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.GEORGIAN_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.DEVANAGARI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.HEBREW_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.RUSSIAN_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.TURKISH_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.BENGALI_INDEX,
			RtfSupport.CharRep.GURMUKHI_INDEX,
			RtfSupport.CharRep.GUJARATI_INDEX,
			RtfSupport.CharRep.ORIYA_INDEX,
			RtfSupport.CharRep.TAMIL_INDEX,
			RtfSupport.CharRep.TELUGU_INDEX,
			RtfSupport.CharRep.KANNADA_INDEX,
			RtfSupport.CharRep.MALAYALAM_INDEX,
			RtfSupport.CharRep.BENGALI_INDEX,
			RtfSupport.CharRep.DEVANAGARI_INDEX,
			RtfSupport.CharRep.DEVANAGARI_INDEX,
			RtfSupport.CharRep.MONGOLIAN_INDEX,
			RtfSupport.CharRep.TIBETAN_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.KHMER_INDEX,
			RtfSupport.CharRep.LAO_INDEX,
			RtfSupport.CharRep.MYANMAR_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.DEVANAGARI_INDEX,
			RtfSupport.CharRep.BENGALI_INDEX,
			RtfSupport.CharRep.GURMUKHI_INDEX,
			RtfSupport.CharRep.SYRIAC_INDEX,
			RtfSupport.CharRep.SINHALA_INDEX,
			RtfSupport.CharRep.CHEROKEE_INDEX,
			RtfSupport.CharRep.ABORIGINAL_INDEX,
			RtfSupport.CharRep.ETHIOPIC_INDEX,
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.DEFAULT_INDEX,
			RtfSupport.CharRep.DEVANAGARI_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.ARABIC_INDEX,
			RtfSupport.CharRep.ANSI_INDEX,
			RtfSupport.CharRep.THAANA_INDEX
		};

		// Token: 0x04001DAB RID: 7595
		private static readonly ushort[] CodePage = new ushort[]
		{
			1252,
			1250,
			1251,
			1253,
			1254,
			1255,
			1256,
			1257,
			1258,
			0,
			42,
			874,
			932,
			936,
			949,
			950,
			437,
			850,
			10000,
			1256
		};

		// Token: 0x04001DAC RID: 7596
		private static readonly byte[] CharSet = new byte[]
		{
			0,
			238,
			204,
			161,
			162,
			177,
			178,
			186,
			163,
			1,
			2,
			222,
			128,
			134,
			129,
			136,
			254,
			byte.MaxValue,
			77,
			180
		};

		// Token: 0x04001DAD RID: 7597
		public static readonly byte[] UnsafeAsciiMap = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x0200025B RID: 603
		public enum CharRep : byte
		{
			// Token: 0x04001DAF RID: 7599
			ANSI_INDEX,
			// Token: 0x04001DB0 RID: 7600
			EASTEUROPE_INDEX,
			// Token: 0x04001DB1 RID: 7601
			RUSSIAN_INDEX,
			// Token: 0x04001DB2 RID: 7602
			GREEK_INDEX,
			// Token: 0x04001DB3 RID: 7603
			TURKISH_INDEX,
			// Token: 0x04001DB4 RID: 7604
			HEBREW_INDEX,
			// Token: 0x04001DB5 RID: 7605
			ARABIC_INDEX,
			// Token: 0x04001DB6 RID: 7606
			BALTIC_INDEX,
			// Token: 0x04001DB7 RID: 7607
			VIET_INDEX,
			// Token: 0x04001DB8 RID: 7608
			DEFAULT_INDEX,
			// Token: 0x04001DB9 RID: 7609
			SYMBOL_INDEX,
			// Token: 0x04001DBA RID: 7610
			THAI_INDEX,
			// Token: 0x04001DBB RID: 7611
			SHIFTJIS_INDEX,
			// Token: 0x04001DBC RID: 7612
			GB2312_INDEX,
			// Token: 0x04001DBD RID: 7613
			HANGUL_INDEX,
			// Token: 0x04001DBE RID: 7614
			BIG5_INDEX,
			// Token: 0x04001DBF RID: 7615
			PC437_INDEX,
			// Token: 0x04001DC0 RID: 7616
			OEM_INDEX,
			// Token: 0x04001DC1 RID: 7617
			MAC_INDEX,
			// Token: 0x04001DC2 RID: 7618
			ARABIC1_INDEX,
			// Token: 0x04001DC3 RID: 7619
			NCHARSETS,
			// Token: 0x04001DC4 RID: 7620
			ARMENIAN_INDEX = 20,
			// Token: 0x04001DC5 RID: 7621
			SYRIAC_INDEX,
			// Token: 0x04001DC6 RID: 7622
			THAANA_INDEX,
			// Token: 0x04001DC7 RID: 7623
			DEVANAGARI_INDEX,
			// Token: 0x04001DC8 RID: 7624
			BENGALI_INDEX,
			// Token: 0x04001DC9 RID: 7625
			GURMUKHI_INDEX,
			// Token: 0x04001DCA RID: 7626
			GUJARATI_INDEX,
			// Token: 0x04001DCB RID: 7627
			ORIYA_INDEX,
			// Token: 0x04001DCC RID: 7628
			TAMIL_INDEX,
			// Token: 0x04001DCD RID: 7629
			TELUGU_INDEX,
			// Token: 0x04001DCE RID: 7630
			KANNADA_INDEX,
			// Token: 0x04001DCF RID: 7631
			MALAYALAM_INDEX,
			// Token: 0x04001DD0 RID: 7632
			SINHALA_INDEX,
			// Token: 0x04001DD1 RID: 7633
			LAO_INDEX,
			// Token: 0x04001DD2 RID: 7634
			TIBETAN_INDEX,
			// Token: 0x04001DD3 RID: 7635
			MYANMAR_INDEX,
			// Token: 0x04001DD4 RID: 7636
			GEORGIAN_INDEX,
			// Token: 0x04001DD5 RID: 7637
			JAMO_INDEX,
			// Token: 0x04001DD6 RID: 7638
			ETHIOPIC_INDEX,
			// Token: 0x04001DD7 RID: 7639
			CHEROKEE_INDEX,
			// Token: 0x04001DD8 RID: 7640
			ABORIGINAL_INDEX,
			// Token: 0x04001DD9 RID: 7641
			OGHAM_INDEX,
			// Token: 0x04001DDA RID: 7642
			RUNIC_INDEX,
			// Token: 0x04001DDB RID: 7643
			KHMER_INDEX,
			// Token: 0x04001DDC RID: 7644
			MONGOLIAN_INDEX,
			// Token: 0x04001DDD RID: 7645
			BRAILLE_INDEX,
			// Token: 0x04001DDE RID: 7646
			YI_INDEX,
			// Token: 0x04001DDF RID: 7647
			JPN2_INDEX,
			// Token: 0x04001DE0 RID: 7648
			CHS2_INDEX,
			// Token: 0x04001DE1 RID: 7649
			KOR2_INDEX,
			// Token: 0x04001DE2 RID: 7650
			CHT2_INDEX,
			// Token: 0x04001DE3 RID: 7651
			GB18030_INDEX,
			// Token: 0x04001DE4 RID: 7652
			NCHARREPERTOIRES,
			// Token: 0x04001DE5 RID: 7653
			UNDEFINED = 255
		}
	}
}
