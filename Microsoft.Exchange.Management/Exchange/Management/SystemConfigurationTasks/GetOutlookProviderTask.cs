using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007FB RID: 2043
	[Cmdlet("Get", "OutlookProvider", DefaultParameterSetName = "Identity")]
	public sealed class GetOutlookProviderTask : GetMultitenancySystemConfigurationObjectTask<OutlookProviderIdParameter, OutlookProvider>
	{
		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x00124BA4 File Offset: 0x00122DA4
		protected override ObjectId RootId
		{
			get
			{
				return OutlookProvider.GetParentContainer(base.DataSession as ITopologyConfigurationSession);
			}
		}
	}
}
