using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000028 RID: 40
	internal static class StringExtensions
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000A910 File Offset: 0x00008B10
		public static List<StringExtensions.PrefixMatch> LongestPrefixMatch(this string source, List<string> toCompare)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				throw new ArgumentNullException("source");
			}
			if (toCompare == null)
			{
				throw new ArgumentNullException("toCompare");
			}
			List<StringExtensions.PrefixMatch> list = new List<StringExtensions.PrefixMatch>(toCompare.Count);
			foreach (string value in toCompare)
			{
				list.Add(new StringExtensions.PrefixMatch
				{
					Value = value
				});
			}
			for (int i = 0; i < source.Length; i++)
			{
				char c = char.ToUpper(source[i]);
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].Value.Length > i && char.ToUpper(list[j].Value[i]) == c && list[j].Mask == i)
					{
						list[j].Mask++;
					}
				}
			}
			list.Sort((StringExtensions.PrefixMatch x, StringExtensions.PrefixMatch y) => y.CompareTo(x));
			return list;
		}

		// Token: 0x02000029 RID: 41
		internal class PrefixMatch : IComparable<StringExtensions.PrefixMatch>
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600014B RID: 331 RVA: 0x0000AA4C File Offset: 0x00008C4C
			// (set) Token: 0x0600014C RID: 332 RVA: 0x0000AA54 File Offset: 0x00008C54
			internal string Value { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600014D RID: 333 RVA: 0x0000AA5D File Offset: 0x00008C5D
			// (set) Token: 0x0600014E RID: 334 RVA: 0x0000AA65 File Offset: 0x00008C65
			internal int Mask { get; set; }

			// Token: 0x0600014F RID: 335 RVA: 0x0000AA70 File Offset: 0x00008C70
			public int CompareTo(StringExtensions.PrefixMatch other)
			{
				if (other == null)
				{
					throw new ArgumentNullException("other");
				}
				if (other.Mask.Equals(this.Mask))
				{
					return this.Value.CompareTo(other.Value);
				}
				return this.Mask.CompareTo(other.Mask);
			}
		}
	}
}
