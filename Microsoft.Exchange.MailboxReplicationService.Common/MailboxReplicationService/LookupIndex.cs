using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000147 RID: 327
	internal abstract class LookupIndex<TKey, TData> : LookupTable<TData>.IIndex where TData : class
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000155B4 File Offset: 0x000137B4
		public LookupIndex()
		{
			this.entries = new Dictionary<TKey, LookupIndexEntry<TData>>(this.GetEqualityComparer());
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000155CD File Offset: 0x000137CD
		public LookupTable<TData> Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000155D8 File Offset: 0x000137D8
		public List<TKey> ResolvedKeys
		{
			get
			{
				this.Owner.LookupUnresolvedKeys();
				List<TKey> list = new List<TKey>();
				foreach (KeyValuePair<TKey, LookupIndexEntry<TData>> keyValuePair in this.entries)
				{
					if (keyValuePair.Value.Data != null)
					{
						list.Add(keyValuePair.Key);
					}
				}
				return list;
			}
		}

		// Token: 0x17000337 RID: 823
		public TData this[TKey key]
		{
			get
			{
				LookupIndexEntry<TData> indexEntry = this.GetIndexEntry(key);
				this.Owner.LookupUnresolvedKeys();
				return indexEntry.Data;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001567E File Offset: 0x0001387E
		public void AddKey(TKey key)
		{
			this.GetIndexEntry(key);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00015688 File Offset: 0x00013888
		public void AddKeys(ICollection<TKey> keys)
		{
			foreach (TKey key in keys)
			{
				this.AddKey(key);
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000156D0 File Offset: 0x000138D0
		void LookupTable<!1>.IIndex.SetOwner(LookupTable<TData> owner)
		{
			this.owner = owner;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000156DC File Offset: 0x000138DC
		void LookupTable<!1>.IIndex.InsertObject(TData data)
		{
			ICollection<TKey> collection = this.RetrieveKeys(data);
			if (collection != null)
			{
				foreach (TKey key in collection)
				{
					LookupIndexEntry<TData> indexEntry = this.GetIndexEntry(key);
					indexEntry.Data = data;
					indexEntry.IsResolved = true;
				}
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00015740 File Offset: 0x00013940
		void LookupTable<!1>.IIndex.LookupUnresolvedKeys()
		{
			List<TKey> list = null;
			foreach (KeyValuePair<TKey, LookupIndexEntry<TData>> keyValuePair in this.entries)
			{
				if (!keyValuePair.Value.IsResolved)
				{
					if (list == null)
					{
						list = new List<TKey>();
					}
					list.Add(keyValuePair.Key);
				}
			}
			if (list == null)
			{
				return;
			}
			TData[] array = this.LookupKeys(list.ToArray());
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					LookupIndexEntry<TData> lookupIndexEntry = this.entries[list[i]];
					lookupIndexEntry.IsResolved = true;
					lookupIndexEntry.Data = default(TData);
				}
				else
				{
					this.Owner.InsertObject(array[i]);
				}
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00015824 File Offset: 0x00013A24
		void LookupTable<!1>.IIndex.Clear()
		{
			this.entries.Clear();
		}

		// Token: 0x06000AEF RID: 2799
		protected abstract ICollection<TKey> RetrieveKeys(TData data);

		// Token: 0x06000AF0 RID: 2800
		protected abstract TData[] LookupKeys(TKey[] keys);

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00015831 File Offset: 0x00013A31
		protected virtual IEqualityComparer<TKey> GetEqualityComparer()
		{
			return null;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00015834 File Offset: 0x00013A34
		private LookupIndexEntry<TData> GetIndexEntry(TKey key)
		{
			LookupIndexEntry<TData> lookupIndexEntry;
			if (!this.entries.TryGetValue(key, out lookupIndexEntry))
			{
				lookupIndexEntry = new LookupIndexEntry<TData>();
				this.entries.Add(key, lookupIndexEntry);
			}
			return lookupIndexEntry;
		}

		// Token: 0x04000657 RID: 1623
		private LookupTable<TData> owner;

		// Token: 0x04000658 RID: 1624
		private Dictionary<TKey, LookupIndexEntry<TData>> entries;
	}
}
