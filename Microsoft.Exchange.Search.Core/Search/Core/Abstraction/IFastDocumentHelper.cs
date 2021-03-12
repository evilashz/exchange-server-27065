using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000023 RID: 35
	internal interface IFastDocumentHelper
	{
		// Token: 0x060000C1 RID: 193
		void PopulateFastDocumentForFolderUpdate(IFastDocument fastDocument, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity, string folderEntryId);

		// Token: 0x060000C2 RID: 194
		void PopulateFastDocumentForWatermarkUpdate(IFastDocument fastDocument, long fastId, long watermark, bool recrawlMailbox);

		// Token: 0x060000C3 RID: 195
		void PopulateFastDocumentForDelete(IFastDocument fastDocument, Guid mailboxGuid, long documentId);

		// Token: 0x060000C4 RID: 196
		void PopulateFastDocumentForDeleteSelection(IFastDocument fastDocument, Guid mailboxGuid);

		// Token: 0x060000C5 RID: 197
		void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, bool isMoveDestination, bool isLocalMdb, long documentId, IIdentity identity);

		// Token: 0x060000C6 RID: 198
		void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity);

		// Token: 0x060000C7 RID: 199
		void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity, int errorCode, int attemptCount);

		// Token: 0x060000C8 RID: 200
		void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, bool isMoveDestination, bool isLocalMdb, long documentId, string identity, int errorCode, int attemptCount);

		// Token: 0x060000C9 RID: 201
		void ValidateDocumentConsistency(IFastDocument fastDocument, string context);
	}
}
