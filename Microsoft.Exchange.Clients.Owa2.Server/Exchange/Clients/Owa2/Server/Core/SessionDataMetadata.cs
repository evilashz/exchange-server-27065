using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200045F RID: 1119
	internal enum SessionDataMetadata
	{
		// Token: 0x040015AF RID: 5551
		[DisplayName("SD", "CV")]
		BootWithConversationView,
		// Token: 0x040015B0 RID: 5552
		[DisplayName("SD", "SDCU")]
		SessionDataCacheUsed,
		// Token: 0x040015B1 RID: 5553
		[DisplayName("SD", "SDCTO")]
		SessionDataCacheWaitTimeOut,
		// Token: 0x040015B2 RID: 5554
		[DisplayName("SD", "SDH.B")]
		SessionDataHandlerBegin,
		// Token: 0x040015B3 RID: 5555
		[DisplayName("SD", "SDH.E")]
		SessionDataHandlerEnd,
		// Token: 0x040015B4 RID: 5556
		[DisplayName("SD", "SDP.B")]
		SessionDataProcessingBegin,
		// Token: 0x040015B5 RID: 5557
		[DisplayName("SD", "SDP.E")]
		SessionDataProcessingEnd,
		// Token: 0x040015B6 RID: 5558
		[DisplayName("SD", "GUCT.B")]
		GetOwaUserContextBegin,
		// Token: 0x040015B7 RID: 5559
		[DisplayName("SD", "GUCT.E")]
		GetOwaUserContextEnd,
		// Token: 0x040015B8 RID: 5560
		[DisplayName("SD", "PLMbxS.E")]
		TryPreLoadMailboxSessionEnd,
		// Token: 0x040015B9 RID: 5561
		[DisplayName("SD", "CAC.B")]
		CreateAggregatedConfigurationBegin,
		// Token: 0x040015BA RID: 5562
		[DisplayName("SD", "CAC.E")]
		CreateAggregatedConfigurationEnd,
		// Token: 0x040015BB RID: 5563
		[DisplayName("SD", "GUC.B")]
		GetOwaUserConfigurationBegin,
		// Token: 0x040015BC RID: 5564
		[DisplayName("SD", "GUC.E")]
		GetOwaUserConfigurationEnd,
		// Token: 0x040015BD RID: 5565
		[DisplayName("SD", "FF.B")]
		FindFoldersBegin,
		// Token: 0x040015BE RID: 5566
		[DisplayName("SD", "FF.E")]
		FindFoldersEnd,
		// Token: 0x040015BF RID: 5567
		[DisplayName("SD", "FCI.B")]
		FindConversationOrItemBegin,
		// Token: 0x040015C0 RID: 5568
		[DisplayName("SD", "FCI.E")]
		FindConversationOrItemEnd,
		// Token: 0x040015C1 RID: 5569
		[DisplayName("SD", "GCI.B")]
		GetConversationItemsOrItemBegin,
		// Token: 0x040015C2 RID: 5570
		[DisplayName("SD", "GCI.E")]
		GetConversationItemsOrItemEnd,
		// Token: 0x040015C3 RID: 5571
		[DisplayName("SD", "AGR.C")]
		AggregationContextReadCount,
		// Token: 0x040015C4 RID: 5572
		[DisplayName("SD", "AGRQ.C")]
		AggregationContextRequestCount,
		// Token: 0x040015C5 RID: 5573
		[DisplayName("SD", "SDC.B1")]
		SessionDataCacheFirstTimeRetriveveBegin,
		// Token: 0x040015C6 RID: 5574
		[DisplayName("SD", "SDC.E1")]
		SessionDataCacheFirstTimeRetriveveEnd,
		// Token: 0x040015C7 RID: 5575
		[DisplayName("SD", "SDC.B2")]
		SessionDataCacheSecondTimeRetriveveBegin,
		// Token: 0x040015C8 RID: 5576
		[DisplayName("SD", "SDC.E2")]
		SessionDataCacheSecondTimeRetriveveEnd
	}
}
