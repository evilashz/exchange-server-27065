using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000792 RID: 1938
	public class HierarchyNotification : BaseNotification
	{
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000CB8E6 File Offset: 0x000C9AE6
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000CB8EE File Offset: 0x000C9AEE
		public FolderId FolderId { get; set; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000CB8F7 File Offset: 0x000C9AF7
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000CB8FF File Offset: 0x000C9AFF
		public string FolderClass { get; set; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000CB908 File Offset: 0x000C9B08
		// (set) Token: 0x060039B8 RID: 14776 RVA: 0x000CB910 File Offset: 0x000C9B10
		internal StoreObjectType FolderType { get; set; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000CB919 File Offset: 0x000C9B19
		// (set) Token: 0x060039BA RID: 14778 RVA: 0x000CB921 File Offset: 0x000C9B21
		public string DisplayName { get; set; }

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000CB92A File Offset: 0x000C9B2A
		// (set) Token: 0x060039BC RID: 14780 RVA: 0x000CB932 File Offset: 0x000C9B32
		public FolderId ParentFolderId { get; set; }

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000CB93B File Offset: 0x000C9B3B
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000CB943 File Offset: 0x000C9B43
		public long ItemCount { get; set; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000CB94C File Offset: 0x000C9B4C
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000CB954 File Offset: 0x000C9B54
		public long UnreadCount { get; set; }

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000CB95D File Offset: 0x000C9B5D
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000CB965 File Offset: 0x000C9B65
		public bool IsHidden { get; set; }

		// Token: 0x060039C3 RID: 14787 RVA: 0x000CB96E File Offset: 0x000C9B6E
		public HierarchyNotification() : base(NotificationKindType.Hierarchy)
		{
		}

		// Token: 0x0400201B RID: 8219
		public byte[] InstanceKey;
	}
}
