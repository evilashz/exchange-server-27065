using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000235 RID: 565
	internal class FolderNode
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x00048A39 File Offset: 0x00046C39
		internal FolderNode(StoreId sourceFolderId, StoreId targetFolderId, string displayName, FolderNode parent) : this(sourceFolderId, targetFolderId, displayName, false, parent)
		{
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00048A47 File Offset: 0x00046C47
		internal FolderNode(StoreId sourceFolderId, StoreId targetFolderId, string displayName, bool isSoftDeleted, FolderNode parent)
		{
			this.SourceFolderId = sourceFolderId;
			this.TargetFolderId = targetFolderId;
			this.DisplayName = displayName;
			this.Parent = parent;
			this.IsSoftDeleted = isSoftDeleted;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00048A74 File Offset: 0x00046C74
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00048A7C File Offset: 0x00046C7C
		internal FolderNode Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00048A85 File Offset: 0x00046C85
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00048A8D File Offset: 0x00046C8D
		internal StoreId SourceFolderId
		{
			get
			{
				return this.sourceFolderId;
			}
			set
			{
				this.sourceFolderId = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00048A96 File Offset: 0x00046C96
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x00048A9E File Offset: 0x00046C9E
		internal StoreId TargetFolderId
		{
			get
			{
				return this.targetFolderId;
			}
			set
			{
				this.targetFolderId = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00048AA7 File Offset: 0x00046CA7
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x00048AAF File Offset: 0x00046CAF
		internal string DisplayName
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

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00048AB8 File Offset: 0x00046CB8
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal bool IsSoftDeleted
		{
			get
			{
				return this.isSoftDeleted;
			}
			set
			{
				this.isSoftDeleted = value;
			}
		}

		// Token: 0x04000AF3 RID: 2803
		private FolderNode parent;

		// Token: 0x04000AF4 RID: 2804
		private StoreId sourceFolderId;

		// Token: 0x04000AF5 RID: 2805
		private StoreId targetFolderId;

		// Token: 0x04000AF6 RID: 2806
		private string displayName;

		// Token: 0x04000AF7 RID: 2807
		private bool isSoftDeleted;
	}
}
