using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000071 RID: 113
	internal class MruFolderItem
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0001B626 File Offset: 0x00019826
		public MruFolderItem(StoreObjectId id, string displayName, int itemCount, int unreadCount, object extendedFolderFlags)
		{
			this.id = id;
			this.displayName = displayName;
			this.itemCount = itemCount;
			this.unreadCount = unreadCount;
			this.extendedFolderFlags = extendedFolderFlags;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001B653 File Offset: 0x00019853
		public StoreObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001B65B File Offset: 0x0001985B
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0001B663 File Offset: 0x00019863
		public int ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001B66B File Offset: 0x0001986B
		public int UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0001B673 File Offset: 0x00019873
		public object ExtendedFolderFlags
		{
			get
			{
				return this.extendedFolderFlags;
			}
		}

		// Token: 0x04000248 RID: 584
		private StoreObjectId id;

		// Token: 0x04000249 RID: 585
		private string displayName;

		// Token: 0x0400024A RID: 586
		private int itemCount;

		// Token: 0x0400024B RID: 587
		private int unreadCount;

		// Token: 0x0400024C RID: 588
		private object extendedFolderFlags;
	}
}
