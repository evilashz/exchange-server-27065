using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D0 RID: 208
	[Flags]
	internal enum Breadcrumb
	{
		// Token: 0x04000380 RID: 896
		None = 0,
		// Token: 0x04000381 RID: 897
		NewItem = 16777216,
		// Token: 0x04000382 RID: 898
		CloneItem = 33554432,
		// Token: 0x04000383 RID: 899
		CloseItem = 50331648,
		// Token: 0x04000384 RID: 900
		BeginTransaction = 67108864,
		// Token: 0x04000385 RID: 901
		CommitTransaction = 83886080,
		// Token: 0x04000386 RID: 902
		MarkDeleted = 100663296,
		// Token: 0x04000387 RID: 903
		Loaded = 117440512,
		// Token: 0x04000388 RID: 904
		Deleted = 134217728,
		// Token: 0x04000389 RID: 905
		Seek = 150994944,
		// Token: 0x0400038A RID: 906
		SeekFail = 167772160,
		// Token: 0x0400038B RID: 907
		InternalCommitTransaction = 184549376,
		// Token: 0x0400038C RID: 908
		MaterializeToRow = 201326592,
		// Token: 0x0400038D RID: 909
		MaterializeNew = 218103808,
		// Token: 0x0400038E RID: 910
		MaterializeUpdate = 234881024,
		// Token: 0x0400038F RID: 911
		AcknowledgeA = 251658240,
		// Token: 0x04000390 RID: 912
		AcknowledgeB = 268435456,
		// Token: 0x04000391 RID: 913
		AsyncExecute = 285212672,
		// Token: 0x04000392 RID: 914
		CommitNow = 301989888,
		// Token: 0x04000393 RID: 915
		CommitForReceive = 318767104,
		// Token: 0x04000394 RID: 916
		CommitLazy = 335544320,
		// Token: 0x04000395 RID: 917
		AppendingToExisting = 352321536,
		// Token: 0x04000396 RID: 918
		Push = 369098752,
		// Token: 0x04000397 RID: 919
		Pending = 385875968,
		// Token: 0x04000398 RID: 920
		TimedOut = 402653184,
		// Token: 0x04000399 RID: 921
		Moved = 419430400,
		// Token: 0x0400039A RID: 922
		Background = 436207616,
		// Token: 0x0400039B RID: 923
		Execute = 452984832,
		// Token: 0x0400039C RID: 924
		Done = 469762048,
		// Token: 0x0400039D RID: 925
		Shutdown = 486539264,
		// Token: 0x0400039E RID: 926
		Signaled = 503316480,
		// Token: 0x0400039F RID: 927
		DehydrateOnAddToShadowQueue = 520093696,
		// Token: 0x040003A0 RID: 928
		DehydrateOnLimitedMailItemMemoryPressure = 536870912,
		// Token: 0x040003A1 RID: 929
		DehydrateOnBackPressure = 553648128,
		// Token: 0x040003A2 RID: 930
		ScopedRecipients = 570425344,
		// Token: 0x040003A3 RID: 931
		UnscopedRecipients = 587202560,
		// Token: 0x040003A4 RID: 932
		DehydrateOnRoutedMailItemDeferral = 603979776,
		// Token: 0x040003A5 RID: 933
		DehydrateOnRoutingDone = 620756992,
		// Token: 0x040003A6 RID: 934
		DehydrationSkippedItemInDelivery = 637534208,
		// Token: 0x040003A7 RID: 935
		DehydrateOnCategorizerDeferral = 654311424,
		// Token: 0x040003A8 RID: 936
		DehydrateOnMailItemUpdate = 671088640,
		// Token: 0x040003A9 RID: 937
		DehydrateOnReleaseFromDumpster = 687865856,
		// Token: 0x040003AA RID: 938
		DehydrateOnReleaseFromShadowRedundancy = 704643072,
		// Token: 0x040003AB RID: 939
		DehydrateOnReleaseFromRemoteDelivery = 721420288,
		// Token: 0x040003AC RID: 940
		DehydrateMinimizedMemory = 738197504,
		// Token: 0x040003AD RID: 941
		DehydrateOnCategorizerMemoryPressure = 754974720,
		// Token: 0x040003AE RID: 942
		Dehydrated = 771751936,
		// Token: 0x040003AF RID: 943
		DehydrationSkippedItemLock = 788529152,
		// Token: 0x040003B0 RID: 944
		MailItemDelivered = 973078528,
		// Token: 0x040003B1 RID: 945
		MailItemDeleted = 989855744,
		// Token: 0x040003B2 RID: 946
		MailItemPoison = 1006632960,
		// Token: 0x040003B3 RID: 947
		DehydrateOnMailItemLocked = 1023410176,
		// Token: 0x040003B4 RID: 948
		OpenMimeReadStream = 1040187392,
		// Token: 0x040003B5 RID: 949
		OpenMimeWriteStream = 1056964608,
		// Token: 0x040003B6 RID: 950
		PromoteHeaders = 1073741824,
		// Token: 0x040003B7 RID: 951
		RestoreLastSavedMime = 1090519040,
		// Token: 0x040003B8 RID: 952
		SetMimeDocument = 1107296256,
		// Token: 0x040003B9 RID: 953
		SetReadOnly = 1124073472,
		// Token: 0x040003BA RID: 954
		MinimizeMemory = 1140850688,
		// Token: 0x040003BB RID: 955
		LoadFromParentRow = 1157627904,
		// Token: 0x040003BC RID: 956
		SaveToParentRow = 1174405120,
		// Token: 0x040003BD RID: 957
		Cleanup = 1191182336,
		// Token: 0x040003BE RID: 958
		NewSideEffectItem = 1207959552
	}
}
