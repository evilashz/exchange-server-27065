using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0C RID: 2572
	[Cmdlet("Get", "GlobalLocatorServiceDomain", DefaultParameterSetName = "DomainNameParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class GetGlobalLocatorServiceDomain : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x001851B0 File Offset: 0x001833B0
		// (set) Token: 0x06005C43 RID: 23619 RVA: 0x001851C7 File Offset: 0x001833C7
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

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x001851DA File Offset: 0x001833DA
		// (set) Token: 0x06005C45 RID: 23621 RVA: 0x00185200 File Offset: 0x00183400
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		public SwitchParameter UseOfflineGLS
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseOfflineGLS"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseOfflineGLS"] = value;
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00185218 File Offset: 0x00183418
		// (set) Token: 0x06005C47 RID: 23623 RVA: 0x0018522F File Offset: 0x0018342F
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

		// Token: 0x06005C48 RID: 23624 RVA: 0x00185248 File Offset: 0x00183448
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceDomain sendToPipeline = new GlobalLocatorServiceDomain();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
			if (this.UseOfflineGLS)
			{
				if (!glsDirectorySession.TryGetTenantDomainFromDomainFqdn(smtpDomain.Domain, out sendToPipeline, true, GlsCacheServiceMode.CacheOnly))
				{
					base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
				}
			}
			else if (!glsDirectorySession.TryGetTenantDomainFromDomainFqdn(smtpDomain.Domain, out sendToPipeline, true))
			{
				base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
			}
			base.WriteObject(sendToPipeline);
		}
	}
}
