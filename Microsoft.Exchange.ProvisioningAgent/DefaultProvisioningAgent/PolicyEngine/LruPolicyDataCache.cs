using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine
{
	// Token: 0x02000038 RID: 56
	internal class LruPolicyDataCache : IDictionary<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>, ICollection<KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>>, IEnumerable<KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>>, IEnumerable
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000089B8 File Offset: 0x00006BB8
		public LruPolicyDataCache(int bucketSize)
		{
			if (bucketSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bucketSize");
			}
			this.bucketSize = bucketSize;
			int capacity = Math.Min(16, bucketSize);
			this.bucket = new Dictionary<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>(capacity);
			this.lruList = new LinkedList<PolicyDataCacheKey>();
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008A0C File Offset: 0x00006C0C
		public int BucketSize
		{
			get
			{
				return this.bucketSize;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008A14 File Offset: 0x00006C14
		private void MarkAsMostRecentlyUsed(PolicyDataCacheKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			lock (this.syncRoot)
			{
				LinkedListNode<PolicyDataCacheKey> linkedListNode = this.lruList.Find(key);
				if (linkedListNode != null && linkedListNode.Next != null)
				{
					this.lruList.Remove(linkedListNode);
					this.lruList.AddLast(linkedListNode);
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008A8C File Offset: 0x00006C8C
		public void Add(PolicyDataCacheKey key, IEnumerable<ADProvisioningPolicy> value)
		{
			lock (this.syncRoot)
			{
				this.bucket.Add(key, value);
				this.lruList.AddLast(key);
				if (this.BucketSize < this.bucket.Count)
				{
					this.bucket.Remove(this.lruList.First.Value);
					this.lruList.RemoveFirst();
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008B1C File Offset: 0x00006D1C
		public bool ContainsKey(PolicyDataCacheKey key)
		{
			return this.bucket.ContainsKey(key);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008B2A File Offset: 0x00006D2A
		public ICollection<PolicyDataCacheKey> Keys
		{
			get
			{
				return this.bucket.Keys;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008B38 File Offset: 0x00006D38
		public bool Remove(PolicyDataCacheKey key)
		{
			bool result;
			lock (this.syncRoot)
			{
				bool flag2 = this.bucket.Remove(key);
				this.lruList.Remove(key);
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008B90 File Offset: 0x00006D90
		public bool TryGetValue(PolicyDataCacheKey key, out IEnumerable<ADProvisioningPolicy> value)
		{
			this.MarkAsMostRecentlyUsed(key);
			return this.bucket.TryGetValue(key, out value);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008BA6 File Offset: 0x00006DA6
		public ICollection<IEnumerable<ADProvisioningPolicy>> Values
		{
			get
			{
				return this.bucket.Values;
			}
		}

		// Token: 0x17000064 RID: 100
		public IEnumerable<ADProvisioningPolicy> this[PolicyDataCacheKey key]
		{
			get
			{
				this.MarkAsMostRecentlyUsed(key);
				return this.bucket[key];
			}
			set
			{
				lock (this.syncRoot)
				{
					this.bucket[key] = value;
				}
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008C10 File Offset: 0x00006E10
		public void Add(KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008C18 File Offset: 0x00006E18
		public void Clear()
		{
			lock (this.syncRoot)
			{
				this.bucket.Clear();
				this.lruList.Clear();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008C68 File Offset: 0x00006E68
		public bool Contains(KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008C6F File Offset: 0x00006E6F
		public void CopyTo(KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008C76 File Offset: 0x00006E76
		public int Count
		{
			get
			{
				return this.bucket.Count;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008C83 File Offset: 0x00006E83
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008C86 File Offset: 0x00006E86
		public bool Remove(KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008C8D File Offset: 0x00006E8D
		public IEnumerator<KeyValuePair<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C94 File Offset: 0x00006E94
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000AE RID: 174
		private int bucketSize;

		// Token: 0x040000AF RID: 175
		private Dictionary<PolicyDataCacheKey, IEnumerable<ADProvisioningPolicy>> bucket;

		// Token: 0x040000B0 RID: 176
		private LinkedList<PolicyDataCacheKey> lruList;

		// Token: 0x040000B1 RID: 177
		private object syncRoot = new object();
	}
}
