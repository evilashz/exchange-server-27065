using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000D RID: 13
	public abstract class EvictionPolicy<TKey>
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x000047C9 File Offset: 0x000029C9
		public EvictionPolicy(int capacity)
		{
			this.capacity = capacity;
			this.keysToCleanup = new HashSet<TKey>();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000047E3 File Offset: 0x000029E3
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000047EB File Offset: 0x000029EB
		public virtual int Count
		{
			get
			{
				return this.keysToCleanup.Count;
			}
		}

		// Token: 0x060001F4 RID: 500
		public abstract void EvictionCheckpoint();

		// Token: 0x060001F5 RID: 501
		public abstract void Insert(TKey key);

		// Token: 0x060001F6 RID: 502
		public abstract void Remove(TKey key);

		// Token: 0x060001F7 RID: 503
		public abstract void KeyAccess(TKey key);

		// Token: 0x060001F8 RID: 504 RVA: 0x000047F8 File Offset: 0x000029F8
		public virtual bool Contains(TKey key)
		{
			return this.ContainsKeyToCleanup(key);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00004801 File Offset: 0x00002A01
		public virtual void Reset()
		{
			this.keysToCleanup.Clear();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00004810 File Offset: 0x00002A10
		public virtual TKey[] GetKeysToCleanup(bool clear)
		{
			TKey[] result = Array<TKey>.Empty;
			if (this.keysToCleanup.Count != 0)
			{
				result = this.keysToCleanup.ToArray<TKey>();
				if (clear)
				{
					this.keysToCleanup.Clear();
				}
			}
			return result;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000484B File Offset: 0x00002A4B
		public int CountOfKeysToCleanup
		{
			get
			{
				return this.keysToCleanup.Count;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00004858 File Offset: 0x00002A58
		public void AddKeyToCleanup(TKey key)
		{
			this.keysToCleanup.Add(key);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00004867 File Offset: 0x00002A67
		public void RemoveKeyToCleanup(TKey key)
		{
			this.keysToCleanup.Remove(key);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004876 File Offset: 0x00002A76
		public bool ContainsKeyToCleanup(TKey key)
		{
			return this.keysToCleanup.Contains(key);
		}

		// Token: 0x040002CE RID: 718
		private readonly int capacity;

		// Token: 0x040002CF RID: 719
		private HashSet<TKey> keysToCleanup;
	}
}
