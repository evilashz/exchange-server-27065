using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal abstract class PopFolder : DisposeTrackableBase, IFolder, IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021B6 File Offset: 0x000003B6
		public PopFolder()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021BE File Offset: 0x000003BE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021C6 File Offset: 0x000003C6
		public PopMailbox Mailbox { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021CF File Offset: 0x000003CF
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021D7 File Offset: 0x000003D7
		public byte[] FolderId { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			return this.Mailbox.GetFolderRec(this.FolderId);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F3 File Offset: 0x000003F3
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			return this.EnumerateMessagesOnPopConnection(0);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FC File Offset: 0x000003FC
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			List<string> list = new List<string>(keysToLookup.Count);
			foreach (byte[] messageEntryId in keysToLookup)
			{
				list.Add(PopEntryId.ParseUid(messageEntryId));
			}
			return this.EnumerateMessagesOnPopConnection(list);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002264 File Offset: 0x00000464
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002267 File Offset: 0x00000467
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226E File Offset: 0x0000046E
		byte[] IFolder.GetFolderId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002275 File Offset: 0x00000475
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000227C File Offset: 0x0000047C
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002283 File Offset: 0x00000483
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000228A File Offset: 0x0000048A
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			return Array<RuleData>.Empty;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002291 File Offset: 0x00000491
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002298 File Offset: 0x00000498
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000229F File Offset: 0x0000049F
		PropProblemData[] IFolder.SetProps(PropValueData[] pvda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022A6 File Offset: 0x000004A6
		internal void Config(byte[] folderId, PopMailbox mailbox)
		{
			this.FolderId = folderId;
			this.Mailbox = mailbox;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022B8 File Offset: 0x000004B8
		protected List<MessageRec> EnumerateMessagesOnPopConnection(int maxItems)
		{
			if (this.FolderId != PopMailbox.InboxEntryId)
			{
				return new List<MessageRec>(0);
			}
			Pop3ResultData uniqueIds = this.Mailbox.PopConnection.GetUniqueIds();
			if (uniqueIds == null)
			{
				return new List<MessageRec>();
			}
			return this.GetMessageRecs(uniqueIds, null, maxItems);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002300 File Offset: 0x00000500
		protected List<MessageRec> EnumerateMessagesOnPopConnection(List<string> uids)
		{
			if (this.FolderId != PopMailbox.InboxEntryId)
			{
				return new List<MessageRec>(0);
			}
			if (uids == null || uids.Count == 0)
			{
				return new List<MessageRec>(0);
			}
			Pop3ResultData uniqueIds = this.Mailbox.PopConnection.GetUniqueIds();
			if (uniqueIds == null)
			{
				return new List<MessageRec>(0);
			}
			return this.GetMessageRecs(uniqueIds, uids, 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002359 File Offset: 0x00000559
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000235B File Offset: 0x0000055B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PopFolder>(this);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002364 File Offset: 0x00000564
		protected MessageRec CreateMessageRec(string uniqueId, long messageSize)
		{
			PropValueData[] messageProps = SyncEmailUtils.GetMessageProps(default(SyncEmailContext), this.Mailbox.PopConnection.ConnectionContext.UserName, this.FolderId, new PropValueData[0]);
			PropValueData[] array = new PropValueData[messageProps.Length + 1];
			messageProps.CopyTo(array, 0);
			array[array.Length - 1] = new PropValueData(PropTag.LastModificationTime, CommonUtils.DefaultLastModificationTime);
			return new MessageRec(PopEntryId.CreateMessageEntryId(uniqueId), this.FolderId, CommonUtils.DefaultLastModificationTime, (int)messageSize, MsgRecFlags.None, array);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023EC File Offset: 0x000005EC
		private List<MessageRec> GetMessageRecs(Pop3ResultData result, List<string> uids, int maxItems = 0)
		{
			bool flag = uids == null || uids.Count == 0;
			List<MessageRec> list = new List<MessageRec>();
			int num = result.EmailDropCount;
			while (num > 1 && (maxItems == 0 || list.Count < maxItems))
			{
				string uniqueId = result.GetUniqueId(num);
				if (uniqueId != null)
				{
					this.Mailbox.UniqueIdMap[uniqueId] = num;
					if (flag || uids.Contains(uniqueId))
					{
						long emailSize = result.GetEmailSize(num);
						list.Add(this.CreateMessageRec(uniqueId, emailSize));
					}
				}
				num--;
			}
			return list;
		}
	}
}
