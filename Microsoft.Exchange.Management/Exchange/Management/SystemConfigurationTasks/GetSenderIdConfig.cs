using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A98 RID: 2712
	[Cmdlet("Get", "SenderIdConfig")]
	public sealed class GetSenderIdConfig : GetSingletonSystemConfigurationObjectTask<SenderIdConfig>
	{
		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x0019151C File Offset: 0x0018F71C
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
