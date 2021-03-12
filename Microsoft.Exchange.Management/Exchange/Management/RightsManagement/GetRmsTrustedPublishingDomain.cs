using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200070A RID: 1802
	[Cmdlet("Get", "RMSTrustedPublishingDomain", DefaultParameterSetName = "OrganizationSet")]
	public sealed class GetRmsTrustedPublishingDomain : GetMultitenancySystemConfigurationObjectTask<RmsTrustedPublishingDomainIdParameter, RMSTrustedPublishingDomain>
	{
		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x00105E14 File Offset: 0x00104014
		// (set) Token: 0x0600400A RID: 16394 RVA: 0x00105E2B File Offset: 0x0010402B
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "OrganizationSet")]
		public override OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x00105E3E File Offset: 0x0010403E
		// (set) Token: 0x0600400C RID: 16396 RVA: 0x00105E55 File Offset: 0x00104055
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override RmsTrustedPublishingDomainIdParameter Identity
		{
			get
			{
				return (RmsTrustedPublishingDomainIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x00105E68 File Offset: 0x00104068
		// (set) Token: 0x0600400E RID: 16398 RVA: 0x00105E8E File Offset: 0x0010408E
		[Parameter(Mandatory = false)]
		public SwitchParameter Default
		{
			get
			{
				return (SwitchParameter)(base.Fields["Default"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Default"] = value;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x00105EA6 File Offset: 0x001040A6
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.Fields.IsModified("Default"))
				{
					return ADObject.BoolFilterBuilder(new ComparisonFilter(ComparisonOperator.Equal, RMSTrustedPublishingDomainSchema.Default, true), new BitMaskAndFilter(RMSTrustedPublishingDomainSchema.Flags, 1UL));
				}
				return base.InternalFilter;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x00105EE3 File Offset: 0x001040E3
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
