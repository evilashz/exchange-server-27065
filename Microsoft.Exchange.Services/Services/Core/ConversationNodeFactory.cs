using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003BC RID: 956
	internal class ConversationNodeFactory : ConversationNodeFactoryBase<ConversationNode>
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x0009AC9C File Offset: 0x00098E9C
		public ConversationNodeFactory(MailboxSession mailboxSession, ICoreConversation conversation, IParticipantResolver participantsResolver, ItemResponseShape itemResponse, bool createdNodeFromSubmittedItems, ICollection<PropertyDefinition> mandatoryPropertiesToLoad, ICollection<PropertyDefinition> conversationPropertiesToLoad, HashSet<PropertyDefinition> propertiesLoaded, Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem, bool isOwaCall) : base(mailboxSession, conversation, participantsResolver, itemResponse, mandatoryPropertiesToLoad, conversationPropertiesToLoad, propertiesLoaded, propertiesLoadedPerItem, isOwaCall)
		{
			this.createdNodeFromSubmittedItems = createdNodeFromSubmittedItems;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0009ACC6 File Offset: 0x00098EC6
		private bool ShouldPopulateItem(IConversationTreeNode node)
		{
			return this.createdNodeFromSubmittedItems || !node.HasBeenSubmitted;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0009ACDB File Offset: 0x00098EDB
		protected override void PopulateConversationNodeComplexProperties(ConversationNode conversationNode, IConversationTreeNode treeNode, Func<StoreObjectId, bool> returnOnlyMandatoryProperties)
		{
			if (this.ShouldPopulateItem(treeNode))
			{
				base.PopulateConversationNodeComplexProperties(conversationNode, treeNode, returnOnlyMandatoryProperties);
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0009ACEF File Offset: 0x00098EEF
		protected override ConversationNode CreateEmptyInstance()
		{
			return new ConversationNode();
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0009ACFC File Offset: 0x00098EFC
		public static ConversationNode[] MergeConversationNodes(ConversationNode[] conversationNodesX, ConversationNode[] conversationNodesY, ConversationNodeSortOrder sortOrder, int maxItemsToReturn)
		{
			IEnumerable<ConversationNode> enumerable;
			if (conversationNodesY != null)
			{
				if (conversationNodesX == null)
				{
					enumerable = conversationNodesY;
				}
				else
				{
					enumerable = conversationNodesX.Union(conversationNodesY, ConversationHelper.ConversationNodeEqualityComparer).OrderBy((ConversationNode x) => x, new ConversationNodeComparer(sortOrder)).Take(maxItemsToReturn);
					ConversationNode[] array = enumerable.ToArray<ConversationNode>();
					for (int i = 0; i < array.Length; i++)
					{
						if (string.IsNullOrEmpty(array[i].ParentInternetMessageId) && i + 1 < array.Length)
						{
							array[i].ParentInternetMessageId = array[i + 1].ParentInternetMessageId;
						}
					}
					enumerable = array;
				}
			}
			else
			{
				enumerable = conversationNodesX;
			}
			if (enumerable != null)
			{
				return enumerable.ToArray<ConversationNode>();
			}
			return null;
		}

		// Token: 0x040011B2 RID: 4530
		private readonly bool createdNodeFromSubmittedItems;
	}
}
