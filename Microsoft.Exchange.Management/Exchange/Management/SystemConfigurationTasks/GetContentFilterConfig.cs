using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A2D RID: 2605
	[Cmdlet("Get", "ContentFilterConfig")]
	public sealed class GetContentFilterConfig : GetSingletonSystemConfigurationObjectTask<ContentFilterConfig>
	{
		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x06005D36 RID: 23862 RVA: 0x00189155 File Offset: 0x00187355
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
