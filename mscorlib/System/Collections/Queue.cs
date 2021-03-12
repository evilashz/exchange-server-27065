using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000462 RID: 1122
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x000D12C9 File Offset: 0x000CF4C9
		public Queue() : this(32, 2f)
		{
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000D12D8 File Offset: 0x000CF4D8
		public Queue(int capacity) : this(capacity, 2f)
		{
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000D12E8 File Offset: 0x000CF4E8
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", Environment.GetResourceString("ArgumentOutOfRange_QueueGrowFactor", new object[]
				{
					1,
					10
				}));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000D138C File Offset: 0x000CF58C
		public Queue(ICollection col) : this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000D13D7 File Offset: 0x000CF5D7
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000D13E0 File Offset: 0x000CF5E0
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = (this._array.Length - this._head < num) ? (this._array.Length - this._head) : num;
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000D1481 File Offset: 0x000CF681
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000D1484 File Offset: 0x000CF684
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000D14A8 File Offset: 0x000CF6A8
		public virtual void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000D1534 File Offset: 0x000CF734
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int length = array.Length;
			if (length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = (this._array.Length - this._head < num) ? (this._array.Length - this._head) : num;
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000D1610 File Offset: 0x000CF810
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000D16A4 File Offset: 0x000CF8A4
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000D16AC File Offset: 0x000CF8AC
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			object result = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return result;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000D1721 File Offset: 0x000CF921
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			return this._array[this._head];
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000D1748 File Offset: 0x000CF948
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000D1760 File Offset: 0x000CF960
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000D17BE File Offset: 0x000CF9BE
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000D17D8 File Offset: 0x000CF9D8
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000D186C File Offset: 0x000CFA6C
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000D192A File Offset: 0x000CFB2A
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x04001800 RID: 6144
		private object[] _array;

		// Token: 0x04001801 RID: 6145
		private int _head;

		// Token: 0x04001802 RID: 6146
		private int _tail;

		// Token: 0x04001803 RID: 6147
		private int _size;

		// Token: 0x04001804 RID: 6148
		private int _growFactor;

		// Token: 0x04001805 RID: 6149
		private int _version;

		// Token: 0x04001806 RID: 6150
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001807 RID: 6151
		private const int _MinimumGrow = 4;

		// Token: 0x04001808 RID: 6152
		private const int _ShrinkThreshold = 32;

		// Token: 0x02000B6A RID: 2922
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06006B80 RID: 27520 RVA: 0x00173473 File Offset: 0x00171673
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this.root = this._q.SyncRoot;
			}

			// Token: 0x17001247 RID: 4679
			// (get) Token: 0x06006B81 RID: 27521 RVA: 0x00173493 File Offset: 0x00171693
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001248 RID: 4680
			// (get) Token: 0x06006B82 RID: 27522 RVA: 0x00173496 File Offset: 0x00171696
			public override object SyncRoot
			{
				get
				{
					return this.root;
				}
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x06006B83 RID: 27523 RVA: 0x001734A0 File Offset: 0x001716A0
			public override int Count
			{
				get
				{
					object obj = this.root;
					int count;
					lock (obj)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06006B84 RID: 27524 RVA: 0x001734E8 File Offset: 0x001716E8
			public override void Clear()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06006B85 RID: 27525 RVA: 0x00173530 File Offset: 0x00171730
			public override object Clone()
			{
				object obj = this.root;
				object result;
				lock (obj)
				{
					result = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return result;
			}

			// Token: 0x06006B86 RID: 27526 RVA: 0x00173584 File Offset: 0x00171784
			public override bool Contains(object obj)
			{
				object obj2 = this.root;
				bool result;
				lock (obj2)
				{
					result = this._q.Contains(obj);
				}
				return result;
			}

			// Token: 0x06006B87 RID: 27527 RVA: 0x001735CC File Offset: 0x001717CC
			public override void CopyTo(Array array, int arrayIndex)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006B88 RID: 27528 RVA: 0x00173614 File Offset: 0x00171814
			public override void Enqueue(object value)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x06006B89 RID: 27529 RVA: 0x0017365C File Offset: 0x0017185C
			public override object Dequeue()
			{
				object obj = this.root;
				object result;
				lock (obj)
				{
					result = this._q.Dequeue();
				}
				return result;
			}

			// Token: 0x06006B8A RID: 27530 RVA: 0x001736A4 File Offset: 0x001718A4
			public override IEnumerator GetEnumerator()
			{
				object obj = this.root;
				IEnumerator enumerator;
				lock (obj)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006B8B RID: 27531 RVA: 0x001736EC File Offset: 0x001718EC
			public override object Peek()
			{
				object obj = this.root;
				object result;
				lock (obj)
				{
					result = this._q.Peek();
				}
				return result;
			}

			// Token: 0x06006B8C RID: 27532 RVA: 0x00173734 File Offset: 0x00171934
			public override object[] ToArray()
			{
				object obj = this.root;
				object[] result;
				lock (obj)
				{
					result = this._q.ToArray();
				}
				return result;
			}

			// Token: 0x06006B8D RID: 27533 RVA: 0x0017377C File Offset: 0x0017197C
			public override void TrimToSize()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x0400344E RID: 13390
			private Queue _q;

			// Token: 0x0400344F RID: 13391
			private object root;
		}

		// Token: 0x02000B6B RID: 2923
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006B8E RID: 27534 RVA: 0x001737C4 File Offset: 0x001719C4
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this.currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06006B8F RID: 27535 RVA: 0x0017381B File Offset: 0x00171A1B
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006B90 RID: 27536 RVA: 0x00173824 File Offset: 0x00171A24
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._index < 0)
				{
					this.currentElement = this._q._array;
					return false;
				}
				this.currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x1700124A RID: 4682
			// (get) Token: 0x06006B91 RID: 27537 RVA: 0x001738B0 File Offset: 0x00171AB0
			public virtual object Current
			{
				get
				{
					if (this.currentElement != this._q._array)
					{
						return this.currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
			}

			// Token: 0x06006B92 RID: 27538 RVA: 0x00173900 File Offset: 0x00171B00
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this.currentElement = this._q._array;
			}

			// Token: 0x04003450 RID: 13392
			private Queue _q;

			// Token: 0x04003451 RID: 13393
			private int _index;

			// Token: 0x04003452 RID: 13394
			private int _version;

			// Token: 0x04003453 RID: 13395
			private object currentElement;
		}

		// Token: 0x02000B6C RID: 2924
		internal class QueueDebugView
		{
			// Token: 0x06006B93 RID: 27539 RVA: 0x0017395E File Offset: 0x00171B5E
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this.queue = queue;
			}

			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x06006B94 RID: 27540 RVA: 0x0017397B File Offset: 0x00171B7B
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.queue.ToArray();
				}
			}

			// Token: 0x04003454 RID: 13396
			private Queue queue;
		}
	}
}
