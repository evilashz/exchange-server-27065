using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B2B RID: 2859
	[Cmdlet("Get", "InboundConnector", DefaultParameterSetName = "Identity")]
	public class GetInboundConnector : GetMultitenancySystemConfigurationObjectTask<InboundConnectorIdParameter, TenantInboundConnector>
	{
		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x001A92E4 File Offset: 0x001A74E4
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
