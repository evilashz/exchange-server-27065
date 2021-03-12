using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000035 RID: 53
	internal enum FindConversationAndItemMetadata
	{
		// Token: 0x0400022D RID: 557
		[DisplayName("FCI", "CID")]
		CorrelationId,
		// Token: 0x0400022E RID: 558
		[DisplayName("FCI", "MBXT")]
		MailboxTarget,
		// Token: 0x0400022F RID: 559
		[DisplayName("FCI", "QS")]
		QueryString,
		// Token: 0x04000230 RID: 560
		[DisplayName("FCI", "TRC")]
		TotalRowCount,
		// Token: 0x04000231 RID: 561
		[DisplayName("FCI", "VF")]
		ViewFilter,
		// Token: 0x04000232 RID: 562
		[DisplayName("FCI", "VFA")]
		ViewFilterActions,
		// Token: 0x04000233 RID: 563
		[DisplayName("FCI", "VFE")]
		ViewFilterSearchFolderException,
		// Token: 0x04000234 RID: 564
		[DisplayName("FCI", "VFPF")]
		ViewFilterSearchFolderPopulateFailed,
		// Token: 0x04000235 RID: 565
		[DisplayName("FCI", "CST")]
		CreateSearchFolderTime,
		// Token: 0x04000236 RID: 566
		[DisplayName("FCI", "CSRC")]
		CreateSearchFolderRpcCount,
		// Token: 0x04000237 RID: 567
		[DisplayName("FCI", "CSRL")]
		CreateSearchFolderRpcLatency,
		// Token: 0x04000238 RID: 568
		[DisplayName("FCI", "CSRLS")]
		CreateSearchFolderRpcLatencyOnStore,
		// Token: 0x04000239 RID: 569
		[DisplayName("FCI", "CSCpu")]
		CreateSearchFolderCPUTime,
		// Token: 0x0400023A RID: 570
		[DisplayName("FCI", "CSSTS")]
		CreateSearchFolderStartTimestamp,
		// Token: 0x0400023B RID: 571
		[DisplayName("FCI", "CSETS")]
		CreateSearchFolderEndTimestamp,
		// Token: 0x0400023C RID: 572
		[DisplayName("FCI", "PST")]
		PopulateSearchFolderTime,
		// Token: 0x0400023D RID: 573
		[DisplayName("FCI", "PSNQT")]
		PopulateSearchFolderNotificationQueueTime,
		// Token: 0x0400023E RID: 574
		[DisplayName("FCI", "PSRC")]
		PopulateSearchFolderRpcCount,
		// Token: 0x0400023F RID: 575
		[DisplayName("FCI", "PSRL")]
		PopulateSearchFolderRpcLatency,
		// Token: 0x04000240 RID: 576
		[DisplayName("FCI", "PSRLS")]
		PopulateSearchFolderRpcLatencyOnStore,
		// Token: 0x04000241 RID: 577
		[DisplayName("FCI", "PSCpu")]
		PopulateSearchFolderCPUTime,
		// Token: 0x04000242 RID: 578
		[DisplayName("FCI", "PSSTS")]
		PopulateSearchFolderStartTimestamp,
		// Token: 0x04000243 RID: 579
		[DisplayName("FCI", "PSETS")]
		PopulateSearchFolderEndTimestamp,
		// Token: 0x04000244 RID: 580
		[DisplayName("FCI", "PSEVT")]
		PopulateSearchFolderEventType,
		// Token: 0x04000245 RID: 581
		[DisplayName("FCI", "PSEVTD")]
		PopulateSearchFolderEventDelay,
		// Token: 0x04000246 RID: 582
		[DisplayName("FCI", "PSSF")]
		PopulateSearchFolderFailed,
		// Token: 0x04000247 RID: 583
		[DisplayName("FCI", "PSMaxRC")]
		PopulateSearchFolderMaxResultsCount,
		// Token: 0x04000248 RID: 584
		[DisplayName("FCI", "ADT")]
		AggregateDataTime,
		// Token: 0x04000249 RID: 585
		[DisplayName("FCI", "ADRC")]
		AggregateDataRpcCount,
		// Token: 0x0400024A RID: 586
		[DisplayName("FCI", "ADRL")]
		AggregateDataRpcLatency,
		// Token: 0x0400024B RID: 587
		[DisplayName("FCI", "ADRLS")]
		AggregateDataRpcLatencyOnStore,
		// Token: 0x0400024C RID: 588
		[DisplayName("FCI", "ADCpu")]
		AggregateDataCPUTime,
		// Token: 0x0400024D RID: 589
		[DisplayName("FCI", "ADSTS")]
		AggregateDataStartTimestamp,
		// Token: 0x0400024E RID: 590
		[DisplayName("FCI", "ADETS")]
		AggregateDataEndTimestamp,
		// Token: 0x0400024F RID: 591
		[DisplayName("FCI", "OPTS")]
		OptimizedSearch,
		// Token: 0x04000250 RID: 592
		[DisplayName("FCI", "AS")]
		ArchiveState,
		// Token: 0x04000251 RID: 593
		[DisplayName("FCI", "ADST")]
		ArchiveDiscoveryStartTimestamp,
		// Token: 0x04000252 RID: 594
		[DisplayName("FCI", "ADET")]
		ArchiveDiscoveryEndTimestamp,
		// Token: 0x04000253 RID: 595
		[DisplayName("FCI", "ADF")]
		ArchiveDiscoveryFailed,
		// Token: 0x04000254 RID: 596
		[DisplayName("FCI", "ERASST")]
		ExecuteRemoteArchiveSearchStartTimestamp,
		// Token: 0x04000255 RID: 597
		[DisplayName("FCI", "ERASET")]
		ExecuteRemoteArchiveSearchEndTimestamp,
		// Token: 0x04000256 RID: 598
		[DisplayName("FCI", "ERASF")]
		ExecuteRemoteArchiveSearchFailed,
		// Token: 0x04000257 RID: 599
		[DisplayName("FCI", "CVT")]
		CalendarViewTime,
		// Token: 0x04000258 RID: 600
		[DisplayName("FCI", "CSIT")]
		CalendarSingleItemsTotalTime,
		// Token: 0x04000259 RID: 601
		[DisplayName("FCI", "CSIC")]
		CalendarSingleItemsCount,
		// Token: 0x0400025A RID: 602
		[DisplayName("FCI", "CSIQ")]
		CalendarSingleItemsQueryRowsTime,
		// Token: 0x0400025B RID: 603
		[DisplayName("FCI", "CSISTC")]
		CalendarSingleItemsSeekToConditionTime,
		// Token: 0x0400025C RID: 604
		[DisplayName("FCI", "CSIG")]
		CalendarSingleItemsGetRowsTime,
		// Token: 0x0400025D RID: 605
		[DisplayName("FCI", "CRIT")]
		CalendarRecurringItemsTotalTime,
		// Token: 0x0400025E RID: 606
		[DisplayName("FCI", "CRIC")]
		CalendarRecurringItemsCount,
		// Token: 0x0400025F RID: 607
		[DisplayName("FCI", "CRIQ")]
		CalendarRecurringItemsQueryTime,
		// Token: 0x04000260 RID: 608
		[DisplayName("FCI", "CRIG")]
		CalendarRecurringItemsGetRowsTime,
		// Token: 0x04000261 RID: 609
		[DisplayName("FCI", "CRIET")]
		CalendarRecurringItemsExpansionTime,
		// Token: 0x04000262 RID: 610
		[DisplayName("FCI", "CRINI")]
		CalendarRecurringItemsNoInstancesInWindow,
		// Token: 0x04000263 RID: 611
		[DisplayName("FCI", "CRIMS")]
		CalendarRecurringItemsMaxSubject,
		// Token: 0x04000264 RID: 612
		[DisplayName("FCI", "CRIMBT")]
		CalendarRecurringItemsMaxBlobStreamTime,
		// Token: 0x04000265 RID: 613
		[DisplayName("FCI", "CRIMET")]
		CalendarRecurringItemsMaxExpansionTime,
		// Token: 0x04000266 RID: 614
		[DisplayName("FCI", "CRIMP")]
		CalendarRecurringItemsMaxParseTime,
		// Token: 0x04000267 RID: 615
		[DisplayName("FCI", "CRIMAR")]
		CalendarRecurringItemsMaxAddRowsTime,
		// Token: 0x04000268 RID: 616
		[DisplayName("FCI", "CVU")]
		CalendarRecurringItemsViewUsed
	}
}
