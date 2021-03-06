using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000252 RID: 594
	[Cmdlet("Set", "ManagementSiteLink", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetManagementSiteLink : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0005C650 File Offset: 0x0005A850
		protected override IConfigurable PrepareDataObject()
		{
			ADSite localSite = ((ITopologyConfigurationSession)this.ConfigurationSession).GetLocalSite();
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.PrepareDataObject();
			exchangeConfigurationUnit.ManagementSiteLink = localSite.Id;
			return exchangeConfigurationUnit;
		}
	}
}
