using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A54 RID: 2644
	[Serializable]
	public class UnifiedPolicyNotificationBase : XsoMailboxConfigurationObject
	{
		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x001973DC File Offset: 0x001955DC
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return UnifiedPolicyNotificationBase.schema;
			}
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001973E3 File Offset: 0x001955E3
		public UnifiedPolicyNotificationBase()
		{
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001973EC File Offset: 0x001955EC
		internal UnifiedPolicyNotificationBase(WorkItemBase workItem, ADObjectId mailboxOwnerId)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.InternalIdentity = new UnifiedPolicySyncNotificationId(workItem.ExternalIdentity);
			this.workItem = workItem;
			base.MailboxOwnerId = mailboxOwnerId;
			if (!string.IsNullOrEmpty(workItem.WorkItemId))
			{
				this.StoreObjectId = VersionedId.Deserialize(workItem.WorkItemId);
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06006096 RID: 24726 RVA: 0x00197458 File Offset: 0x00195658
		// (set) Token: 0x06006097 RID: 24727 RVA: 0x0019746A File Offset: 0x0019566A
		internal UnifiedPolicySyncNotificationId InternalIdentity
		{
			get
			{
				return (UnifiedPolicySyncNotificationId)this[UnifiedPolicyNotificationBaseSchema.Identity];
			}
			set
			{
				this[UnifiedPolicyNotificationBaseSchema.Identity] = value;
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06006098 RID: 24728 RVA: 0x00197478 File Offset: 0x00195678
		// (set) Token: 0x06006099 RID: 24729 RVA: 0x0019748A File Offset: 0x0019568A
		internal Guid Version
		{
			get
			{
				return (Guid)this[UnifiedPolicyNotificationBaseSchema.Version];
			}
			set
			{
				this[UnifiedPolicyNotificationBaseSchema.Version] = value;
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x0600609A RID: 24730 RVA: 0x0019749D File Offset: 0x0019569D
		[Parameter]
		public override ObjectId Identity
		{
			get
			{
				return this.InternalIdentity;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x0600609B RID: 24731 RVA: 0x001974A5 File Offset: 0x001956A5
		[Parameter]
		public ExDateTime? ExecuteTime
		{
			get
			{
				return new ExDateTime?((ExDateTime)this.workItem.ExecuteTimeUTC);
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x001974BC File Offset: 0x001956BC
		[Parameter]
		public WorkItemStatus Status
		{
			get
			{
				return this.workItem.Status;
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x001974C9 File Offset: 0x001956C9
		[Parameter]
		public TenantContext TenantContext
		{
			get
			{
				return this.workItem.TenantContext;
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x001974D6 File Offset: 0x001956D6
		[Parameter]
		public List<SyncAgentExceptionBase> Erros
		{
			get
			{
				return this.workItem.Errors;
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x001974E3 File Offset: 0x001956E3
		[Parameter]
		public int RetryCount
		{
			get
			{
				return this.workItem.TryCount;
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x001974F0 File Offset: 0x001956F0
		// (set) Token: 0x060060A1 RID: 24737 RVA: 0x001974F8 File Offset: 0x001956F8
		internal StoreId StoreObjectId { get; set; }

		// Token: 0x060060A2 RID: 24738 RVA: 0x00197501 File Offset: 0x00195701
		internal WorkItemBase GetWorkItem()
		{
			return this.workItem;
		}

		// Token: 0x040036F8 RID: 14072
		private static XsoMailboxConfigurationObjectSchema schema = ObjectSchema.GetInstance<UnifiedPolicyNotificationBaseSchema>();

		// Token: 0x040036F9 RID: 14073
		protected WorkItemBase workItem;
	}
}
