using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F7 RID: 1783
	[ProvisioningObjectTag("DistributionGroup")]
	[Serializable]
	public class DistributionGroup : DistributionGroupBase
	{
		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x0013136B File Offset: 0x0012F56B
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DistributionGroup.schema;
			}
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x00131372 File Offset: 0x0012F572
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00131379 File Offset: 0x0012F579
		public DistributionGroup()
		{
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00131381 File Offset: 0x0012F581
		public DistributionGroup(ADGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0013138A File Offset: 0x0012F58A
		internal static DistributionGroup FromDataObject(ADGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new DistributionGroup(dataObject);
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x00131397 File Offset: 0x0012F597
		public GroupTypeFlags GroupType
		{
			get
			{
				return (GroupTypeFlags)this[DistributionGroupSchema.GroupType];
			}
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x001313A9 File Offset: 0x0012F5A9
		// (set) Token: 0x060053E1 RID: 21473 RVA: 0x001313BB File Offset: 0x0012F5BB
		[Parameter(Mandatory = false)]
		public string SamAccountName
		{
			get
			{
				return (string)this[DistributionGroupSchema.SamAccountName];
			}
			set
			{
				this[DistributionGroupSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x001313C9 File Offset: 0x0012F5C9
		// (set) Token: 0x060053E3 RID: 21475 RVA: 0x001313DB File Offset: 0x0012F5DB
		[Parameter(Mandatory = false)]
		public bool BypassNestedModerationEnabled
		{
			get
			{
				return (bool)this[DistributionGroupSchema.BypassNestedModerationEnabled];
			}
			set
			{
				this[DistributionGroupSchema.BypassNestedModerationEnabled] = value;
			}
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x001313EE File Offset: 0x0012F5EE
		// (set) Token: 0x060053E5 RID: 21477 RVA: 0x00131400 File Offset: 0x0012F600
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[DistributionGroupSchema.ManagedBy];
			}
			set
			{
				this[DistributionGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x0013140E File Offset: 0x0012F60E
		// (set) Token: 0x060053E7 RID: 21479 RVA: 0x00131420 File Offset: 0x0012F620
		public MemberUpdateType MemberJoinRestriction
		{
			get
			{
				return (MemberUpdateType)this[DistributionGroupSchema.MemberJoinRestriction];
			}
			set
			{
				this[DistributionGroupSchema.MemberJoinRestriction] = value;
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x00131433 File Offset: 0x0012F633
		// (set) Token: 0x060053E9 RID: 21481 RVA: 0x00131445 File Offset: 0x0012F645
		public MemberUpdateType MemberDepartRestriction
		{
			get
			{
				return (MemberUpdateType)this[DistributionGroupSchema.MemberDepartRestriction];
			}
			set
			{
				this[DistributionGroupSchema.MemberDepartRestriction] = value;
			}
		}

		// Token: 0x0400387C RID: 14460
		private static DistributionGroupSchema schema = ObjectSchema.GetInstance<DistributionGroupSchema>();
	}
}
