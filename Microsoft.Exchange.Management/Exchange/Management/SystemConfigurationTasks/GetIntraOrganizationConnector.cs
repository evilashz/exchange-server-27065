using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A11 RID: 2577
	[Cmdlet("Get", "IntraOrganizationConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetIntraOrganizationConnector : GetMultitenancySystemConfigurationObjectTask<IntraOrganizationConnectorIdParameter, IntraOrganizationConnector>
	{
		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x00185ECB File Offset: 0x001840CB
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
