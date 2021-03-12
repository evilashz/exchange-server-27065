using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000028 RID: 40
	internal class MailboxData
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00007242 File Offset: 0x00005442
		internal MailboxData(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.LoadData(session);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00007266 File Offset: 0x00005466
		internal byte[] SentItemsFolderId
		{
			get
			{
				return this.sentItemsFolderId;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000726E File Offset: 0x0000546E
		internal bool ConversationActionsEnabled
		{
			get
			{
				return this.conversationActionEnabled;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007278 File Offset: 0x00005478
		internal void LoadData(MailboxSession session)
		{
			if (this.sentItemsFolderId == null)
			{
				StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.SentItems);
				if (defaultFolderId != null)
				{
					this.sentItemsFolderId = defaultFolderId.ProviderLevelItemId;
				}
				else
				{
					MailboxData.Tracer.TraceError((long)this.GetHashCode(), "{0}: Sent items folder id is null", new object[]
					{
						TraceContext.Get()
					});
				}
			}
			if (this.outboxFolderId == null)
			{
				StoreObjectId defaultFolderId2 = session.GetDefaultFolderId(DefaultFolderType.Outbox);
				if (defaultFolderId2 != null)
				{
					this.outboxFolderId = defaultFolderId2.ProviderLevelItemId;
				}
				else
				{
					MailboxData.Tracer.TraceError((long)this.GetHashCode(), "{0}: Outbox folder id is null", new object[]
					{
						TraceContext.Get()
					});
				}
			}
			if (this.inboxFolderId == null)
			{
				StoreObjectId defaultFolderId3 = session.GetDefaultFolderId(DefaultFolderType.Inbox);
				if (defaultFolderId3 != null)
				{
					this.inboxFolderId = defaultFolderId3.ProviderLevelItemId;
				}
				else
				{
					MailboxData.Tracer.TraceError((long)this.GetHashCode(), "{0}: Inbox folder id is null", new object[]
					{
						TraceContext.Get()
					});
				}
			}
			int totalActionItemCount;
			ConversationActionItem.QueryConversationActionsFolder(session, null, 0, out totalActionItemCount);
			this.UpdateConversationActionsEnabledStatus(totalActionItemCount);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007377 File Offset: 0x00005577
		internal void UpdateConversationActionsEnabledStatus(int totalActionItemCount)
		{
			this.conversationActionEnabled = (totalActionItemCount > 0);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007388 File Offset: 0x00005588
		public DefaultFolderType MatchCachedDefaultFolderType(byte[] entryId)
		{
			if (this.sentItemsFolderId != null && ArrayComparer<byte>.Comparer.Equals(entryId, this.sentItemsFolderId))
			{
				return DefaultFolderType.SentItems;
			}
			if (this.outboxFolderId != null && ArrayComparer<byte>.Comparer.Equals(entryId, this.outboxFolderId))
			{
				return DefaultFolderType.Outbox;
			}
			if (this.inboxFolderId != null && ArrayComparer<byte>.Comparer.Equals(entryId, this.inboxFolderId))
			{
				return DefaultFolderType.Inbox;
			}
			return DefaultFolderType.None;
		}

		// Token: 0x0400011D RID: 285
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0400011E RID: 286
		private byte[] sentItemsFolderId;

		// Token: 0x0400011F RID: 287
		private byte[] outboxFolderId;

		// Token: 0x04000120 RID: 288
		private byte[] inboxFolderId;

		// Token: 0x04000121 RID: 289
		private bool conversationActionEnabled = true;
	}
}
