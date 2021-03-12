using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	internal class ImapSourceFolder : ImapFolder, ISourceFolder, IFolder, IDisposable
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003C29 File Offset: 0x00001E29
		void ISourceFolder.CopyTo(IFxProxy fxFolderProxy, CopyPropertiesFlags flags, PropTag[] propTagsToExclude)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C2B File Offset: 0x00001E2B
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C34 File Offset: 0x00001E34
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			FolderChangesManifest folderChangesManifest = new FolderChangesManifest(base.FolderId);
			folderChangesManifest.ChangedMessages = new List<MessageRec>();
			folderChangesManifest.ReadMessages = new List<byte[]>();
			folderChangesManifest.UnreadMessages = new List<byte[]>();
			if (!base.Folder.IsSelectable)
			{
				return folderChangesManifest;
			}
			SyncContentsManifestState syncContentsManifestState = base.Mailbox.SyncState[base.FolderId];
			ImapFolderState imapFolderState;
			if (syncContentsManifestState.Data != null)
			{
				imapFolderState = ImapFolderState.Deserialize(syncContentsManifestState.Data);
				if (imapFolderState.UidValidity != base.Folder.UidValidity)
				{
					syncContentsManifestState.Data = ImapFolderState.CreateNew(base.Folder).Serialize();
					this.nextSeqNumCrawl = null;
					folderChangesManifest.FolderRecoverySync = true;
					return folderChangesManifest;
				}
			}
			else
			{
				imapFolderState = ImapFolderState.CreateNew(base.Folder);
			}
			List<MessageRec> messages = base.EnumerateMessages(FetchMessagesFlags.None, null, null);
			ImapFolderState imapFolderState2 = ImapFolderState.Create(messages, imapFolderState.SeqNumCrawl, base.Folder.UidNext, base.Folder.UidValidity);
			this.EnumerateIncrementalChanges(imapFolderState2, imapFolderState, folderChangesManifest, messages);
			syncContentsManifestState.Data = imapFolderState2.Serialize();
			return folderChangesManifest;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D4C File Offset: 0x00001F4C
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			if (!base.Folder.IsSelectable)
			{
				return null;
			}
			SyncContentsManifestState syncContentsManifestState = base.Mailbox.SyncState[base.FolderId];
			ImapFolderState imapFolderState = (syncContentsManifestState.Data != null) ? ImapFolderState.Deserialize(syncContentsManifestState.Data) : ImapFolderState.CreateNew(base.Folder);
			if (this.nextSeqNumCrawl == null)
			{
				this.nextSeqNumCrawl = new int?((imapFolderState.SeqNumCrawl == int.MaxValue) ? (base.Folder.NumberOfMessages ?? 0) : imapFolderState.SeqNumCrawl);
			}
			else
			{
				imapFolderState.SeqNumCrawl = this.nextSeqNumCrawl.Value;
				syncContentsManifestState.Data = imapFolderState.Serialize();
			}
			if (this.nextSeqNumCrawl == 0)
			{
				return null;
			}
			int num = Math.Max(1, (this.nextSeqNumCrawl - maxPageSize + 1).Value);
			List<MessageRec> result = base.EnumerateMessages(FetchMessagesFlags.IncludeExtendedData, new int?(this.nextSeqNumCrawl.Value), new int?(num));
			this.nextSeqNumCrawl = new int?(Math.Max(0, num - 1));
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003EC4 File Offset: 0x000020C4
		int ISourceFolder.GetEstimatedItemCount()
		{
			int? numberOfMessages = base.Folder.NumberOfMessages;
			if (numberOfMessages == null)
			{
				return 0;
			}
			return numberOfMessages.GetValueOrDefault();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003EF0 File Offset: 0x000020F0
		private void EnumerateIncrementalChanges(ImapFolderState currentState, ImapFolderState lastSyncedState, FolderChangesManifest changes, IEnumerable<MessageRec> messages)
		{
			Dictionary<uint, MessageRec> dictionary = new Dictionary<uint, MessageRec>();
			foreach (MessageRec messageRec in messages)
			{
				uint key = ImapEntryId.ParseUid(messageRec.EntryId);
				dictionary.Add(key, messageRec);
			}
			this.EnumerateNewMessages(currentState, lastSyncedState, changes, dictionary);
			this.EnumerateReadUnreadFlagChanges(currentState, lastSyncedState, changes, dictionary);
			this.EnumerateMessageDeletes(currentState, lastSyncedState, changes, dictionary);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003F6C File Offset: 0x0000216C
		private void EnumerateNewMessages(ImapFolderState currentState, ImapFolderState lastSyncedState, FolderChangesManifest changes, Dictionary<uint, MessageRec> lookup)
		{
			if (lastSyncedState.UidNext == 0U || lastSyncedState.UidNext == 1U)
			{
				foreach (MessageRec messageRec in lookup.Values)
				{
					messageRec.Flags |= MsgRecFlags.New;
					changes.ChangedMessages.Add(messageRec);
				}
				return;
			}
			for (uint num = currentState.UidNext - 1U; num > lastSyncedState.UidNext - 1U; num -= 1U)
			{
				MessageRec messageRec2 = null;
				if (lookup.TryGetValue(num, out messageRec2))
				{
					messageRec2.Flags |= MsgRecFlags.New;
					changes.ChangedMessages.Add(messageRec2);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000040DC File Offset: 0x000022DC
		private void EnumerateMessageDeletes(ImapFolderState currentState, ImapFolderState lastSyncedState, FolderChangesManifest changes, Dictionary<uint, MessageRec> lookup)
		{
			Action<uint> uidInclusionAction = delegate(uint uid)
			{
				MessageRec item = new MessageRec(ImapEntryId.CreateMessageEntryId(uid, this.Folder.UidValidity, this.Folder.Name, this.Mailbox.ImapConnection.ConnectionContext.UserName), this.FolderId, CommonUtils.DefaultLastModificationTime, 0, MsgRecFlags.Deleted, Array<PropValueData>.Empty);
				changes.ChangedMessages.Add(item);
			};
			Action<uint> uidExclusionAction = delegate(uint uid)
			{
				MessageRec item = null;
				if (lookup.TryGetValue(uid, out item))
				{
					changes.ChangedMessages.Add(item);
				}
			};
			ImapFolderState.EnumerateMessageDeletes(currentState, lastSyncedState, uidInclusionAction, uidExclusionAction);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041A0 File Offset: 0x000023A0
		private void EnumerateReadUnreadFlagChanges(ImapFolderState currentState, ImapFolderState lastSyncedState, FolderChangesManifest changes, Dictionary<uint, MessageRec> lookup)
		{
			Action<uint> uidInclusionAction = delegate(uint uid)
			{
				MessageRec messageRec = null;
				if (lookup.TryGetValue(uid, out messageRec))
				{
					changes.ReadMessages.Add(messageRec.EntryId);
				}
			};
			Action<uint> uidExclusionAction = delegate(uint uid)
			{
				MessageRec messageRec = null;
				if (lookup.TryGetValue(uid, out messageRec))
				{
					changes.UnreadMessages.Add(messageRec.EntryId);
				}
			};
			ImapFolderState.EnumerateReadUnreadFlagChanges(currentState, lastSyncedState, uidInclusionAction, uidExclusionAction);
		}

		// Token: 0x04000039 RID: 57
		private int? nextSeqNumCrawl;
	}
}
