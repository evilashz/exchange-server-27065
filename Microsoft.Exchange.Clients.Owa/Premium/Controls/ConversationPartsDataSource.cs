using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200033F RID: 831
	internal class ConversationPartsDataSource : FolderListViewDataSource, IListViewDataSource
	{
		// Token: 0x06001F78 RID: 8056 RVA: 0x000B54AF File Offset: 0x000B36AF
		internal ConversationPartsDataSource(UserContext context, Hashtable properties, Folder folder, OwaStoreObjectId conversationId, ConversationTreeSortOrder sortOrder) : this(context, properties, folder, conversationId, null, sortOrder)
		{
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000B54BF File Offset: 0x000B36BF
		internal ConversationPartsDataSource(UserContext context, Hashtable properties, Folder folder, OwaStoreObjectId conversationId, QueryFilter filter, ConversationTreeSortOrder sortOrder) : base(context, false, properties, folder, null, filter)
		{
			this.conversationId = conversationId;
			this.sortOrder = sortOrder;
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000B54DE File Offset: 0x000B36DE
		public new int TotalItemCount
		{
			get
			{
				return this.TotalCount;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000B54E6 File Offset: 0x000B36E6
		internal ConversationTreeSortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000B54EE File Offset: 0x000B36EE
		internal Conversation Conversation
		{
			get
			{
				return this.conversation;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000B54F6 File Offset: 0x000B36F6
		public new int UnreadCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000B54FD File Offset: 0x000B36FD
		public new void Load(string seekValue, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000B5504 File Offset: 0x000B3704
		public new bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000B550B File Offset: 0x000B370B
		public new void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000B5512 File Offset: 0x000B3712
		public new void Load(int startRange, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000B551C File Offset: 0x000B371C
		public void Load()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ConversationPartsDataSource.Load()");
			if (!base.UserHasRightToLoad)
			{
				return;
			}
			PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
			MailboxSession mailboxSession = base.Folder.Session as MailboxSession;
			this.conversation = ConversationUtilities.LoadConversation(mailboxSession, this.conversationId, requestedProperties);
			this.conversation.TrimToNewest(Globals.MaxItemsInConversationExpansion);
			this.totalCount = this.conversation.ConversationTree.Count;
			if (this.totalCount == 0)
			{
				return;
			}
			this.conversation.ConversationTree.Sort(this.sortOrder);
			this.nodes = new IConversationTreeNode[this.totalCount];
			List<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds(this.conversation, base.Folder);
			int num = 0;
			foreach (IConversationTreeNode conversationTreeNode in this.conversation.ConversationTree)
			{
				ConversationUtilities.SortPropertyBags(conversationTreeNode, localItemIds, mailboxSession);
				this.nodes[num] = conversationTreeNode;
				num++;
			}
			if (0 < this.nodes.Length)
			{
				base.StartRange = 0;
				base.EndRange = this.totalCount - 1;
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000B5658 File Offset: 0x000B3858
		public override T GetItemProperty<T>(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			return this.nodes[this.CurrentItem].GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000B5691 File Offset: 0x000B3891
		public override T GetItemProperty<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			return this.nodes[this.CurrentItem].GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000B56B5 File Offset: 0x000B38B5
		public IConversationTreeNode GetCurrentNode()
		{
			return this.nodes[this.CurrentItem];
		}

		// Token: 0x040016E4 RID: 5860
		private OwaStoreObjectId conversationId;

		// Token: 0x040016E5 RID: 5861
		private ConversationTreeSortOrder sortOrder;

		// Token: 0x040016E6 RID: 5862
		private IConversationTreeNode[] nodes;

		// Token: 0x040016E7 RID: 5863
		private Conversation conversation;
	}
}
