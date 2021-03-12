using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EasFolderBase : DisposeTrackableBase, IFolder, IDisposable
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002E84 File Offset: 0x00001084
		protected EasFolderBase()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E8C File Offset: 0x0000108C
		protected EasFolderBase(string serverId, string parentId, string displayName, EasFolderType folderType)
		{
			this.serverId = serverId;
			this.parentId = parentId;
			this.displayName = displayName;
			this.folderType = folderType;
			this.EntryId = EasMailbox.GetEntryId(serverId);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002EBD File Offset: 0x000010BD
		internal string ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002EC5 File Offset: 0x000010C5
		internal string ParentId
		{
			get
			{
				return this.parentId;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002ECD File Offset: 0x000010CD
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002ED5 File Offset: 0x000010D5
		internal EasFolderType EasFolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EDD File Offset: 0x000010DD
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002EE5 File Offset: 0x000010E5
		internal byte[] EntryId { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EEE File Offset: 0x000010EE
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002EF6 File Offset: 0x000010F6
		internal EasMailbox Mailbox { get; private set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002EFF File Offset: 0x000010FF
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			return this.InternalGetFolderRec(additionalPtagsToLoad, flags);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F09 File Offset: 0x00001109
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			return this.InternalLookupMessages(ptagToLookup, keysToLookup, additionalPtagsToLoad);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F14 File Offset: 0x00001114
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F17 File Offset: 0x00001117
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F1E File Offset: 0x0000111E
		byte[] IFolder.GetFolderId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F25 File Offset: 0x00001125
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F2C File Offset: 0x0000112C
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F33 File Offset: 0x00001133
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F3A File Offset: 0x0000113A
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			return Array<RuleData>.Empty;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F41 File Offset: 0x00001141
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F48 File Offset: 0x00001148
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F4F File Offset: 0x0000114F
		PropProblemData[] IFolder.SetProps(PropValueData[] pvda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F56 File Offset: 0x00001156
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F5D File Offset: 0x0000115D
		internal EasFolderBase Configure(EasMailbox mailbox)
		{
			this.Mailbox = mailbox;
			return this;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F67 File Offset: 0x00001167
		protected static FolderRec CreateGenericFolderRec(EasFolderBase folder)
		{
			return new FolderRec(folder.EntryId, EasMailbox.GetEntryId(folder.ParentId), FolderType.Generic, folder.DisplayName, DateTime.MinValue, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F8C File Offset: 0x0000118C
		protected virtual FolderRec InternalGetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			return EasFolderBase.CreateGenericFolderRec(this);
		}

		// Token: 0x06000068 RID: 104
		protected abstract List<MessageRec> InternalLookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad);

		// Token: 0x06000069 RID: 105 RVA: 0x00002F94 File Offset: 0x00001194
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Mailbox = null;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EasFolderBase>(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FA8 File Offset: 0x000011A8
		protected FolderChangesManifest CreateInitializedChangesManifest()
		{
			return new FolderChangesManifest(this.EntryId)
			{
				ChangedMessages = new List<MessageRec>(),
				ReadMessages = new List<byte[]>(),
				UnreadMessages = new List<byte[]>()
			};
		}

		// Token: 0x0400001C RID: 28
		private readonly string serverId;

		// Token: 0x0400001D RID: 29
		private readonly string parentId;

		// Token: 0x0400001E RID: 30
		private readonly string displayName;

		// Token: 0x0400001F RID: 31
		private readonly EasFolderType folderType;
	}
}
