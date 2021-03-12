using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000B8 RID: 184
	public sealed class ChangeNotificationData
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		internal Guid Id { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001C005 File Offset: 0x0001A205
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001C00D File Offset: 0x0001A20D
		internal Guid ParentId { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001C016 File Offset: 0x0001A216
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001C01E File Offset: 0x0001A21E
		internal ConfigurationObjectType ObjectType { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001C027 File Offset: 0x0001A227
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001C02F File Offset: 0x0001A22F
		internal ChangeType ChangeType { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001C038 File Offset: 0x0001A238
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001C040 File Offset: 0x0001A240
		internal PolicyVersion Version { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001C049 File Offset: 0x0001A249
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001C051 File Offset: 0x0001A251
		internal Workload Workload { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001C05A File Offset: 0x0001A25A
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001C062 File Offset: 0x0001A262
		internal UnifiedPolicyErrorCode ErrorCode { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001C06B File Offset: 0x0001A26B
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001C073 File Offset: 0x0001A273
		internal string ErrorMessage { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001C07C File Offset: 0x0001A27C
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001C084 File Offset: 0x0001A284
		internal bool ShouldNotify { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001C08D File Offset: 0x0001A28D
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001C095 File Offset: 0x0001A295
		internal bool UseFullSync { get; set; }

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
		internal ChangeNotificationData(Guid id, Guid parentId, ConfigurationObjectType objectType, ChangeType changeType, Workload workload, PolicyVersion version, UnifiedPolicyErrorCode errorCode = UnifiedPolicyErrorCode.Unknown, string errorMessage = "")
		{
			this.Id = id;
			this.ParentId = parentId;
			this.ObjectType = objectType;
			this.ChangeType = changeType;
			this.Workload = workload;
			this.Version = version;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
			this.ShouldNotify = true;
			this.UseFullSync = false;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001C100 File Offset: 0x0001A300
		internal SyncChangeInfo CreateSyncChangeInfo(bool setObjectId)
		{
			return new SyncChangeInfo(this.ObjectType, this.ChangeType, this.Version, setObjectId ? new Guid?(this.Id) : null);
		}
	}
}
