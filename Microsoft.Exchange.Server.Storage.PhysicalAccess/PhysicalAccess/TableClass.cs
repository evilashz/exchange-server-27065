using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200008D RID: 141
	public enum TableClass
	{
		// Token: 0x04000201 RID: 513
		Unknown,
		// Token: 0x04000202 RID: 514
		Other = 0,
		// Token: 0x04000203 RID: 515
		MSysObjects = 0,
		// Token: 0x04000204 RID: 516
		MSysObjids = 0,
		// Token: 0x04000205 RID: 517
		Temp,
		// Token: 0x04000206 RID: 518
		TableFunction,
		// Token: 0x04000207 RID: 519
		LazyIndex,
		// Token: 0x04000208 RID: 520
		Message,
		// Token: 0x04000209 RID: 521
		Attachment,
		// Token: 0x0400020A RID: 522
		Folder,
		// Token: 0x0400020B RID: 523
		PseudoIndexMaintenance,
		// Token: 0x0400020C RID: 524
		Events,
		// Token: 0x0400020D RID: 525
		PseudoIndexControl,
		// Token: 0x0400020E RID: 526
		PseudoIndexDefinition,
		// Token: 0x0400020F RID: 527
		DeliveredTo,
		// Token: 0x04000210 RID: 528
		ReceiveFolder,
		// Token: 0x04000211 RID: 529
		ReceiveFolder2,
		// Token: 0x04000212 RID: 530
		SearchQueue,
		// Token: 0x04000213 RID: 531
		Tombstone,
		// Token: 0x04000214 RID: 532
		Watermarks,
		// Token: 0x04000215 RID: 533
		ExtendedPropertyNameMapping,
		// Token: 0x04000216 RID: 534
		Globals,
		// Token: 0x04000217 RID: 535
		MailboxIdentity,
		// Token: 0x04000218 RID: 536
		Mailbox,
		// Token: 0x04000219 RID: 537
		ReplidGuidMap,
		// Token: 0x0400021A RID: 538
		TimedEvents,
		// Token: 0x0400021B RID: 539
		PerUser,
		// Token: 0x0400021C RID: 540
		InferenceLog,
		// Token: 0x0400021D RID: 541
		UpgradeHistory,
		// Token: 0x0400021E RID: 542
		UserInfo,
		// Token: 0x0400021F RID: 543
		ApplyOperatorSuite,
		// Token: 0x04000220 RID: 544
		ColumnSuite,
		// Token: 0x04000221 RID: 545
		ColumnSuite2,
		// Token: 0x04000222 RID: 546
		CommonDataRowSuite,
		// Token: 0x04000223 RID: 547
		ConnectionSuiteHelper,
		// Token: 0x04000224 RID: 548
		CountOperatorSuite,
		// Token: 0x04000225 RID: 549
		DeleteOperatorSuite,
		// Token: 0x04000226 RID: 550
		InsertOperatorSuite,
		// Token: 0x04000227 RID: 551
		InsertOperatorSuite2,
		// Token: 0x04000228 RID: 552
		InsertOperatorSuite3,
		// Token: 0x04000229 RID: 553
		JoinOperatorSuite,
		// Token: 0x0400022A RID: 554
		JoinOperatorSuite2,
		// Token: 0x0400022B RID: 555
		OrdinalPositionOperatorSuite,
		// Token: 0x0400022C RID: 556
		OrdinalPositionOperatorSuite2,
		// Token: 0x0400022D RID: 557
		PreReadOperatorSuite,
		// Token: 0x0400022E RID: 558
		DataRowSuite,
		// Token: 0x0400022F RID: 559
		ReaderSuite,
		// Token: 0x04000230 RID: 560
		ReaderSuite2,
		// Token: 0x04000231 RID: 561
		SearchCriteriaSuite,
		// Token: 0x04000232 RID: 562
		SortOperatorSuite,
		// Token: 0x04000233 RID: 563
		TableOperatorSuite,
		// Token: 0x04000234 RID: 564
		TableOperatorSuite2,
		// Token: 0x04000235 RID: 565
		TableOperatorSuite3,
		// Token: 0x04000236 RID: 566
		TableOperatorSuite4,
		// Token: 0x04000237 RID: 567
		CategorizedTableOperatorSuiteHeader,
		// Token: 0x04000238 RID: 568
		CategorizedTableOperatorSuiteLeaf,
		// Token: 0x04000239 RID: 569
		CategorizedTableOperatorSuiteMessage,
		// Token: 0x0400023A RID: 570
		UpdateOperatorSuite,
		// Token: 0x0400023B RID: 571
		IndexAndOperatorSuite2,
		// Token: 0x0400023C RID: 572
		IndexAndOperatorSuite3,
		// Token: 0x0400023D RID: 573
		IndexOrOperatorSuite2,
		// Token: 0x0400023E RID: 574
		IndexOrOperatorSuite3,
		// Token: 0x0400023F RID: 575
		IndexNotOperatorSuite2,
		// Token: 0x04000240 RID: 576
		IndexNotOperatorSuite3,
		// Token: 0x04000241 RID: 577
		SqlConnectionSuite,
		// Token: 0x04000242 RID: 578
		JetColumnSuite,
		// Token: 0x04000243 RID: 579
		Partitioned,
		// Token: 0x04000244 RID: 580
		JetTableSuite,
		// Token: 0x04000245 RID: 581
		DistinctOperatorSuite,
		// Token: 0x04000246 RID: 582
		MaxValue
	}
}
