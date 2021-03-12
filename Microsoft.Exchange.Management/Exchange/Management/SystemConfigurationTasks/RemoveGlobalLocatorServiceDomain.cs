using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0E RID: 2574
	[Cmdlet("Remove", "GlobalLocatorServiceDomain", DefaultParameterSetName = "DomainNameParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveGlobalLocatorServiceDomain : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x001855CF File Offset: 0x001837CF
		// (set) Token: 0x06005C56 RID: 23638 RVA: 0x001855E6 File Offset: 0x001837E6
		[ValidateNotNull]
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x06005C57 RID: 23639 RVA: 0x001855F9 File Offset: 0x001837F9
		// (set) Token: 0x06005C58 RID: 23640 RVA: 0x00185610 File Offset: 0x00183810
		private new Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			}
			set
			{
				base.Fields["ExternalDirectoryOrganizationId"] = value;
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x00185628 File Offset: 0x00183828
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string domain = ((SmtpDomain)base.Fields["DomainName"]).Domain;
				return Strings.ConfirmationMessageRemoveGls("Domain", domain);
			}
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x0018565C File Offset: 0x0018385C
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceDomain globalLocatorServiceDomain = new GlobalLocatorServiceDomain();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
			if (!glsDirectorySession.TryGetTenantDomainFromDomainFqdn(smtpDomain.Domain, out globalLocatorServiceDomain, true))
			{
				base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
			}
			glsDirectorySession.RemoveAcceptedDomain(globalLocatorServiceDomain.ExternalDirectoryOrganizationId, smtpDomain.Domain, true);
		}
	}
}
