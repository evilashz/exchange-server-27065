using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000953 RID: 2387
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSyncJobRpcInParameters : RpcParameters
	{
		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x060058B3 RID: 22707 RVA: 0x0016CE6C File Offset: 0x0016B06C
		// (set) Token: 0x060058B4 RID: 22708 RVA: 0x0016CE74 File Offset: 0x0016B074
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0016CE7D File Offset: 0x0016B07D
		// (set) Token: 0x060058B6 RID: 22710 RVA: 0x0016CE85 File Offset: 0x0016B085
		public Guid ContentMailboxGuid { get; private set; }

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x0016CE8E File Offset: 0x0016B08E
		// (set) Token: 0x060058B8 RID: 22712 RVA: 0x0016CE96 File Offset: 0x0016B096
		public PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction SyncAction { get; private set; }

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0016CE9F File Offset: 0x0016B09F
		// (set) Token: 0x060058BA RID: 22714 RVA: 0x0016CEA7 File Offset: 0x0016B0A7
		public byte[] FolderId { get; private set; }

		// Token: 0x060058BB RID: 22715 RVA: 0x0016CEB0 File Offset: 0x0016B0B0
		public PublicFolderSyncJobRpcInParameters(byte[] data) : base(data)
		{
			this.OrganizationId = (OrganizationId)base.GetParameterValue("OrganizationId");
			this.ContentMailboxGuid = (Guid)base.GetParameterValue("ContentMailboxGuid");
			this.SyncAction = (PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction)base.GetParameterValue("PublicFolderSyncAction");
			this.FolderId = (byte[])base.GetParameterValue("PublicFolderId");
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x0016CF1C File Offset: 0x0016B11C
		public PublicFolderSyncJobRpcInParameters(OrganizationId organizationId, Guid contentMailboxGuid, PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction syncAction) : this(organizationId, contentMailboxGuid, syncAction, null)
		{
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x0016CF28 File Offset: 0x0016B128
		public PublicFolderSyncJobRpcInParameters(OrganizationId organizationId, Guid contentMailboxGuid, byte[] folderId) : this(organizationId, contentMailboxGuid, PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.SyncFolder, folderId)
		{
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0016CF34 File Offset: 0x0016B134
		private PublicFolderSyncJobRpcInParameters(OrganizationId organizationId, Guid contentMailboxGuid, PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction syncAction, byte[] folderId)
		{
			this.OrganizationId = organizationId;
			this.ContentMailboxGuid = contentMailboxGuid;
			this.SyncAction = syncAction;
			this.FolderId = folderId;
			base.SetParameterValue("ContentMailboxGuid", this.ContentMailboxGuid);
			base.SetParameterValue("OrganizationId", this.OrganizationId);
			base.SetParameterValue("PublicFolderSyncAction", this.SyncAction);
			base.SetParameterValue("PublicFolderId", this.FolderId);
		}

		// Token: 0x0400306D RID: 12397
		private const string ContentMailboxGuidParameterName = "ContentMailboxGuid";

		// Token: 0x0400306E RID: 12398
		private const string OrganizationIdParameterName = "OrganizationId";

		// Token: 0x0400306F RID: 12399
		private const string PublicFolderSyncActionParameterName = "PublicFolderSyncAction";

		// Token: 0x04003070 RID: 12400
		private const string PublicFolderIdParameterName = "PublicFolderId";

		// Token: 0x02000954 RID: 2388
		public enum PublicFolderSyncAction : uint
		{
			// Token: 0x04003076 RID: 12406
			StartSyncHierarchy,
			// Token: 0x04003077 RID: 12407
			QueryStatusSyncHierarchy,
			// Token: 0x04003078 RID: 12408
			SyncFolder,
			// Token: 0x04003079 RID: 12409
			StartSyncHierarchyWithFolderReconciliation
		}
	}
}
