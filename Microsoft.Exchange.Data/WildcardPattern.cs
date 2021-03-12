using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E3 RID: 483
	internal class WildcardPattern
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x00032D5F File Offset: 0x00030F5F
		public WildcardPattern(string pattern)
		{
			this.patternType = WildcardPattern.NormalizePattern(pattern, out this.pattern);
			if (WildcardPattern.PatternType.Mixed == this.patternType)
			{
				this.splitPattern = this.pattern.Split(WildcardPattern.WildcardChars, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00032D99 File Offset: 0x00030F99
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00032DA1 File Offset: 0x00030FA1
		public WildcardPattern.PatternType Type
		{
			get
			{
				return this.patternType;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00032DA9 File Offset: 0x00030FA9
		private bool StartsWithWildcard
		{
			get
			{
				return '*' == this.pattern[0];
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00032DBB File Offset: 0x00030FBB
		private bool EndsWithWildcard
		{
			get
			{
				return '*' == this.pattern[this.pattern.Length - 1];
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00032DD9 File Offset: 0x00030FD9
		public bool Equals(WildcardPattern other)
		{
			return this.pattern.Equals(other.pattern, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00032DF0 File Offset: 0x00030FF0
		public override bool Equals(object other)
		{
			WildcardPattern wildcardPattern = other as WildcardPattern;
			return wildcardPattern != null && this.Equals(wildcardPattern);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00032E10 File Offset: 0x00031010
		public override int GetHashCode()
		{
			return this.pattern.GetHashCode();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00032E1D File Offset: 0x0003101D
		public int Match(string address)
		{
			return this.Match(address, '\0');
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00032E28 File Offset: 0x00031028
		public int Match(string address, char singleCharWildcard)
		{
			int num = 0;
			if (this.patternType == WildcardPattern.PatternType.NoWildcards)
			{
				num = WildcardPattern.WildcardEqual(address, this.pattern, singleCharWildcard);
				if (address.Length == num)
				{
					num++;
				}
				return num;
			}
			if (WildcardPattern.PatternType.Wildcard == this.patternType)
			{
				return 0;
			}
			int i = 0;
			int num2 = 0;
			int num3 = this.splitPattern.Length;
			if (!this.StartsWithWildcard)
			{
				num = WildcardPattern.WildcardStartsWith(address, this.splitPattern[0], singleCharWildcard);
				if (-1 == num)
				{
					return -1;
				}
				i++;
				num2 = this.splitPattern[0].Length;
			}
			int num4 = num3;
			if (!this.EndsWithWildcard)
			{
				num4--;
			}
			while (i < num4)
			{
				int num5;
				num2 = WildcardPattern.WildcardIndexOf(address, num2, this.splitPattern[i], singleCharWildcard, out num5);
				if (-1 == num2)
				{
					return -1;
				}
				num += num5;
				num2 += this.splitPattern[i].Length;
				i++;
			}
			if (num4 < num3)
			{
				int length = this.splitPattern[i].Length;
				if (num2 + length > address.Length)
				{
					return -1;
				}
				int num5 = WildcardPattern.WildcardEndsWith(address, this.splitPattern[i], singleCharWildcard);
				if (-1 == num5)
				{
					return -1;
				}
				num += num5;
			}
			return num;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00032F2D File Offset: 0x0003112D
		public override string ToString()
		{
			return this.pattern;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00032F38 File Offset: 0x00031138
		private static WildcardPattern.PatternType NormalizePattern(string pattern, out string normalizedPattern)
		{
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = null;
			int i = 0;
			while (i < pattern.Length)
			{
				if ('*' != pattern[i])
				{
					flag2 = true;
					goto IL_40;
				}
				flag = true;
				if (i <= 0 || '*' != pattern[i - 1])
				{
					goto IL_40;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(pattern, 0, i, pattern.Length - 1);
				}
				IL_51:
				i++;
				continue;
				IL_40:
				if (stringBuilder != null)
				{
					stringBuilder.Append(pattern[i]);
					goto IL_51;
				}
				goto IL_51;
			}
			WildcardPattern.PatternType result;
			if (flag && flag2)
			{
				result = WildcardPattern.PatternType.Mixed;
				normalizedPattern = ((stringBuilder == null) ? pattern : stringBuilder.ToString());
			}
			else if (flag && !flag2)
			{
				result = WildcardPattern.PatternType.Wildcard;
				normalizedPattern = "*";
			}
			else
			{
				result = WildcardPattern.PatternType.NoWildcards;
				normalizedPattern = pattern;
			}
			return result;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00032FD6 File Offset: 0x000311D6
		private static int WildcardStartsWith(string str, string subStr, char singleCharWildcard)
		{
			if (singleCharWildcard == '\0')
			{
				if (!str.StartsWith(subStr, StringComparison.OrdinalIgnoreCase))
				{
					return -1;
				}
				return subStr.Length;
			}
			else
			{
				if (subStr.Length > str.Length)
				{
					return -1;
				}
				return WildcardPattern.WildcardEqual(str, 0, subStr, singleCharWildcard);
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00033008 File Offset: 0x00031208
		private static int WildcardEndsWith(string str, string subStr, char singleCharWildcard)
		{
			if (singleCharWildcard == '\0')
			{
				if (!str.EndsWith(subStr, StringComparison.OrdinalIgnoreCase))
				{
					return -1;
				}
				return subStr.Length;
			}
			else
			{
				int length = str.Length;
				int length2 = subStr.Length;
				if (length2 > length)
				{
					return -1;
				}
				return WildcardPattern.WildcardEqual(str, length - length2, subStr, singleCharWildcard);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003304C File Offset: 0x0003124C
		private static int WildcardIndexOf(string str, int startIndex, string subStr, char singleCharWildcard, out int matchCount)
		{
			matchCount = -1;
			if (singleCharWildcard == '\0')
			{
				int num = str.IndexOf(subStr, startIndex, StringComparison.OrdinalIgnoreCase);
				if (num >= 0)
				{
					matchCount = subStr.Length;
				}
				return num;
			}
			int length = str.Length;
			int length2 = subStr.Length;
			for (int i = startIndex; i < length - length2 + 1; i++)
			{
				matchCount = WildcardPattern.WildcardEqual(str, i, subStr, singleCharWildcard);
				if (matchCount >= 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000330AC File Offset: 0x000312AC
		private static int WildcardEqual(string s1, string s2, char singleCharWildcard)
		{
			if (singleCharWildcard == '\0')
			{
				if (!s1.Equals(s2, StringComparison.OrdinalIgnoreCase))
				{
					return -1;
				}
				return s1.Length;
			}
			else
			{
				if (s1.Length != s2.Length)
				{
					return -1;
				}
				return WildcardPattern.WildcardEqual(s1, 0, s2, singleCharWildcard);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000330E0 File Offset: 0x000312E0
		private static int WildcardEqual(string str, int startIndex, string subStr, char singleCharWildcard)
		{
			int num = 0;
			int i = 0;
			int length = subStr.Length;
			while (i < length)
			{
				switch (WildcardPattern.WildcardEqualChars(str, startIndex + i, subStr, i, singleCharWildcard))
				{
				case -1:
					return -1;
				case 1:
					num++;
					break;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0003312C File Offset: 0x0003132C
		private static int WildcardEqualChars(string s1, int index1, string s2, int index2, char singleCharWildcard)
		{
			if (singleCharWildcard == s2[index2])
			{
				return 0;
			}
			if (string.Compare(s1, index1, s2, index2, 1, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x04000A64 RID: 2660
		public const char WildcardChar = '*';

		// Token: 0x04000A65 RID: 2661
		public const string WildcardString = "*";

		// Token: 0x04000A66 RID: 2662
		private const char NoSingleCharWildcard = '\0';

		// Token: 0x04000A67 RID: 2663
		private static readonly char[] WildcardChars = new char[]
		{
			'*'
		};

		// Token: 0x04000A68 RID: 2664
		private string pattern;

		// Token: 0x04000A69 RID: 2665
		private WildcardPattern.PatternType patternType;

		// Token: 0x04000A6A RID: 2666
		private string[] splitPattern;

		// Token: 0x020001E4 RID: 484
		internal enum PatternType
		{
			// Token: 0x04000A6C RID: 2668
			NoWildcards,
			// Token: 0x04000A6D RID: 2669
			Wildcard,
			// Token: 0x04000A6E RID: 2670
			Mixed
		}
	}
}
