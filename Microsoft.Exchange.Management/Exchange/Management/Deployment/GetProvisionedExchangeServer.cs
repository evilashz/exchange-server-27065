using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D7 RID: 471
	[Cmdlet("Get", "ProvisionedExchangeServer")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class GetProvisionedExchangeServer : GetSystemConfigurationObjectTask<ServerIdParameter, Server>
	{
		// Token: 0x06001059 RID: 4185 RVA: 0x000489CC File Offset: 0x00046BCC
		protected override void WriteResult(IConfigurable dataObject)
		{
			Server server = (Server)dataObject;
			if (server.IsProvisionedServer)
			{
				base.WriteResult(dataObject);
			}
		}
	}
}
