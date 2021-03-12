using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F8 RID: 504
	internal sealed class MailboxStatsComparer : IComparer, IComparer<MailboxStatistics>
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00037891 File Offset: 0x00035A91
		internal MailboxStatsComparer(bool ascendingSort)
		{
			this.ascendingSort = ascendingSort;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000378A0 File Offset: 0x00035AA0
		public int Compare(object first, object second)
		{
			MailboxStatistics lhsItem = first as MailboxStatistics;
			MailboxStatistics rhsItem = second as MailboxStatistics;
			return this.Compare(lhsItem, rhsItem);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000378C4 File Offset: 0x00035AC4
		private static void Swap(ref MailboxStatistics lhs, ref MailboxStatistics rhs)
		{
			MailboxStatistics mailboxStatistics = rhs;
			rhs = lhs;
			lhs = mailboxStatistics;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000378DB File Offset: 0x00035ADB
		public int Compare(MailboxStatistics lhsItem, MailboxStatistics rhsItem)
		{
			if (!this.ascendingSort)
			{
				MailboxStatsComparer.Swap(ref lhsItem, ref rhsItem);
			}
			if (rhsItem == null && lhsItem == null)
			{
				return 0;
			}
			if (lhsItem != null && rhsItem == null)
			{
				return 1;
			}
			if (lhsItem == null && rhsItem != null)
			{
				return -1;
			}
			return lhsItem.CompareTo(rhsItem);
		}

		// Token: 0x0400093A RID: 2362
		private readonly bool ascendingSort;
	}
}
