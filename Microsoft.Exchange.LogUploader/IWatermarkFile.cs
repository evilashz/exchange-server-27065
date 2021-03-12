using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000016 RID: 22
	internal interface IWatermarkFile : IDisposable
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000105 RID: 261
		string WatermarkFileFullName { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000106 RID: 262
		string LogFileFullName { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000107 RID: 263
		long ProcessedSize { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000108 RID: 264
		bool IsDisposed { get; }

		// Token: 0x06000109 RID: 265
		void InMemoryCountDecrease();

		// Token: 0x0600010A RID: 266
		void InMemoryCountIncrease();

		// Token: 0x0600010B RID: 267
		void WriteWatermark(List<LogFileRange> logfileRanges);

		// Token: 0x0600010C RID: 268
		LogFileRange GetBlockToReprocess();

		// Token: 0x0600010D RID: 269
		LogFileRange GetNewBlockToProcess();

		// Token: 0x0600010E RID: 270
		void UpdateLastReaderParsedEndOffset(long newEndOffset);

		// Token: 0x0600010F RID: 271
		bool ReaderHasBytesToParse();

		// Token: 0x06000110 RID: 272
		bool IsLogCompleted();

		// Token: 0x06000111 RID: 273
		void CreateDoneFile();
	}
}
