using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000033 RID: 51
	internal static class StringHelper
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public static bool IsWhitespaceCharacter(char ch)
		{
			return (StringHelper.FindMask(ch) & 16384) != 0;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D850 File Offset: 0x0000BA50
		public static bool IsLeftHandSideDelimiter(char ch, BoundaryType boundaryType)
		{
			switch (boundaryType)
			{
			case BoundaryType.None:
			case BoundaryType.NormalRightOnly:
				return true;
			case BoundaryType.Normal:
			case BoundaryType.NormalLeftOnly:
				return (StringHelper.FindMask(ch) & 32768) != 0;
			case BoundaryType.Url:
				return (StringHelper.FindMask(ch) & 8192) != 0;
			case BoundaryType.FullUrl:
				return (StringHelper.FindMask(ch) & 4096) != 0;
			default:
				throw new InvalidOperationException(Strings.InvalidBoundaryType(boundaryType.ToString()));
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public static bool IsRightHandSideDelimiter(char ch, BoundaryType boundaryType)
		{
			switch (boundaryType)
			{
			case BoundaryType.None:
			case BoundaryType.NormalLeftOnly:
				return true;
			case BoundaryType.Normal:
			case BoundaryType.NormalRightOnly:
				return (StringHelper.FindMask(ch) & 32768) != 0;
			case BoundaryType.Url:
			case BoundaryType.FullUrl:
				return ch != '.' && ch != '@' && ch != '-' && !char.IsLetterOrDigit(ch);
			default:
				throw new InvalidOperationException(Strings.InvalidBoundaryType(boundaryType.ToString()));
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D94D File Offset: 0x0000BB4D
		public static string NormalizeKeyword(string keyword)
		{
			return StringHelper.NormalizeString(StringHelper.removeSpaces.Replace(keyword.Trim(), " "));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D969 File Offset: 0x0000BB69
		public static string NormalizeString(string value)
		{
			return value.ToUpperInvariant();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D971 File Offset: 0x0000BB71
		public static ushort FindMask(char ch)
		{
			if ((int)ch > StringHelper.MaximumLookupIndex)
			{
				return 0;
			}
			return StringHelper.CharacterLookupTable[(int)ch];
		}

		// Token: 0x04000111 RID: 273
		private const ushort NoFlag = 0;

		// Token: 0x04000112 RID: 274
		private const ushort CustomFlag1 = 1;

		// Token: 0x04000113 RID: 275
		private const ushort CustomFlag2 = 2;

		// Token: 0x04000114 RID: 276
		private const ushort ABFlag = 4;

		// Token: 0x04000115 RID: 277
		private const ushort CDFlag = 8;

		// Token: 0x04000116 RID: 278
		private const ushort EFFlag = 16;

		// Token: 0x04000117 RID: 279
		private const ushort GHFlag = 32;

		// Token: 0x04000118 RID: 280
		private const ushort IJFlag = 64;

		// Token: 0x04000119 RID: 281
		private const ushort KLMFlag = 128;

		// Token: 0x0400011A RID: 282
		private const ushort NOPFlag = 256;

		// Token: 0x0400011B RID: 283
		private const ushort QRSFlag = 512;

		// Token: 0x0400011C RID: 284
		private const ushort TUVFlag = 1024;

		// Token: 0x0400011D RID: 285
		private const ushort WXYZFlag = 2048;

		// Token: 0x0400011E RID: 286
		private const ushort FullUrlFlag = 4096;

		// Token: 0x0400011F RID: 287
		private const ushort UrlFlag = 8192;

		// Token: 0x04000120 RID: 288
		private const ushort WhitespaceFlag = 16384;

		// Token: 0x04000121 RID: 289
		private const ushort NormalBoundaryFlag = 32768;

		// Token: 0x04000122 RID: 290
		private static readonly Regex removeSpaces = new Regex("\\s+", RegexOptions.Compiled);

		// Token: 0x04000123 RID: 291
		private static readonly ushort[] CharacterLookupTable = new ushort[]
		{
			32768,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			61440,
			61440,
			0,
			61440,
			61440,
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
			61440,
			32768,
			45056,
			32768,
			32768,
			32768,
			32768,
			45056,
			32768,
			32768,
			32768,
			32768,
			32768,
			32768,
			40960,
			45056,
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
			32768,
			45056,
			32768,
			32768,
			32768,
			32768,
			45056,
			4,
			4,
			8,
			8,
			16,
			16,
			32,
			32,
			64,
			64,
			128,
			128,
			128,
			256,
			256,
			256,
			512,
			512,
			512,
			1024,
			1024,
			1024,
			2048,
			2048,
			2048,
			2048,
			32768,
			32768,
			32768,
			32768,
			32768,
			32768,
			4,
			4,
			8,
			8,
			16,
			16,
			32,
			32,
			64,
			64,
			128,
			128,
			128,
			256,
			256,
			256,
			512,
			512,
			512,
			1024,
			1024,
			1024,
			2048,
			2048,
			2048,
			2048,
			32768,
			32768,
			32768,
			32768,
			0
		};

		// Token: 0x04000124 RID: 292
		private static readonly int MaximumLookupIndex = StringHelper.CharacterLookupTable.Length - 1;
	}
}
