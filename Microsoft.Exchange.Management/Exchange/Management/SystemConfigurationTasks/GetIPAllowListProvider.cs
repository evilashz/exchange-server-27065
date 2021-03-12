using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A73 RID: 2675
	[Cmdlet("Get", "IPAllowListProvider")]
	public sealed class GetIPAllowListProvider : GetSystemConfigurationObjectTask<IPAllowListProviderIdParameter, IPAllowListProvider>
	{
		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x0018F905 File Offset: 0x0018DB05
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
