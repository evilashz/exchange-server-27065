using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200016A RID: 362
	internal class NavigationNodeList<NodeType> : IList<NodeType>, ICollection<NodeType>, IEnumerable<NodeType>, IEnumerable where NodeType : NavigationNode
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0005515D File Offset: 0x0005335D
		public NavigationNodeList()
		{
			this.data = new List<NodeType>();
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00055170 File Offset: 0x00053370
		public void Insert(int index, NodeType node)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid position to insert: {0}.", index));
			}
			this.OnBeforeNodeAdd(node);
			this.data.Insert(index, node);
			this.OnAfterNodeAdd(index);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000551B0 File Offset: 0x000533B0
		public void Add(NodeType node)
		{
			this.OnBeforeNodeAdd(node);
			this.data.Add(node);
			this.OnAfterNodeAdd(this.Count - 1);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000551D3 File Offset: 0x000533D3
		public bool Remove(NodeType node)
		{
			return this.data.Remove(node);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000551E1 File Offset: 0x000533E1
		public void RemoveAt(int index)
		{
			this.data.RemoveAt(index);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000551EF File Offset: 0x000533EF
		public int IndexOf(NodeType item)
		{
			return this.data.IndexOf(item);
		}

		// Token: 0x1700035C RID: 860
		public NodeType this[int index]
		{
			get
			{
				return this.data[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Invalid position to modify: {0}.", index));
				}
				this.OnBeforeNodeAdd(value);
				this.data[index] = value;
				this.OnAfterNodeAdd(index);
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0005524B File Offset: 0x0005344B
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00055258 File Offset: 0x00053458
		public bool Contains(NodeType item)
		{
			return this.data.Contains(item);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00055266 File Offset: 0x00053466
		public void CopyTo(NodeType[] array, int arrayIndex)
		{
			this.data.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00055275 File Offset: 0x00053475
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00055282 File Offset: 0x00053482
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00055285 File Offset: 0x00053485
		public IEnumerator<NodeType> GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00055297 File Offset: 0x00053497
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000552A0 File Offset: 0x000534A0
		public int FindChildByNodeId(StoreObjectId nodeId)
		{
			for (int i = 0; i < this.Count; i++)
			{
				NodeType nodeType = this[i];
				if (nodeType.NavigationNodeId != null)
				{
					NodeType nodeType2 = this[i];
					if (nodeType2.NavigationNodeId.ObjectId.Equals(nodeId))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000552FC File Offset: 0x000534FC
		public NavigationNode RemoveChildByNodeId(StoreObjectId nodeId)
		{
			for (int i = 0; i < this.Count; i++)
			{
				NodeType nodeType = this[i];
				if (nodeType.NavigationNodeId != null)
				{
					NodeType nodeType2 = this[i];
					if (nodeType2.NavigationNodeId.ObjectId.Equals(nodeId))
					{
						NavigationNode result = this[i];
						this.RemoveAt(i);
						return result;
					}
				}
			}
			return null;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0005536C File Offset: 0x0005356C
		internal void CopyToList(NavigationNodeList<NodeType> nodeList)
		{
			foreach (NodeType nodeType in this.data)
			{
				nodeList.data.Add((NodeType)((object)((ICloneable)((object)nodeType)).Clone()));
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000553D8 File Offset: 0x000535D8
		private void CheckSequence(int index, NodeType node)
		{
			byte[] array = null;
			byte[] array2 = null;
			if (index > 0)
			{
				NodeType nodeType = this[index - 1];
				array2 = nodeType.NavigationNodeOrdinal;
			}
			if (index < this.Count - 1)
			{
				NodeType nodeType2 = this[index + 1];
				array = nodeType2.NavigationNodeOrdinal;
			}
			byte[] navigationNodeOrdinal = node.NavigationNodeOrdinal;
			if (navigationNodeOrdinal == null || (array != null && Utilities.CompareByteArrays(navigationNodeOrdinal, array) >= 0) || (array2 != null && Utilities.CompareByteArrays(navigationNodeOrdinal, array2) <= 0))
			{
				if (Utilities.CompareByteArrays(array2, array) == 0)
				{
					int i;
					for (i = index - 1; i >= 0; i--)
					{
						NodeType nodeType3 = this[i];
						if (Utilities.CompareByteArrays(nodeType3.NavigationNodeOrdinal, array2) != 0)
						{
							break;
						}
					}
					int j;
					for (j = index + 1; j < this.Count; j++)
					{
						byte[] array3 = array;
						NodeType nodeType4 = this[j];
						if (Utilities.CompareByteArrays(array3, nodeType4.NavigationNodeOrdinal) != 0)
						{
							break;
						}
					}
					byte[] array4;
					if (i >= 0)
					{
						NodeType nodeType5 = this[i];
						array4 = nodeType5.NavigationNodeOrdinal;
					}
					else
					{
						array4 = null;
					}
					array2 = array4;
					byte[] array5;
					if (j < this.Count)
					{
						NodeType nodeType6 = this[j];
						array5 = nodeType6.NavigationNodeOrdinal;
					}
					else
					{
						array5 = null;
					}
					array = array5;
					for (int k = i + 1; k < j; k++)
					{
						NodeType nodeType7 = this[k];
						nodeType7.NavigationNodeOrdinal = BinaryOrdinalGenerator.GetInbetweenOrdinalValue(array2, array);
						NodeType nodeType8 = this[k];
						array2 = nodeType8.NavigationNodeOrdinal;
					}
					return;
				}
				node.NavigationNodeOrdinal = BinaryOrdinalGenerator.GetInbetweenOrdinalValue(array2, array);
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0005556C File Offset: 0x0005376C
		private void ThrowIfDuplicateExists(NodeType node)
		{
			foreach (NodeType nodeType in this.data)
			{
				if (node.Equals(nodeType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "This is a duplicate node for folder: {0}", new object[]
					{
						node.Subject
					}));
				}
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000555FC File Offset: 0x000537FC
		protected virtual void OnBeforeNodeAdd(NodeType item)
		{
			this.ThrowIfDuplicateExists(item);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00055605 File Offset: 0x00053805
		protected virtual void OnAfterNodeAdd(int index)
		{
			this.CheckSequence(index, this[index]);
		}

		// Token: 0x040008F1 RID: 2289
		private readonly List<NodeType> data;
	}
}
