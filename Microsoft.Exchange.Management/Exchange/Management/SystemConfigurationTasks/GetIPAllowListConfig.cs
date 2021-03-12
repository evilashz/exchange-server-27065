using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A71 RID: 2673
	[Cmdlet("Get", "IPAllowListConfig")]
	public sealed class GetIPAllowListConfig : GetSingletonSystemConfigurationObjectTask<IPAllowListConfig>
	{
		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06005F51 RID: 24401 RVA: 0x0018F8E8 File Offset: 0x0018DAE8
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
