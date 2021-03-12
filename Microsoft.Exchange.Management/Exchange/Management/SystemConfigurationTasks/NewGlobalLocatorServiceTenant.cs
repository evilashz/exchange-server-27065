using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A09 RID: 2569
	[Cmdlet("New", "GlobalLocatorServiceTenant", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true)]
	public sealed class NewGlobalLocatorServiceTenant : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x06005C1A RID: 23578 RVA: 0x00184962 File Offset: 0x00182B62
		// (set) Token: 0x06005C1B RID: 23579 RVA: 0x00184979 File Offset: 0x00182B79
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string ResourceForest
		{
			get
			{
				return (string)base.Fields["ResourceForest"];
			}
			set
			{
				base.Fields["ResourceForest"] = value;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x0018498C File Offset: 0x00182B8C
		// (set) Token: 0x06005C1D RID: 23581 RVA: 0x001849A3 File Offset: 0x00182BA3
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string AccountForest
		{
			get
			{
				return (string)base.Fields["AccountForest"];
			}
			set
			{
				base.Fields["AccountForest"] = value;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x001849B6 File Offset: 0x00182BB6
		// (set) Token: 0x06005C1F RID: 23583 RVA: 0x001849CD File Offset: 0x00182BCD
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string PrimarySite
		{
			get
			{
				return (string)base.Fields["PrimarySite"];
			}
			set
			{
				base.Fields["PrimarySite"] = value;
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x001849E0 File Offset: 0x00182BE0
		// (set) Token: 0x06005C21 RID: 23585 RVA: 0x001849F7 File Offset: 0x00182BF7
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public SmtpDomain SmtpNextHopDomain
		{
			get
			{
				return (SmtpDomain)base.Fields["SmtpNextHopDomain"];
			}
			set
			{
				base.Fields["SmtpNextHopDomain"] = value;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x00184A0A File Offset: 0x00182C0A
		// (set) Token: 0x06005C23 RID: 23587 RVA: 0x00184A21 File Offset: 0x00182C21
		[Parameter(Mandatory = false)]
		public GlsTenantFlags TenantFlags
		{
			get
			{
				return (GlsTenantFlags)base.Fields["TenantFlags"];
			}
			set
			{
				base.Fields["TenantFlags"] = value;
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06005C24 RID: 23588 RVA: 0x00184A39 File Offset: 0x00182C39
		// (set) Token: 0x06005C25 RID: 23589 RVA: 0x00184A50 File Offset: 0x00182C50
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string TenantContainerCN
		{
			get
			{
				return (string)base.Fields["TenantContainerCN"];
			}
			set
			{
				base.Fields["TenantContainerCN"] = value;
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06005C26 RID: 23590 RVA: 0x00184A64 File Offset: 0x00182C64
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string id;
				if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
				{
					id = ((Guid)base.Fields["ExternalDirectoryOrganizationId"]).ToString();
				}
				else
				{
					id = ((SmtpDomain)base.Fields["DomainName"]).Domain;
				}
				return Strings.ConfirmationMessageNewGls("Tenant", id);
			}
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00184ACF File Offset: 0x00182CCF
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x00184AD8 File Offset: 0x00182CD8
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant = new GlobalLocatorServiceTenant();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			globalLocatorServiceTenant.ExternalDirectoryOrganizationId = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			globalLocatorServiceTenant.ResourceForest = (string)base.Fields["ResourceForest"];
			PartitionId partitionId;
			Exception ex;
			if (!PartitionId.TryParse(globalLocatorServiceTenant.ResourceForest, out partitionId, out ex))
			{
				base.WriteInvalidFqdnError(globalLocatorServiceTenant.ResourceForest);
			}
			globalLocatorServiceTenant.AccountForest = (string)base.Fields["AccountForest"];
			if (!PartitionId.TryParse(globalLocatorServiceTenant.AccountForest, out partitionId, out ex))
			{
				base.WriteInvalidFqdnError(globalLocatorServiceTenant.AccountForest);
			}
			globalLocatorServiceTenant.PrimarySite = (string)base.Fields["PrimarySite"];
			globalLocatorServiceTenant.SmtpNextHopDomain = (SmtpDomain)base.Fields["SmtpNextHopDomain"];
			globalLocatorServiceTenant.TenantContainerCN = (string)base.Fields["TenantContainerCN"];
			if (base.Fields.IsModified("TenantFlags"))
			{
				globalLocatorServiceTenant.TenantFlags = (GlsTenantFlags)base.Fields["TenantFlags"];
			}
			glsDirectorySession.AddTenant(globalLocatorServiceTenant.ExternalDirectoryOrganizationId, globalLocatorServiceTenant.ResourceForest, globalLocatorServiceTenant.AccountForest, globalLocatorServiceTenant.SmtpNextHopDomain.Domain, globalLocatorServiceTenant.TenantFlags, globalLocatorServiceTenant.TenantContainerCN, globalLocatorServiceTenant.PrimarySite);
			base.WriteObject(globalLocatorServiceTenant);
		}
	}
}
