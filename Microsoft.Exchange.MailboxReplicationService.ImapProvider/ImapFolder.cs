using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Imap;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal abstract class ImapFolder : DisposeTrackableBase, IFolder, IDisposable
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C18 File Offset: 0x00000E18
		public ImapFolder()
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002C20 File Offset: 0x00000E20
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002C28 File Offset: 0x00000E28
		public ImapClientFolder Folder { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C31 File Offset: 0x00000E31
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002C39 File Offset: 0x00000E39
		public ImapMailbox Mailbox { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C42 File Offset: 0x00000E42
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002C4A File Offset: 0x00000E4A
		public byte[] FolderId { get; private set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002C53 File Offset: 0x00000E53
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			return this.Mailbox.CreateFolderRec(this.Folder);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C66 File Offset: 0x00000E66
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C78 File Offset: 0x00000E78
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			List<uint> list = new List<uint>(keysToLookup.Count);
			list.AddRange(keysToLookup.Select(new Func<byte[], uint>(ImapEntryId.ParseUid)));
			list.Sort((uint x, uint y) => y.CompareTo(x));
			List<ImapMessageRec> imapMessageRecs = this.Folder.LookupMessages(this.Mailbox.ImapConnection, list);
			return this.GetMessageRecs(imapMessageRecs);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CEB File Offset: 0x00000EEB
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			return null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002CEE File Offset: 0x00000EEE
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CF5 File Offset: 0x00000EF5
		byte[] IFolder.GetFolderId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CFC File Offset: 0x00000EFC
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D03 File Offset: 0x00000F03
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D0A File Offset: 0x00000F0A
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D11 File Offset: 0x00000F11
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			return new RuleData[0];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D19 File Offset: 0x00000F19
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D20 File Offset: 0x00000F20
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D27 File Offset: 0x00000F27
		PropProblemData[] IFolder.SetProps(PropValueData[] pvda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D30 File Offset: 0x00000F30
		internal static SyncEmailContext GetSyncEmailContext(ImapClientFolder folder, ImapMessageRec messageRec)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfNull("messageRec", messageRec);
			SyncEmailContext result = default(SyncEmailContext);
			ImapMailFlags imapMailFlags = messageRec.ImapMailFlags;
			ImapMailFlags imapMailFlags2 = folder.SupportedFlags;
			if (folder.DefaultFolderType.Equals(ImapDefaultFolderType.Drafts))
			{
				imapMailFlags |= ImapMailFlags.Draft;
				imapMailFlags2 |= ImapMailFlags.Draft;
			}
			if (imapMailFlags2.HasFlag(ImapMailFlags.Draft))
			{
				result.IsDraft = new bool?(imapMailFlags.HasFlag(ImapMailFlags.Draft));
			}
			if (imapMailFlags2.HasFlag(ImapMailFlags.Seen))
			{
				result.IsRead = new bool?(imapMailFlags.HasFlag(ImapMailFlags.Seen));
			}
			if (imapMailFlags2.HasFlag(ImapMailFlags.Answered))
			{
				result.ResponseType = new SyncMessageResponseType?(imapMailFlags.HasFlag(ImapMailFlags.Answered) ? SyncMessageResponseType.Replied : SyncMessageResponseType.None);
			}
			ImapExtendedMessageRec imapExtendedMessageRec = messageRec as ImapExtendedMessageRec;
			if (imapExtendedMessageRec != null)
			{
				result.SyncMessageId = imapExtendedMessageRec.MessageId;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E3B File Offset: 0x0000103B
		internal void Config(byte[] folderId, ImapClientFolder folder, ImapMailbox mailbox)
		{
			this.FolderId = folderId;
			this.Folder = folder;
			this.Mailbox = mailbox;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E52 File Offset: 0x00001052
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Folder = null;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E5E File Offset: 0x0000105E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ImapFolder>(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E68 File Offset: 0x00001068
		protected List<MessageRec> EnumerateMessages(FetchMessagesFlags flags, int? highFetchValue = null, int? lowFetchValue = null)
		{
			if (!this.Folder.IsSelectable)
			{
				return new List<MessageRec>(0);
			}
			List<ImapMessageRec> imapMessageRecs = this.Folder.EnumerateMessages(this.Mailbox.ImapConnection, flags, highFetchValue, lowFetchValue);
			return this.GetMessageRecs(imapMessageRecs);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EAC File Offset: 0x000010AC
		private List<MessageRec> GetMessageRecs(List<ImapMessageRec> imapMessageRecs)
		{
			if (imapMessageRecs.Count == 0)
			{
				return new List<MessageRec>(0);
			}
			List<MessageRec> list = new List<MessageRec>(imapMessageRecs.Count);
			foreach (ImapMessageRec imapMessageRec in imapMessageRecs)
			{
				SyncEmailContext syncEmailContext = ImapFolder.GetSyncEmailContext(this.Folder, imapMessageRec);
				int messageSize = 0;
				ImapExtendedMessageRec imapExtendedMessageRec = imapMessageRec as ImapExtendedMessageRec;
				if (imapExtendedMessageRec != null)
				{
					messageSize = (int)imapExtendedMessageRec.MessageSize;
				}
				PropValueData[] messageProps = SyncEmailUtils.GetMessageProps(syncEmailContext, this.Mailbox.ImapConnection.ConnectionContext.UserName, this.FolderId, new PropValueData[0]);
				MessageRec item = new MessageRec(ImapEntryId.CreateMessageEntryId(imapMessageRec.Uid, this.Folder.UidValidity, this.Folder.Name, this.Mailbox.ImapConnection.ConnectionContext.UserName), this.FolderId, CommonUtils.DefaultLastModificationTime, messageSize, MsgRecFlags.None, messageProps);
				list.Add(item);
			}
			return list;
		}
	}
}
