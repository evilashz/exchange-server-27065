using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0B RID: 2571
	[Cmdlet("Set", "GlobalLocatorServiceTenant", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetGlobalLocatorServiceTenant : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x00184D71 File Offset: 0x00182F71
		// (set) Token: 0x06005C30 RID: 23600 RVA: 0x00184D88 File Offset: 0x00182F88
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "DomainNameParameterSet")]
		[ValidateNotNull]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x00184D9B File Offset: 0x00182F9B
		// (set) Token: 0x06005C32 RID: 23602 RVA: 0x00184DB2 File Offset: 0x00182FB2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06005C33 RID: 23603 RVA: 0x00184DC5 File Offset: 0x00182FC5
		// (set) Token: 0x06005C34 RID: 23604 RVA: 0x00184DDC File Offset: 0x00182FDC
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
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

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06005C35 RID: 23605 RVA: 0x00184DEF File Offset: 0x00182FEF
		// (set) Token: 0x06005C36 RID: 23606 RVA: 0x00184E06 File Offset: 0x00183006
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
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

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x00184E19 File Offset: 0x00183019
		// (set) Token: 0x06005C38 RID: 23608 RVA: 0x00184E30 File Offset: 0x00183030
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06005C39 RID: 23609 RVA: 0x00184E43 File Offset: 0x00183043
		// (set) Token: 0x06005C3A RID: 23610 RVA: 0x00184E5A File Offset: 0x0018305A
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x00184E72 File Offset: 0x00183072
		// (set) Token: 0x06005C3C RID: 23612 RVA: 0x00184E89 File Offset: 0x00183089
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x06005C3D RID: 23613 RVA: 0x00184E9C File Offset: 0x0018309C
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x06005C3E RID: 23614 RVA: 0x00184EA4 File Offset: 0x001830A4
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
				return Strings.ConfirmationMessageSetGls("Tenant", id);
			}
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x00184F10 File Offset: 0x00183110
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant = new GlobalLocatorServiceTenant();
			GlobalLocatorServiceTenant oldGlsTenant = new GlobalLocatorServiceTenant();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				Guid guid = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
				if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(guid, out oldGlsTenant))
				{
					base.WriteGlsTenantNotFoundError(guid);
				}
			}
			else
			{
				SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
				if (!glsDirectorySession.TryGetTenantInfoByDomain(smtpDomain.Domain, out oldGlsTenant))
				{
					base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
				}
			}
			globalLocatorServiceTenant = this.GetUpdatedGLSTenant(oldGlsTenant);
			glsDirectorySession.UpdateTenant(globalLocatorServiceTenant.ExternalDirectoryOrganizationId, globalLocatorServiceTenant.ResourceForest, globalLocatorServiceTenant.AccountForest, globalLocatorServiceTenant.SmtpNextHopDomain.Domain, globalLocatorServiceTenant.TenantFlags, globalLocatorServiceTenant.TenantContainerCN, globalLocatorServiceTenant.PrimarySite);
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x00184FE0 File Offset: 0x001831E0
		private GlobalLocatorServiceTenant GetUpdatedGLSTenant(GlobalLocatorServiceTenant oldGlsTenant)
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant = new GlobalLocatorServiceTenant();
			globalLocatorServiceTenant.ExternalDirectoryOrganizationId = oldGlsTenant.ExternalDirectoryOrganizationId;
			globalLocatorServiceTenant.DomainNames = oldGlsTenant.DomainNames;
			if (base.Fields.IsModified("ResourceForest"))
			{
				globalLocatorServiceTenant.ResourceForest = (string)base.Fields["ResourceForest"];
				PartitionId partitionId;
				Exception ex;
				if (!PartitionId.TryParse(globalLocatorServiceTenant.ResourceForest, out partitionId, out ex))
				{
					base.WriteInvalidFqdnError(globalLocatorServiceTenant.ResourceForest);
				}
			}
			else
			{
				globalLocatorServiceTenant.ResourceForest = oldGlsTenant.ResourceForest;
			}
			if (base.Fields.IsModified("AccountForest"))
			{
				globalLocatorServiceTenant.AccountForest = (string)base.Fields["AccountForest"];
				PartitionId partitionId2;
				Exception ex2;
				if (!PartitionId.TryParse(globalLocatorServiceTenant.AccountForest, out partitionId2, out ex2))
				{
					base.WriteInvalidFqdnError(globalLocatorServiceTenant.AccountForest);
				}
			}
			else
			{
				globalLocatorServiceTenant.AccountForest = oldGlsTenant.AccountForest;
			}
			if (base.Fields.IsModified("PrimarySite"))
			{
				globalLocatorServiceTenant.PrimarySite = (string)base.Fields["PrimarySite"];
			}
			else
			{
				globalLocatorServiceTenant.PrimarySite = oldGlsTenant.PrimarySite;
			}
			if (base.Fields.IsModified("SmtpNextHopDomain"))
			{
				globalLocatorServiceTenant.SmtpNextHopDomain = (SmtpDomain)base.Fields["SmtpNextHopDomain"];
			}
			else
			{
				globalLocatorServiceTenant.SmtpNextHopDomain = oldGlsTenant.SmtpNextHopDomain;
			}
			if (base.Fields.IsModified("TenantFlags"))
			{
				globalLocatorServiceTenant.TenantFlags = (GlsTenantFlags)base.Fields["TenantFlags"];
			}
			else
			{
				globalLocatorServiceTenant.TenantFlags = oldGlsTenant.TenantFlags;
			}
			if (base.Fields.IsModified("TenantContainerCN"))
			{
				globalLocatorServiceTenant.TenantContainerCN = (string)base.Fields["TenantContainerCN"];
			}
			else
			{
				globalLocatorServiceTenant.TenantContainerCN = oldGlsTenant.TenantContainerCN;
			}
			return globalLocatorServiceTenant;
		}
	}
}
