using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7D RID: 2685
	[Cmdlet("Get", "IPBlockListProvidersConfig")]
	public sealed class GetIPBlockListProviderConfig : GetSingletonSystemConfigurationObjectTask<IPBlockListProviderConfig>
	{
		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x0018FDDE File Offset: 0x0018DFDE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
