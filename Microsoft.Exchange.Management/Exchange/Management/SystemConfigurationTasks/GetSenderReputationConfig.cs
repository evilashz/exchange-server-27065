using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200075D RID: 1885
	[Cmdlet("Get", "SenderReputationConfig")]
	public sealed class GetSenderReputationConfig : GetSingletonSystemConfigurationObjectTask<SenderReputationConfig>
	{
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x00113C0D File Offset: 0x00111E0D
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
