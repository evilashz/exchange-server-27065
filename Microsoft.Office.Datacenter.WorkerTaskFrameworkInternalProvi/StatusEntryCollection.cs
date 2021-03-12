using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000035 RID: 53
	public class StatusEntryCollection : IEnumerable<StatusEntry>, IEnumerable
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000BA84 File Offset: 0x00009C84
		public StatusEntryCollection(string collectionKey)
		{
			this.collectionKey = collectionKey;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		internal StatusEntry[] ItemsToRemove
		{
			get
			{
				return this.itemsToRemove.ToArray();
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BC24 File Offset: 0x00009E24
		public IEnumerator<StatusEntry> GetEnumerator()
		{
			foreach (StatusEntry item in (from i in this.items
			where !i.Remove
			select i).ToArray<StatusEntry>())
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BC40 File Offset: 0x00009E40
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BC48 File Offset: 0x00009E48
		public StatusEntry CreateStatusEntry()
		{
			StatusEntry statusEntry = new StatusEntry(this.collectionKey);
			this.items.Add(statusEntry);
			return statusEntry;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BC70 File Offset: 0x00009E70
		public void Remove(StatusEntry entry)
		{
			lock (this.removalLock)
			{
				entry.Remove = true;
				this.itemsToRemove.Add(entry);
				this.items.Remove(entry);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BCCC File Offset: 0x00009ECC
		internal void Add(StatusEntry entry)
		{
			if (entry.Key != this.collectionKey)
			{
				throw new ArgumentException(string.Format("The entry's key '{0}' does not match the key assigned to this collection: '{1}'", entry.Key, this.collectionKey));
			}
			this.items.Add(entry);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BD0C File Offset: 0x00009F0C
		internal void PurgeInvalidEntries()
		{
			lock (this.removalLock)
			{
				int i = 0;
				while (i < this.itemsToRemove.Count)
				{
					StatusEntry statusEntry = this.itemsToRemove[i];
					if (!statusEntry.EntryExistsInDatabase())
					{
						this.itemsToRemove.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
				int j = 0;
				while (j < this.items.Count)
				{
					StatusEntry statusEntry2 = this.items[j];
					if (DateTime.UtcNow.Subtract(statusEntry2.CreatedTime) > StatusEntryCollection.RetentionTime)
					{
						this.itemsToRemove.Add(this.items[j]);
						this.items.RemoveAt(j);
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x04000151 RID: 337
		private static readonly TimeSpan RetentionTime = TimeSpan.FromHours((double)(4 * Settings.ProbeResultHistoryWindowSize));

		// Token: 0x04000152 RID: 338
		private readonly string collectionKey;

		// Token: 0x04000153 RID: 339
		private List<StatusEntry> items = new List<StatusEntry>();

		// Token: 0x04000154 RID: 340
		private List<StatusEntry> itemsToRemove = new List<StatusEntry>();

		// Token: 0x04000155 RID: 341
		private object removalLock = new object();
	}
}
