using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7C RID: 2684
	[Cmdlet("Get", "IPBlockListProvider")]
	public sealed class GetIPBlockListProvider : GetSystemConfigurationObjectTask<IPBlockListProviderIdParameter, IPBlockListProvider>
	{
		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x0018FDD3 File Offset: 0x0018DFD3
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
