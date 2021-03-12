using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000256 RID: 598
	[Cmdlet("Set", "TeanantObjectVersion", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetTenantObjectVersion : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x0005DD00 File Offset: 0x0005BF00
		protected override IConfigurable PrepareDataObject()
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.PrepareDataObject();
			exchangeConfigurationUnit.ObjectVersion = Organization.OrgConfigurationVersion;
			exchangeConfigurationUnit.SetBuildVersion(OrganizationTaskHelper.ManagementDllVersion.FileBuildPart, OrganizationTaskHelper.ManagementDllVersion.FilePrivatePart);
			return exchangeConfigurationUnit;
		}
	}
}
