﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200023E RID: 574
	internal class LogSearchMailboxAction : SearchMailboxAction
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x0004B72B File Offset: 0x0004992B
		public LogSearchMailboxAction(LoggingLevel loggingLevel)
		{
			this.loggingLevel = loggingLevel;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004B741 File Offset: 0x00049941
		public override SearchMailboxAction Clone()
		{
			return new LogSearchMailboxAction(this.loggingLevel);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004B750 File Offset: 0x00049950
		public override void PerformBatchOperation(object[][] batchedItemBuffer, int fetchedItemCount, StoreId currentFolderId, MailboxSession sourceMailbox, MailboxSession targetMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, SearchResultProcessor processor)
		{
			if (this.loggingLevel != LoggingLevel.Full)
			{
				return;
			}
			LocalizedString[] array = new LocalizedString[fetchedItemCount];
			for (int i = 0; i < fetchedItemCount; i++)
			{
				VersionedId versionedId = (VersionedId)batchedItemBuffer[i][0];
				string text = SearchMailboxAction.PropertyExists(batchedItemBuffer[i][3]) ? ((string)batchedItemBuffer[i][3]) : string.Empty;
				bool? flag = null;
				if (SearchMailboxAction.PropertyExists(batchedItemBuffer[i][4]))
				{
					flag = new bool?((bool)batchedItemBuffer[i][4]);
				}
				ExDateTime? exDateTime = null;
				if (SearchMailboxAction.PropertyExists(batchedItemBuffer[i][5]))
				{
					exDateTime = new ExDateTime?((ExDateTime)batchedItemBuffer[i][5]);
				}
				ExDateTime? exDateTime2 = null;
				if (SearchMailboxAction.PropertyExists(batchedItemBuffer[i][6]))
				{
					exDateTime2 = new ExDateTime?((ExDateTime)batchedItemBuffer[i][6]);
				}
				Participant participant = null;
				if (SearchMailboxAction.PropertyExists(batchedItemBuffer[i][7]))
				{
					participant = (Participant)batchedItemBuffer[i][7];
				}
				string text2 = null;
				if (SearchMailboxAction.PropertyExists(batchedItemBuffer[i][8]))
				{
					text2 = (string)batchedItemBuffer[i][8];
				}
				string text3 = (participant != null) ? participant.DisplayName : string.Empty;
				string text4 = (participant != null) ? participant.EmailAddress : string.Empty;
				string displayName = folderNodeMap[currentFolderId].DisplayName;
				array[i] = new LocalizedString(string.Format("{0},\"{1}\",\"{2}\",{3},{4},{5},{6},{7},{8}", new object[]
				{
					sourceMailbox.MailboxOwner.MailboxInfo.DisplayName,
					displayName,
					text,
					flag,
					exDateTime,
					exDateTime2,
					text3,
					text2 ?? text4,
					versionedId.ObjectId
				}));
			}
			StreamLogItem.LogItem logItem = new StreamLogItem.LogItem(processor.WorkerId, array);
			processor.ReportLogs(logItem);
		}

		// Token: 0x04000B3F RID: 2879
		private LoggingLevel loggingLevel = LoggingLevel.Basic;
	}
}
