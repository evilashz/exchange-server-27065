using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0D RID: 3597
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderNode
	{
		// Token: 0x06007C17 RID: 31767 RVA: 0x00223B15 File Offset: 0x00221D15
		public FolderNode(string serverId, string displayName, string parentId, string contentClass)
		{
			this.serverId = serverId;
			this.displayName = displayName;
			this.parentId = parentId;
			this.contentClass = contentClass.Substring(contentClass.LastIndexOf(':') + 1);
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06007C18 RID: 31768 RVA: 0x00223B4A File Offset: 0x00221D4A
		// (set) Token: 0x06007C19 RID: 31769 RVA: 0x00223B52 File Offset: 0x00221D52
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x00223B5B File Offset: 0x00221D5B
		// (set) Token: 0x06007C1B RID: 31771 RVA: 0x00223B63 File Offset: 0x00221D63
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06007C1C RID: 31772 RVA: 0x00223B6C File Offset: 0x00221D6C
		// (set) Token: 0x06007C1D RID: 31773 RVA: 0x00223B74 File Offset: 0x00221D74
		public string ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06007C1E RID: 31774 RVA: 0x00223B7D File Offset: 0x00221D7D
		// (set) Token: 0x06007C1F RID: 31775 RVA: 0x00223B85 File Offset: 0x00221D85
		public string ContentClass
		{
			get
			{
				return this.contentClass;
			}
			set
			{
				this.contentClass = value;
			}
		}

		// Token: 0x04005507 RID: 21767
		private string serverId;

		// Token: 0x04005508 RID: 21768
		private string displayName;

		// Token: 0x04005509 RID: 21769
		private string parentId;

		// Token: 0x0400550A RID: 21770
		private string contentClass;
	}
}
