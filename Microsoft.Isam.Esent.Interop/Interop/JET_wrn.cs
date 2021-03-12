using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D7 RID: 727
	public enum JET_wrn
	{
		// Token: 0x040008CC RID: 2252
		Success,
		// Token: 0x040008CD RID: 2253
		RemainingVersions = 321,
		// Token: 0x040008CE RID: 2254
		UniqueKey = 345,
		// Token: 0x040008CF RID: 2255
		SeparateLongValue = 406,
		// Token: 0x040008D0 RID: 2256
		ExistingLogFileHasBadSignature = 558,
		// Token: 0x040008D1 RID: 2257
		ExistingLogFileIsNotContiguous,
		// Token: 0x040008D2 RID: 2258
		SkipThisRecord = 564,
		// Token: 0x040008D3 RID: 2259
		TargetInstanceRunning = 578,
		// Token: 0x040008D4 RID: 2260
		CommittedLogFilesLost = 585,
		// Token: 0x040008D5 RID: 2261
		CommittedLogFilesRemoved = 587,
		// Token: 0x040008D6 RID: 2262
		FinishWithUndo,
		// Token: 0x040008D7 RID: 2263
		DatabaseRepaired = 595,
		// Token: 0x040008D8 RID: 2264
		ColumnNull = 1004,
		// Token: 0x040008D9 RID: 2265
		BufferTruncated = 1006,
		// Token: 0x040008DA RID: 2266
		DatabaseAttached,
		// Token: 0x040008DB RID: 2267
		SortOverflow = 1009,
		// Token: 0x040008DC RID: 2268
		SeekNotEqual = 1039,
		// Token: 0x040008DD RID: 2269
		NoErrorInfo = 1055,
		// Token: 0x040008DE RID: 2270
		NoIdleActivity = 1058,
		// Token: 0x040008DF RID: 2271
		NoWriteLock = 1067,
		// Token: 0x040008E0 RID: 2272
		ColumnSetNull,
		// Token: 0x040008E1 RID: 2273
		ShrinkNotPossible = 1122,
		// Token: 0x040008E2 RID: 2274
		DTCCommitTransaction = 1163,
		// Token: 0x040008E3 RID: 2275
		DTCRollbackTransaction,
		// Token: 0x040008E4 RID: 2276
		TableEmpty = 1301,
		// Token: 0x040008E5 RID: 2277
		TableInUseBySystem = 1327,
		// Token: 0x040008E6 RID: 2278
		CorruptIndexDeleted = 1415,
		// Token: 0x040008E7 RID: 2279
		PrimaryIndexOutOfDate = 1417,
		// Token: 0x040008E8 RID: 2280
		SecondaryIndexOutOfDate,
		// Token: 0x040008E9 RID: 2281
		ColumnMaxTruncated = 1512,
		// Token: 0x040008EA RID: 2282
		CopyLongValue = 1520,
		// Token: 0x040008EB RID: 2283
		TaggedColumnsRemaining = 1523,
		// Token: 0x040008EC RID: 2284
		ColumnSkipped = 1531,
		// Token: 0x040008ED RID: 2285
		ColumnNotLocal,
		// Token: 0x040008EE RID: 2286
		ColumnMoreTags,
		// Token: 0x040008EF RID: 2287
		ColumnTruncated,
		// Token: 0x040008F0 RID: 2288
		ColumnPresent,
		// Token: 0x040008F1 RID: 2289
		ColumnSingleValue,
		// Token: 0x040008F2 RID: 2290
		ColumnDefault,
		// Token: 0x040008F3 RID: 2291
		ColumnNotInRecord = 1539,
		// Token: 0x040008F4 RID: 2292
		DataHasChanged = 1610,
		// Token: 0x040008F5 RID: 2293
		KeyChanged = 1618,
		// Token: 0x040008F6 RID: 2294
		FileOpenReadOnly = 1813,
		// Token: 0x040008F7 RID: 2295
		IdleFull = 1908,
		// Token: 0x040008F8 RID: 2296
		DefragAlreadyRunning = 2000,
		// Token: 0x040008F9 RID: 2297
		DefragNotRunning,
		// Token: 0x040008FA RID: 2298
		DatabaseScanAlreadyRunning,
		// Token: 0x040008FB RID: 2299
		DatabaseScanNotRunning,
		// Token: 0x040008FC RID: 2300
		CallbackNotRegistered = 2100,
		// Token: 0x040008FD RID: 2301
		PreviousLogFileIncomplete = 2602
	}
}
