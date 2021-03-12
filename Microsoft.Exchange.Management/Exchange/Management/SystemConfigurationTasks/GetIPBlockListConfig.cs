using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7A RID: 2682
	[Cmdlet("Get", "IPBlockListConfig")]
	public sealed class GetIPBlockListConfig : GetSingletonSystemConfigurationObjectTask<IPBlockListConfig>
	{
		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x06005F75 RID: 24437 RVA: 0x0018FDB6 File Offset: 0x0018DFB6
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
