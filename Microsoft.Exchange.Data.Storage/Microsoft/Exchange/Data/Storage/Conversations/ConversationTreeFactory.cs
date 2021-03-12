using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C6 RID: 2246
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeFactory : IConversationTreeFactory
	{
		// Token: 0x0600536A RID: 21354 RVA: 0x0015B301 File Offset: 0x00159501
		public ConversationTreeFactory(IMailboxSession mailboxSession) : this(mailboxSession, ConversationTreeNodeFactory.DefaultTreeNodeIndexPropertyDefinition)
		{
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0015B30F File Offset: 0x0015950F
		public ConversationTreeFactory(IMailboxSession mailboxSession, PropertyDefinition indexPropertyDefinition)
		{
			this.treeNodeFactory = new ConversationTreeNodeFactory(indexPropertyDefinition);
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0015B32C File Offset: 0x0015952C
		public HashSet<PropertyDefinition> CalculatePropertyDefinitionsToBeLoaded(ICollection<PropertyDefinition> requestedProperties)
		{
			ICollection<PropertyDefinition> collection = InternalSchema.Combine<PropertyDefinition>(ConversationTreeFactory.RequiredBuildTreeProperties, requestedProperties);
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>(collection);
			hashSet.ExceptWith(ConversationDataExtractor.BodyPropertiesCanBeExtracted);
			return hashSet;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0015B358 File Offset: 0x00159558
		public IConversationTree Create(IEnumerable<IStorePropertyBag> queryResult, IEnumerable<PropertyDefinition> propertyDefinitions)
		{
			IConversationTreeNode rootNode = this.treeNodeFactory.CreateRootNode();
			if (queryResult == null)
			{
				return this.BuildTree(rootNode, null, null);
			}
			Dictionary<UniqueItemHash, List<IStorePropertyBag>> dictionary = this.AggregateDuplicates(queryResult);
			IList<IConversationTreeNode> nodes = this.InstantiateNodes(dictionary.Values);
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			if (propertyDefinitions != null)
			{
				hashSet.AddRange(propertyDefinitions);
			}
			return this.BuildTree(rootNode, nodes, hashSet);
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0015B3AC File Offset: 0x001595AC
		private Dictionary<UniqueItemHash, List<IStorePropertyBag>> AggregateDuplicates(IEnumerable<IStorePropertyBag> propertyBags)
		{
			StoreObjectId defaultFolderId = this.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			StoreObjectId defaultFolderId2 = this.MailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
			Dictionary<UniqueItemHash, List<IStorePropertyBag>> dictionary = new Dictionary<UniqueItemHash, List<IStorePropertyBag>>();
			foreach (IStorePropertyBag storePropertyBag in propertyBags)
			{
				StoreObjectId storeObjectId = storePropertyBag.TryGetProperty(StoreObjectSchema.ParentItemId) as StoreObjectId;
				UniqueItemHash key = UniqueItemHash.Create(storePropertyBag, storeObjectId.Equals(defaultFolderId2));
				if (dictionary.ContainsKey(key))
				{
					if (storeObjectId.Equals(defaultFolderId))
					{
						dictionary[key].Insert(0, storePropertyBag);
					}
					else if (storeObjectId.Equals(defaultFolderId2))
					{
						StoreObjectId storeObjectId2 = dictionary[key][0].TryGetProperty(StoreObjectSchema.ParentItemId) as StoreObjectId;
						if (storeObjectId2.Equals(defaultFolderId))
						{
							dictionary[key].Insert(1, storePropertyBag);
						}
						else
						{
							dictionary[key].Insert(0, storePropertyBag);
						}
					}
					else
					{
						dictionary[key].Add(storePropertyBag);
					}
				}
				else
				{
					dictionary.Add(key, new List<IStorePropertyBag>
					{
						storePropertyBag
					});
				}
			}
			return dictionary;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0015B4E4 File Offset: 0x001596E4
		private IList<IConversationTreeNode> InstantiateNodes(ICollection<List<IStorePropertyBag>> propertyBagsOfTreeNodes)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>();
			foreach (List<IStorePropertyBag> storePropertyBags in propertyBagsOfTreeNodes)
			{
				list.Add(this.treeNodeFactory.CreateInstance(storePropertyBags));
			}
			return list;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0015B540 File Offset: 0x00159740
		private IConversationTree BuildTree(IConversationTreeNode rootNode, IList<IConversationTreeNode> nodes = null, HashSet<PropertyDefinition> loadedProperties = null)
		{
			nodes = (nodes ?? new List<IConversationTreeNode>());
			loadedProperties = (loadedProperties ?? new HashSet<PropertyDefinition>());
			ConversationTreeSortOrder sortOrder = ConversationTreeSortOrder.DeepTraversalAscending;
			foreach (IConversationTreeNode node in nodes)
			{
				rootNode.TryAddChild(node);
			}
			rootNode.SortChildNodes(sortOrder);
			return this.InternalInstantiate(sortOrder, rootNode, nodes, loadedProperties);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0015B5B4 File Offset: 0x001597B4
		protected virtual IConversationTree InternalInstantiate(ConversationTreeSortOrder sortOrder, IConversationTreeNode rootNode, IList<IConversationTreeNode> nodes, HashSet<PropertyDefinition> loadedProperties)
		{
			return new ConversationTree(sortOrder, rootNode, nodes, loadedProperties);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0015B65C File Offset: 0x0015985C
		public IConversationTree GetNewestSubTree(IConversationTree conversationTree, int count)
		{
			if (count <= 0)
			{
				throw new ArgumentException("Count should be greater than 0", "count");
			}
			ExTraceGlobals.ConversationTracer.TraceDebug<int, int>((long)this.GetHashCode(), "ConversationTreeFactory.GetNewestSubTree: count: {0}, tree size: {1}", count, conversationTree.Count);
			if (count >= conversationTree.Count)
			{
				return conversationTree;
			}
			IConversationTree trimmedConversationTree = null;
			ConversationTreeSortOrder sortOrder = ConversationTreeSortOrder.ChronologicalDescending;
			conversationTree.ExecuteSortedAction(sortOrder, delegate(ConversationTreeSortOrder treeOriginalSortOrder)
			{
				List<IStorePropertyBag> list = new List<IStorePropertyBag>(count);
				foreach (IConversationTreeNode conversationTreeNode in conversationTree)
				{
					if (count-- <= 0)
					{
						break;
					}
					list.AddRange(conversationTreeNode.StorePropertyBags);
				}
				trimmedConversationTree = this.Create(list, null);
				trimmedConversationTree.Sort(treeOriginalSortOrder);
			});
			return trimmedConversationTree;
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x0015B705 File Offset: 0x00159905
		protected IMailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x04002D59 RID: 11609
		private const ConversationTreeSortOrder DefaultSortOrder = ConversationTreeSortOrder.DeepTraversalAscending;

		// Token: 0x04002D5A RID: 11610
		public static readonly PropertyDefinition[] RequiredBuildTreeProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.BodyTag,
			InternalSchema.DisplayBccInternal,
			InternalSchema.DisplayCcInternal,
			InternalSchema.DisplayToInternal,
			ItemSchema.HasAttachment,
			ItemSchema.ConversationId,
			ItemSchema.ConversationFamilyId,
			StoreObjectSchema.ParentItemId,
			ItemSchema.ConversationTopic,
			ItemSchema.Subject,
			ItemSchema.ConversationIndex,
			ItemSchema.Categories,
			ItemSchema.ReceivedTime,
			ItemSchema.InternetMessageId,
			MessageItemSchema.IsDraft,
			StoreObjectSchema.IsRestricted,
			ItemSchema.ExchangeApplicationFlags,
			MessageItemSchema.ReplyToNames,
			ItemSchema.From,
			ItemSchema.Sender,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04002D5B RID: 11611
		private readonly ConversationTreeNodeFactory treeNodeFactory;

		// Token: 0x04002D5C RID: 11612
		private readonly IMailboxSession mailboxSession;
	}
}
