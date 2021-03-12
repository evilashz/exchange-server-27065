using System;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ConversationHelper
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x000128F7 File Offset: 0x00010AF7
		internal static T[] MergeArray<T>(T[] a, T[] b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			return a.Union(b).ToArray<T>();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012910 File Offset: 0x00010B10
		internal static int? MergeInts(int? a, int? b)
		{
			if (a != null && b != null)
			{
				int? num = a;
				int? num2 = b;
				if (!(num != null & num2 != null))
				{
					return null;
				}
				return new int?(num.GetValueOrDefault() + num2.GetValueOrDefault());
			}
			else
			{
				if (a != null)
				{
					return a;
				}
				return b;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012970 File Offset: 0x00010B70
		internal static string MergeDates(string a, string b)
		{
			if (string.IsNullOrEmpty(a))
			{
				return b;
			}
			if (string.IsNullOrEmpty(b))
			{
				return a;
			}
			DateTime dateTime = DateTime.Parse(a);
			DateTime value = DateTime.Parse(b);
			if (dateTime.CompareTo(value) > 0)
			{
				return a;
			}
			return b;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000129AD File Offset: 0x00010BAD
		internal static bool? MergeBoolNullable(bool? a, bool? b)
		{
			if (a == null)
			{
				return b;
			}
			if (b != null)
			{
				return new bool?(a.Value || b.Value);
			}
			return a;
		}

		// Token: 0x0400060D RID: 1549
		internal static ConversationTypeEqualityComparer ConversationTypeEqualityComparer = new ConversationTypeEqualityComparer();

		// Token: 0x0400060E RID: 1550
		internal static ConversationNodeEqualityComparer ConversationNodeEqualityComparer = new ConversationNodeEqualityComparer();

		// Token: 0x0400060F RID: 1551
		internal static DateTimeStringComparer DateTimeStringComparer = new DateTimeStringComparer();
	}
}
