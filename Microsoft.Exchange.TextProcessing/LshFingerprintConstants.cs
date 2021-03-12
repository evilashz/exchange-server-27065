using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000013 RID: 19
	internal class LshFingerprintConstants
	{
		// Token: 0x04000071 RID: 113
		public const int MaximumContentLength = 50000;

		// Token: 0x04000072 RID: 114
		public const int MaxShingleLength = 8000;

		// Token: 0x04000073 RID: 115
		public const short AlgorithmVersion = 2;

		// Token: 0x04000074 RID: 116
		public const int HashPermutation = 64;

		// Token: 0x04000075 RID: 117
		public const int HashBinNumbers = 4;

		// Token: 0x04000076 RID: 118
		public const int HashBits = 2;

		// Token: 0x04000077 RID: 119
		public const int CharBits = 16;

		// Token: 0x04000078 RID: 120
		public const int FingerprintDenorminator = 48;

		// Token: 0x04000079 RID: 121
		public const int FingerprintDenorminatorOne = 32;

		// Token: 0x0400007A RID: 122
		public const int FingerprintNumeratorOffset = 16;

		// Token: 0x0400007B RID: 123
		public const int ShortBits = 16;

		// Token: 0x0400007C RID: 124
		public const uint BBits = 3U;

		// Token: 0x0400007D RID: 125
		public const uint BBitsOne = 1U;

		// Token: 0x0400007E RID: 126
		public const int MinimumShingleCountExclude = 10;

		// Token: 0x0400007F RID: 127
		public const int OneBitMask = 1431655765;

		// Token: 0x04000080 RID: 128
		public const ulong Fnv64OffsetBasis = 14695981039346656037UL;

		// Token: 0x04000081 RID: 129
		public const ulong Fnv64Prime = 1099511628211UL;

		// Token: 0x04000082 RID: 130
		public const uint Fnv32OffsetBasis = 2166136261U;

		// Token: 0x04000083 RID: 131
		public const uint Fnv32Prime = 16777619U;

		// Token: 0x04000084 RID: 132
		public const int ReasonableTriesForFingreprintConflicts = 4;

		// Token: 0x04000085 RID: 133
		public static readonly char[] DotDelimit = new char[]
		{
			'.'
		};

		// Token: 0x04000086 RID: 134
		public static readonly char[] BreakerRange = new char[]
		{
			'0',
			'9',
			'A',
			'Z',
			'a',
			'﻿',
			'Ａ',
			'⿿',
			'぀',
			'ｚ',
			'ｦ',
			'‐',
			'⷟'
		};

		// Token: 0x04000087 RID: 135
		public static readonly char[] UrlStart = new char[]
		{
			'h',
			't',
			't',
			'p',
			's',
			':',
			'/',
			'/'
		};

		// Token: 0x04000088 RID: 136
		public static readonly char[] UrlEnd = new char[]
		{
			' ',
			'>'
		};

		// Token: 0x04000089 RID: 137
		public static readonly char[] DomanEnd = new char[]
		{
			'/',
			':',
			' ',
			'>'
		};
	}
}
