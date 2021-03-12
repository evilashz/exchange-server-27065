using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1D RID: 2845
	[Cmdlet("Set", "SyncDaemonArbitrationConfig", SupportsShouldProcess = true)]
	public sealed class SetSyncDaemonArbitrationConfig : SetSingletonSystemConfigurationObjectTask<SyncDaemonArbitrationConfig>
	{
		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x060064F6 RID: 25846 RVA: 0x001A548C File Offset: 0x001A368C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmSetSyncDaemonArbitrationConfig;
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x001A5493 File Offset: 0x001A3693
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
