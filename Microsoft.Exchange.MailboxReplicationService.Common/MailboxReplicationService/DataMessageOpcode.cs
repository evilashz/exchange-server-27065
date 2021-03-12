using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200013B RID: 315
	internal enum DataMessageOpcode
	{
		// Token: 0x0400062A RID: 1578
		None,
		// Token: 0x0400062B RID: 1579
		MapiFxConfig,
		// Token: 0x0400062C RID: 1580
		MapiFxTransferBuffer,
		// Token: 0x0400062D RID: 1581
		MapiFxIsInterfaceOk,
		// Token: 0x0400062E RID: 1582
		MapiFxTellPartnerVersion,
		// Token: 0x0400062F RID: 1583
		MapiFxStartMdbEventsImport = 11,
		// Token: 0x04000630 RID: 1584
		MapiFxFinishMdbEventsImport,
		// Token: 0x04000631 RID: 1585
		MapiFxAddMdbEvents,
		// Token: 0x04000632 RID: 1586
		MapiFxSetWatermarks,
		// Token: 0x04000633 RID: 1587
		MapiFxSetReceiveFolder,
		// Token: 0x04000634 RID: 1588
		MapiFxSetPerUser,
		// Token: 0x04000635 RID: 1589
		MapiFxSetProps,
		// Token: 0x04000636 RID: 1590
		FxProxyPoolOpenFolder = 100,
		// Token: 0x04000637 RID: 1591
		FxProxyPoolCloseEntry,
		// Token: 0x04000638 RID: 1592
		FxProxyPoolOpenItem,
		// Token: 0x04000639 RID: 1593
		FxProxyPoolCreateItem,
		// Token: 0x0400063A RID: 1594
		FxProxyPoolCreateFAIItem,
		// Token: 0x0400063B RID: 1595
		FxProxyPoolSetProps,
		// Token: 0x0400063C RID: 1596
		FxProxyPoolSaveChanges,
		// Token: 0x0400063D RID: 1597
		FxProxyPoolDeleteItem,
		// Token: 0x0400063E RID: 1598
		FxProxyPoolWriteToMime,
		// Token: 0x0400063F RID: 1599
		FxProxyPoolCreateFolder,
		// Token: 0x04000640 RID: 1600
		FxProxyPoolSetExtendedAcl,
		// Token: 0x04000641 RID: 1601
		FxProxyPoolSetItemProperties,
		// Token: 0x04000642 RID: 1602
		BufferBatch = 200,
		// Token: 0x04000643 RID: 1603
		BufferBatchWithFlush,
		// Token: 0x04000644 RID: 1604
		PagedDataChunk = 210,
		// Token: 0x04000645 RID: 1605
		PagedLastDataChunk,
		// Token: 0x04000646 RID: 1606
		MessageExportResults = 220,
		// Token: 0x04000647 RID: 1607
		Flush = 1000,
		// Token: 0x04000648 RID: 1608
		FxProxyGetObjectDataRequest,
		// Token: 0x04000649 RID: 1609
		FxProxyGetObjectDataResponse,
		// Token: 0x0400064A RID: 1610
		FxProxyPoolGetFolderDataRequest,
		// Token: 0x0400064B RID: 1611
		FxProxyPoolGetFolderDataResponse,
		// Token: 0x0400064C RID: 1612
		FxProxyPoolGetUploadedIDsRequest,
		// Token: 0x0400064D RID: 1613
		FxProxyPoolGetUploadedIDsResponse
	}
}
