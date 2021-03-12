using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200020B RID: 523
	internal sealed class PreviewItemComparer : IComparer, IComparer<PreviewItem>
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x0003D9AE File Offset: 0x0003BBAE
		internal PreviewItemComparer(bool ascendingSort)
		{
			this.ascendingSort = ascendingSort;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		public int Compare(object first, object second)
		{
			PreviewItem lhsItem = first as PreviewItem;
			PreviewItem rhsItem = second as PreviewItem;
			return this.Compare(lhsItem, rhsItem);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		private static void Swap(ref PreviewItem lhs, ref PreviewItem rhs)
		{
			PreviewItem previewItem = rhs;
			rhs = lhs;
			lhs = previewItem;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003D9FB File Offset: 0x0003BBFB
		public int Compare(PreviewItem lhsItem, PreviewItem rhsItem)
		{
			if (!this.ascendingSort)
			{
				PreviewItemComparer.Swap(ref lhsItem, ref rhsItem);
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

		// Token: 0x040009B1 RID: 2481
		private readonly bool ascendingSort;
	}
}
