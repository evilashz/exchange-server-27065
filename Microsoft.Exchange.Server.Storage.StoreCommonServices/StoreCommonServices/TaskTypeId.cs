using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000145 RID: 325
	public enum TaskTypeId : byte
	{
		// Token: 0x0400070B RID: 1803
		None,
		// Token: 0x0400070C RID: 1804
		ForTestPurposes,
		// Token: 0x0400070D RID: 1805
		BadPlanDetector = 11,
		// Token: 0x0400070E RID: 1806
		CheckpointSmoother,
		// Token: 0x0400070F RID: 1807
		ApplyLogicalIndexMaintenance,
		// Token: 0x04000710 RID: 1808
		LongOperationReporter,
		// Token: 0x04000711 RID: 1809
		EnumeratePreQuarantinedMailboxes,
		// Token: 0x04000712 RID: 1810
		CleanupNonActiveMailboxStates,
		// Token: 0x04000713 RID: 1811
		MaintenanceIdleCheck,
		// Token: 0x04000714 RID: 1812
		MarkForMaintenance,
		// Token: 0x04000715 RID: 1813
		ResourceMonitorDigest,
		// Token: 0x04000716 RID: 1814
		DrainSearchQueue,
		// Token: 0x04000717 RID: 1815
		DismountDatabase,
		// Token: 0x04000718 RID: 1816
		TimedEventsProcessing,
		// Token: 0x04000719 RID: 1817
		PropertyPromotion,
		// Token: 0x0400071A RID: 1818
		SearchFolderPopulation,
		// Token: 0x0400071B RID: 1819
		RopSummaryCollection,
		// Token: 0x0400071C RID: 1820
		PerfCounterFlush,
		// Token: 0x0400071D RID: 1821
		FlushDirtyPerUserCaches,
		// Token: 0x0400071E RID: 1822
		PropertyPromotionBootstrap,
		// Token: 0x0400071F RID: 1823
		CategorizedViewSearchFolderRestriction,
		// Token: 0x04000720 RID: 1824
		OnlineIntegrityCheck,
		// Token: 0x04000721 RID: 1825
		DatabaseSizeCheck,
		// Token: 0x04000722 RID: 1826
		RopLockContentionCollection,
		// Token: 0x04000723 RID: 1827
		RopResourceCollection,
		// Token: 0x04000724 RID: 1828
		CalculateTombstoneTableSize,
		// Token: 0x04000725 RID: 1829
		UrgentTombstoneTableCleanup,
		// Token: 0x04000726 RID: 1830
		FlushEventCounterUpperBound
	}
}
