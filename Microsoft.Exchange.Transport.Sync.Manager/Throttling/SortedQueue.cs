using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SortedQueue<T> : IEnumerable<T>, IEnumerable where T : IComparable<T>
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x00018841 File Offset: 0x00016A41
		public SortedQueue() : this(4)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001884A File Offset: 0x00016A4A
		public SortedQueue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("Capacity should not be less than zero.");
			}
			this.array = new T[capacity];
			this.head = 0;
			this.tail = 0;
			this.count = 0;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00018882 File Offset: 0x00016A82
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001888A File Offset: 0x00016A8A
		public bool IsEmpty()
		{
			return this.count == 0;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00018898 File Offset: 0x00016A98
		public void Clear()
		{
			if (this.head < this.tail)
			{
				Array.Clear(this.array, this.head, this.count);
			}
			else
			{
				Array.Clear(this.array, this.head, this.array.Length - this.head);
				Array.Clear(this.array, 0, this.tail);
			}
			this.head = 0;
			this.tail = 0;
			this.count = 0;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00018914 File Offset: 0x00016B14
		public T Dequeue()
		{
			T result = this.Peek();
			this.array[this.head] = default(T);
			this.head = (this.head + 1) % this.array.Length;
			this.count--;
			return result;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00018968 File Offset: 0x00016B68
		public void Enqueue(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.count == this.array.Length)
			{
				this.GrowArray();
			}
			int num;
			int num2;
			for (num = this.tail; num != this.head; num = num2)
			{
				num2 = (num - 1 + this.array.Length) % this.array.Length;
				if (item.CompareTo(this.array[num2]) >= 0)
				{
					break;
				}
				this.array[num] = this.array[num2];
			}
			this.array[num] = item;
			this.tail = (this.tail + 1) % this.array.Length;
			this.count++;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00018A2E File Offset: 0x00016C2E
		public T Peek()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("SortedQueue is empty");
			}
			return this.array[this.head];
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00018A54 File Offset: 0x00016C54
		public void TrimExcess()
		{
			int num = (int)((double)this.array.Length * 0.9);
			if (this.count < num)
			{
				this.SetCapacity(this.count);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00018B6C File Offset: 0x00016D6C
		public IEnumerator<T> GetEnumerator()
		{
			int currentSlot = this.head;
			for (int counted = 0; counted < this.count; counted++)
			{
				yield return this.array[currentSlot];
				currentSlot = (currentSlot + 1) % this.array.Length;
			}
			yield break;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00018B88 File Offset: 0x00016D88
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00018B90 File Offset: 0x00016D90
		private void GrowArray()
		{
			int num = this.array.Length * 2;
			if (num < this.array.Length + 4)
			{
				num = this.array.Length + 4;
			}
			this.SetCapacity(num);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00018BC8 File Offset: 0x00016DC8
		private void SetCapacity(int capacity)
		{
			T[] destinationArray = new T[capacity];
			if (this.count > 0)
			{
				if (this.head < this.tail)
				{
					Array.Copy(this.array, this.head, destinationArray, 0, this.count);
				}
				else
				{
					Array.Copy(this.array, this.head, destinationArray, 0, this.array.Length - this.head);
					Array.Copy(this.array, 0, destinationArray, this.array.Length - this.head, this.tail);
				}
			}
			this.array = destinationArray;
			this.head = 0;
			this.tail = ((this.count == capacity) ? 0 : this.count);
		}

		// Token: 0x0400022A RID: 554
		private const int DefaultCapacity = 4;

		// Token: 0x0400022B RID: 555
		private const int GrowFactor = 2;

		// Token: 0x0400022C RID: 556
		private const int MinimumGrow = 4;

		// Token: 0x0400022D RID: 557
		private const double ShrinkFactor = 0.9;

		// Token: 0x0400022E RID: 558
		private T[] array;

		// Token: 0x0400022F RID: 559
		private int head;

		// Token: 0x04000230 RID: 560
		private int tail;

		// Token: 0x04000231 RID: 561
		private int count;
	}
}
