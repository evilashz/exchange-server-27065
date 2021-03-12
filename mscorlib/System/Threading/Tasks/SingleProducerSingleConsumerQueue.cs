using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000557 RID: 1367
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600414D RID: 16717 RVA: 0x000F28E0 File Offset: 0x000F0AE0
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000F2910 File Offset: 0x000F0B10
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = last + 1 & array.Length - 1;
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000F2974 File Offset: 0x000F0B74
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000F2A3C File Offset: 0x000F0C3C
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1 & array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000F2AB8 File Offset: 0x000F0CB8
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1 & segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000F2BC8 File Offset: 0x000F0DC8
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000F2C1C File Offset: 0x000F0E1C
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000F2CE4 File Offset: 0x000F0EE4
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1 & array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000F2D78 File Offset: 0x000F0F78
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1 & segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000F2EA8 File Offset: 0x000F10A8
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000F2EC0 File Offset: 0x000F10C0
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000F2F19 File Offset: 0x000F1119
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1 & segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000F2F28 File Offset: 0x000F1128
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x000F2F30 File Offset: 0x000F1130
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first & num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000F2F98 File Offset: 0x000F1198
		int IProducerConsumerQueue<!0>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001AE0 RID: 6880
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001AE1 RID: 6881
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001AE2 RID: 6882
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001AE3 RID: 6883
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000BF6 RID: 3062
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x06006EF9 RID: 28409 RVA: 0x0017DE96 File Offset: 0x0017C096
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x0400360F RID: 13839
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x04003610 RID: 13840
			internal readonly T[] m_array;

			// Token: 0x04003611 RID: 13841
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000BF7 RID: 3063
		private struct SegmentState
		{
			// Token: 0x04003612 RID: 13842
			internal PaddingFor32 m_pad0;

			// Token: 0x04003613 RID: 13843
			internal volatile int m_first;

			// Token: 0x04003614 RID: 13844
			internal int m_lastCopy;

			// Token: 0x04003615 RID: 13845
			internal PaddingFor32 m_pad1;

			// Token: 0x04003616 RID: 13846
			internal int m_firstCopy;

			// Token: 0x04003617 RID: 13847
			internal volatile int m_last;

			// Token: 0x04003618 RID: 13848
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x02000BF8 RID: 3064
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x06006EFA RID: 28410 RVA: 0x0017DEAA File Offset: 0x0017C0AA
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x1700131C RID: 4892
			// (get) Token: 0x06006EFB RID: 28411 RVA: 0x0017DEBC File Offset: 0x0017C0BC
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					List<T> list = new List<T>();
					foreach (T item in this.m_queue)
					{
						list.Add(item);
					}
					return list.ToArray();
				}
			}

			// Token: 0x04003619 RID: 13849
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}
	}
}
