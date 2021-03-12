using System;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000073 RID: 115
	internal static class MimeScan
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00019192 File Offset: 0x00017392
		public static bool IsLWSP(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Lwsp);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000191A8 File Offset: 0x000173A8
		public static bool IsFWSP(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Fwsp);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000191BE File Offset: 0x000173BE
		public static bool IsCTRL(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Ctl);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000191D0 File Offset: 0x000173D0
		public static bool IsAtom(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Atom);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000191E3 File Offset: 0x000173E3
		public static bool IsToken(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Token);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000191F5 File Offset: 0x000173F5
		public static bool IsAlpha(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Alpha);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001920B File Offset: 0x0001740B
		public static bool IsDigit(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Digit);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001921E File Offset: 0x0001741E
		public static bool IsAlphaOrDigit(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & (MimeScan.Token.Digit | MimeScan.Token.Alpha));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00019234 File Offset: 0x00017434
		public static bool IsHex(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Hex);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00019247 File Offset: 0x00017447
		public static bool IsBChar(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.BChar);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001925D File Offset: 0x0001745D
		public static bool IsField(byte ch)
		{
			return 0 != (short)(MimeScan.Dictionary[(int)ch] & MimeScan.Token.Field);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00019274 File Offset: 0x00017474
		public static bool IsUTF8NonASCII(byte[] bytes, int startOffset, int endOffset, out int bytesUsed)
		{
			if (endOffset == -1)
			{
				endOffset = bytes.Length;
			}
			byte ch = (startOffset < endOffset) ? bytes[startOffset] : 0;
			byte ch2 = (startOffset + 1 < endOffset) ? bytes[startOffset + 1] : 0;
			byte ch3 = (startOffset + 2 < endOffset) ? bytes[startOffset + 2] : 0;
			byte ch4 = (startOffset + 3 < endOffset) ? bytes[startOffset + 3] : 0;
			return ByteString.IsUTF8NonASCII(ch, ch2, ch3, ch4, out bytesUsed);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000192CC File Offset: 0x000174CC
		public static bool IsEncodingRequired(byte ch)
		{
			return 0 == (short)(MimeScan.Dictionary[(int)ch] & (MimeScan.Token.Spec | MimeScan.Token.Atom | MimeScan.Token.Fwsp));
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000192DF File Offset: 0x000174DF
		public static bool IsEscapingRequired(byte ch)
		{
			return ch == 34 || ch == 92 || ch == 13;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000192F2 File Offset: 0x000174F2
		public static bool IsSegmentEncodingRequired(byte ch)
		{
			return (short)(MimeScan.Dictionary[(int)ch] & (MimeScan.Token.Ctl | MimeScan.Token.TSpec | MimeScan.Token.Lwsp)) != 0 || ch == 39 || ch == 42 || ch == 37;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00019318 File Offset: 0x00017518
		public static int FindEndOf(MimeScan.Token token, byte[] bytes, int start, int length, out int characterCount, bool allowUTF8)
		{
			int num = start - 1;
			int num2 = start + length;
			characterCount = 0;
			while (++num < num2)
			{
				if ((short)(MimeScan.Dictionary[(int)bytes[num]] & token) != 0)
				{
					characterCount++;
				}
				else
				{
					if (!allowUTF8 || bytes[num] < 128)
					{
						break;
					}
					int num3 = 0;
					if (!MimeScan.IsUTF8NonASCII(bytes, num, num2, out num3) || (short)((MimeScan.Token.Token | MimeScan.Token.Atom) & token) == 0)
					{
						break;
					}
					num += num3 - 1;
					characterCount++;
				}
			}
			return num - start;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00019388 File Offset: 0x00017588
		public static int FindEndOf(MimeScan.Token token, string value, int currentOffset, bool allowUTF8)
		{
			int num = currentOffset - 1;
			while (++num < value.Length && ((value[num] < '\u0080' && (short)(MimeScan.Dictionary[(int)value[num]] & token) != 0) || (allowUTF8 && value[num] >= '\u0080' && (short)((MimeScan.Token.Token | MimeScan.Token.Atom) & token) != 0)))
			{
			}
			return num - currentOffset;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000193E4 File Offset: 0x000175E4
		public static int FindNextOf(MimeScan.Token token, byte[] bytes, int start, int length, out int characterCount, bool allowUTF8)
		{
			int num = start - 1;
			int num2 = start + length;
			characterCount = 0;
			while (++num < num2 && (short)(MimeScan.Dictionary[(int)bytes[num]] & token) == 0)
			{
				if (allowUTF8 && bytes[num] >= 128)
				{
					int num3 = 0;
					if (MimeScan.IsUTF8NonASCII(bytes, num, num2, out num3))
					{
						if ((short)((MimeScan.Token.Token | MimeScan.Token.Atom) & token) == 0)
						{
							num += num3 - 1;
							characterCount++;
							continue;
						}
						break;
					}
				}
				characterCount++;
			}
			return num - start;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00019454 File Offset: 0x00017654
		public static int SkipLwsp(byte[] bytes, int offset, int length)
		{
			int num = 0;
			return MimeScan.FindEndOf(MimeScan.Token.Lwsp, bytes, offset, length, out num, false);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00019474 File Offset: 0x00017674
		public static int SkipToLwspOrEquals(byte[] bytes, int start, int length)
		{
			int num = start - 1;
			int num2 = start + length;
			while (++num < num2)
			{
				byte b = bytes[num];
				MimeScan.Token token = MimeScan.Dictionary[(int)b];
				if ((short)(token & (MimeScan.Token.TSpec | MimeScan.Token.Lwsp)) != 0 && ((short)(token & MimeScan.Token.Lwsp) != 0 || b == 61))
				{
					break;
				}
			}
			return num - start;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000194BC File Offset: 0x000176BC
		public static int ScanComment(byte[] bytes, int start, int length, bool handleISO2022, ref int level, ref bool quotedPair)
		{
			int num = start - 1;
			int num2 = start + length;
			while (++num < num2)
			{
				byte b = bytes[num];
				if (quotedPair)
				{
					quotedPair = false;
				}
				else if (92 == b)
				{
					quotedPair = true;
				}
				else if (40 == b)
				{
					level++;
				}
				else if (41 == b)
				{
					level--;
					if (level == 0)
					{
						num++;
						break;
					}
				}
				else if (handleISO2022 && (b == 14 || b == 27))
				{
					break;
				}
			}
			return num - start;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001952C File Offset: 0x0001772C
		public static int ScanQuotedString(byte[] bytes, int start, int length, bool handleISO2022, ref bool quotedPair)
		{
			int num = start - 1;
			int num2 = start + length;
			while (++num < num2)
			{
				byte b = bytes[num];
				if (quotedPair)
				{
					quotedPair = false;
				}
				else if (92 == b || 34 == b || (handleISO2022 && (b == 14 || b == 27)))
				{
					break;
				}
			}
			return num - start;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00019574 File Offset: 0x00017774
		public static int ScanJISString(byte[] bytes, int start, int length, ref bool done)
		{
			int num = start - 1;
			int num2 = start + length;
			while (++num < num2)
			{
				byte b = bytes[num];
				if (b < 33)
				{
					done = true;
					break;
				}
			}
			return num - start;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000195A4 File Offset: 0x000177A4
		// Note: this type is marked as 'beforefieldinit'.
		static MimeScan()
		{
			MimeScan.Token[] array = new MimeScan.Token[256];
			array[0] = MimeScan.Token.Ctl;
			array[1] = MimeScan.Token.Ctl;
			array[2] = MimeScan.Token.Ctl;
			array[3] = MimeScan.Token.Ctl;
			array[4] = MimeScan.Token.Ctl;
			array[5] = MimeScan.Token.Ctl;
			array[6] = MimeScan.Token.Ctl;
			array[7] = MimeScan.Token.Ctl;
			array[8] = MimeScan.Token.Ctl;
			array[9] = (MimeScan.Token.Ctl | MimeScan.Token.Lwsp | MimeScan.Token.Fwsp);
			array[10] = (MimeScan.Token.Ctl | MimeScan.Token.Lwsp);
			array[11] = MimeScan.Token.Ctl;
			array[12] = MimeScan.Token.Ctl;
			array[13] = (MimeScan.Token.Ctl | MimeScan.Token.Lwsp);
			array[14] = MimeScan.Token.Ctl;
			array[15] = MimeScan.Token.Ctl;
			array[16] = MimeScan.Token.Ctl;
			array[17] = MimeScan.Token.Ctl;
			array[18] = MimeScan.Token.Ctl;
			array[19] = MimeScan.Token.Ctl;
			array[20] = MimeScan.Token.Ctl;
			array[21] = MimeScan.Token.Ctl;
			array[22] = MimeScan.Token.Ctl;
			array[23] = MimeScan.Token.Ctl;
			array[24] = MimeScan.Token.Ctl;
			array[25] = MimeScan.Token.Ctl;
			array[26] = MimeScan.Token.Ctl;
			array[27] = MimeScan.Token.Ctl;
			array[28] = MimeScan.Token.Ctl;
			array[29] = MimeScan.Token.Ctl;
			array[30] = MimeScan.Token.Ctl;
			array[31] = MimeScan.Token.Ctl;
			array[32] = (MimeScan.Token.Lwsp | MimeScan.Token.BChar | MimeScan.Token.Fwsp);
			array[33] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[34] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[35] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[36] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[37] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[38] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[39] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[40] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[41] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[42] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[43] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[44] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[45] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[46] = (MimeScan.Token.Spec | MimeScan.Token.Token | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[47] = (MimeScan.Token.TSpec | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[48] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[49] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[50] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[51] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[52] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[53] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[54] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[55] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[56] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[57] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Digit | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[58] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.BChar);
			array[59] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[60] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[61] = (MimeScan.Token.TSpec | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[62] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[63] = (MimeScan.Token.TSpec | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[64] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[65] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[66] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[67] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[68] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[69] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[70] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[71] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[72] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[73] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[74] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[75] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[76] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[77] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[78] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[79] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[80] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[81] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[82] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[83] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[84] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[85] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[86] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[87] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[88] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[89] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[90] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[91] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[92] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[93] = (MimeScan.Token.Spec | MimeScan.Token.TSpec | MimeScan.Token.Field);
			array[94] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[95] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field);
			array[96] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[97] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[98] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[99] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[100] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[101] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[102] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Hex | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[103] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[104] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[105] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[106] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[107] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[108] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[109] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[110] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[111] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[112] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[113] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[114] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[115] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[116] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[117] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[118] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[119] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[120] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[121] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[122] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.BChar | MimeScan.Token.Field | MimeScan.Token.Alpha);
			array[123] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[124] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[125] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[126] = (MimeScan.Token.Token | MimeScan.Token.Atom | MimeScan.Token.Field);
			array[127] = MimeScan.Token.Ctl;
			MimeScan.Dictionary = array;
		}

		// Token: 0x0400033D RID: 829
		private static readonly MimeScan.Token[] Dictionary;

		// Token: 0x02000074 RID: 116
		[Flags]
		internal enum Token : short
		{
			// Token: 0x0400033F RID: 831
			Ctl = 1,
			// Token: 0x04000340 RID: 832
			Spec = 2,
			// Token: 0x04000341 RID: 833
			TSpec = 4,
			// Token: 0x04000342 RID: 834
			Token = 8,
			// Token: 0x04000343 RID: 835
			Atom = 16,
			// Token: 0x04000344 RID: 836
			Digit = 32,
			// Token: 0x04000345 RID: 837
			Hex = 64,
			// Token: 0x04000346 RID: 838
			Lwsp = 128,
			// Token: 0x04000347 RID: 839
			BChar = 256,
			// Token: 0x04000348 RID: 840
			Field = 512,
			// Token: 0x04000349 RID: 841
			Alpha = 1024,
			// Token: 0x0400034A RID: 842
			Fwsp = 2048
		}
	}
}
