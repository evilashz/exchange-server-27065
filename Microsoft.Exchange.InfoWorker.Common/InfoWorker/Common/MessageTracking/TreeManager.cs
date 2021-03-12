using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200030F RID: 783
	internal abstract class TreeManager
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0006B58E File Offset: 0x0006978E
		public TreeManager()
		{
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0006B5A8 File Offset: 0x000697A8
		public virtual ICollection<Node> GetPathToLeaf(string leafId)
		{
			if (this.recipientsDroppedDueToDuplication != null && this.recipientsDroppedDueToDuplication.Contains(leafId))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Not returning path for key {0} because it is on the list of duplicate recipients.", leafId);
				return TreeManager.EmptyNodeCollection;
			}
			Node node = this.GetBestResultLeaf(leafId);
			if (node == null)
			{
				return TreeManager.EmptyNodeCollection;
			}
			LinkedList<Node> linkedList = new LinkedList<Node>();
			while (node != this.root)
			{
				linkedList.AddFirst(node);
				node = node.Parent;
			}
			return linkedList;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0006B61C File Offset: 0x0006981C
		public ICollection<Node> GetLeafNodes()
		{
			Dictionary<string, LinkedList<Node>>.KeyCollection keys = this.leafRecordTable.Keys;
			if (keys == null || keys.Count == 0)
			{
				return TreeManager.EmptyNodeCollection;
			}
			LinkedList<Node> linkedList = new LinkedList<Node>();
			foreach (string text in keys)
			{
				if (!text.Equals(string.Empty))
				{
					if (this.recipientsDroppedDueToDuplication != null && this.recipientsDroppedDueToDuplication.Contains(text))
					{
						TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Dropping record for key {0} because it is on the list of duplicate recipients.", text);
					}
					else
					{
						linkedList.AddLast(this.GetBestResultLeaf(text));
					}
				}
			}
			return linkedList;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0006B6D0 File Offset: 0x000698D0
		protected bool Insert(Node node, TreeManager.DoPreInsertoinProcessingDelegate doPreInsertionProcessing, TreeManager.DoPostInsertionProcessingDelegate doPostInsertionProcessing)
		{
			bool flag;
			Node node2 = this.FindParent(node.ParentKey, node, out flag);
			if (node2 == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Cannot insert node because no suitable parent with key {0} is found.", node.ParentKey);
				return false;
			}
			if (doPreInsertionProcessing != null && !doPreInsertionProcessing(node2, node))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "node {0} not inserted based on shouldChildrenBeInsertedUnderParent check for parent node {1}", node.Key, node2.Key);
				return false;
			}
			bool flag2 = this.InsertChildUnderParent(node2, node, doPreInsertionProcessing, doPostInsertionProcessing);
			if (flag2 && !flag)
			{
				this.RemoveNodeFromLeafRecords(node2);
			}
			return true;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0006B758 File Offset: 0x00069958
		protected bool InsertAllChildrenForOneNode(string parentKey, IList<Node> nodes, TreeManager.DoPreInsertoinProcessingDelegate doPreInsertionProcessing, TreeManager.DoPostInsertionProcessingDelegate doPostInsertionProcessing)
		{
			if (nodes == null || nodes.Count == 0)
			{
				return this.RemoveKeyFromLeafSet(parentKey);
			}
			bool flag;
			Node node = this.FindParent(parentKey, nodes[0], out flag);
			if (node == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Cannot insert any children because no suitable parent with key {0} is found.", parentKey);
				return false;
			}
			bool flag2 = false;
			foreach (Node childNode in nodes)
			{
				if (this.InsertChildUnderParent(node, childNode, doPreInsertionProcessing, doPostInsertionProcessing))
				{
					flag2 = true;
				}
			}
			if (flag2 && !flag)
			{
				this.RemoveNodeFromLeafRecords(node);
			}
			return true;
		}

		// Token: 0x06001725 RID: 5925
		protected abstract Node DisambiguateParentRecord(LinkedList<Node> possibleParents, Node node);

		// Token: 0x06001726 RID: 5926
		protected abstract bool IsNodeRootChildCandidate(Node node);

		// Token: 0x06001727 RID: 5927
		protected abstract int GetNodePriorities(Node node, out int secondaryPriority);

		// Token: 0x06001728 RID: 5928 RVA: 0x0006B7FC File Offset: 0x000699FC
		protected void RemoveNodeFromLeafRecords(Node node)
		{
			LinkedList<Node> leavesByKey = this.GetLeavesByKey(node.Key);
			if (leavesByKey != null)
			{
				leavesByKey.Remove(node);
				if (leavesByKey.Count == 0)
				{
					this.leafRecordTable.Remove(node.Key);
				}
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0006B83C File Offset: 0x00069A3C
		private Node GetBestResultLeaf(string leafId)
		{
			LinkedList<Node> leavesByKey = this.GetLeavesByKey(leafId);
			if (leavesByKey == null || leavesByKey.Count == 0)
			{
				return null;
			}
			if (leavesByKey.Count == 1)
			{
				return leavesByKey.First.Value;
			}
			Node result = null;
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			foreach (Node node in leavesByKey)
			{
				int num3;
				int nodePriorities = this.GetNodePriorities(node, out num3);
				if (nodePriorities < num || (nodePriorities == num && num3 <= num2))
				{
					result = node;
					num = nodePriorities;
					num2 = num3;
				}
			}
			return result;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0006B8E4 File Offset: 0x00069AE4
		private Node GetLeaf(string leafId)
		{
			LinkedList<Node> leavesByKey = this.GetLeavesByKey(leafId);
			if (leavesByKey == null || leavesByKey.Count < 1)
			{
				return null;
			}
			return leavesByKey.Last.Value;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0006B914 File Offset: 0x00069B14
		private Node FindParent(string parentKey, Node node, out bool parentIsRoot)
		{
			if (this.root == null)
			{
				this.root = new Node(string.Empty, string.Empty, node.Value);
			}
			LinkedList<Node> leavesByKey = this.GetLeavesByKey(parentKey);
			Node bestParent = this.GetBestParent(leavesByKey, node, out parentIsRoot);
			if (bestParent == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "No suitable parent with key {0} is found.", parentKey);
			}
			return bestParent;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0006B970 File Offset: 0x00069B70
		private Node GetBestParent(LinkedList<Node> possibleParents, Node node, out bool bestParentIsRoot)
		{
			bestParentIsRoot = false;
			Node node2 = this.DisambiguateParentRecord(possibleParents, node);
			if (node2 == null)
			{
				if (!this.IsNodeRootChildCandidate(node))
				{
					return null;
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Inserting node with key '{0}' as a child of the root.", node.Key);
				bestParentIsRoot = true;
				node2 = this.Root;
			}
			return node2;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0006B9C0 File Offset: 0x00069BC0
		private bool InsertChildUnderParent(Node parentNode, Node childNode, TreeManager.DoPreInsertoinProcessingDelegate doPreInsertionProcessing, TreeManager.DoPostInsertionProcessingDelegate doPostInsertionProcessing)
		{
			if (doPreInsertionProcessing != null && !doPreInsertionProcessing(parentNode, childNode))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "node list with first node {0} not inserted based on pre insertion check for parent node {1}", childNode.Key, parentNode.Key);
				return false;
			}
			parentNode.AddChild(childNode);
			Node node = doPostInsertionProcessing(parentNode, childNode);
			this.InsertLeafNode(node);
			return true;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0006BA18 File Offset: 0x00069C18
		private void InsertLeafNode(Node node)
		{
			string key = node.Key;
			LinkedList<Node> linkedList = this.GetLeavesByKey(key);
			if (linkedList == null)
			{
				linkedList = new LinkedList<Node>();
			}
			linkedList.AddLast(node);
			this.leafRecordTable[key] = linkedList;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0006BA54 File Offset: 0x00069C54
		protected LinkedList<Node> GetLeavesByKey(string key)
		{
			LinkedList<Node> result = null;
			this.leafRecordTable.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0006BA73 File Offset: 0x00069C73
		internal bool RemoveKeyFromLeafSet(string key)
		{
			return this.leafRecordTable.Remove(key);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0006BA81 File Offset: 0x00069C81
		internal void DropRecipientDueToPotentialDuplication(string key)
		{
			if (this.recipientsDroppedDueToDuplication == null)
			{
				this.recipientsDroppedDueToDuplication = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			}
			if (!this.recipientsDroppedDueToDuplication.Contains(key))
			{
				this.recipientsDroppedDueToDuplication.Add(key);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0006BAB6 File Offset: 0x00069CB6
		public Node Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x04000EBF RID: 3775
		private Node root;

		// Token: 0x04000EC0 RID: 3776
		private static readonly ICollection<Node> EmptyNodeCollection = new Node[0];

		// Token: 0x04000EC1 RID: 3777
		private Dictionary<string, LinkedList<Node>> leafRecordTable = new Dictionary<string, LinkedList<Node>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000EC2 RID: 3778
		private HashSet<string> recipientsDroppedDueToDuplication;

		// Token: 0x02000310 RID: 784
		// (Invoke) Token: 0x06001735 RID: 5941
		public delegate bool DoPreInsertoinProcessingDelegate(Node parent, Node child);

		// Token: 0x02000311 RID: 785
		// (Invoke) Token: 0x06001739 RID: 5945
		public delegate Node DoPostInsertionProcessingDelegate(Node parent, Node child);
	}
}
