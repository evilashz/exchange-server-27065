using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000482 RID: 1154
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06003858 RID: 14424 RVA: 0x000D7980 File Offset: 0x000D5B80
		[__DynamicallyInvokable]
		public ConcurrentQueue()
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L, this));
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000D79B0 File Offset: 0x000D5BB0
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(0L, this);
			this.m_head = segment;
			int num = 0;
			foreach (T value in collection)
			{
				segment.UnsafeAdd(value);
				num++;
				if (num >= 32)
				{
					segment = segment.UnsafeGrow();
					num = 0;
				}
			}
			this.m_tail = segment;
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000D7A28 File Offset: 0x000D5C28
		[__DynamicallyInvokable]
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000D7A45 File Offset: 0x000D5C45
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000D7A53 File Offset: 0x000D5C53
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitializeFromCollection(this.m_serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000D7A68 File Offset: 0x000D5C68
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000D7A85 File Offset: 0x000D5C85
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000D7A88 File Offset: 0x000D5C88
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000D7A99 File Offset: 0x000D5C99
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000D7AA1 File Offset: 0x000D5CA1
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<!0>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000D7AAB File Offset: 0x000D5CAB
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<!0>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000D7AB4 File Offset: 0x000D5CB4
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (!head.IsEmpty)
				{
					return false;
				}
				if (head.Next == null)
				{
					return true;
				}
				SpinWait spinWait = default(SpinWait);
				while (head.IsEmpty)
				{
					if (head.Next == null)
					{
						return true;
					}
					spinWait.SpinOnce();
					head = this.m_head;
				}
				return false;
			}
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000D7B0B File Offset: 0x000D5D0B
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000D7B18 File Offset: 0x000D5D18
		private List<T> ToList()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			List<T> list = new List<T>();
			try
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int start;
				int end;
				this.GetHeadTailPositions(out segment, out segment2, out start, out end);
				if (segment == segment2)
				{
					segment.AddToList(list, start, end);
				}
				else
				{
					segment.AddToList(list, start, 31);
					for (ConcurrentQueue<T>.Segment next = segment.Next; next != segment2; next = next.Next)
					{
						next.AddToList(list, 0, 31);
					}
					segment2.AddToList(list, 0, end);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			return list;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000D7BAC File Offset: 0x000D5DAC
		private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
		{
			head = this.m_head;
			tail = this.m_tail;
			headLow = head.Low;
			tailHigh = tail.High;
			SpinWait spinWait = default(SpinWait);
			while (head != this.m_head || tail != this.m_tail || headLow != head.Low || tailHigh != tail.High || head.m_index > tail.m_index)
			{
				spinWait.SpinOnce();
				head = this.m_head;
				tail = this.m_tail;
				headLow = head.Low;
				tailHigh = tail.High;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000D7C58 File Offset: 0x000D5E58
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					return num2 - num + 1;
				}
				int num3 = 32 - num;
				num3 += 32 * (int)(segment2.m_index - segment.m_index - 1L);
				return num3 + (num2 + 1);
			}
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000D7CA6 File Offset: 0x000D5EA6
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000D7CC4 File Offset: 0x000D5EC4
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			ConcurrentQueue<T>.Segment head;
			ConcurrentQueue<T>.Segment tail;
			int headLow;
			int tailHigh;
			this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
			return this.GetEnumerator(head, tail, headLow, tailHigh);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000D7CF5 File Offset: 0x000D5EF5
		private IEnumerator<T> GetEnumerator(ConcurrentQueue<T>.Segment head, ConcurrentQueue<T>.Segment tail, int headLow, int tailHigh)
		{
			try
			{
				SpinWait spin = default(SpinWait);
				if (head == tail)
				{
					int num;
					for (int i = headLow; i <= tailHigh; i = num + 1)
					{
						spin.Reset();
						while (!head.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[i];
						num = i;
					}
				}
				else
				{
					int num;
					for (int j = headLow; j < 32; j = num + 1)
					{
						spin.Reset();
						while (!head.m_state[j].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[j];
						num = j;
					}
					ConcurrentQueue<T>.Segment curr;
					for (curr = head.Next; curr != tail; curr = curr.Next)
					{
						for (int k = 0; k < 32; k = num + 1)
						{
							spin.Reset();
							while (!curr.m_state[k].m_value)
							{
								spin.SpinOnce();
							}
							yield return curr.m_array[k];
							num = k;
						}
					}
					for (int l = 0; l <= tailHigh; l = num + 1)
					{
						spin.Reset();
						while (!tail.m_state[l].m_value)
						{
							spin.SpinOnce();
						}
						yield return tail.m_array[l];
						num = l;
					}
					curr = null;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000D7D24 File Offset: 0x000D5F24
		[__DynamicallyInvokable]
		public void Enqueue(T item)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				ConcurrentQueue<T>.Segment tail = this.m_tail;
				if (tail.TryAppend(item))
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000D7D54 File Offset: 0x000D5F54
		[__DynamicallyInvokable]
		public bool TryDequeue(out T result)
		{
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryRemove(out result))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000D7D88 File Offset: 0x000D5F88
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryPeek(out result))
				{
					Interlocked.Decrement(ref this.m_numSnapshotTakers);
					return true;
				}
			}
			result = default(T);
			Interlocked.Decrement(ref this.m_numSnapshotTakers);
			return false;
		}

		// Token: 0x0400186B RID: 6251
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_head;

		// Token: 0x0400186C RID: 6252
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_tail;

		// Token: 0x0400186D RID: 6253
		private T[] m_serializationArray;

		// Token: 0x0400186E RID: 6254
		private const int SEGMENT_SIZE = 32;

		// Token: 0x0400186F RID: 6255
		[NonSerialized]
		internal volatile int m_numSnapshotTakers;

		// Token: 0x02000B91 RID: 2961
		private class Segment
		{
			// Token: 0x06006D53 RID: 27987 RVA: 0x0017869B File Offset: 0x0017689B
			internal Segment(long index, ConcurrentQueue<T> source)
			{
				this.m_array = new T[32];
				this.m_state = new VolatileBool[32];
				this.m_high = -1;
				this.m_index = index;
				this.m_source = source;
			}

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x06006D54 RID: 27988 RVA: 0x001786DA File Offset: 0x001768DA
			internal ConcurrentQueue<T>.Segment Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06006D55 RID: 27989 RVA: 0x001786E4 File Offset: 0x001768E4
			internal bool IsEmpty
			{
				get
				{
					return this.Low > this.High;
				}
			}

			// Token: 0x06006D56 RID: 27990 RVA: 0x001786F4 File Offset: 0x001768F4
			internal void UnsafeAdd(T value)
			{
				this.m_high++;
				this.m_array[this.m_high] = value;
				this.m_state[this.m_high].m_value = true;
			}

			// Token: 0x06006D57 RID: 27991 RVA: 0x00178748 File Offset: 0x00176948
			internal ConcurrentQueue<T>.Segment UnsafeGrow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = segment;
				return segment;
			}

			// Token: 0x06006D58 RID: 27992 RVA: 0x00178778 File Offset: 0x00176978
			internal void Grow()
			{
				ConcurrentQueue<T>.Segment next = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = next;
				this.m_source.m_tail = this.m_next;
			}

			// Token: 0x06006D59 RID: 27993 RVA: 0x001787BC File Offset: 0x001769BC
			internal bool TryAppend(T value)
			{
				if (this.m_high >= 31)
				{
					return false;
				}
				int num = 32;
				try
				{
				}
				finally
				{
					num = Interlocked.Increment(ref this.m_high);
					if (num <= 31)
					{
						this.m_array[num] = value;
						this.m_state[num].m_value = true;
					}
					if (num == 31)
					{
						this.Grow();
					}
				}
				return num <= 31;
			}

			// Token: 0x06006D5A RID: 27994 RVA: 0x00178838 File Offset: 0x00176A38
			internal bool TryRemove(out T result)
			{
				SpinWait spinWait = default(SpinWait);
				int i = this.Low;
				int high = this.High;
				while (i <= high)
				{
					if (Interlocked.CompareExchange(ref this.m_low, i + 1, i) == i)
					{
						SpinWait spinWait2 = default(SpinWait);
						while (!this.m_state[i].m_value)
						{
							spinWait2.SpinOnce();
						}
						result = this.m_array[i];
						if (this.m_source.m_numSnapshotTakers <= 0)
						{
							this.m_array[i] = default(T);
						}
						if (i + 1 >= 32)
						{
							spinWait2 = default(SpinWait);
							while (this.m_next == null)
							{
								spinWait2.SpinOnce();
							}
							this.m_source.m_head = this.m_next;
						}
						return true;
					}
					spinWait.SpinOnce();
					i = this.Low;
					high = this.High;
				}
				result = default(T);
				return false;
			}

			// Token: 0x06006D5B RID: 27995 RVA: 0x0017893C File Offset: 0x00176B3C
			internal bool TryPeek(out T result)
			{
				result = default(T);
				int low = this.Low;
				if (low > this.High)
				{
					return false;
				}
				SpinWait spinWait = default(SpinWait);
				while (!this.m_state[low].m_value)
				{
					spinWait.SpinOnce();
				}
				result = this.m_array[low];
				return true;
			}

			// Token: 0x06006D5C RID: 27996 RVA: 0x001789A0 File Offset: 0x00176BA0
			internal void AddToList(List<T> list, int start, int end)
			{
				for (int i = start; i <= end; i++)
				{
					SpinWait spinWait = default(SpinWait);
					while (!this.m_state[i].m_value)
					{
						spinWait.SpinOnce();
					}
					list.Add(this.m_array[i]);
				}
			}

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06006D5D RID: 27997 RVA: 0x001789F5 File Offset: 0x00176BF5
			internal int Low
			{
				get
				{
					return Math.Min(this.m_low, 32);
				}
			}

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x06006D5E RID: 27998 RVA: 0x00178A06 File Offset: 0x00176C06
			internal int High
			{
				get
				{
					return Math.Min(this.m_high, 31);
				}
			}

			// Token: 0x040034BA RID: 13498
			internal volatile T[] m_array;

			// Token: 0x040034BB RID: 13499
			internal volatile VolatileBool[] m_state;

			// Token: 0x040034BC RID: 13500
			private volatile ConcurrentQueue<T>.Segment m_next;

			// Token: 0x040034BD RID: 13501
			internal readonly long m_index;

			// Token: 0x040034BE RID: 13502
			private volatile int m_low;

			// Token: 0x040034BF RID: 13503
			private volatile int m_high;

			// Token: 0x040034C0 RID: 13504
			private volatile ConcurrentQueue<T> m_source;
		}
	}
}
