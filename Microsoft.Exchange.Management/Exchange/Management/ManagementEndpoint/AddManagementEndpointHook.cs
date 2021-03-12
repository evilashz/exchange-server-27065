using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x0200047F RID: 1151
	[Cmdlet("Add", "ManagementEndPointHook", SupportsShouldProcess = true)]
	public sealed class AddManagementEndpointHook : ManagementEndpointBase
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x0009EE45 File Offset: 0x0009D045
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x0009EE4D File Offset: 0x0009D04D
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x0009EE56 File Offset: 0x0009D056
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x0009EE5E File Offset: 0x0009D05E
		[Parameter(Mandatory = true, ParameterSetName = "Domain")]
		[ValidateNotNullOrEmpty]
		public SmtpDomain DomainName { get; set; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x0009EE67 File Offset: 0x0009D067
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x0009EE6F File Offset: 0x0009D06F
		[Parameter(Mandatory = true, ParameterSetName = "Organization")]
		[ValidateNotNullOrEmpty]
		public AccountPartitionIdParameter AccountPartition { get; set; }

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x0009EE78 File Offset: 0x0009D078
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x0009EE80 File Offset: 0x0009D080
		[Parameter(Mandatory = false, ParameterSetName = "Organization")]
		public SmtpDomain PopulateCacheWithDomainName { get; set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x0009EE89 File Offset: 0x0009D089
		// (set) Token: 0x06002873 RID: 10355 RVA: 0x0009EE91 File Offset: 0x0009D091
		[Parameter(Mandatory = false, ParameterSetName = "Organization")]
		public string TenantContainerCN { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0009EE9A File Offset: 0x0009D09A
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x0009EEA2 File Offset: 0x0009D0A2
		[Parameter(Mandatory = false, ParameterSetName = "Domain")]
		public bool InitialDomain { get; set; }

		// Token: 0x06002876 RID: 10358 RVA: 0x0009EEAB File Offset: 0x0009D0AB
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.AccountPartition != null)
			{
				this.accountPartitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0009EED8 File Offset: 0x0009D0D8
		internal override void ProcessRedirectionEntry(IGlobalDirectorySession session)
		{
			if (this.accountPartitionId != null)
			{
				ADForest localForest = ADForest.GetLocalForest();
				session.AddTenant(this.ExternalDirectoryOrganizationId, localForest.Fqdn, this.accountPartitionId.ForestFQDN, ManagementEndpointBase.GetSmtpNextHopDomain(), GlsTenantFlags.None, this.TenantContainerCN);
				if (this.PopulateCacheWithDomainName != null)
				{
					ADAccountPartitionLocator.AddTenantDataToCache(this.ExternalDirectoryOrganizationId, localForest.Fqdn, this.accountPartitionId.ForestFQDN, this.PopulateCacheWithDomainName.Domain, this.TenantContainerCN);
					return;
				}
			}
			else
			{
				session.AddAcceptedDomain(this.ExternalDirectoryOrganizationId, this.DomainName.Domain, this.InitialDomain);
			}
		}

		// Token: 0x04001E01 RID: 7681
		private const string OrganizationParameterSet = "Organization";

		// Token: 0x04001E02 RID: 7682
		private const string DomainParameterSet = "Domain";

		// Token: 0x04001E03 RID: 7683
		private PartitionId accountPartitionId;
	}
}
