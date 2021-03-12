using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A96 RID: 2710
	[Cmdlet("Get", "SenderFilterConfig")]
	public sealed class GetSenderFilterConfig : GetMultitenancySingletonSystemConfigurationObjectTask<SenderFilterConfig>
	{
		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x001914FF File Offset: 0x0018F6FF
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
