using System;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000163 RID: 355
	internal enum EventId
	{
		// Token: 0x04000645 RID: 1605
		TestSuccessful = 1000,
		// Token: 0x04000646 RID: 1606
		TestFailed,
		// Token: 0x04000647 RID: 1607
		DetailedStatus,
		// Token: 0x04000648 RID: 1608
		TestFailedWithPassive,
		// Token: 0x04000649 RID: 1609
		ServiceNotRunning,
		// Token: 0x0400064A RID: 1610
		ServerIsNull,
		// Token: 0x0400064B RID: 1611
		ADError,
		// Token: 0x0400064C RID: 1612
		MapiError,
		// Token: 0x0400064D RID: 1613
		SCError,
		// Token: 0x0400064E RID: 1614
		RecoveryMailboxDatabaseNotTested,
		// Token: 0x0400064F RID: 1615
		ServerNoMdbs,
		// Token: 0x04000650 RID: 1616
		MdbSysMbxIsNull,
		// Token: 0x04000651 RID: 1617
		ActiveManagerError,
		// Token: 0x04000652 RID: 1618
		TaskBaseError,
		// Token: 0x04000653 RID: 1619
		MapiStoreError,
		// Token: 0x04000654 RID: 1620
		GetNonIpmSubTreeFolderError,
		// Token: 0x04000655 RID: 1621
		CreateTestFolderError,
		// Token: 0x04000656 RID: 1622
		CreateSearchFolderError,
		// Token: 0x04000657 RID: 1623
		CatalogInStateNew,
		// Token: 0x04000658 RID: 1624
		CatalogInStateCrawling,
		// Token: 0x04000659 RID: 1625
		TestTimeOutError,
		// Token: 0x0400065A RID: 1626
		MailboxInStateNotStarted,
		// Token: 0x0400065B RID: 1627
		MailboxInStateNormalCrawlInProgress,
		// Token: 0x0400065C RID: 1628
		MailboxInStateDeletionPending,
		// Token: 0x0400065D RID: 1629
		MailboxInStateInTransit,
		// Token: 0x0400065E RID: 1630
		MailboxInStateFailed,
		// Token: 0x0400065F RID: 1631
		CIIsDisabled,
		// Token: 0x04000660 RID: 1632
		MailboxNotArchived,
		// Token: 0x04000661 RID: 1633
		CatalogInUnhealthyState,
		// Token: 0x04000662 RID: 1634
		CatalogBacklog
	}
}
