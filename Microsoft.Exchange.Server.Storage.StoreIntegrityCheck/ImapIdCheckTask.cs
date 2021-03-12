﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000013 RID: 19
	public class ImapIdCheckTask : IntegrityCheckTaskBase, IIntegrityCheckTaskWithContinuation
	{
		// Token: 0x06000056 RID: 86 RVA: 0x0000463A File Offset: 0x0000283A
		public ImapIdCheckTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004643 File Offset: 0x00002843
		public override string TaskName
		{
			get
			{
				return "ImapId";
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000464A File Offset: 0x0000284A
		internal static int BatchSize
		{
			get
			{
				return ImapIdCheckTask.messageSaveBatchCount;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004654 File Offset: 0x00002854
		public bool ContinueExecuteOnFolder(Context context, MailboxEntry mailboxEntry, FolderEntry folderEntry)
		{
			if (this.currentMailbox == null || mailboxEntry.MailboxGuid != this.currentMailbox.MailboxGuid)
			{
				this.corruptedMessages = null;
			}
			if (this.currentFolder == null || folderEntry.FolderId != this.currentFolder.FolderId)
			{
				this.corruptedMessages = null;
			}
			return this.corruptedMessages != null && this.corruptedMessages.Count > 0;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000046C8 File Offset: 0x000028C8
		public override ErrorCode ExecuteOneFolder(Mailbox mailbox, MailboxEntry mailboxEntry, FolderEntry folderEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string, string>(0L, "Execute task {0} on folder {1}", this.TaskName, folderEntry.ToString());
			}
			this.currentMailbox = mailboxEntry;
			this.currentFolder = folderEntry;
			ErrorCode errorCode = ErrorCode.NoError;
			Context currentOperationContext = mailbox.CurrentOperationContext;
			using (Folder folder = Folder.OpenFolder(currentOperationContext, mailbox, folderEntry.FolderId))
			{
				if (folder == null)
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string>(0L, "Folder {0} has been removed, continue with other folders", folderEntry.ToString());
					}
					return ErrorCode.NoError;
				}
				if (this.corruptedMessages == null)
				{
					int num = (int)folder.GetColumnValue(currentOperationContext, folder.FolderTable.NextArticleNumber);
					List<MessageEntry> list;
					int num2;
					errorCode = this.GetCorruptedMessages(mailbox, folderEntry.FolderId, num, out list, out num2, shouldContinue);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.OnlineIsintegTracer.TraceError<string, ErrorCode>(0L, "Unexpected error when retrieving corrupted messages in folder {0}, error code {1}", folderEntry.ToString(), errorCode);
						}
						return errorCode.Propagate((LID)57628U);
					}
					if (list.Count == 0 && num2 < num)
					{
						if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string>(0L, "No corrupted message found in folder {0}", folderEntry.ToString());
						}
						return ErrorCode.NoError;
					}
					this.ReportAndFixCorruptionOnFolder(mailbox, folder, num, num2, detectOnly);
					this.corruptedMessages = list;
				}
				if (!shouldContinue())
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
					}
					return ErrorCode.CreateExiting((LID)53532U);
				}
				if (this.corruptedMessages != null && this.corruptedMessages.Count > 0)
				{
					int count = Math.Min(this.corruptedMessages.Count, ImapIdCheckTask.messageSaveBatchCount);
					List<MessageEntry> range = this.corruptedMessages.GetRange(0, count);
					errorCode = this.ReportAndFixCorruption(mailbox, folder, range, detectOnly, shouldContinue);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.OnlineIsintegTracer.TraceError<string, ErrorCode>(0L, "Unexpected error when fixing corruption in folder {0}, error code {1}", folderEntry.ToString(), errorCode);
						}
						this.corruptedMessages = null;
						return errorCode.Propagate((LID)61724U);
					}
					this.corruptedMessages.RemoveRange(0, count);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004A6C File Offset: 0x00002C6C
		private ErrorCode GetCorruptedMessages(Mailbox mailbox, ExchangeId folderId, int nextArticleNumber, out List<MessageEntry> corruptedMessages, out int maxArticleNumber, Func<bool> shouldContinue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			Context context = mailbox.CurrentOperationContext;
			HashSet<MessageEntry> localCorruptedMessages = new HashSet<MessageEntry>();
			int localMaxArticleNumber = 0;
			MessagePropValueGetter messagePropValueGetter = new MessagePropValueGetter(context, mailbox.MailboxPartitionNumber, folderId);
			if (!shouldContinue())
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
				}
				corruptedMessages = new List<MessageEntry>(0);
				maxArticleNumber = 0;
				return ErrorCode.CreateExiting((LID)37148U);
			}
			Dictionary<int, MessageEntry> msgByImapId = new Dictionary<int, MessageEntry>();
			Dictionary<int, MessageEntry> msgByArticleNum = new Dictionary<int, MessageEntry>();
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			errorCode = messagePropValueGetter.Execute(false, new Column[]
			{
				messageTable.MessageDocumentId,
				messageTable.MessageId,
				messageTable.IMAPId,
				messageTable.ArticleNumber
			}, delegate(Reader reader)
			{
				int @int = reader.GetInt32(messageTable.MessageDocumentId);
				byte[] binary = reader.GetBinary(messageTable.MessageId);
				int int2 = reader.GetInt32(messageTable.IMAPId);
				int int3 = reader.GetInt32(messageTable.ArticleNumber);
				ExchangeId messageId = ExchangeId.CreateFrom26ByteArray(context, mailbox.ReplidGuidMap, binary);
				MessageEntry messageEntry = new MessageEntry(@int, messageId);
				MessageEntry item;
				if (msgByImapId.TryGetValue(int2, out item))
				{
					localCorruptedMessages.Add(item);
					localCorruptedMessages.Add(messageEntry);
				}
				if (msgByArticleNum.TryGetValue(int3, out item))
				{
					localCorruptedMessages.Add(item);
					localCorruptedMessages.Add(messageEntry);
				}
				msgByImapId[int2] = messageEntry;
				msgByArticleNum[int3] = messageEntry;
				localMaxArticleNumber = Math.Max(localMaxArticleNumber, int2);
				localMaxArticleNumber = Math.Max(localMaxArticleNumber, int3);
				return ErrorCode.NoError;
			}, shouldContinue);
			maxArticleNumber = localMaxArticleNumber;
			corruptedMessages = new List<MessageEntry>(0);
			corruptedMessages.AddRange(localCorruptedMessages);
			if (errorCode != ErrorCode.NoError)
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError<ErrorCode>(0L, "Unexpected error when check messages for corruption, error code {0}", errorCode);
				}
				return errorCode.Propagate((LID)45340U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004BF0 File Offset: 0x00002DF0
		private void ReportAndFixCorruptionOnFolder(Mailbox mailbox, Folder folder, int nextArticleNumber, int maxArticleNumber, bool detectOnly)
		{
			Context currentOperationContext = mailbox.CurrentOperationContext;
			bool problemFixed = false;
			if (!detectOnly)
			{
				if (maxArticleNumber >= nextArticleNumber)
				{
					folder.SetColumn(currentOperationContext, folder.FolderTable.NextArticleNumber, maxArticleNumber + 1);
					folder.Save(currentOperationContext);
				}
				problemFixed = true;
			}
			if (maxArticleNumber >= nextArticleNumber)
			{
				base.ReportCorruption("Folder has an invalid next article number", this.currentMailbox, this.currentFolder, null, CorruptionType.InvalidImapID, problemFixed);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004C54 File Offset: 0x00002E54
		private ErrorCode ReportAndFixCorruption(Mailbox mailbox, Folder folder, List<MessageEntry> corruptedMessages, bool detectOnly, Func<bool> shouldContinue)
		{
			Context currentOperationContext = mailbox.CurrentOperationContext;
			bool problemFixed = false;
			if (!detectOnly)
			{
				foreach (MessageEntry messageEntry in corruptedMessages)
				{
					if (!shouldContinue())
					{
						if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
						}
						return ErrorCode.CreateExiting((LID)44476U);
					}
					using (TopMessage topMessage = TopMessage.OpenMessage(currentOperationContext, mailbox, folder.GetId(currentOperationContext), messageEntry.MessageId))
					{
						topMessage.SetColumn(currentOperationContext, topMessage.MessageTable.ArticleNumber, null);
						topMessage.SetColumn(currentOperationContext, topMessage.MessageTable.IMAPId, null);
						topMessage.SaveChanges(currentOperationContext, SaveMessageChangesFlags.ForceSave | SaveMessageChangesFlags.SkipMailboxQuotaCheck | SaveMessageChangesFlags.SkipFolderQuotaCheck);
					}
				}
				folder.Save(currentOperationContext);
				problemFixed = true;
			}
			foreach (MessageEntry messageEntry2 in corruptedMessages)
			{
				base.ReportCorruption("Message has an invalid IMAP ID or Article Number", this.currentMailbox, this.currentFolder, messageEntry2, CorruptionType.InvalidImapID, problemFixed);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x04000015 RID: 21
		private static int messageSaveBatchCount = 512;

		// Token: 0x04000016 RID: 22
		private MailboxEntry currentMailbox;

		// Token: 0x04000017 RID: 23
		private FolderEntry currentFolder;

		// Token: 0x04000018 RID: 24
		private List<MessageEntry> corruptedMessages;
	}
}
