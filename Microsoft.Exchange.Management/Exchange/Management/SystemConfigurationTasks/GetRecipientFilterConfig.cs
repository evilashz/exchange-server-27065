using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A94 RID: 2708
	[Cmdlet("Get", "RecipientFilterConfig")]
	public sealed class GetRecipientFilterConfig : GetMultitenancySingletonSystemConfigurationObjectTask<RecipientFilterConfig>
	{
		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x001914E2 File Offset: 0x0018F6E2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
