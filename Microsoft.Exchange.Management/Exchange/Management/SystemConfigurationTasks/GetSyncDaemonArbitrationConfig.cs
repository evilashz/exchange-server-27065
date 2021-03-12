using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1C RID: 2844
	[Cmdlet("Get", "SyncDaemonArbitrationConfig")]
	public sealed class GetSyncDaemonArbitrationConfig : GetSingletonSystemConfigurationObjectTask<SyncDaemonArbitrationConfig>
	{
		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x060064F4 RID: 25844 RVA: 0x001A5481 File Offset: 0x001A3681
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
