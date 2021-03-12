using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000036 RID: 54
	public class Btree<TKey, TValue> : IEnumerable<TValue>, IEnumerable where TKey : IComparable<TKey> where TValue : ISortKey<TKey>
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00006BEC File Offset: 0x00004DEC
		public Btree(int nodeDensity)
		{
			this.nodeDensity = ((nodeDensity > 5) ? nodeDensity : 5);
			this.root = new Btree<TKey, TValue>.Node
			{
				Key = default(TKey)
			};
			this.count = 0;
			this.version = 0;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006C38 File Offset: 0x00004E38
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this.root.Type == Btree<TKey, TValue>.NodeType.Empty)
			{
				value = default(TValue);
				return false;
			}
			TValue[] array = null;
			int num = 0;
			if (this.root.Type == Btree<TKey, TValue>.NodeType.InnerNode)
			{
				Btree<TKey, TValue>.Node[] children = this.root.Children;
				int num2 = this.root.Count;
				while (array == null)
				{
					int num3;
					Btree<TKey, TValue>.BinarySearch<Btree<TKey, TValue>.Node>(key, children, num2, out num3);
					Btree<TKey, TValue>.Node node = children[(num3 < 0) ? 0 : num3];
					if (node.Type == Btree<TKey, TValue>.NodeType.LeafNode)
					{
						array = node.LeafValues;
						num = node.Count;
					}
					else
					{
						children = node.Children;
						num2 = node.Count;
					}
				}
			}
			else
			{
				array = this.root.LeafValues;
				num = this.root.Count;
			}
			int num4;
			if (!Btree<TKey, TValue>.BinarySearch<TValue>(key, array, num, out num4))
			{
				value = default(TValue);
				return false;
			}
			value = array[num4];
			return true;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006D18 File Offset: 0x00004F18
		public bool TryGetFirst(out TValue value)
		{
			if (this.root.Type == Btree<TKey, TValue>.NodeType.Empty)
			{
				value = default(TValue);
				return false;
			}
			Btree<TKey, TValue>.Node node = this.root;
			while (node.Type == Btree<TKey, TValue>.NodeType.InnerNode)
			{
				node = node.Children[0];
			}
			value = node.LeafValues[0];
			return true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006D78 File Offset: 0x00004F78
		public bool TryGetLast(out TValue value)
		{
			if (this.root.Type == Btree<TKey, TValue>.NodeType.Empty)
			{
				value = default(TValue);
				return false;
			}
			Btree<TKey, TValue>.Node node = this.root;
			while (node.Type == Btree<TKey, TValue>.NodeType.InnerNode)
			{
				node = node.Children[node.Count - 1];
			}
			value = node.LeafValues[node.Count - 1];
			return true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public void Add(TValue value)
		{
			if (this.root.Type == Btree<TKey, TValue>.NodeType.Empty)
			{
				this.root.Type = Btree<TKey, TValue>.NodeType.LeafNode;
				this.root.LeafValues = new TValue[this.nodeDensity];
				this.root.LeafValues[0] = value;
				this.root.Count = 1;
				this.root.Key = value.SortKey;
				this.count = 1;
				this.version++;
				return;
			}
			if (this.root.Type == Btree<TKey, TValue>.NodeType.LeafNode)
			{
				Btree<TKey, TValue>.InsertToLeaf(ref this.root, value);
			}
			else
			{
				Btree<TKey, TValue>.InsertToInner(ref this.root, value);
			}
			if (this.root.IsFull)
			{
				Btree<TKey, TValue>.Node[] array = new Btree<TKey, TValue>.Node[this.nodeDensity];
				if (this.root.Type == Btree<TKey, TValue>.NodeType.LeafNode)
				{
					array[0] = this.root;
					Btree<TKey, TValue>.Node node = Btree<TKey, TValue>.SplitLeaf(ref array[0]);
					array[1] = node;
				}
				else
				{
					array[0] = this.root;
					Btree<TKey, TValue>.Node node2 = Btree<TKey, TValue>.SplitInner(ref array[0]);
					array[1] = node2;
				}
				this.root = new Btree<TKey, TValue>.Node
				{
					Type = Btree<TKey, TValue>.NodeType.InnerNode,
					Count = 2,
					Children = array,
					Key = array[0].Key
				};
			}
			this.count++;
			this.version++;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006F74 File Offset: 0x00005174
		public bool Remove(TKey key, out TValue value)
		{
			if (this.root.Type == Btree<TKey, TValue>.NodeType.Empty)
			{
				value = default(TValue);
				return false;
			}
			bool flag;
			if (this.root.Type == Btree<TKey, TValue>.NodeType.LeafNode)
			{
				flag = Btree<TKey, TValue>.RemoveFromLeaf(ref this.root, key, out value);
				if (flag)
				{
					if (this.root.Count == 0)
					{
						this.root = default(Btree<TKey, TValue>.Node);
						this.root.Key = default(TKey);
					}
					this.count--;
					this.version++;
				}
			}
			else
			{
				flag = Btree<TKey, TValue>.RemoveFromInner(ref this.root, key, out value);
				if (flag)
				{
					if (this.root.Count == 1)
					{
						this.root = this.root.Children[0];
					}
					this.count--;
					this.version++;
				}
			}
			return flag;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000705C File Offset: 0x0000525C
		public void Clear()
		{
			this.root = new Btree<TKey, TValue>.Node
			{
				Key = default(TKey)
			};
			this.count = 0;
			this.version++;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000709A File Offset: 0x0000529A
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000070A2 File Offset: 0x000052A2
		public Btree<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Btree<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000070AA File Offset: 0x000052AA
		IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
		{
			return new Btree<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000070B7 File Offset: 0x000052B7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Btree<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000070C4 File Offset: 0x000052C4
		private static bool RemoveFromLeaf(ref Btree<TKey, TValue>.Node leaf, TKey key, out TValue value)
		{
			int num;
			if (!Btree<TKey, TValue>.BinarySearch<TValue>(key, leaf.LeafValues, leaf.Count, out num))
			{
				value = default(TValue);
				return false;
			}
			value = leaf.LeafValues[num];
			Btree<TKey, TValue>.RemoveAt<TValue>(leaf.LeafValues, ref leaf.Count, num);
			if (num == 0 && leaf.Count > 0)
			{
				leaf.Key = leaf.LeafValues[0].SortKey;
			}
			return true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007140 File Offset: 0x00005340
		private static bool RemoveFromInner(ref Btree<TKey, TValue>.Node innerNode, TKey key, out TValue value)
		{
			Btree<TKey, TValue>.Node[] children = innerNode.Children;
			int num;
			Btree<TKey, TValue>.BinarySearch<Btree<TKey, TValue>.Node>(key, children, innerNode.Count, out num);
			if (num < 0)
			{
				num = 0;
			}
			bool flag;
			if (children[num].Type == Btree<TKey, TValue>.NodeType.LeafNode)
			{
				flag = Btree<TKey, TValue>.RemoveFromLeaf(ref children[num], key, out value);
			}
			else
			{
				flag = Btree<TKey, TValue>.RemoveFromInner(ref children[num], key, out value);
			}
			if (!flag)
			{
				return false;
			}
			if (num == 0)
			{
				innerNode.Key = children[0].Key;
			}
			if (children[num].NeedRebalance && innerNode.Count > 1)
			{
				int num2;
				if (num > 0 && num < innerNode.Count - 1)
				{
					if (children[num - 1].Count + children[num].Count < children.Length)
					{
						num2 = num - 1;
					}
					else if (children[num].Count + children[num + 1].Count < children.Length)
					{
						num2 = num;
					}
					else if (children[num - 1].Count < children[num + 1].Count)
					{
						num2 = num;
					}
					else
					{
						num2 = num - 1;
					}
				}
				else if (num == 0)
				{
					num2 = 0;
				}
				else
				{
					num2 = num - 1;
				}
				int num3 = num2 + 1;
				Btree<TKey, TValue>.Rebalance(ref children[num2], ref children[num3]);
				if (children[num3].Count == 0)
				{
					Btree<TKey, TValue>.RemoveAt<Btree<TKey, TValue>.Node>(children, ref innerNode.Count, num3);
				}
			}
			return true;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007294 File Offset: 0x00005494
		private static Btree<TKey, TValue>.Node SplitLeaf(ref Btree<TKey, TValue>.Node leafNode)
		{
			TValue[] leafValues = leafNode.LeafValues;
			TValue[] array = new TValue[leafValues.Length];
			int num = leafNode.Count / 2;
			int num2 = leafValues.Length - num;
			Array.Copy(leafValues, num, array, 0, num2);
			Array.Clear(leafValues, num, num2);
			leafNode.Count -= num2;
			return new Btree<TKey, TValue>.Node
			{
				Type = Btree<TKey, TValue>.NodeType.LeafNode,
				Count = num2,
				LeafValues = array,
				Key = array[0].SortKey
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007320 File Offset: 0x00005520
		private static Btree<TKey, TValue>.Node SplitInner(ref Btree<TKey, TValue>.Node innerNode)
		{
			Btree<TKey, TValue>.Node[] children = innerNode.Children;
			Btree<TKey, TValue>.Node[] array = new Btree<TKey, TValue>.Node[children.Length];
			int num = innerNode.Count / 2;
			int num2 = children.Length - num;
			Array.Copy(children, num, array, 0, num2);
			Array.Clear(children, num, num2);
			innerNode.Count -= num2;
			return new Btree<TKey, TValue>.Node
			{
				Type = Btree<TKey, TValue>.NodeType.InnerNode,
				Count = num2,
				Children = array,
				Key = array[0].Key
			};
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000073A4 File Offset: 0x000055A4
		private static void Rebalance(ref Btree<TKey, TValue>.Node left, ref Btree<TKey, TValue>.Node right)
		{
			int num = (left.Type == Btree<TKey, TValue>.NodeType.LeafNode) ? left.LeafValues.Length : left.Children.Length;
			if (left.Count + right.Count < num)
			{
				if (left.Type == Btree<TKey, TValue>.NodeType.InnerNode)
				{
					Array.Copy(right.Children, 0, left.Children, left.Count, right.Count);
					right.Children = null;
				}
				else
				{
					Array.Copy(right.LeafValues, 0, left.LeafValues, left.Count, right.Count);
					right.LeafValues = null;
				}
				left.Count += right.Count;
				right.Count = 0;
				return;
			}
			if (left.Count + right.Count > num)
			{
				int num2 = (left.Count + right.Count) / 2;
				if (left.Count < num2)
				{
					int num3 = num2 - left.Count;
					if (left.Type == Btree<TKey, TValue>.NodeType.InnerNode)
					{
						Array.Copy(right.Children, 0, left.Children, left.Count, num3);
						Array.Copy(right.Children, num3, right.Children, 0, right.Count - num3);
						right.Count -= num3;
						Array.Clear(right.Children, right.Count, num3);
						right.Key = right.Children[0].Key;
					}
					else
					{
						Array.Copy(right.LeafValues, 0, left.LeafValues, left.Count, num3);
						Array.Copy(right.LeafValues, num3, right.LeafValues, 0, right.Count - num3);
						right.Count -= num3;
						Array.Clear(right.LeafValues, right.Count, num3);
						right.Key = right.LeafValues[0].SortKey;
					}
					left.Count += num3;
					return;
				}
				if (left.Count > num2)
				{
					int num4 = left.Count - num2;
					if (left.Type == Btree<TKey, TValue>.NodeType.InnerNode)
					{
						Array.Copy(right.Children, 0, right.Children, num4, right.Count);
						Array.Copy(left.Children, num2, right.Children, 0, num4);
						Array.Clear(left.Children, num2, num4);
						right.Key = right.Children[0].Key;
					}
					else
					{
						Array.Copy(right.LeafValues, 0, right.LeafValues, num4, right.Count);
						Array.Copy(left.LeafValues, num2, right.LeafValues, 0, num4);
						Array.Clear(left.LeafValues, num2, num4);
						right.Key = right.LeafValues[0].SortKey;
					}
					left.Count -= num4;
					right.Count += num4;
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007663 File Offset: 0x00005863
		private static void InsertAt<T>(T[] items, ref int count, int index, T value)
		{
			if (index < count)
			{
				Array.Copy(items, index, items, index + 1, count - index);
			}
			items[index] = value;
			count++;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007688 File Offset: 0x00005888
		private static void RemoveAt<T>(T[] items, ref int count, int index)
		{
			Array.Copy(items, index + 1, items, index, count - index);
			items[count] = default(T);
			count--;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000076BC File Offset: 0x000058BC
		private static void InsertToLeaf(ref Btree<TKey, TValue>.Node leaf, TValue value)
		{
			int num;
			if (Btree<TKey, TValue>.BinarySearch<TValue>(value.SortKey, leaf.LeafValues, leaf.Count, out num))
			{
				throw new ArgumentException("An element with the same key already exists in the tree.", "key");
			}
			Btree<TKey, TValue>.InsertAt<TValue>(leaf.LeafValues, ref leaf.Count, num + 1, value);
			if (num < 0)
			{
				leaf.Key = value.SortKey;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007728 File Offset: 0x00005928
		private static void InsertToInner(ref Btree<TKey, TValue>.Node innerNode, TValue value)
		{
			Btree<TKey, TValue>.Node[] children = innerNode.Children;
			TKey sortKey = value.SortKey;
			int num = children.Length;
			int num2;
			Btree<TKey, TValue>.BinarySearch<Btree<TKey, TValue>.Node>(sortKey, children, innerNode.Count, out num2);
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (children[num2].Type == Btree<TKey, TValue>.NodeType.LeafNode)
			{
				Btree<TKey, TValue>.InsertToLeaf(ref children[num2], value);
				if (children[num2].IsFull)
				{
					if (num2 < innerNode.Count - 1 && children[num2 + 1].Count <= num * 3 / 4)
					{
						Btree<TKey, TValue>.Rebalance(ref children[num2], ref children[num2 + 1]);
					}
					else if (num2 > 0 && children[num2 - 1].Count <= num * 3 / 4)
					{
						Btree<TKey, TValue>.Rebalance(ref children[num2 - 1], ref children[num2]);
					}
					else
					{
						Btree<TKey, TValue>.Node value2 = Btree<TKey, TValue>.SplitLeaf(ref children[num2]);
						Btree<TKey, TValue>.InsertAt<Btree<TKey, TValue>.Node>(children, ref innerNode.Count, num2 + 1, value2);
					}
				}
			}
			else
			{
				Btree<TKey, TValue>.InsertToInner(ref children[num2], value);
				if (children[num2].IsFull)
				{
					if (num2 < innerNode.Count - 1 && children[num2 + 1].Count <= num * 3 / 4)
					{
						Btree<TKey, TValue>.Rebalance(ref children[num2], ref children[num2 + 1]);
					}
					else if (num2 > 0 && children[num2 - 1].Count <= num * 3 / 4)
					{
						Btree<TKey, TValue>.Rebalance(ref children[num2 - 1], ref children[num2]);
					}
					else
					{
						Btree<TKey, TValue>.Node value3 = Btree<TKey, TValue>.SplitInner(ref children[num2]);
						Btree<TKey, TValue>.InsertAt<Btree<TKey, TValue>.Node>(children, ref innerNode.Count, num2 + 1, value3);
					}
				}
			}
			if (num2 == 0)
			{
				innerNode.Key = children[0].Key;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000078E0 File Offset: 0x00005AE0
		private static bool BinarySearch<TData>(TKey key, TData[] data, int count, out int index) where TData : ISortKey<TKey>
		{
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				int num3 = key.CompareTo(data[num2].SortKey);
				if (num3 < 0)
				{
					num = num2 - 1;
				}
				else
				{
					if (num3 <= 0)
					{
						index = num2;
						return true;
					}
					i = num2 + 1;
				}
			}
			index = num;
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000793E File Offset: 0x00005B3E
		[Conditional("DEBUG")]
		private static void AssertConsistent(Btree<TKey, TValue>.Node node)
		{
			if (node.Type == Btree<TKey, TValue>.NodeType.LeafNode)
			{
				return;
			}
			Btree<TKey, TValue>.NodeType type = node.Type;
		}

		// Token: 0x040000F8 RID: 248
		private const int MinimumNodeDensity = 5;

		// Token: 0x040000F9 RID: 249
		private Btree<TKey, TValue>.Node root;

		// Token: 0x040000FA RID: 250
		private readonly int nodeDensity;

		// Token: 0x040000FB RID: 251
		private int count;

		// Token: 0x040000FC RID: 252
		private int version;

		// Token: 0x02000037 RID: 55
		private enum NodeType
		{
			// Token: 0x040000FE RID: 254
			Empty,
			// Token: 0x040000FF RID: 255
			InnerNode,
			// Token: 0x04000100 RID: 256
			LeafNode
		}

		// Token: 0x02000038 RID: 56
		[DebuggerDisplay("[{Type}, {Count}, First={Key}]")]
		private struct Node : ISortKey<TKey>
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000159 RID: 345 RVA: 0x00007955 File Offset: 0x00005B55
			public TKey SortKey
			{
				get
				{
					return this.Key;
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600015A RID: 346 RVA: 0x0000795D File Offset: 0x00005B5D
			public bool IsFull
			{
				get
				{
					if (this.Type == Btree<TKey, TValue>.NodeType.InnerNode)
					{
						return this.Count == this.Children.Length;
					}
					return this.Type == Btree<TKey, TValue>.NodeType.LeafNode && this.Count == this.LeafValues.Length;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600015B RID: 347 RVA: 0x00007994 File Offset: 0x00005B94
			public bool NeedRebalance
			{
				get
				{
					if (this.Type == Btree<TKey, TValue>.NodeType.InnerNode)
					{
						return this.Count <= this.Children.Length / 2;
					}
					return this.Type == Btree<TKey, TValue>.NodeType.LeafNode && this.Count <= this.LeafValues.Length / 2;
				}
			}

			// Token: 0x04000101 RID: 257
			public Btree<TKey, TValue>.NodeType Type;

			// Token: 0x04000102 RID: 258
			public int Count;

			// Token: 0x04000103 RID: 259
			public TKey Key;

			// Token: 0x04000104 RID: 260
			public TValue[] LeafValues;

			// Token: 0x04000105 RID: 261
			public Btree<TKey, TValue>.Node[] Children;
		}

		// Token: 0x02000039 RID: 57
		public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x0600015C RID: 348 RVA: 0x000079E0 File Offset: 0x00005BE0
			public Enumerator(Btree<TKey, TValue> btree)
			{
				this.btree = btree;
				this.version = btree.version;
				this.node = default(Btree<TKey, TValue>.Node);
				this.valueIndex = -1;
				this.leafIndex = 0;
				this.current = default(TValue);
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x0600015D RID: 349 RVA: 0x00007A1B File Offset: 0x00005C1B
			public TValue Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600015E RID: 350 RVA: 0x00007A23 File Offset: 0x00005C23
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00007A30 File Offset: 0x00005C30
			public bool MoveNext()
			{
				if (this.version != this.btree.version)
				{
					throw new InvalidOperationException("Collection changed.");
				}
				if (this.node.Type == Btree<TKey, TValue>.NodeType.Empty && !Btree<TKey, TValue>.Enumerator.MoveFirst(this.btree, ref this.node))
				{
					return false;
				}
				if (this.node.Type == Btree<TKey, TValue>.NodeType.LeafNode)
				{
					if (this.valueIndex < this.node.Count - 1)
					{
						this.valueIndex++;
						this.current = this.node.LeafValues[this.valueIndex];
						return true;
					}
					return false;
				}
				else
				{
					if (this.node.Type == Btree<TKey, TValue>.NodeType.InnerNode)
					{
						if (this.valueIndex < this.node.Children[this.leafIndex].Count - 1)
						{
							this.valueIndex++;
						}
						else if (this.leafIndex < this.node.Count - 1)
						{
							this.leafIndex++;
							this.valueIndex = 0;
						}
						else
						{
							if (!Btree<TKey, TValue>.Enumerator.FindNext(this.btree.root, this.current.SortKey, ref this.node))
							{
								return false;
							}
							this.leafIndex = 0;
							this.valueIndex = 0;
						}
						this.current = this.node.Children[this.leafIndex].LeafValues[this.valueIndex];
						return true;
					}
					this.current = default(TValue);
					return false;
				}
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00007BB4 File Offset: 0x00005DB4
			public void Reset()
			{
				this.version = this.btree.version;
				this.node = default(Btree<TKey, TValue>.Node);
				this.valueIndex = -1;
				this.leafIndex = 0;
				this.current = default(TValue);
			}

			// Token: 0x06000161 RID: 353 RVA: 0x00007BED File Offset: 0x00005DED
			public void Dispose()
			{
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00007BEF File Offset: 0x00005DEF
			public Btree<TKey, TValue>.Enumerator Clone()
			{
				return this;
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00007BF8 File Offset: 0x00005DF8
			private static bool MoveFirst(Btree<TKey, TValue> btree, ref Btree<TKey, TValue>.Node node)
			{
				if (btree.root.Type == Btree<TKey, TValue>.NodeType.Empty)
				{
					return false;
				}
				if (btree.root.Type == Btree<TKey, TValue>.NodeType.LeafNode)
				{
					node = btree.root;
					return true;
				}
				node = btree.root;
				while (node.Children[0].Type == Btree<TKey, TValue>.NodeType.InnerNode)
				{
					node = node.Children[0];
				}
				return true;
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00007C6C File Offset: 0x00005E6C
			private static bool FindNext(Btree<TKey, TValue>.Node root, TKey key, ref Btree<TKey, TValue>.Node node)
			{
				if (root.Type != Btree<TKey, TValue>.NodeType.InnerNode || root.Children[0].Type == Btree<TKey, TValue>.NodeType.LeafNode)
				{
					return false;
				}
				Btree<TKey, TValue>.Node? node2 = null;
				int num = -1;
				Btree<TKey, TValue>.Node value = root;
				while (value.Children[0].Type == Btree<TKey, TValue>.NodeType.InnerNode)
				{
					int num2;
					Btree<TKey, TValue>.BinarySearch<Btree<TKey, TValue>.Node>(key, value.Children, value.Count, out num2);
					if (num2 < value.Count - 1)
					{
						node2 = new Btree<TKey, TValue>.Node?(value);
						num = num2 + 1;
					}
					value = value.Children[num2];
				}
				if (node2 == null)
				{
					return false;
				}
				node = node2.Value.Children[num];
				while (node.Children[0].Type == Btree<TKey, TValue>.NodeType.InnerNode)
				{
					node = node.Children[0];
				}
				return true;
			}

			// Token: 0x04000106 RID: 262
			private Btree<TKey, TValue> btree;

			// Token: 0x04000107 RID: 263
			private int version;

			// Token: 0x04000108 RID: 264
			private Btree<TKey, TValue>.Node node;

			// Token: 0x04000109 RID: 265
			private int valueIndex;

			// Token: 0x0400010A RID: 266
			private int leafIndex;

			// Token: 0x0400010B RID: 267
			private TValue current;
		}
	}
}
