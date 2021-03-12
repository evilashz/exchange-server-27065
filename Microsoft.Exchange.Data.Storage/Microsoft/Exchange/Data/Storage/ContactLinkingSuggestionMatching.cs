using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C9 RID: 1225
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactLinkingSuggestionMatching
	{
		// Token: 0x060035D3 RID: 13779 RVA: 0x000D8AAC File Offset: 0x000D6CAC
		private ContactLinkingSuggestionMatching(double minimumNameMatchPercentage, int minimumNameLengthForPartialCompare)
		{
			this.minimumNameMatchPercentage = minimumNameMatchPercentage;
			this.minimumNameLengthForPartialCompare = minimumNameLengthForPartialCompare;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000D8AC2 File Offset: 0x000D6CC2
		internal bool IsFullMatch(CultureInfo culture, string a, string b)
		{
			return !string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b) && 0 == culture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000D8AE8 File Offset: 0x000D6CE8
		internal int GetPartialMatchCount(CultureInfo culture, string a, string b)
		{
			if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
			{
				return 0;
			}
			int num = Math.Min(a.Length, b.Length);
			if (num < this.minimumNameLengthForPartialCompare)
			{
				return 0;
			}
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && culture.CompareInfo.Compare(a, num3, 1, b, num3, 1, CompareOptions.IgnoreCase) == 0)
			{
				num2++;
				num3++;
			}
			int num4 = 0;
			int num5 = 0;
			while (num5 < num && culture.CompareInfo.Compare(a, a.Length - 1 - num5, 1, b, b.Length - 1 - num5, 1, CompareOptions.IgnoreCase) == 0)
			{
				num4++;
				num5++;
			}
			int num6 = (int)Math.Ceiling((double)num * this.minimumNameMatchPercentage);
			int num7 = Math.Max(num2, num4);
			if (num7 < num6)
			{
				return 0;
			}
			return num7;
		}

		// Token: 0x04001CDA RID: 7386
		private readonly double minimumNameMatchPercentage;

		// Token: 0x04001CDB RID: 7387
		private readonly int minimumNameLengthForPartialCompare;

		// Token: 0x04001CDC RID: 7388
		internal static readonly ContactLinkingSuggestionMatching FirstOrLastName = new ContactLinkingSuggestionMatching(0.5, 5);

		// Token: 0x04001CDD RID: 7389
		internal static readonly ContactLinkingSuggestionMatching AliasOrEmailAddress = new ContactLinkingSuggestionMatching(0.75, 6);
	}
}
