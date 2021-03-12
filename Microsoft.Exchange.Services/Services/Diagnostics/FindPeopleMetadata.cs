using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000038 RID: 56
	internal enum FindPeopleMetadata
	{
		// Token: 0x04000271 RID: 625
		[DisplayName("FP", "QS")]
		QueryString,
		// Token: 0x04000272 RID: 626
		[DisplayName("FP", "PC")]
		PersonalCount,
		// Token: 0x04000273 RID: 627
		[DisplayName("FP", "NPV")]
		TotalNumberOfPeopleInView,
		// Token: 0x04000274 RID: 628
		[DisplayName("FP", "GC")]
		GalCount,
		// Token: 0x04000275 RID: 629
		[DisplayName("FP", "PT")]
		PersonalSearchTime,
		// Token: 0x04000276 RID: 630
		[DisplayName("FP", "GT")]
		GalSearchTime,
		// Token: 0x04000277 RID: 631
		[DisplayName("FP", "PM")]
		PersonalSearchMode,
		// Token: 0x04000278 RID: 632
		[DisplayName("FP", "CST")]
		CreateSearchFolderTime,
		// Token: 0x04000279 RID: 633
		[DisplayName("FP", "CSRC")]
		CreateSearchFolderRpcCount,
		// Token: 0x0400027A RID: 634
		[DisplayName("FP", "CSRL")]
		CreateSearchFolderRpcLatency,
		// Token: 0x0400027B RID: 635
		[DisplayName("FP", "CSRLS")]
		CreateSearchFolderRpcLatencyOnStore,
		// Token: 0x0400027C RID: 636
		[DisplayName("FP", "CSCpu")]
		CreateSearchFolderCPUTime,
		// Token: 0x0400027D RID: 637
		[DisplayName("FP", "CSSTS")]
		CreateSearchFolderStartTimestamp,
		// Token: 0x0400027E RID: 638
		[DisplayName("FP", "CSETS")]
		CreateSearchFolderEndTimestamp,
		// Token: 0x0400027F RID: 639
		[DisplayName("FP", "PFT")]
		PublicFolderSearchTime,
		// Token: 0x04000280 RID: 640
		[DisplayName("FP", "PFRC")]
		PublicFolderSearchRpcCount,
		// Token: 0x04000281 RID: 641
		[DisplayName("FP", "PFRL")]
		PublicFolderSearchRpcLatency,
		// Token: 0x04000282 RID: 642
		[DisplayName("FP", "PFRLS")]
		PublicFolderSearchRpcLatencyOnStore,
		// Token: 0x04000283 RID: 643
		[DisplayName("FP", "PFCpu")]
		PublicFolderSearchCPUTime,
		// Token: 0x04000284 RID: 644
		[DisplayName("FP", "PFSTS")]
		PublicFolderSearchStartTimestamp,
		// Token: 0x04000285 RID: 645
		[DisplayName("FP", "PFETS")]
		PublicFolderSearchEndTimestamp,
		// Token: 0x04000286 RID: 646
		[DisplayName("FP", "PST")]
		PopulateSearchFolderTime,
		// Token: 0x04000287 RID: 647
		[DisplayName("FP", "PSRC")]
		PopulateSearchFolderRpcCount,
		// Token: 0x04000288 RID: 648
		[DisplayName("FP", "PSRL")]
		PopulateSearchFolderRpcLatency,
		// Token: 0x04000289 RID: 649
		[DisplayName("FP", "PSRLS")]
		PopulateSearchFolderRpcLatencyOnStore,
		// Token: 0x0400028A RID: 650
		[DisplayName("FP", "PSCpu")]
		PopulateSearchFolderCPUTime,
		// Token: 0x0400028B RID: 651
		[DisplayName("FP", "PSSTS")]
		PopulateSearchFolderStartTimestamp,
		// Token: 0x0400028C RID: 652
		[DisplayName("FP", "PSETS")]
		PopulateSearchFolderEndTimestamp,
		// Token: 0x0400028D RID: 653
		[DisplayName("FP", "PSNQT")]
		PopulateSearchFolderNotificationQueueTime,
		// Token: 0x0400028E RID: 654
		[DisplayName("FP", "ADT")]
		AggregateDataTime,
		// Token: 0x0400028F RID: 655
		[DisplayName("FP", "ADRC")]
		AggregateDataRpcCount,
		// Token: 0x04000290 RID: 656
		[DisplayName("FP", "ADRL")]
		AggregateDataRpcLatency,
		// Token: 0x04000291 RID: 657
		[DisplayName("FP", "ADRLS")]
		AggregateDataRpcLatencyOnStore,
		// Token: 0x04000292 RID: 658
		[DisplayName("FP", "ADCpu")]
		AggregateDataCPUTime,
		// Token: 0x04000293 RID: 659
		[DisplayName("FP", "ADSTS")]
		AggregateDataStartTimestamp,
		// Token: 0x04000294 RID: 660
		[DisplayName("FP", "ADETS")]
		AggregateDataEndTimestamp,
		// Token: 0x04000295 RID: 661
		[DisplayName("FP", "PSS")]
		PersonalSearchSuccessful,
		// Token: 0x04000296 RID: 662
		[DisplayName("FP", "FID")]
		FolderId,
		// Token: 0x04000297 RID: 663
		[DisplayName("FP", "CID")]
		CorrelationId,
		// Token: 0x04000298 RID: 664
		[DisplayName("FP", "GDC")]
		GalDataConversion,
		// Token: 0x04000299 RID: 665
		[DisplayName("FP", "PDC")]
		PersonalDataConversion,
		// Token: 0x0400029A RID: 666
		[DisplayName("FP", "CES")]
		CommandExecutionStart,
		// Token: 0x0400029B RID: 667
		[DisplayName("FP", "CEE")]
		CommandExecutionEnd,
		// Token: 0x0400029C RID: 668
		[DisplayName("FP", "BST")]
		BrowseSendersTime
	}
}
