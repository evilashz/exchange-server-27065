using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001D RID: 29
	internal class BigMap<TKey, TValue>
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000B895 File Offset: 0x00009A95
		public BigMap() : this(31, 71993)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public BigMap(int internalStoreNumber, int initialCapacity = 71993)
		{
			this.concurrentDictionaries = new ConcurrentDictionary<TKey, TValue>[internalStoreNumber];
			for (int i = 0; i < internalStoreNumber; i++)
			{
				this.concurrentDictionaries[i] = new ConcurrentDictionary<TKey, TValue>(4 * Environment.ProcessorCount, initialCapacity);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public DateTime TimeStamp { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000B8F5 File Offset: 0x00009AF5
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000B8FD File Offset: 0x00009AFD
		internal int NumberOfDictionary
		{
			get
			{
				return this.concurrentDictionaries.Length;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000B907 File Offset: 0x00009B07
		public ConcurrentDictionary<TKey, TValue> GetDictionary(int i)
		{
			if (i >= this.NumberOfDictionary || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.concurrentDictionaries[i];
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000B92C File Offset: 0x00009B2C
		public bool TryGetValue(TKey key, out TValue value)
		{
			ConcurrentDictionary<TKey, TValue> concurrentDictionary = this.concurrentDictionaries[(int)((UIntPtr)this.GetDictionaryIdx(key))];
			return concurrentDictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000B954 File Offset: 0x00009B54
		public TValue GetOrAdd(TKey key, TValue value)
		{
			ConcurrentDictionary<TKey, TValue> concurrentDictionary = this.concurrentDictionaries[(int)((UIntPtr)this.GetDictionaryIdx(key))];
			TValue result;
			if (concurrentDictionary.TryGetValue(key, out result))
			{
				return result;
			}
			if (concurrentDictionary.TryAdd(key, value))
			{
				Interlocked.Increment(ref this.count);
				return value;
			}
			concurrentDictionary.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			ConcurrentDictionary<TKey, TValue> concurrentDictionary = this.concurrentDictionaries[(int)((UIntPtr)this.GetDictionaryIdx(key))];
			TValue result;
			if (concurrentDictionary.TryGetValue(key, out result))
			{
				return result;
			}
			TValue tvalue = valueFactory(key);
			if (concurrentDictionary.TryAdd(key, tvalue))
			{
				Interlocked.Increment(ref this.count);
				return tvalue;
			}
			concurrentDictionary.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public TValue AddOrGet(TKey key, TValue value, Action postAdd)
		{
			ConcurrentDictionary<TKey, TValue> concurrentDictionary = this.concurrentDictionaries[(int)((UIntPtr)this.GetDictionaryIdx(key))];
			if (concurrentDictionary.TryAdd(key, value))
			{
				Interlocked.Increment(ref this.count);
				postAdd();
				return value;
			}
			TValue result;
			concurrentDictionary.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000BA44 File Offset: 0x00009C44
		public TValue AddOrUpdate(TKey key, TValue value, Func<TKey, TValue, TValue> updateValueFactory)
		{
			ConcurrentDictionary<TKey, TValue> concurrentDictionary = this.concurrentDictionaries[(int)((UIntPtr)this.GetDictionaryIdx(key))];
			if (concurrentDictionary.TryAdd(key, value))
			{
				Interlocked.Increment(ref this.count);
				return value;
			}
			return concurrentDictionary.AddOrUpdate(key, value, updateValueFactory);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000BA84 File Offset: 0x00009C84
		public void Clear()
		{
			for (int i = 0; i < this.NumberOfDictionary; i++)
			{
				this.concurrentDictionaries[i] = new ConcurrentDictionary<TKey, TValue>(4 * Environment.ProcessorCount, 71993);
			}
			this.count = 0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		private uint GetDictionaryIdx(TKey key)
		{
			if (this.NumberOfDictionary == 1)
			{
				return 0U;
			}
			return FnvHash.Fnv1A32(key.GetHashCode().ToString(CultureInfo.InvariantCulture)) % (uint)this.NumberOfDictionary;
		}

		// Token: 0x040000BB RID: 187
		public const int DefaultInternalStoreNumber = 31;

		// Token: 0x040000BC RID: 188
		public const int InitialCapacity = 71993;

		// Token: 0x040000BD RID: 189
		private const int DefaultConcurrencyMultiplier = 4;

		// Token: 0x040000BE RID: 190
		private readonly ConcurrentDictionary<TKey, TValue>[] concurrentDictionaries;

		// Token: 0x040000BF RID: 191
		private int count;
	}
}
