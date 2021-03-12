using System;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000012 RID: 18
	internal class FastDocumentHelper : IFastDocumentHelper
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00005E38 File Offset: 0x00004038
		public void PopulateFastDocumentForFolderUpdate(IFastDocument fastDocument, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity, string folderEntryId)
		{
			fastDocument.FlowOperation = "FolderUpdate";
			fastDocument.TenantId = mailboxGuid;
			fastDocument.IsMoveDestination = isMoveDestination;
			fastDocument.IsLocalMdb = isLocalMdb;
			fastDocument.CompositeItemId = identity.ToString();
			fastDocument.IndexId = IndexId.CreateIndexId(mailboxNumber, documentId);
			fastDocument.FolderId = folderEntryId;
			this.ValidateDocumentConsistency(fastDocument, "FastDocumentHelper.PopulateFastDocumentForFolderUpdate");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005E96 File Offset: 0x00004096
		public void PopulateFastDocumentForWatermarkUpdate(IFastDocument fastDocument, long fastId, long watermark, bool recrawlMailbox)
		{
			fastDocument.FlowOperation = "WatermarkUpdate";
			fastDocument.IndexId = fastId;
			fastDocument.TenantId = WatermarkStorageId.FastWatermarkTenantId;
			fastDocument.MailboxGuid = WatermarkStorageId.FastWatermarkTenantId;
			fastDocument.Watermark = watermark;
			if (recrawlMailbox)
			{
				fastDocument.ErrorCode = 203;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005ED6 File Offset: 0x000040D6
		public void PopulateFastDocumentForDelete(IFastDocument fastDocument, Guid mailboxGuid, long documentId)
		{
			fastDocument.FlowOperation = "Delete";
			fastDocument.TenantId = mailboxGuid;
			fastDocument.IndexId = documentId;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005EF1 File Offset: 0x000040F1
		public void PopulateFastDocumentForDeleteSelection(IFastDocument fastDocument, Guid mailboxGuid)
		{
			fastDocument.FlowOperation = "DeleteSelection";
			fastDocument.TenantId = mailboxGuid;
			fastDocument.IndexId = -1L;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005F10 File Offset: 0x00004110
		public void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, bool isMoveDestination, bool isLocalMdb, long documentId, IIdentity identity)
		{
			this.PopulateFastDocumentForIndexing(fastDocument, version, mailboxGuid, isMoveDestination, isLocalMdb, documentId, identity.ToString(), 0, 0);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005F38 File Offset: 0x00004138
		public void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity)
		{
			this.PopulateFastDocumentForIndexing(fastDocument, version, mailboxGuid, isMoveDestination, isLocalMdb, IndexId.CreateIndexId(mailboxNumber, documentId), identity.ToString(), 0, 0);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005F64 File Offset: 0x00004164
		public void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, int mailboxNumber, bool isMoveDestination, bool isLocalMdb, int documentId, IIdentity identity, int errorCode, int attemptCount)
		{
			this.PopulateFastDocumentForIndexing(fastDocument, version, mailboxGuid, isMoveDestination, isLocalMdb, IndexId.CreateIndexId(mailboxNumber, documentId), identity.ToString(), errorCode, attemptCount);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005F94 File Offset: 0x00004194
		public void PopulateFastDocumentForIndexing(IFastDocument fastDocument, int version, Guid mailboxGuid, bool isMoveDestination, bool isLocalMdb, long documentId, string identity, int errorCode, int attemptCount)
		{
			fastDocument.FeedingVersion = version;
			fastDocument.FlowOperation = "Indexing";
			fastDocument.TenantId = mailboxGuid;
			fastDocument.IsMoveDestination = isMoveDestination;
			fastDocument.IsLocalMdb = isLocalMdb;
			fastDocument.MailboxGuid = mailboxGuid;
			fastDocument.CompositeItemId = identity;
			fastDocument.DocumentId = documentId;
			fastDocument.IndexId = documentId;
			if (errorCode != 0)
			{
				fastDocument.ErrorCode = errorCode;
			}
			if (attemptCount != 0)
			{
				fastDocument.AttemptCount = attemptCount;
			}
			this.ValidateDocumentConsistency(fastDocument, "FastDocumentHelper.PopulateFastDocumentForIndexing");
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006010 File Offset: 0x00004210
		public void ValidateDocumentConsistency(IFastDocument fastDocument, string context)
		{
			if (fastDocument.FlowOperation != null && fastDocument.FlowOperation != "WatermarkUpdate" && !string.IsNullOrEmpty(fastDocument.IndexSystemName))
			{
				if (IndexId.IsWatermarkIndexId(fastDocument.IndexId))
				{
					throw new DocumentValidationException(string.Format("FastDocument with FlowOperation type: {0} has a Watermark Id: {1}, Context: {2}", fastDocument.FlowOperation, fastDocument.IndexId, context));
				}
				if (fastDocument.MailboxGuid.Equals(WatermarkStorageId.FastWatermarkTenantId))
				{
					throw new DocumentValidationException(string.Format("FastDocument with FlowOperation type: {0} has the WatermarkTenantId as it's MailboxGuid. Context: {1}", fastDocument.FlowOperation, context));
				}
			}
		}
	}
}
