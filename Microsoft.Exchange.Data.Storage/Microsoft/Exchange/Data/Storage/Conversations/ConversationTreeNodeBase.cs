using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C9 RID: 2249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConversationTreeNodeBase : IConversationTreeNode, IEnumerable<IConversationTreeNode>, IEnumerable, IComparable<IConversationTreeNode>
	{
		// Token: 0x06005397 RID: 21399 RVA: 0x0015B839 File Offset: 0x00159A39
		internal ConversationTreeNodeBase(IConversationTreeNodeSorter conversationTreeNodeSorter)
		{
			this.childNodes = new List<IConversationTreeNode>();
			this.readonlyChildNodes = new ReadOnlyCollection<IConversationTreeNode>(this.childNodes);
			this.sortOrder = ConversationTreeSortOrder.DeepTraversalAscending;
			this.childNodeSorter = conversationTreeNodeSorter;
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x0015B86B File Offset: 0x00159A6B
		public IList<IConversationTreeNode> ChildNodes
		{
			get
			{
				return this.readonlyChildNodes;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06005399 RID: 21401
		public abstract ConversationId ConversationThreadId { get; }

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x0015B873 File Offset: 0x00159A73
		// (set) Token: 0x0600539B RID: 21403 RVA: 0x0015B87B File Offset: 0x00159A7B
		public IConversationTreeNode ParentNode { get; set; }

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x0600539C RID: 21404
		public abstract List<IStorePropertyBag> StorePropertyBags { get; }

		// Token: 0x0600539D RID: 21405
		public abstract bool UpdatePropertyBag(StoreObjectId itemId, IStorePropertyBag bag);

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x0600539E RID: 21406
		public abstract bool HasAttachments { get; }

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x0600539F RID: 21407
		public abstract bool HasBeenSubmitted { get; }

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060053A0 RID: 21408
		public abstract bool IsSpecificMessageReplyStamped { get; }

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060053A1 RID: 21409
		public abstract bool IsSpecificMessageReply { get; }

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060053A2 RID: 21410
		public abstract ConversationId ConversationId { get; }

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060053A3 RID: 21411
		public abstract bool HasData { get; }

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060053A4 RID: 21412
		public abstract byte[] Index { get; }

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060053A5 RID: 21413
		public abstract StoreObjectId MainStoreObjectId { get; }

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060053A6 RID: 21414
		public abstract ExDateTime? ReceivedTime { get; }

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060053A7 RID: 21415
		public abstract string ItemClass { get; }

		// Token: 0x060053A8 RID: 21416
		public abstract List<StoreObjectId> ToListStoreObjectId();

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0015B884 File Offset: 0x00159A84
		// (set) Token: 0x060053AA RID: 21418 RVA: 0x0015B8A6 File Offset: 0x00159AA6
		private List<IConversationTreeNode> FlatSortNodes
		{
			get
			{
				if (this.flatSortNodes == null)
				{
					this.flatSortNodes = this.CalculateFlatSortNodes(this.SortOrder);
				}
				return this.flatSortNodes;
			}
			set
			{
				this.flatSortNodes = value;
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0015B8AF File Offset: 0x00159AAF
		public static void SortByDate(List<IConversationTreeNode> nodes)
		{
			ConversationTreeNodeBase.SortByDate(nodes, true);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0015B8B8 File Offset: 0x00159AB8
		public static List<IConversationTreeNode> TrimToNewest(List<IConversationTreeNode> relevantNodes, int maxItemsToReturn)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>(relevantNodes);
			ConversationTreeNodeBase.SortByDate(list);
			if (list.Count > maxItemsToReturn)
			{
				list.RemoveRange(0, list.Count - maxItemsToReturn);
			}
			return list;
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x0015B8EB File Offset: 0x00159AEB
		public static IComparer<IConversationTreeNode> ChronologicalComparer
		{
			get
			{
				return ConversationTreeNodeChronologicalComparer.Default;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x0015B8F2 File Offset: 0x00159AF2
		public static IEqualityComparer<IConversationTreeNode> EqualityComparer
		{
			get
			{
				return ConversationTreeNodeEqualityComparer.Default;
			}
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0015B907 File Offset: 0x00159B07
		private static void SortByDate(List<IConversationTreeNode> nodes, bool isAscending)
		{
			nodes.Sort((IConversationTreeNode left, IConversationTreeNode right) => ConversationTreeNodeBase.ChronologicalComparer.Compare(left, right));
			if (!isAscending)
			{
				nodes.Reverse();
			}
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0015B935 File Offset: 0x00159B35
		private static bool IsChronologicalSortOrder(ConversationTreeSortOrder sortOrder)
		{
			return sortOrder == ConversationTreeSortOrder.ChronologicalAscending || sortOrder == ConversationTreeSortOrder.ChronologicalDescending || sortOrder == ConversationTreeSortOrder.TraversalChronologicalAscending || sortOrder == ConversationTreeSortOrder.TraversalChronologicalDescending;
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0015B949 File Offset: 0x00159B49
		private static bool IsTraversalChronologicalSortOrder(ConversationTreeSortOrder sortOrder)
		{
			return sortOrder == ConversationTreeSortOrder.TraversalChronologicalAscending || sortOrder == ConversationTreeSortOrder.TraversalChronologicalDescending;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0015B958 File Offset: 0x00159B58
		internal static int CompareNodesTraversal(IConversationTreeNode left, IConversationTreeNode right)
		{
			IList<byte> index = left.Index;
			IList<byte> index2 = right.Index;
			if (index == null && index2 == null)
			{
				return 0;
			}
			if (index == null)
			{
				return -1;
			}
			if (index2 == null)
			{
				return 1;
			}
			int num = Math.Min(index.Count, index2.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)(index[i] - index2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			if (index.Count == index2.Count)
			{
				return right.ChildNodes.Count - left.ChildNodes.Count;
			}
			return index.Count - index2.Count;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0015B9F0 File Offset: 0x00159BF0
		public virtual ConversationTreeNodeRelation GetRelationTo(IConversationTreeNode otherNode)
		{
			if (!this.HasData && otherNode.HasData)
			{
				return ConversationTreeNodeRelation.Parent;
			}
			if (this.HasData && !otherNode.HasData)
			{
				return ConversationTreeNodeRelation.Child;
			}
			IList<byte> index = this.Index;
			IList<byte> index2 = otherNode.Index;
			if (index == null || index2 == null || index.Count == index2.Count)
			{
				return ConversationTreeNodeRelation.Unrelated;
			}
			int num = Math.Min(index.Count, index2.Count);
			for (int i = 0; i < num; i++)
			{
				if (index[i] != index2[i])
				{
					return ConversationTreeNodeRelation.Unrelated;
				}
			}
			if (index.Count != num)
			{
				return ConversationTreeNodeRelation.Child;
			}
			return ConversationTreeNodeRelation.Parent;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0015BA81 File Offset: 0x00159C81
		public void AddChild(IConversationTreeNode node)
		{
			if (!this.TryAddChild(node))
			{
				throw new ArgumentException("node");
			}
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0015BA98 File Offset: 0x00159C98
		public bool TryAddChild(IConversationTreeNode node)
		{
			if (node.ParentNode != null)
			{
				throw new ArgumentException("Can't already node again", "node");
			}
			if (this.GetRelationTo(node) != ConversationTreeNodeRelation.Parent)
			{
				return false;
			}
			for (int i = this.childNodes.Count - 1; i >= 0; i--)
			{
				ConversationTreeNodeRelation relationTo = node.GetRelationTo(this.ChildNodes[i]);
				if (relationTo != ConversationTreeNodeRelation.Unrelated)
				{
					if (relationTo != ConversationTreeNodeRelation.Parent)
					{
						return this.childNodes[i].TryAddChild(node);
					}
					this.childNodes[i].ParentNode = null;
					node.AddChild(this.childNodes[i]);
					this.childNodes.RemoveAt(i);
				}
			}
			this.childNodes.Add(node);
			node.ParentNode = this;
			return true;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0015BB54 File Offset: 0x00159D54
		public void SortChildNodes(ConversationTreeSortOrder sortOrder)
		{
			if (this.SortOrder == sortOrder)
			{
				return;
			}
			if (ConversationTreeNodeBase.IsChronologicalSortOrder(sortOrder))
			{
				this.FlatSortNodes = this.CalculateFlatSortNodes(sortOrder);
			}
			else
			{
				this.TraversalSortChildNodes(sortOrder);
			}
			this.SortOrder = sortOrder;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0015BB85 File Offset: 0x00159D85
		public bool IsPartOf(StoreObjectId itemId)
		{
			return this.ToListStoreObjectId().Contains(itemId);
		}

		// Token: 0x060053B8 RID: 21432
		public abstract T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue = default(T));

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x0015BB93 File Offset: 0x00159D93
		public bool HasChildren
		{
			get
			{
				return this.childNodes.Count > 0;
			}
		}

		// Token: 0x060053BA RID: 21434
		public abstract T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T));

		// Token: 0x060053BB RID: 21435
		public abstract bool TryGetPropertyBag(StoreObjectId itemId, out IStorePropertyBag bag);

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x060053BC RID: 21436
		public abstract IStorePropertyBag MainPropertyBag { get; }

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x060053BD RID: 21437 RVA: 0x0015BBA3 File Offset: 0x00159DA3
		// (set) Token: 0x060053BE RID: 21438 RVA: 0x0015BBAB File Offset: 0x00159DAB
		public ConversationTreeSortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				this.sortOrder = value;
			}
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0015BBB4 File Offset: 0x00159DB4
		private List<IConversationTreeNode> CalculateFlatSortNodes(ConversationTreeSortOrder sortOrder)
		{
			if (ConversationTreeNodeBase.IsTraversalChronologicalSortOrder(sortOrder))
			{
				return this.childNodeSorter.Sort(this, sortOrder);
			}
			return this.FlatSortChildNodes(sortOrder);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0015BBD4 File Offset: 0x00159DD4
		public void ApplyActionToChild(Action<IConversationTreeNode> action)
		{
			action(this);
			foreach (IConversationTreeNode conversationTreeNode in this.childNodes)
			{
				conversationTreeNode.ApplyActionToChild(action);
			}
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0015BC5C File Offset: 0x00159E5C
		private List<IConversationTreeNode> FlatSortChildNodes(ConversationTreeSortOrder sortOrder)
		{
			List<IConversationTreeNode> flatSortNodes = new List<IConversationTreeNode>(0);
			this.ApplyActionToChild(delegate(IConversationTreeNode treeNode)
			{
				treeNode.SortOrder = sortOrder;
				if (treeNode.HasData)
				{
					flatSortNodes.Add(treeNode);
				}
			});
			ConversationTreeNodeBase.SortByDate(flatSortNodes, sortOrder == ConversationTreeSortOrder.ChronologicalAscending);
			return flatSortNodes;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0015BCE0 File Offset: 0x00159EE0
		private void TraversalSortChildNodes(ConversationTreeSortOrder sortOrder)
		{
			for (int i = 0; i < this.childNodes.Count; i++)
			{
				this.childNodes[i].SortChildNodes(sortOrder);
			}
			this.childNodes.Sort(delegate(IConversationTreeNode left, IConversationTreeNode right)
			{
				int num = ConversationTreeNodeBase.CompareNodesTraversal(left, right);
				return (sortOrder == ConversationTreeSortOrder.DeepTraversalAscending) ? num : (-1 * num);
			});
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0015BD3E File Offset: 0x00159F3E
		public int CompareTo(IConversationTreeNode otherNode)
		{
			return ConversationTreeNodeChronologicalComparer.Default.Compare(this, otherNode);
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0015C058 File Offset: 0x0015A258
		public IEnumerator<IConversationTreeNode> GetEnumerator()
		{
			if (ConversationTreeNodeBase.IsChronologicalSortOrder(this.SortOrder))
			{
				foreach (IConversationTreeNode node in this.FlatSortNodes)
				{
					yield return node;
				}
			}
			else
			{
				foreach (IConversationTreeNode firstLevelIteration in this.childNodes)
				{
					if (this.SortOrder != ConversationTreeSortOrder.DeepTraversalDescending)
					{
						yield return firstLevelIteration;
					}
					foreach (IConversationTreeNode secondLevelIteration in firstLevelIteration)
					{
						yield return secondLevelIteration;
					}
					if (this.SortOrder == ConversationTreeSortOrder.DeepTraversalDescending)
					{
						yield return firstLevelIteration;
					}
				}
			}
			yield break;
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0015C074 File Offset: 0x0015A274
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002D60 RID: 11616
		private readonly List<IConversationTreeNode> childNodes;

		// Token: 0x04002D61 RID: 11617
		private readonly ReadOnlyCollection<IConversationTreeNode> readonlyChildNodes;

		// Token: 0x04002D62 RID: 11618
		private readonly IConversationTreeNodeSorter childNodeSorter;

		// Token: 0x04002D63 RID: 11619
		private ConversationTreeSortOrder sortOrder;

		// Token: 0x04002D64 RID: 11620
		private List<IConversationTreeNode> flatSortNodes;
	}
}
