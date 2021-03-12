using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A74 RID: 2676
	[Cmdlet("Get", "IPAllowListProvidersConfig")]
	public sealed class GetIPAllowListProviderConfig : GetSingletonSystemConfigurationObjectTask<IPAllowListProviderConfig>
	{
		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x0018F910 File Offset: 0x0018DB10
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
