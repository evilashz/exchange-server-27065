using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000020 RID: 32
	internal class EllipsisTrailer
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000045CD File Offset: 0x000027CD
		public EllipsisTrailer(CodingScheme codingScheme)
		{
			this.Coder = CodingSchemeInfo.GetCodingSchemeInfo(codingScheme).Coder;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000045E6 File Offset: 0x000027E6
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000045EE File Offset: 0x000027EE
		private ICoder Coder { get; set; }

		// Token: 0x060000B7 RID: 183 RVA: 0x000045F7 File Offset: 0x000027F7
		public string Trail(string original)
		{
			return this.Trail(original, -1);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004604 File Offset: 0x00002804
		public string Trail(string original, int endLocation)
		{
			if (string.IsNullOrEmpty(original))
			{
				return original;
			}
			if (original.Length <= endLocation)
			{
				throw new ArgumentOutOfRangeException("endLocation");
			}
			if (0 > endLocation)
			{
				endLocation = original.Length - 1;
			}
			foreach (string text in EllipsisTrailer.EllipsisCandidates)
			{
				string str = text;
				int num = 0;
				foreach (char ch in text)
				{
					num += this.Coder.GetCodedRadixCount(ch);
				}
				if (0 < num)
				{
					int num2 = 0;
					int num3 = endLocation;
					while (0 <= num3)
					{
						num2 += this.Coder.GetCodedRadixCount(original[num3]);
						if (num <= num2)
						{
							return original.Substring(0, num3) + str;
						}
						num3--;
					}
				}
			}
			return original;
		}

		// Token: 0x04000044 RID: 68
		public const char Ellipsis = '…';

		// Token: 0x04000045 RID: 69
		public const char Dot = '.';

		// Token: 0x04000046 RID: 70
		public static readonly string EllipsisString = '…'.ToString();

		// Token: 0x04000047 RID: 71
		public static readonly string DotDotDotString = string.Format("{0}{1}{2}", '.', '.', '.');

		// Token: 0x04000048 RID: 72
		public static readonly IList<string> EllipsisCandidates = new ReadOnlyCollection<string>(new string[]
		{
			EllipsisTrailer.EllipsisString,
			EllipsisTrailer.DotDotDotString
		});
	}
}
