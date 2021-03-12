using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000072 RID: 114
	public sealed class PersistentAvlTree<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable where TKey : IComparable<TKey>
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x000120CC File Offset: 0x000102CC
		private PersistentAvlTree()
		{
			this.root = null;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000120DB File Offset: 0x000102DB
		private PersistentAvlTree(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root)
		{
			this.root = root;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000120EA File Offset: 0x000102EA
		public static PersistentAvlTree<TKey, TValue> Empty
		{
			get
			{
				return PersistentAvlTree<TKey, TValue>.empty;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000120F1 File Offset: 0x000102F1
		public int Count
		{
			get
			{
				return PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>.GetCount(this.root);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000120FE File Offset: 0x000102FE
		int ICollection<KeyValuePair<!0, !1>>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00012106 File Offset: 0x00010306
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000170 RID: 368
		public TValue this[TKey key]
		{
			get
			{
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = this.root;
				while (node != null)
				{
					int num = key.CompareTo(node.Key);
					if (num == 0)
					{
						return node.Value;
					}
					if (num < 0)
					{
						node = node.Left;
					}
					else
					{
						node = node.Right;
					}
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001215C File Offset: 0x0001035C
		public PersistentAvlTree<TKey, TValue> Add(TKey key, TValue value)
		{
			return new PersistentAvlTree<TKey, TValue>(PersistentAvlTree<TKey, TValue>.InternalInsert(this.root, ref key, ref value));
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00012174 File Offset: 0x00010374
		public bool Contains(TKey key)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = this.root;
			while (node != null)
			{
				int num = key.CompareTo(node.Key);
				if (num == 0)
				{
					return true;
				}
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			return false;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000121BC File Offset: 0x000103BC
		public TValue GetValue(TKey key)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = this.root;
			while (node != null)
			{
				int num = key.CompareTo(node.Key);
				if (num == 0)
				{
					return node.Value;
				}
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001220C File Offset: 0x0001040C
		public bool TryGetValue(TKey key, out TValue value)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = this.root;
			while (node != null)
			{
				int num = key.CompareTo(node.Key);
				if (num == 0)
				{
					value = node.Value;
					return true;
				}
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00012266 File Offset: 0x00010466
		public PersistentAvlTree<TKey, TValue> SetValue(TKey key, TValue value)
		{
			return new PersistentAvlTree<TKey, TValue>(PersistentAvlTree<TKey, TValue>.InternalSetValue(this.root, ref key, ref value));
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001227C File Offset: 0x0001047C
		public PersistentAvlTree<TKey, TValue> Remove(TKey key)
		{
			bool flag;
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = PersistentAvlTree<TKey, TValue>.InternalRemove(this.root, ref key, out flag);
			if (!flag)
			{
				throw new KeyNotFoundException("The key is not found in the tree.");
			}
			if (node == null)
			{
				return PersistentAvlTree<TKey, TValue>.Empty;
			}
			return new PersistentAvlTree<TKey, TValue>(node);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000122B8 File Offset: 0x000104B8
		public PersistentAvlTree<TKey, TValue> Remove(TKey key, out bool successful)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = PersistentAvlTree<TKey, TValue>.InternalRemove(this.root, ref key, out successful);
			if (object.ReferenceEquals(node, this.root))
			{
				return this;
			}
			if (node == null)
			{
				return PersistentAvlTree<TKey, TValue>.Empty;
			}
			return new PersistentAvlTree<TKey, TValue>(node);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000122F3 File Offset: 0x000104F3
		public IEnumerable<TValue> GetValuesLmr()
		{
			return PersistentAvlTree<TKey, TValue>.TraverseInLeftMiddleRightOrder<TValue>(this.root, PersistentAvlTree<TKey, TValue>.ValueConverter);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00012305 File Offset: 0x00010505
		public IEnumerable<KeyValuePair<TKey, TValue>> GetMembersLmr()
		{
			return PersistentAvlTree<TKey, TValue>.TraverseInLeftMiddleRightOrder<KeyValuePair<TKey, TValue>>(this.root, PersistentAvlTree<TKey, TValue>.KeyValuePairConverter);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00012317 File Offset: 0x00010517
		public IEnumerable<TKey> GetKeysLmr()
		{
			return PersistentAvlTree<TKey, TValue>.TraverseInLeftMiddleRightOrder<TKey>(this.root, PersistentAvlTree<TKey, TValue>.KeyConverter);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00012329 File Offset: 0x00010529
		public IEnumerable<TKey> GetKeysMlr()
		{
			return PersistentAvlTree<TKey, TValue>.TraverseInMiddleLeftRightOrder<TKey>(this.root, PersistentAvlTree<TKey, TValue>.KeyConverter);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001233B File Offset: 0x0001053B
		public IEnumerable<TKey> GetKeysMrl()
		{
			return PersistentAvlTree<TKey, TValue>.TraverseInMiddleRightLeftOrder<TKey>(this.root, PersistentAvlTree<TKey, TValue>.KeyConverter);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001234D File Offset: 0x0001054D
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("The collection is read-only.");
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00012359 File Offset: 0x00010559
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException("The collection is read-only.");
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00012365 File Offset: 0x00010565
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.Contains(item.Key);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00012374 File Offset: 0x00010574
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			IEnumerable<KeyValuePair<TKey, TValue>> membersLmr = this.GetMembersLmr();
			int num = arrayIndex;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in membersLmr)
			{
				array[num] = keyValuePair;
				num++;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000123D0 File Offset: 0x000105D0
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("The collection is read-only.");
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000123DC File Offset: 0x000105DC
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetMembersLmr().GetEnumerator();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000123E9 File Offset: 0x000105E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetMembersLmr().GetEnumerator();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000123F8 File Offset: 0x000105F8
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> InternalInsert(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node, ref TKey key, ref TValue value)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node2;
			if (node == null)
			{
				node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(null, null, key, value);
			}
			else
			{
				int num = key.CompareTo(node.Key);
				if (num == 0)
				{
					throw new ArgumentException("An element with the same key already exists in the tree.", "key");
				}
				if (num < 0)
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(PersistentAvlTree<TKey, TValue>.InternalInsert(node.Left, ref key, ref value), node.Right, node.Key, node.Value);
				}
				else
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node.Left, PersistentAvlTree<TKey, TValue>.InternalInsert(node.Right, ref key, ref value), node.Key, node.Value);
				}
			}
			return PersistentAvlTree<TKey, TValue>.BalanceNode(node2);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001249C File Offset: 0x0001069C
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> InternalSetValue(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node, ref TKey key, ref TValue value)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node2;
			if (node == null)
			{
				node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(null, null, key, value);
			}
			else
			{
				int num = key.CompareTo(node.Key);
				if (num == 0)
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node.Left, node.Right, key, value);
				}
				else if (num < 0)
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(PersistentAvlTree<TKey, TValue>.InternalSetValue(node.Left, ref key, ref value), node.Right, node.Key, node.Value);
				}
				else
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node.Left, PersistentAvlTree<TKey, TValue>.InternalSetValue(node.Right, ref key, ref value), node.Key, node.Value);
				}
			}
			return PersistentAvlTree<TKey, TValue>.BalanceNode(node2);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00012554 File Offset: 0x00010754
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> InternalRemove(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node, ref TKey key, out bool found)
		{
			if (node == null)
			{
				found = false;
				return null;
			}
			int num = key.CompareTo(node.Key);
			if (num == 0)
			{
				found = true;
				return PersistentAvlTree<TKey, TValue>.RemoveRoot(node);
			}
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node2;
			if (num < 0)
			{
				node2 = PersistentAvlTree<TKey, TValue>.InternalRemove(node.Left, ref key, out found);
				if (!object.ReferenceEquals(node2, node.Left))
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node2, node.Right, node.Key, node.Value);
				}
				else
				{
					node2 = node;
				}
			}
			else
			{
				node2 = PersistentAvlTree<TKey, TValue>.InternalRemove(node.Right, ref key, out found);
				if (!object.ReferenceEquals(node2, node.Right))
				{
					node2 = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node.Left, node2, node.Key, node.Value);
				}
				else
				{
					node2 = node;
				}
			}
			if (!object.ReferenceEquals(node2, node))
			{
				node2 = PersistentAvlTree<TKey, TValue>.BalanceNode(node2);
			}
			return node2;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00012614 File Offset: 0x00010814
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> BalanceNode(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root)
		{
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> result = root;
			if (root != null)
			{
				if (root.Balance == 2)
				{
					if (root.Right.Balance == 1)
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left, root.Right.Left, root.Key, root.Value), root.Right.Right, root.Right.Key, root.Right.Value);
					}
					else if (root.Right.Balance == -1)
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left, root.Right.Left.Left, root.Key, root.Value), new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Right.Left.Right, root.Right.Right, root.Right.Key, root.Right.Value), root.Right.Left.Key, root.Right.Left.Value);
					}
					else
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left, root.Right.Left, root.Key, root.Value), root.Right.Right, root.Right.Key, root.Right.Value);
					}
				}
				else if (root.Balance == -2)
				{
					if (root.Left.Balance == -1)
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Left, new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Right, root.Right, root.Key, root.Value), root.Left.Key, root.Left.Value);
					}
					else if (root.Left.Balance == 1)
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Left, root.Left.Right.Left, root.Left.Key, root.Left.Value), new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Right.Right, root.Right, root.Key, root.Value), root.Left.Right.Key, root.Left.Right.Value);
					}
					else
					{
						result = new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Left, new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left.Right, root.Right, root.Key, root.Value), root.Left.Key, root.Left.Value);
					}
				}
			}
			return result;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000128C4 File Offset: 0x00010AC4
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> RemoveRoot(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root)
		{
			if (root.Left == null && root.Right == null)
			{
				return null;
			}
			if (root.Left == null)
			{
				return root.Right;
			}
			if (root.Right == null)
			{
				return root.Left;
			}
			if (root.Balance == 1)
			{
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node;
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> right = PersistentAvlTree<TKey, TValue>.RemoveLeftMost(root.Right, out node);
				return PersistentAvlTree<TKey, TValue>.BalanceNode(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(root.Left, right, node.Key, node.Value));
			}
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node2;
			PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> left = PersistentAvlTree<TKey, TValue>.RemoveRightMost(root.Left, out node2);
			return PersistentAvlTree<TKey, TValue>.BalanceNode(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(left, root.Right, node2.Key, node2.Value));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00012961 File Offset: 0x00010B61
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> RemoveLeftMost(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node, out PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> leftMost)
		{
			if (node.Left == null)
			{
				leftMost = node;
				return node.Right;
			}
			return PersistentAvlTree<TKey, TValue>.BalanceNode(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(PersistentAvlTree<TKey, TValue>.RemoveLeftMost(node.Left, out leftMost), node.Right, node.Key, node.Value));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001299D File Offset: 0x00010B9D
		private static PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> RemoveRightMost(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node, out PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> rightMost)
		{
			if (node.Right == null)
			{
				rightMost = node;
				return node.Left;
			}
			return PersistentAvlTree<TKey, TValue>.BalanceNode(new PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>(node.Left, PersistentAvlTree<TKey, TValue>.RemoveRightMost(node.Right, out rightMost), node.Key, node.Value));
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000129D9 File Offset: 0x00010BD9
		[Conditional("DEBUG")]
		private static void VerifyBalance(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00012B5C File Offset: 0x00010D5C
		private static IEnumerable<R> TraverseInLeftMiddleRightOrder<R>(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root, Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, R> converter)
		{
			if (root != null)
			{
				Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>> stack = new Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>>(root.Height);
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = root;
				do
				{
					if (node != null)
					{
						stack.Push(node);
						node = node.Left;
					}
					else
					{
						node = stack.Pop();
						yield return converter(node);
						node = node.Right;
					}
				}
				while (node != null || stack.Count > 0);
			}
			yield break;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00012CF4 File Offset: 0x00010EF4
		private static IEnumerable<R> TraverseInMiddleLeftRightOrder<R>(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root, Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, R> converter)
		{
			if (root != null)
			{
				Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>> stack = new Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>>(root.Height);
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = root;
				do
				{
					if (node == null)
					{
						node = stack.Pop();
					}
					yield return converter(node);
					if (node.Right != null)
					{
						stack.Push(node.Right);
					}
					node = node.Left;
				}
				while (stack.Count > 0);
			}
			yield break;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00012E8C File Offset: 0x0001108C
		private static IEnumerable<R> TraverseInMiddleRightLeftOrder<R>(PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root, Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, R> converter)
		{
			if (root != null)
			{
				Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>> stack = new Stack<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>>(root.Height);
				PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node = root;
				do
				{
					if (node == null)
					{
						node = stack.Pop();
					}
					yield return converter(node);
					if (node.Left != null)
					{
						stack.Push(node.Left);
					}
					node = node.Right;
				}
				while (stack.Count > 0);
			}
			yield break;
		}

		// Token: 0x04000612 RID: 1554
		private static readonly PersistentAvlTree<TKey, TValue> empty = new PersistentAvlTree<TKey, TValue>();

		// Token: 0x04000613 RID: 1555
		private static readonly Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, TKey> KeyConverter = (PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node) => node.Key;

		// Token: 0x04000614 RID: 1556
		private static readonly Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, TValue> ValueConverter = (PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node) => node.Value;

		// Token: 0x04000615 RID: 1557
		private static readonly Converter<PersistentAvlTree<TKey, TValue>.Node<TKey, TValue>, KeyValuePair<TKey, TValue>> KeyValuePairConverter = (PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> node) => new KeyValuePair<TKey, TValue>(node.Key, node.Value);

		// Token: 0x04000616 RID: 1558
		private readonly PersistentAvlTree<TKey, TValue>.Node<TKey, TValue> root;

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x0600069B RID: 1691
		private delegate R Convertor<T, R>(T value);

		// Token: 0x02000074 RID: 116
		private sealed class Node<K, V>
		{
			// Token: 0x0600069E RID: 1694 RVA: 0x00012F54 File Offset: 0x00011154
			internal Node(PersistentAvlTree<TKey, TValue>.Node<K, V> left, PersistentAvlTree<TKey, TValue>.Node<K, V> right, K key, V value)
			{
				this.Left = left;
				this.Right = right;
				this.Key = key;
				this.Value = value;
				this.Height = 1 + Math.Max(PersistentAvlTree<TKey, TValue>.Node<K, V>.GetHeight(left), PersistentAvlTree<TKey, TValue>.Node<K, V>.GetHeight(right));
				this.Count = 1 + PersistentAvlTree<TKey, TValue>.Node<K, V>.GetCount(left) + PersistentAvlTree<TKey, TValue>.Node<K, V>.GetCount(right);
				this.Balance = PersistentAvlTree<TKey, TValue>.Node<K, V>.GetHeight(right) - PersistentAvlTree<TKey, TValue>.Node<K, V>.GetHeight(left);
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x00012FC5 File Offset: 0x000111C5
			internal static int GetHeight(PersistentAvlTree<TKey, TValue>.Node<K, V> node)
			{
				if (node == null)
				{
					return 0;
				}
				return node.Height;
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x00012FD2 File Offset: 0x000111D2
			internal static int GetCount(PersistentAvlTree<TKey, TValue>.Node<K, V> node)
			{
				if (node == null)
				{
					return 0;
				}
				return node.Count;
			}

			// Token: 0x0400061A RID: 1562
			internal readonly PersistentAvlTree<TKey, TValue>.Node<K, V> Left;

			// Token: 0x0400061B RID: 1563
			internal readonly PersistentAvlTree<TKey, TValue>.Node<K, V> Right;

			// Token: 0x0400061C RID: 1564
			internal readonly K Key;

			// Token: 0x0400061D RID: 1565
			internal readonly V Value;

			// Token: 0x0400061E RID: 1566
			internal readonly int Height;

			// Token: 0x0400061F RID: 1567
			internal readonly int Count;

			// Token: 0x04000620 RID: 1568
			internal readonly int Balance;
		}
	}
}
