using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0A RID: 2570
	[Cmdlet("Remove", "GlobalLocatorServiceTenant", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveGlobalLocatorServiceTenant : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x00184C3E File Offset: 0x00182E3E
		// (set) Token: 0x06005C2B RID: 23595 RVA: 0x00184C55 File Offset: 0x00182E55
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

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00184C68 File Offset: 0x00182E68
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
				return Strings.ConfirmationMessageRemoveGls("Tenant", id);
			}
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x00184CD4 File Offset: 0x00182ED4
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			Guid guid = Guid.Empty;
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				guid = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
				GlobalLocatorServiceTenant globalLocatorServiceTenant;
				if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(guid, out globalLocatorServiceTenant))
				{
					base.WriteGlsTenantNotFoundError(guid);
				}
			}
			else
			{
				SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
				GlobalLocatorServiceTenant globalLocatorServiceTenant;
				if (!glsDirectorySession.TryGetTenantInfoByDomain(smtpDomain.Domain, out globalLocatorServiceTenant))
				{
					base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
				}
				guid = globalLocatorServiceTenant.ExternalDirectoryOrganizationId;
			}
			glsDirectorySession.RemoveTenant(guid);
		}
	}
}
