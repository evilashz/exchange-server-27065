using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x02000480 RID: 1152
	[Cmdlet("Set", "ManagementEndPointHook", SupportsShouldProcess = true)]
	public sealed class SetManagementEndpointHook : ManagementEndpointBase
	{
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x0009EF7D File Offset: 0x0009D17D
		// (set) Token: 0x0600287A RID: 10362 RVA: 0x0009EF85 File Offset: 0x0009D185
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x0009EF8E File Offset: 0x0009D18E
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x0009EF96 File Offset: 0x0009D196
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Domain")]
		public SmtpDomain DomainName { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0009EF9F File Offset: 0x0009D19F
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x0009EFA7 File Offset: 0x0009D1A7
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Organization")]
		public AccountPartitionIdParameter AccountPartition { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x0009EFB0 File Offset: 0x0009D1B0
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x0009EFB8 File Offset: 0x0009D1B8
		[Parameter(Mandatory = true, ParameterSetName = "TenantFlag")]
		public GlsTenantFlags? TenantFlag { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0009EFC1 File Offset: 0x0009D1C1
		// (set) Token: 0x06002882 RID: 10370 RVA: 0x0009EFC9 File Offset: 0x0009D1C9
		[Parameter(Mandatory = false, ParameterSetName = "Organization")]
		public string TenantContainerCN { get; set; }

		// Token: 0x06002883 RID: 10371 RVA: 0x0009EFD4 File Offset: 0x0009D1D4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.AccountPartition != null)
			{
				PartitionId partitionId;
				Guid guid;
				if (ADAccountPartitionLocator.IsSingleForestTopology(out partitionId) && Guid.TryParse(this.AccountPartition.RawIdentity, out guid) && guid.Equals(ADObjectId.ResourcePartitionGuid))
				{
					this.accountPartitionId = partitionId;
				}
				if (null == this.accountPartitionId)
				{
					this.accountPartitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0009F04C File Offset: 0x0009D24C
		internal override void ProcessRedirectionEntry(IGlobalDirectorySession session)
		{
			if (this.accountPartitionId != null)
			{
				ADForest localForest = ADForest.GetLocalForest();
				session.UpdateTenant(this.ExternalDirectoryOrganizationId, localForest.Fqdn, this.accountPartitionId.ForestFQDN, ManagementEndpointBase.GetSmtpNextHopDomain(), GlsTenantFlags.None, this.TenantContainerCN);
				return;
			}
			if (this.TenantFlag != null)
			{
				session.SetTenantFlag(this.ExternalDirectoryOrganizationId, this.TenantFlag.Value, true);
				return;
			}
			session.UpdateAcceptedDomain(this.ExternalDirectoryOrganizationId, this.DomainName.Domain);
		}

		// Token: 0x04001E0A RID: 7690
		private const string OrganizationParameterSet = "Organization";

		// Token: 0x04001E0B RID: 7691
		private const string DomainParameterSet = "Domain";

		// Token: 0x04001E0C RID: 7692
		private const string TenantFlagParameterSet = "TenantFlag";

		// Token: 0x04001E0D RID: 7693
		private PartitionId accountPartitionId;
	}
}
