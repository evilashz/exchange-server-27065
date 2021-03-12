using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200004A RID: 74
	internal sealed class MruDictionaryCache<TToken, TData> : IDisposable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000987E File Offset: 0x00007A7E
		public MruDictionaryCache(int capacity, int expireTimeInMinutes) : this(capacity, capacity, expireTimeInMinutes)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009889 File Offset: 0x00007A89
		public MruDictionaryCache(int initialCapacity, int capacity, int expireTimeInMinutes) : this(initialCapacity, capacity, expireTimeInMinutes, null)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009898 File Offset: 0x00007A98
		public MruDictionaryCache(int initialCapacity, int capacity, int expireTimeInMinutes, Action<TToken, TData> onEntryExpired)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", initialCapacity, "InitialCapacity must be greater or equal 0");
			}
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Capacity must be larger than 0");
			}
			if (expireTimeInMinutes < 1)
			{
				throw new ArgumentOutOfRangeException("expireTimeInMinutes", expireTimeInMinutes, "Expire times must be larger than 0");
			}
			this.dataFromToken = new Dictionary<TToken, LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo>>(initialCapacity);
			this.mruEntries = new LinkedList<MruDictionaryCache<TToken, TData>.EntryInfo>();
			this.expireTime = TimeSpan.FromMinutes((double)expireTimeInMinutes);
			this.onEntryExpired = onEntryExpired;
			this.expiryTimer = new Timer(new TimerCallback(this.ExpireEntries), null, (int)this.expireTime.TotalMilliseconds, (int)this.expireTime.TotalMilliseconds);
			this.capacity = capacity;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009968 File Offset: 0x00007B68
		public int Count
		{
			get
			{
				int count;
				lock (this.dictionarySynchronizationObject)
				{
					count = this.dataFromToken.Count;
				}
				return count;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000099B0 File Offset: 0x00007BB0
		public List<MruCacheDiagnosticEntryInfo> GetDiagnosticsInfo(Func<TData, string> dataToStringDelegate)
		{
			List<MruCacheDiagnosticEntryInfo> list;
			lock (this.dictionarySynchronizationObject)
			{
				list = new List<MruCacheDiagnosticEntryInfo>(this.dataFromToken.Count);
				DateTime d = DateTime.UtcNow.Subtract(this.expireTime);
				foreach (KeyValuePair<TToken, LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo>> keyValuePair in this.dataFromToken)
				{
					list.Add(new MruCacheDiagnosticEntryInfo(dataToStringDelegate(keyValuePair.Value.Value.NonExtendingData), keyValuePair.Value.Value.LastAccessed - d));
				}
			}
			return list;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009A88 File Offset: 0x00007C88
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00009A90 File Offset: 0x00007C90
		public Action OnExpirationStart
		{
			get
			{
				return this.onExpirationStart;
			}
			set
			{
				this.onExpirationStart = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009A99 File Offset: 0x00007C99
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00009AA1 File Offset: 0x00007CA1
		public Action OnExpirationComplete
		{
			get
			{
				return this.onExpirationComplete;
			}
			set
			{
				this.onExpirationComplete = value;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009AAC File Offset: 0x00007CAC
		public bool ContainsKey(TToken token)
		{
			bool result;
			lock (this.dictionarySynchronizationObject)
			{
				result = this.dataFromToken.ContainsKey(token);
			}
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public int AddAndCount(TToken token, TData data)
		{
			int count;
			lock (this.dictionarySynchronizationObject)
			{
				this[token] = data;
				count = this.dataFromToken.Count;
			}
			return count;
		}

		// Token: 0x17000064 RID: 100
		public TData this[TToken key]
		{
			get
			{
				TData result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				TData data;
				lock (this.dictionarySynchronizationObject)
				{
					LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> linkedListNode;
					if (this.dataFromToken.TryGetValue(key, out linkedListNode))
					{
						this.UpdateRecentlyUsedNode(linkedListNode);
						data = linkedListNode.Value.SetEntryInformation(key, value);
					}
					else
					{
						data = this.InternalAdd(key, value);
					}
				}
				MruDictionaryCache<TToken, TData>.DisposeData(data);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009BD4 File Offset: 0x00007DD4
		public bool TryGetValue(TToken token, out TData data)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> linkedListNode = null;
			lock (this.dictionarySynchronizationObject)
			{
				if (this.dataFromToken.TryGetValue(token, out linkedListNode))
				{
					data = linkedListNode.Value.Data;
					this.UpdateRecentlyUsedNode(linkedListNode);
					return true;
				}
			}
			data = default(TData);
			return false;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009C5C File Offset: 0x00007E5C
		public bool TryGetAndRemoveValue(TToken token, out TData data)
		{
			lock (this.dictionarySynchronizationObject)
			{
				if (this.TryGetValue(token, out data))
				{
					this.Remove(token);
					return true;
				}
			}
			data = default(TData);
			return false;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public void Add(TToken token, TData data)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			TData data2 = default(TData);
			try
			{
				lock (this.dictionarySynchronizationObject)
				{
					data2 = this.InternalAdd(token, data);
				}
			}
			finally
			{
				MruDictionaryCache<TToken, TData>.DisposeData(data2);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009D2C File Offset: 0x00007F2C
		public TData AddAndReturnOriginalData(TToken token, TData data)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			TData result;
			lock (this.dictionarySynchronizationObject)
			{
				result = this.InternalAdd(token, data);
			}
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009D84 File Offset: 0x00007F84
		public bool Remove(TToken key)
		{
			bool result;
			lock (this.dictionarySynchronizationObject)
			{
				LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> node = null;
				if (this.dataFromToken.TryGetValue(key, out node))
				{
					this.mruEntries.Remove(node);
				}
				result = this.dataFromToken.Remove(key);
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009DEC File Offset: 0x00007FEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009DFC File Offset: 0x00007FFC
		internal static void DisposeData(TData data)
		{
			IDisposable disposable = data as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009E20 File Offset: 0x00008020
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.expiryTimer.Dispose();
					this.expiryTimer = null;
					lock (this.dictionarySynchronizationObject)
					{
						this.mruEntries.Clear();
						this.mruEntries = null;
						foreach (LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> linkedListNode in this.dataFromToken.Values)
						{
							linkedListNode.Value.DisposeData();
						}
						this.dataFromToken.Clear();
						this.dataFromToken = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009EF4 File Offset: 0x000080F4
		private void UpdateRecentlyUsedNode(LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> entryInfoNode)
		{
			this.mruEntries.Remove(entryInfoNode);
			this.mruEntries.AddFirst(entryInfoNode);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009F10 File Offset: 0x00008110
		private void ExpireEntries(object state)
		{
			if (Interlocked.Exchange(ref this.expiringEntries, 1) != 1)
			{
				DateTime expirationTime = DateTime.UtcNow.Subtract(this.expireTime);
				try
				{
					bool flag = true;
					List<MruDictionaryCache<TToken, TData>.EntryInfo> list = new List<MruDictionaryCache<TToken, TData>.EntryInfo>(MruDictionaryCache<TToken, TData>.ExpireChunkSize);
					do
					{
						try
						{
							if (this.OnExpirationStart != null)
							{
								this.OnExpirationStart();
							}
							list.Clear();
							lock (this.dictionarySynchronizationObject)
							{
								for (int i = 0; i < MruDictionaryCache<TToken, TData>.ExpireChunkSize; i++)
								{
									LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> last = this.mruEntries.Last;
									if (last == null || !last.Value.IsExpired(expirationTime))
									{
										flag = false;
										break;
									}
									if (this.onEntryExpired != null)
									{
										this.onEntryExpired(last.Value.Token, last.Value.Data);
									}
									list.Add(last.Value);
									this.dataFromToken.Remove(last.Value.Token);
									this.mruEntries.Remove(last);
								}
							}
						}
						finally
						{
							try
							{
								if (this.OnExpirationComplete != null)
								{
									this.OnExpirationComplete();
								}
							}
							finally
							{
								foreach (MruDictionaryCache<TToken, TData>.EntryInfo entryInfo in list)
								{
									entryInfo.DisposeData();
								}
								list.Clear();
							}
						}
					}
					while (flag);
				}
				finally
				{
					Interlocked.Exchange(ref this.expiringEntries, 0);
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A104 File Offset: 0x00008304
		private TData InternalAdd(TToken token, TData data)
		{
			LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo> linkedListNode;
			if (this.mruEntries.Count < this.capacity)
			{
				linkedListNode = this.mruEntries.AddFirst(new MruDictionaryCache<TToken, TData>.EntryInfo());
			}
			else
			{
				linkedListNode = this.mruEntries.Last;
				this.UpdateRecentlyUsedNode(linkedListNode);
				this.dataFromToken.Remove(linkedListNode.Value.Token);
			}
			this.dataFromToken[token] = linkedListNode;
			return linkedListNode.Value.SetEntryInformation(token, data);
		}

		// Token: 0x04000148 RID: 328
		private static readonly int ExpireChunkSize = 100;

		// Token: 0x04000149 RID: 329
		private LinkedList<MruDictionaryCache<TToken, TData>.EntryInfo> mruEntries;

		// Token: 0x0400014A RID: 330
		private Dictionary<TToken, LinkedListNode<MruDictionaryCache<TToken, TData>.EntryInfo>> dataFromToken;

		// Token: 0x0400014B RID: 331
		private object dictionarySynchronizationObject = new object();

		// Token: 0x0400014C RID: 332
		private int expiringEntries;

		// Token: 0x0400014D RID: 333
		private Timer expiryTimer;

		// Token: 0x0400014E RID: 334
		private Action<TToken, TData> onEntryExpired;

		// Token: 0x0400014F RID: 335
		private Action onExpirationStart;

		// Token: 0x04000150 RID: 336
		private Action onExpirationComplete;

		// Token: 0x04000151 RID: 337
		private int capacity;

		// Token: 0x04000152 RID: 338
		private bool disposed;

		// Token: 0x04000153 RID: 339
		private TimeSpan expireTime;

		// Token: 0x0200004B RID: 75
		private class EntryInfo
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A184 File Offset: 0x00008384
			public TData Data
			{
				get
				{
					this.lastAccessed = DateTime.UtcNow;
					return this.data;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000205 RID: 517 RVA: 0x0000A197 File Offset: 0x00008397
			public TToken Token
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000A19F File Offset: 0x0000839F
			public bool IsExpired(DateTime expirationTime)
			{
				return this.lastAccessed < expirationTime;
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000A1B0 File Offset: 0x000083B0
			public TData SetEntryInformation(TToken token, TData data)
			{
				TData result = default(TData);
				this.token = token;
				if (!object.ReferenceEquals(this.data, data))
				{
					result = this.data;
					this.data = data;
				}
				this.lastAccessed = DateTime.UtcNow;
				return result;
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000A1FE File Offset: 0x000083FE
			public void DisposeData()
			{
				MruDictionaryCache<TToken, TData>.DisposeData(this.data);
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A20B File Offset: 0x0000840B
			internal TData NonExtendingData
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A213 File Offset: 0x00008413
			internal DateTime LastAccessed
			{
				get
				{
					return this.lastAccessed;
				}
			}

			// Token: 0x04000154 RID: 340
			private TData data;

			// Token: 0x04000155 RID: 341
			private TToken token;

			// Token: 0x04000156 RID: 342
			private DateTime lastAccessed;
		}
	}
}
