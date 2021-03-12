using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuthIdentifierCache
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000B64C File Offset: 0x0000984C
		public AuthIdentifierCache(int partitions, int buckets, TimeSpan lifetime)
		{
			if (partitions < 1)
			{
				throw new ArgumentException("Number of partitions must be at least one.");
			}
			this.partitions = new AuthIdentifierCache.CachePartition[partitions];
			for (int i = 0; i < partitions; i++)
			{
				this.partitions[i] = new AuthIdentifierCache.CachePartition(buckets, lifetime);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B698 File Offset: 0x00009898
		public string Lookup(string identityKey)
		{
			int num = this.ComputePartitionIndex(identityKey);
			return this.partitions[num].Lookup(identityKey);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B6BC File Offset: 0x000098BC
		public void Add(string identityKey, string authIdentifierInfo)
		{
			int num = this.ComputePartitionIndex(identityKey);
			this.partitions[num].Add(identityKey, authIdentifierInfo);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B6E0 File Offset: 0x000098E0
		private int ComputePartitionIndex(string identityKey)
		{
			return Math.Abs(identityKey.GetHashCode()) % this.partitions.Length;
		}

		// Token: 0x040001AE RID: 430
		private readonly AuthIdentifierCache.CachePartition[] partitions;

		// Token: 0x02000038 RID: 56
		private class CachePartition
		{
			// Token: 0x06000171 RID: 369 RVA: 0x0000B704 File Offset: 0x00009904
			public CachePartition(int numBuckets, TimeSpan lifetime)
			{
				if (numBuckets < 2)
				{
					throw new ArgumentException("Number of buckets must be two or more.");
				}
				if (lifetime < TimeSpan.FromMinutes(1.0))
				{
					throw new ArgumentException("Lifetime must be at least one minute.");
				}
				this.buckets = new ConcurrentDictionary<string, string>[numBuckets];
				for (int i = 0; i < numBuckets; i++)
				{
					this.buckets[i] = new ConcurrentDictionary<string, string>();
				}
				this.bucketLifetime = (uint)lifetime.TotalMilliseconds / (uint)numBuckets;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0000B794 File Offset: 0x00009994
			public string Lookup(string identityKey)
			{
				this.FlushCheck();
				string result = null;
				ConcurrentDictionary<string, string>[] array = this.buckets;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].TryGetValue(identityKey, out result))
					{
						return result;
					}
				}
				return null;
			}

			// Token: 0x06000173 RID: 371 RVA: 0x0000B7D0 File Offset: 0x000099D0
			public void Add(string identityKey, string authIdentifierInfo)
			{
				this.FlushCheck();
				ConcurrentDictionary<string, string>[] array = this.buckets;
				ConcurrentDictionary<string, string> concurrentDictionary = array[0];
				concurrentDictionary[identityKey] = authIdentifierInfo;
			}

			// Token: 0x06000174 RID: 372 RVA: 0x0000B7F8 File Offset: 0x000099F8
			private void FlushCheck()
			{
				if (this.flushInProgress)
				{
					return;
				}
				uint num = (uint)(Environment.TickCount - (int)this.lastFlush);
				if (num <= this.bucketLifetime)
				{
					return;
				}
				lock (this.flushCheckLock)
				{
					if (this.flushInProgress)
					{
						return;
					}
					num = (uint)(Environment.TickCount - (int)this.lastFlush);
					if (num <= this.bucketLifetime)
					{
						return;
					}
					this.flushInProgress = true;
				}
				try
				{
					int num2 = (int)Math.Max(Math.Min(num / this.bucketLifetime, 1U), (uint)this.buckets.Length);
					ConcurrentDictionary<string, string>[] array = new ConcurrentDictionary<string, string>[this.buckets.Length];
					if (num2 < this.buckets.Length)
					{
						for (int i = this.buckets.Length - 1; i >= num2; i--)
						{
							array[i] = this.buckets[i - num2];
						}
					}
					for (int j = 0; j < num2; j++)
					{
						array[j] = new ConcurrentDictionary<string, string>();
					}
					this.buckets = array;
					this.lastFlush = (uint)Environment.TickCount;
				}
				finally
				{
					this.flushInProgress = false;
				}
			}

			// Token: 0x040001AF RID: 431
			private readonly object flushCheckLock = new object();

			// Token: 0x040001B0 RID: 432
			private readonly uint bucketLifetime;

			// Token: 0x040001B1 RID: 433
			private ConcurrentDictionary<string, string>[] buckets;

			// Token: 0x040001B2 RID: 434
			private uint lastFlush = (uint)Environment.TickCount;

			// Token: 0x040001B3 RID: 435
			private bool flushInProgress;
		}
	}
}
