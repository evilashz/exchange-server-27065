using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000290 RID: 656
	internal class ManifestContentsCallback : IMapiManifestCallback
	{
		// Token: 0x06002005 RID: 8197 RVA: 0x000440B3 File Offset: 0x000422B3
		public ManifestContentsCallback(byte[] folderId, bool isPagedEnumeration)
		{
			this.folderId = folderId;
			this.isPagedEnumeration = isPagedEnumeration;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000440CC File Offset: 0x000422CC
		public void InitializeNextPage(FolderChangesManifest folderChangesManifest, int maxChanges)
		{
			this.changes = folderChangesManifest;
			this.changes.ChangedMessages = new List<MessageRec>((!this.isPagedEnumeration) ? 0 : maxChanges);
			this.changes.ReadMessages = new List<byte[]>();
			this.changes.UnreadMessages = new List<byte[]>();
			this.maxChanges = maxChanges;
			this.countEnumeratedChanges = 0;
			bool flag = this.isPagedEnumeration;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00044134 File Offset: 0x00042334
		ManifestCallbackStatus IMapiManifestCallback.Change(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModificationTime, ManifestChangeType changeType, bool associated, PropValue[] props)
		{
			int messageSize = 0;
			if (props != null)
			{
				foreach (PropValue propValue in props)
				{
					PropTag propTag = propValue.PropTag;
					if (propTag == PropTag.MessageSize)
					{
						messageSize = propValue.GetInt();
					}
				}
			}
			MsgRecFlags msgRecFlags = associated ? MsgRecFlags.Associated : MsgRecFlags.None;
			if (changeType.Equals(ManifestChangeType.Add))
			{
				msgRecFlags |= MsgRecFlags.New;
			}
			MessageRec item = new MessageRec(entryId, this.folderId, DateTime.MinValue, messageSize, msgRecFlags, null);
			this.changes.ChangedMessages.Add(item);
			this.countEnumeratedChanges++;
			if (this.isPagedEnumeration && this.countEnumeratedChanges == this.maxChanges)
			{
				this.changes.HasMoreChanges = true;
				return ManifestCallbackStatus.Yield;
			}
			return ManifestCallbackStatus.Continue;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00044204 File Offset: 0x00042404
		ManifestCallbackStatus IMapiManifestCallback.Delete(byte[] entryId, bool softDelete, bool expiry)
		{
			MessageRec item = new MessageRec(entryId, this.folderId, DateTime.MinValue, 0, MsgRecFlags.Deleted, null);
			this.changes.ChangedMessages.Add(item);
			return ManifestCallbackStatus.Continue;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00044238 File Offset: 0x00042438
		ManifestCallbackStatus IMapiManifestCallback.ReadUnread(byte[] entryId, bool read)
		{
			if (read)
			{
				this.changes.ReadMessages.Add(entryId);
			}
			else
			{
				this.changes.UnreadMessages.Add(entryId);
			}
			return ManifestCallbackStatus.Continue;
		}

		// Token: 0x04000CF1 RID: 3313
		private readonly byte[] folderId;

		// Token: 0x04000CF2 RID: 3314
		private readonly bool isPagedEnumeration;

		// Token: 0x04000CF3 RID: 3315
		private int maxChanges;

		// Token: 0x04000CF4 RID: 3316
		private int countEnumeratedChanges;

		// Token: 0x04000CF5 RID: 3317
		private FolderChangesManifest changes;
	}
}
