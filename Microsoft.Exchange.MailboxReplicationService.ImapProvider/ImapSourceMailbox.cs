using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Imap;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000C RID: 12
	internal class ImapSourceMailbox : ImapMailbox, ISourceMailbox, IMailbox, IDisposable, ISupportMime, IReplayProvider
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000041E5 File Offset: 0x000023E5
		public ImapSourceMailbox(ConnectionParameters connectionParameters, ImapAuthenticationParameters authenticationParameters, ImapServerParameters serverParameters, SmtpServerParameters smtpParameters) : base(connectionParameters, authenticationParameters, serverParameters, smtpParameters)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000041F2 File Offset: 0x000023F2
		internal override bool SupportsSavingSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000041F5 File Offset: 0x000023F5
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.Imap;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000041F8 File Offset: 0x000023F8
		Stream ISupportMime.GetMimeStream(MessageRec message, out PropValueData[] extraPropValues)
		{
			extraPropValues = null;
			string messageUid = ImapEntryId.ParseUid(message.EntryId).ToString(CultureInfo.InvariantCulture);
			Stream result;
			ExDateTime? exDateTime;
			this.FetchMessage(messageUid, out result, out exDateTime);
			if (exDateTime != null)
			{
				extraPropValues = new PropValueData[]
				{
					new PropValueData(PropTag.MessageDeliveryTime, exDateTime.Value.ToUtc())
				};
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004265 File Offset: 0x00002465
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000426C File Offset: 0x0000246C
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<ImapSourceFolder>(entryId);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000042A0 File Offset: 0x000024A0
		void ISourceMailbox.CopyTo(IFxProxy destMailboxProxy, PropTag[] excludeTags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000042A7 File Offset: 0x000024A7
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			base.SetMailboxSyncState(syncStateStr);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000042B0 File Offset: 0x000024B0
		string ISourceMailbox.GetMailboxSyncState()
		{
			return base.GetMailboxSyncState();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000042D4 File Offset: 0x000024D4
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			bool catchup = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			return this.EnumerateHierarchyChanges(catchup, (SyncHierarchyManifestState hierarchyData) => this.RunManualHierarchySync(catchup, hierarchyData));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004370 File Offset: 0x00002570
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.ExportMessages({0} messages)", new object[]
			{
				messages.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.CopyMessagesOneByOne(messages, proxyPool, propsToCopyExplicitly, excludeProps, delegate(MessageRec curMsg)
			{
				using (ImapFolder folder = base.GetFolder<ImapSourceFolder>(curMsg.FolderId))
				{
					if (folder == null)
					{
						throw new FolderIsMissingTransientException();
					}
					folder.Folder.SelectImapFolder(base.ImapConnection);
				}
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000043C1 File Offset: 0x000025C1
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000043C8 File Offset: 0x000025C8
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.ReplayActions({0} actions)", new object[]
			{
				actions.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			return this.Replay(actions);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004408 File Offset: 0x00002608
		void IReplayProvider.MarkAsRead(IReadOnlyCollection<MarkAsReadAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.MarkAsRead({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (MarkAsReadAction markAsReadAction in actions)
			{
				this.SetReadFlags(markAsReadAction.ItemId, markAsReadAction.FolderId, true);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004484 File Offset: 0x00002684
		void IReplayProvider.MarkAsUnRead(IReadOnlyCollection<MarkAsUnReadAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.MarkAsUnRead({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (MarkAsUnReadAction markAsUnReadAction in actions)
			{
				this.SetReadFlags(markAsUnReadAction.ItemId, markAsUnReadAction.FolderId, false);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004500 File Offset: 0x00002700
		IReadOnlyCollection<MoveActionResult> IReplayProvider.Move(IReadOnlyCollection<MoveAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.Move({0} actions)", new object[]
			{
				actions.Count
			});
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004538 File Offset: 0x00002738
		void IReplayProvider.Send(SendAction action)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.Send({0})", new object[]
			{
				action
			});
			SmtpClientHelper.Submit(action, base.SmtpParameters.Server, base.SmtpParameters.Port, base.AuthenticationParameters.NetworkCredential);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004588 File Offset: 0x00002788
		void IReplayProvider.Delete(IReadOnlyCollection<DeleteAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.Delete({0} actions)", new object[]
			{
				actions.Count
			});
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000045C0 File Offset: 0x000027C0
		void IReplayProvider.Flag(IReadOnlyCollection<FlagAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.Flag({0} actions)", new object[]
			{
				actions.Count
			});
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000045F8 File Offset: 0x000027F8
		void IReplayProvider.FlagClear(IReadOnlyCollection<FlagClearAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.FlagClear({0} actions)", new object[]
			{
				actions.Count
			});
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004630 File Offset: 0x00002830
		void IReplayProvider.FlagComplete(IReadOnlyCollection<FlagCompleteAction> actions)
		{
			MrsTracer.Provider.Function("ImapSourceMailbox.FlagComplete({0} actions)", new object[]
			{
				actions.Count
			});
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004667 File Offset: 0x00002867
		IReadOnlyCollection<CreateCalendarEventActionResult> IReplayProvider.CreateCalendarEvent(IReadOnlyCollection<CreateCalendarEventAction> actions)
		{
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000466E File Offset: 0x0000286E
		void IReplayProvider.UpdateCalendarEvent(IReadOnlyCollection<UpdateCalendarEventAction> actions)
		{
			throw new ActionNotSupportedException();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004698 File Offset: 0x00002898
		protected override void CopySingleMessage(MessageRec message, IFolderProxy folderProxy, PropTag[] propTagsToExclude, PropTag[] excludeProps)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("ImapSourceMailbox.CopySingleMessage", OperationType.None),
				new EntryIDsDataContext(message.EntryId)
			}).Execute(delegate
			{
				SyncEmailUtils.CopyMimeStream(this, message, folderProxy);
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004700 File Offset: 0x00002900
		private void FetchMessage(string messageUid, out Stream mimeStream, out ExDateTime? internalDate)
		{
			base.CheckDisposed();
			internalDate = null;
			mimeStream = null;
			ImapResultData messageItemByUid = base.ImapConnection.GetMessageItemByUid(messageUid, ImapConnection.MessageBodyDataItems);
			if (messageItemByUid.MessageStream == null)
			{
				throw new UnableToFetchMimeStreamException(messageUid);
			}
			mimeStream = messageItemByUid.MessageStream;
			internalDate = ((messageItemByUid.MessageInternalDates != null && messageItemByUid.MessageInternalDates.Count == 1) ? messageItemByUid.MessageInternalDates[0] : null);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000477C File Offset: 0x0000297C
		private void SetReadFlags(byte[] messageEntryId, byte[] folderEntryId, bool isRead)
		{
			base.CheckDisposed();
			using (ImapFolder folder = base.GetFolder<ImapSourceFolder>(folderEntryId))
			{
				if (folder == null)
				{
					MrsTracer.Provider.Warning("Source folder {0} doesn't exist", new object[]
					{
						TraceUtils.DumpBytes(folderEntryId)
					});
					throw new ImapObjectNotFoundException(TraceUtils.DumpBytes(folderEntryId));
				}
				uint item = ImapEntryId.ParseUid(messageEntryId);
				List<uint> list = new List<uint>(1);
				list.Add(item);
				List<ImapMessageRec> list2 = folder.Folder.LookupMessages(base.ImapConnection, list);
				if (list2.Count == 0)
				{
					MrsTracer.Provider.Warning("Source message {0} doesn't exist", new object[]
					{
						TraceUtils.DumpBytes(messageEntryId)
					});
					throw new ImapObjectNotFoundException(TraceUtils.DumpBytes(messageEntryId));
				}
				ImapMailFlags imapMailFlags = list2[0].ImapMailFlags;
				ImapMailFlags imapMailFlags2 = isRead ? (imapMailFlags | ImapMailFlags.Seen) : (imapMailFlags & ~ImapMailFlags.Seen);
				if (imapMailFlags != imapMailFlags2)
				{
					string text = item.ToString(CultureInfo.InvariantCulture);
					MrsTracer.Provider.Debug("StoreMessageFlags - uid: {0}, flagsToStore: {1}, previousFlags {2}", new object[]
					{
						text,
						imapMailFlags2,
						imapMailFlags
					});
					base.ImapConnection.StoreMessageFlags(text, imapMailFlags2, imapMailFlags);
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000048C8 File Offset: 0x00002AC8
		ResourceHealthTracker ISupportMime.get_RHTracker()
		{
			return base.RHTracker;
		}
	}
}
