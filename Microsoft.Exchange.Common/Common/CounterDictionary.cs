using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class CounterDictionary<T>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021A6 File Offset: 0x000003A6
		public CounterDictionary() : this(0, int.MaxValue)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B4 File Offset: 0x000003B4
		public CounterDictionary(int capacity) : this(capacity, int.MaxValue)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C2 File Offset: 0x000003C2
		public CounterDictionary(int capacity, int maxSize)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (maxSize < 0)
			{
				throw new ArgumentOutOfRangeException("maxSize");
			}
			this.dictionary = new Dictionary<T, int>(capacity);
			this.maxSize = maxSize;
		}

		// Token: 0x17000002 RID: 2
		public int this[T key]
		{
			get
			{
				int result;
				lock (this.dictionary)
				{
					int num = 0;
					this.dictionary.TryGetValue(key, out num);
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224C File Offset: 0x0000044C
		public int IncrementCounter(T key)
		{
			return this.AddCounter(key, 1);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002256 File Offset: 0x00000456
		public int DecrementCounter(T key)
		{
			return this.AddCounter(key, -1);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002260 File Offset: 0x00000460
		private int AddCounter(T key, int delta)
		{
			int result;
			lock (this.dictionary)
			{
				int num = 0;
				bool flag2 = this.dictionary.TryGetValue(key, out num);
				int num2 = num + delta;
				if (flag2 || this.dictionary.Count < this.maxSize)
				{
					this.dictionary[key] = num2;
					result = num2;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private Dictionary<T, int> dictionary;

		// Token: 0x04000005 RID: 5
		private int maxSize;
	}
}
