using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C4 RID: 2244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTree : IConversationTree, ICollection<IConversationTreeNode>, IEnumerable<IConversationTreeNode>, IEnumerable
	{
		// Token: 0x0600534A RID: 21322 RVA: 0x0015ADF8 File Offset: 0x00158FF8
		internal ConversationTree(ConversationTreeSortOrder sortOrder, IConversationTreeNode rootNode, IList<IConversationTreeNode> nodes, HashSet<PropertyDefinition> loadedProperties)
		{
			this.rootNode = rootNode;
			this.nodes = nodes;
			this.loadedProperties = loadedProperties;
			this.sortOrder = sortOrder;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0015AE1D File Offset: 0x0015901D
		public bool TryGetConversationTreeNode(StoreObjectId storeObjectId, out IConversationTreeNode conversationTreeNode)
		{
			return this.StoreIdToNode.TryGetValue(storeObjectId, out conversationTreeNode);
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x0015AE2C File Offset: 0x0015902C
		public int Count
		{
			get
			{
				return this.GetNodeCount(true);
			}
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0015AE40 File Offset: 0x00159040
		public int GetNodeCount(bool includeSubmitted)
		{
			if (includeSubmitted)
			{
				return this.nodes.Count;
			}
			return this.nodes.Count((IConversationTreeNode node) => !node.HasBeenSubmitted);
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x0015AE7C File Offset: 0x0015907C
		public string Topic
		{
			get
			{
				if (this.conversationTopic == null)
				{
					foreach (IConversationTreeNode conversationTreeNode in this)
					{
						this.conversationTopic = conversationTreeNode.GetValueOrDefault<string>(ItemSchema.ConversationTopic, null);
						if (!string.IsNullOrEmpty(this.conversationTopic))
						{
							break;
						}
					}
				}
				return this.conversationTopic;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x0600534F RID: 21327 RVA: 0x0015AEEC File Offset: 0x001590EC
		public byte[] ConversationCreatorSID
		{
			get
			{
				if (this.RootMessageNode != null)
				{
					return this.RootMessageNode.GetValueOrDefault<byte[]>(ItemSchema.ConversationCreatorSID, null);
				}
				return null;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06005350 RID: 21328 RVA: 0x0015AF09 File Offset: 0x00159109
		public EffectiveRights EffectiveRights
		{
			get
			{
				if (this.RootMessageNode != null)
				{
					return this.RootMessageNode.GetValueOrDefault<EffectiveRights>(StoreObjectSchema.EffectiveRights, EffectiveRights.None);
				}
				return EffectiveRights.None;
			}
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0015AF26 File Offset: 0x00159126
		public void Sort(ConversationTreeSortOrder sortOrder)
		{
			EnumValidator.ThrowIfInvalid<ConversationTreeSortOrder>(sortOrder, "sortOrder");
			this.sortOrder = sortOrder;
			this.rootNode.SortChildNodes(sortOrder);
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0015AF48 File Offset: 0x00159148
		public T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T))
		{
			IConversationTreeNode conversationTreeNode = null;
			if (this.TryGetConversationTreeNode(itemId, out conversationTreeNode))
			{
				return conversationTreeNode.GetValueOrDefault<T>(itemId, propertyDefinition, defaultValue);
			}
			throw new ArgumentException("No ConversationTreeNode can be found for the passed StoreObjectId");
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0015AF76 File Offset: 0x00159176
		public bool IsPropertyLoaded(PropertyDefinition propertyDefinition)
		{
			return this.loadedProperties.Contains(propertyDefinition);
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x0015AF8C File Offset: 0x0015918C
		public IEnumerable<IStorePropertyBag> StorePropertyBags
		{
			get
			{
				if (this.allPropertyBags == null)
				{
					this.allPropertyBags = this.nodes.SelectMany((IConversationTreeNode node) => node.StorePropertyBags);
				}
				return this.allPropertyBags;
			}
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0015B02C File Offset: 0x0015922C
		public Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph()
		{
			Dictionary<IConversationTreeNode, IConversationTreeNode> previousNodeMap = new Dictionary<IConversationTreeNode, IConversationTreeNode>(ConversationTreeNodeBase.EqualityComparer);
			this.ExecuteSortedAction(ConversationTreeSortOrder.ChronologicalAscending, delegate(ConversationTreeSortOrder param0)
			{
				for (int i = 1; i < this.nodes.Count; i++)
				{
					previousNodeMap.Add(this.nodes[i], this.nodes[i - 1]);
				}
			});
			return previousNodeMap;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0015B070 File Offset: 0x00159270
		public void ExecuteSortedAction(ConversationTreeSortOrder sortOrder, SortedActionDelegate action)
		{
			ConversationTreeSortOrder treeOriginalSortOrder = this.sortOrder;
			this.Sort(sortOrder);
			action(treeOriginalSortOrder);
			this.Sort(treeOriginalSortOrder);
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0015B09C File Offset: 0x0015929C
		public IConversationTreeNode RootMessageNode
		{
			get
			{
				if (this.rootMessageNode == null)
				{
					this.rootMessageNode = this.FirstDeliveredNode;
					while (this.rootMessageNode != null && this.rootMessageNode.ParentNode != null && this.rootMessageNode.ParentNode.HasData)
					{
						this.rootMessageNode = this.rootMessageNode.ParentNode;
					}
				}
				return this.rootMessageNode;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06005358 RID: 21336 RVA: 0x0015B0FD File Offset: 0x001592FD
		public StoreObjectId RootMessageId
		{
			get
			{
				if (this.RootMessageNode != null)
				{
					return this.RootMessageNode.MainStoreObjectId;
				}
				return null;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x0015B114 File Offset: 0x00159314
		private IConversationTreeNode FirstDeliveredNode
		{
			get
			{
				if (this.firstDeliveredNode == null)
				{
					if (this.Count == 0)
					{
						return null;
					}
					this.firstDeliveredNode = this.ElementAt(0);
					foreach (IConversationTreeNode y in this)
					{
						if (ConversationTreeNodeBase.ChronologicalComparer.Compare(this.firstDeliveredNode, y) > 0)
						{
							this.firstDeliveredNode = y;
						}
					}
				}
				return this.firstDeliveredNode;
			}
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0015B198 File Offset: 0x00159398
		public bool Remove(IConversationTreeNode item)
		{
			throw new NotSupportedException("The method or operation is not implemented.");
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0015B1A4 File Offset: 0x001593A4
		void ICollection<IConversationTreeNode>.Clear()
		{
			throw new NotSupportedException("The method or operation is not implemented.");
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0015B1B0 File Offset: 0x001593B0
		void ICollection<IConversationTreeNode>.Add(IConversationTreeNode item)
		{
			throw new NotSupportedException("The method or operation is not implemented.");
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0015B1BC File Offset: 0x001593BC
		bool ICollection<IConversationTreeNode>.Contains(IConversationTreeNode item)
		{
			foreach (IConversationTreeNode y in this.nodes)
			{
				if (ConversationTreeNodeBase.EqualityComparer.Equals(item, y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0015B218 File Offset: 0x00159418
		void ICollection<IConversationTreeNode>.CopyTo(IConversationTreeNode[] array, int arrayIndex)
		{
			this.nodes.CopyTo(array, arrayIndex);
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x0015B227 File Offset: 0x00159427
		bool ICollection<IConversationTreeNode>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06005360 RID: 21344 RVA: 0x0015B22A File Offset: 0x0015942A
		public IConversationTreeNode RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06005361 RID: 21345 RVA: 0x0015B232 File Offset: 0x00159432
		private Dictionary<StoreObjectId, IConversationTreeNode> StoreIdToNode
		{
			get
			{
				if (this.storeIdToNode == null)
				{
					this.storeIdToNode = this.BuildMapPropertyBagsToNode();
				}
				return this.storeIdToNode;
			}
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0015B250 File Offset: 0x00159450
		private Dictionary<StoreObjectId, IConversationTreeNode> BuildMapPropertyBagsToNode()
		{
			Dictionary<StoreObjectId, IConversationTreeNode> dictionary = new Dictionary<StoreObjectId, IConversationTreeNode>();
			foreach (IConversationTreeNode conversationTreeNode in this.nodes)
			{
				foreach (StoreObjectId key in conversationTreeNode.ToListStoreObjectId())
				{
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, conversationTreeNode);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0015B2EC File Offset: 0x001594EC
		public IEnumerator<IConversationTreeNode> GetEnumerator()
		{
			return this.rootNode.GetEnumerator();
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0015B2F9 File Offset: 0x001594F9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002D4E RID: 11598
		private readonly HashSet<PropertyDefinition> loadedProperties;

		// Token: 0x04002D4F RID: 11599
		private readonly IList<IConversationTreeNode> nodes;

		// Token: 0x04002D50 RID: 11600
		private readonly IConversationTreeNode rootNode;

		// Token: 0x04002D51 RID: 11601
		private Dictionary<StoreObjectId, IConversationTreeNode> storeIdToNode;

		// Token: 0x04002D52 RID: 11602
		private string conversationTopic;

		// Token: 0x04002D53 RID: 11603
		private ConversationTreeSortOrder sortOrder;

		// Token: 0x04002D54 RID: 11604
		private IConversationTreeNode rootMessageNode;

		// Token: 0x04002D55 RID: 11605
		private IConversationTreeNode firstDeliveredNode;

		// Token: 0x04002D56 RID: 11606
		private IEnumerable<IStorePropertyBag> allPropertyBags;
	}
}
