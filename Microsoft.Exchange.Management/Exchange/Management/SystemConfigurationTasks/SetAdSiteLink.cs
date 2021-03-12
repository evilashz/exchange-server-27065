using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200005A RID: 90
	[Cmdlet("Set", "AdSiteLink", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAdSiteLink : SetSystemConfigurationObjectTask<AdSiteLinkIdParameter, ADSiteLink>
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00009FD9 File Offset: 0x000081D9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAdSiteLink(this.Identity.ToString());
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009FEB File Offset: 0x000081EB
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009FEE File Offset: 0x000081EE
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009FF4 File Offset: 0x000081F4
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 69, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AdSiteLink\\SetAdSiteLink.cs");
		}
	}
}
