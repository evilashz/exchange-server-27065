using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000177 RID: 375
	internal class GetSyncRequestStatisticsCommand : MrsAccessorCommand
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x0004AEEF File Offset: 0x000490EF
		public GetSyncRequestStatisticsCommand() : base("Get-SyncRequestStatistics", null, null)
		{
		}

		// Token: 0x04000625 RID: 1573
		internal const string CmdletName = "Get-SyncRequestStatistics";
	}
}
