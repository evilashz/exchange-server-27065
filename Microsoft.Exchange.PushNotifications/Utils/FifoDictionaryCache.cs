using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x02000042 RID: 66
	internal class FifoDictionaryCache<TKey, TObject> where TKey : class where TObject : class
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00005778 File Offset: 0x00003978
		public FifoDictionaryCache(int maximumSize = 10000, IEqualityComparer<TKey> comparer = null) : this(maximumSize, null, null, comparer)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005784 File Offset: 0x00003984
		protected FifoDictionaryCache(int maximumSize = 10000, Dictionary<TKey, TObject> hashSet = null, Queue<TKey> creationOrder = null, IEqualityComparer<TKey> comparer = null)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("maximumSize", maximumSize, 1, int.MaxValue);
			this.maxNumberOfElements = maximumSize;
			this.existingInstances = (hashSet ?? new Dictionary<TKey, TObject>(comparer));
			this.creationOrder = (creationOrder ?? new Queue<TKey>(maximumSize));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000057D4 File Offset: 0x000039D4
		public virtual bool Add(TKey key, TObject property)
		{
			if (this.existingInstances.ContainsKey(key))
			{
				return false;
			}
			bool result = false;
			if (this.creationOrder.Count >= this.maxNumberOfElements)
			{
				TKey key2 = this.creationOrder.Dequeue();
				this.existingInstances.Remove(key2);
				result = true;
			}
			this.existingInstances.Add(key, property);
			this.creationOrder.Enqueue(key);
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000583B File Offset: 0x00003A3B
		public virtual bool ContainsKey(TKey key)
		{
			return this.existingInstances.ContainsKey(key);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005849 File Offset: 0x00003A49
		public virtual bool TryGetValue(TKey key, out TObject property)
		{
			return this.existingInstances.TryGetValue(key, out property);
		}

		// Token: 0x04000092 RID: 146
		public const int DefaultMaximumSize = 10000;

		// Token: 0x04000093 RID: 147
		private readonly Dictionary<TKey, TObject> existingInstances;

		// Token: 0x04000094 RID: 148
		private readonly Queue<TKey> creationOrder;

		// Token: 0x04000095 RID: 149
		private readonly int maxNumberOfElements;
	}
}
