using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D5 RID: 2517
	[Cmdlet("Upgrade", "ExchangeServer", DefaultParameterSetName = "Identity")]
	public sealed class UpgradeExchangeServer : SetTopologySystemConfigurationObjectTask<ServerIdParameter, Server>
	{
		// Token: 0x06005A16 RID: 23062 RVA: 0x001798BC File Offset: 0x00177ABC
		protected override IConfigurable PrepareDataObject()
		{
			Server server = (Server)base.PrepareDataObject();
			server.AdminDisplayVersion = ConfigurationContext.Setup.InstalledVersion;
			server.VersionNumber = SystemConfigurationTasksHelper.GenerateVersionNumber(ConfigurationContext.Setup.InstalledVersion);
			return server;
		}
	}
}
