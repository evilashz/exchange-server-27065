using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200001C RID: 28
	internal interface IDocumentTracker : IDiagnosable
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007D RID: 125
		int PoisonDocumentsCount { get; }

		// Token: 0x0600007E RID: 126
		void Initialize(IFailedItemStorage failedItemStorage);

		// Token: 0x0600007F RID: 127
		void RecordDocumentProcessing(Guid instance, Guid correlationId, long docId);

		// Token: 0x06000080 RID: 128
		void RecordDocumentProcessingComplete(Guid correlationId, long docId, bool isTrackedAsPoison);

		// Token: 0x06000081 RID: 129
		void MarkCurrentlyTrackedDocumentsAsPoison();

		// Token: 0x06000082 RID: 130
		void MarkDocumentAsPoison(long docId);

		// Token: 0x06000083 RID: 131
		void MarkDocumentAsRetriablePoison(long docId);

		// Token: 0x06000084 RID: 132
		int ShouldDocumentBeStampedWithError(long docId);

		// Token: 0x06000085 RID: 133
		bool ShouldDocumentBeSkipped(long docId);
	}
}
