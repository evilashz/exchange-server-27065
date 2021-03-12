using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasSourceFolder : EasFolder, ISourceFolder, IFolder, IDisposable
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005097 File Offset: 0x00003297
		internal EasSourceFolder(Add add, UserSmtpAddress userSmtpAddress) : base(add, userSmtpAddress)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000050B1 File Offset: 0x000032B1
		void ISourceFolder.CopyTo(IFxProxy fxFolderProxy, CopyPropertiesFlags flags, PropTag[] propTagsToExclude)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000050B3 File Offset: 0x000032B3
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000050BC File Offset: 0x000032BC
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			FolderChangesManifest folderChangesManifest = base.CreateInitializedChangesManifest();
			SyncContentsManifestState syncContentsManifestState = base.Mailbox.SyncState[base.EntryId];
			EasFolderSyncState persistedSyncState = base.Mailbox.GetPersistedSyncState(syncContentsManifestState);
			string syncKey = persistedSyncState.SyncKey;
			EasSyncOptions options = new EasSyncOptions
			{
				SyncKey = syncKey,
				RecentOnly = true,
				MaxNumberOfMessage = 512
			};
			EasSyncResult easSyncResult = base.SyncMessages(base.Mailbox.EasConnectionWrapper, options);
			List<MessageRec> messageRecs = easSyncResult.MessageRecs;
			this.EnumerateIncrementalChanges(folderChangesManifest, messageRecs);
			this.MergePendingDeletes(folderChangesManifest, persistedSyncState.CrawlerDeletions);
			persistedSyncState.SyncKey = easSyncResult.NewSyncKey;
			persistedSyncState.ChangesSynced = new DateTime?(DateTime.UtcNow);
			persistedSyncState.CrawlerDeletions.Clear();
			syncContentsManifestState.Data = persistedSyncState.Serialize();
			return folderChangesManifest;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000518C File Offset: 0x0000338C
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			SyncContentsManifestState syncContentsManifestState = base.Mailbox.SyncState[base.EntryId];
			EasFolderSyncState persistedSyncState = base.Mailbox.GetPersistedSyncState(syncContentsManifestState);
			if (string.IsNullOrEmpty(this.nextEnumerateKey))
			{
				this.nextEnumerateKey = persistedSyncState.CrawlerSyncKey;
			}
			else
			{
				persistedSyncState.CrawlerSyncKey = this.nextEnumerateKey;
				persistedSyncState.CrawlerDeletions.AddRange(this.pendingDeletes);
				syncContentsManifestState.Data = persistedSyncState.Serialize();
			}
			EasSyncOptions options = new EasSyncOptions
			{
				SyncKey = this.nextEnumerateKey,
				RecentOnly = false,
				MaxNumberOfMessage = maxPageSize
			};
			EasSyncResult easSyncResult = base.SyncMessages(base.Mailbox.CrawlerConnectionWrapper, options);
			if (this.estimatedItemCount == 0)
			{
				options.SyncKey = easSyncResult.SyncKeyRequested;
				this.estimatedItemCount = base.GetItemEstimate(base.Mailbox.CrawlerConnectionWrapper, options);
			}
			if (easSyncResult.MessageRecs.Count == 0)
			{
				return null;
			}
			this.nextEnumerateKey = easSyncResult.NewSyncKey;
			List<MessageRec> list = new List<MessageRec>(easSyncResult.MessageRecs.Count);
			foreach (MessageRec messageRec in easSyncResult.MessageRecs)
			{
				if (persistedSyncState.ChangesSynced == null || !(messageRec.CreationTimestamp < persistedSyncState.ChangesSynced.Value) || !(messageRec.CreationTimestamp > persistedSyncState.ChangesSynced.Value - EasRequestGenerator.RecentSyncTimeSpan))
				{
					if (EasSourceFolder.FindMessageCategory(messageRec) == EasMessageCategory.Delete)
					{
						this.pendingDeletes.Add(EasMailbox.GetStringId(messageRec.EntryId));
					}
					else
					{
						list.Add(messageRec);
					}
				}
			}
			return list;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005368 File Offset: 0x00003568
		int ISourceFolder.GetEstimatedItemCount()
		{
			return this.estimatedItemCount;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005380 File Offset: 0x00003580
		internal static EasMessageCategory FindMessageCategory(MessageRec message)
		{
			PropValueData propValueData = Array.Find<PropValueData>(message.AdditionalProps, (PropValueData pdv) => pdv.PropTag == 268304387);
			if (propValueData == null)
			{
				throw new EasMissingMessageCategoryException();
			}
			return (EasMessageCategory)propValueData.Value;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000053CC File Offset: 0x000035CC
		private void EnumerateIncrementalChanges(FolderChangesManifest changes, IReadOnlyCollection<MessageRec> messageRecs)
		{
			if (messageRecs.Count == 0)
			{
				return;
			}
			foreach (MessageRec messageRec in messageRecs)
			{
				switch (EasSourceFolder.FindMessageCategory(messageRec))
				{
				case EasMessageCategory.AddOrUpdate:
				case EasMessageCategory.Delete:
					changes.ChangedMessages.Add(messageRec);
					break;
				case EasMessageCategory.ChangeToRead:
					changes.ReadMessages.Add(messageRec.EntryId);
					break;
				case EasMessageCategory.ChangeToUnread:
					changes.UnreadMessages.Add(messageRec.EntryId);
					break;
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005494 File Offset: 0x00003694
		private void MergePendingDeletes(FolderChangesManifest changes, IReadOnlyCollection<string> pendingDeletes)
		{
			if (pendingDeletes.Count == 0)
			{
				return;
			}
			List<DeleteCommand> list = new List<DeleteCommand>(pendingDeletes.Count);
			foreach (string serverId in pendingDeletes)
			{
				list.Add(new DeleteCommand
				{
					ServerId = serverId
				});
			}
			using (IEnumerator<MessageRec> enumerator2 = base.CreateMessageRecsForDeletions(list).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					MessageRec messageRec = enumerator2.Current;
					if (changes.ChangedMessages.Find((MessageRec rec) => ArrayComparer<byte>.EqualityComparer.Equals(rec.EntryId, messageRec.EntryId)) == null)
					{
						changes.ChangedMessages.Add(messageRec);
					}
				}
			}
		}

		// Token: 0x0400004B RID: 75
		private readonly List<string> pendingDeletes = new List<string>(512);

		// Token: 0x0400004C RID: 76
		private string nextEnumerateKey;

		// Token: 0x0400004D RID: 77
		private int estimatedItemCount;
	}
}
