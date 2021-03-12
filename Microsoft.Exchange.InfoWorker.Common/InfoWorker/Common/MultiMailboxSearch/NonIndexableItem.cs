using System;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000203 RID: 515
	internal class NonIndexableItem
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0003C5D0 File Offset: 0x0003A7D0
		public MdbItemIdentity CompositeId { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0003C5D9 File Offset: 0x0003A7D9
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0003C5E1 File Offset: 0x0003A7E1
		public ItemId ItemId { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003C5EA File Offset: 0x0003A7EA
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003C5F2 File Offset: 0x0003A7F2
		public ItemIndexError ErrorCode { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0003C5FB File Offset: 0x0003A7FB
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0003C603 File Offset: 0x0003A803
		public string ErrorDescription { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0003C60C File Offset: 0x0003A80C
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0003C614 File Offset: 0x0003A814
		public bool IsPartiallyIndexed { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003C61D File Offset: 0x0003A81D
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0003C625 File Offset: 0x0003A825
		public bool IsPermanentFailure { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003C62E File Offset: 0x0003A82E
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0003C636 File Offset: 0x0003A836
		public int AttemptCount { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003C63F File Offset: 0x0003A83F
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0003C647 File Offset: 0x0003A847
		public DateTime? LastAttemptTime { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003C650 File Offset: 0x0003A850
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0003C658 File Offset: 0x0003A858
		public string AdditionalInfo { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003C661 File Offset: 0x0003A861
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0003C669 File Offset: 0x0003A869
		public string SortValue { get; set; }

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003C672 File Offset: 0x0003A872
		public static ItemIndexError ConvertSearchErrorCode(EvaluationErrors searchErrorCode)
		{
			return NonIndexableItem.ConvertSearchErrorCode(searchErrorCode.ToString());
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003C684 File Offset: 0x0003A884
		public static ItemIndexError ConvertSearchErrorCode(string searchErrorCode)
		{
			ItemIndexError result = ItemIndexError.None;
			Enum.TryParse<ItemIndexError>(searchErrorCode, true, out result);
			return result;
		}
	}
}
