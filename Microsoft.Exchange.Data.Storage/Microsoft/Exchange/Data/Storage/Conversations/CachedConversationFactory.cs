using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000879 RID: 2169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CachedConversationFactory : ConversationFactory
	{
		// Token: 0x060051B2 RID: 20914 RVA: 0x00154E0E File Offset: 0x0015300E
		public CachedConversationFactory(IMailboxSession session) : base(session)
		{
			this.conversationMap = new Dictionary<string, Conversation>();
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x00154E22 File Offset: 0x00153022
		public CachedConversationFactory(IMailboxSession session, IConversationTreeFactory treeFactory, IConversationMembersQuery membersQuery, IConversationDataExtractorFactory dataExtractorFactory) : base(session, treeFactory, membersQuery, dataExtractorFactory)
		{
			this.conversationMap = new Dictionary<string, Conversation>();
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x00154E3C File Offset: 0x0015303C
		public override Conversation CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] requestedProperties)
		{
			string key = this.CalculateConversationMapKey(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, requestedProperties);
			Conversation conversation;
			if (!this.conversationMap.TryGetValue(key, out conversation))
			{
				conversation = base.CreateConversation(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, requestedProperties);
				this.conversationMap.Add(key, conversation);
			}
			return conversation;
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x00154EA0 File Offset: 0x001530A0
		private string CalculateConversationMapKey(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, PropertyDefinition[] requestedProperties)
		{
			string text = null;
			if (folderIds != null)
			{
				List<string> list = new List<string>(from id in folderIds
				select id.ToString().ToLower());
				list.Sort(new Comparison<string>(string.CompareOrdinal));
				text = string.Join(",", list);
			}
			string text2 = null;
			if (requestedProperties != null)
			{
				List<string> list2 = new List<string>(from prop in requestedProperties
				select prop.Name.ToLower());
				list2.Sort(new Comparison<string>(string.CompareOrdinal));
				text2 = string.Join(",", list2);
			}
			return string.Format("{0}-{1}-{2}-{3}-{4}", new object[]
			{
				conversationId,
				text ?? "<null>",
				useFolderIdsAsExclusionList,
				text2 ?? "<null>",
				isIrmEnabled
			});
		}

		// Token: 0x04002C6D RID: 11373
		private readonly Dictionary<string, Conversation> conversationMap;
	}
}
