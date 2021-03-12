using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0F RID: 2575
	[Cmdlet("Set", "GlobalLocatorServiceDomain", DefaultParameterSetName = "DomainNameParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetGlobalLocatorServiceDomain : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x001856C3 File Offset: 0x001838C3
		// (set) Token: 0x06005C5D RID: 23645 RVA: 0x001856DA File Offset: 0x001838DA
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

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x001856ED File Offset: 0x001838ED
		// (set) Token: 0x06005C5F RID: 23647 RVA: 0x00185704 File Offset: 0x00183904
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		public GlsDomainFlags DomainFlags
		{
			get
			{
				return (GlsDomainFlags)base.Fields["DomainFlags"];
			}
			set
			{
				base.Fields["DomainFlags"] = value;
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06005C60 RID: 23648 RVA: 0x0018571C File Offset: 0x0018391C
		// (set) Token: 0x06005C61 RID: 23649 RVA: 0x00185733 File Offset: 0x00183933
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		[ValidateNotNull]
		public bool DomainInUse
		{
			get
			{
				return (bool)base.Fields["DomainInUse"];
			}
			set
			{
				base.Fields["DomainInUse"] = value;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06005C62 RID: 23650 RVA: 0x0018574B File Offset: 0x0018394B
		// (set) Token: 0x06005C63 RID: 23651 RVA: 0x00185762 File Offset: 0x00183962
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

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x0018577C File Offset: 0x0018397C
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
				return Strings.ConfirmationMessageSetGls("Domain", id);
			}
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x001857E8 File Offset: 0x001839E8
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceDomain globalLocatorServiceDomain = new GlobalLocatorServiceDomain();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
			if (!glsDirectorySession.TryGetTenantDomainFromDomainFqdn(smtpDomain.Domain, out globalLocatorServiceDomain, true))
			{
				base.WriteGlsDomainNotFoundError(smtpDomain.Domain);
			}
			List<KeyValuePair<DomainProperty, PropertyValue>> list = new List<KeyValuePair<DomainProperty, PropertyValue>>();
			if (base.Fields.IsModified("DomainInUse"))
			{
				list.Add(new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoDomainInUse, new PropertyValue((bool)base.Fields["DomainInUse"])));
			}
			if (base.Fields.IsModified("DomainFlags"))
			{
				list.Add(new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoFlags, new PropertyValue((int)base.Fields["DomainFlags"])));
			}
			glsDirectorySession.UpdateAcceptedDomain(globalLocatorServiceDomain.ExternalDirectoryOrganizationId, globalLocatorServiceDomain.DomainName.Domain, list.ToArray());
		}
	}
}
