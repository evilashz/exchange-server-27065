using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.SharedCache.Exceptions;
using Microsoft.Isam.Esent.Collections.Generic;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200000A RID: 10
	internal sealed class PersistentDatabase : SharedCacheBase
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public PersistentDatabase(CacheSettings cacheSettings) : base(cacheSettings)
		{
			this.internalDictionary = new PersistentDictionary<string, PersistentBlob>(cacheSettings.DatabaseDirectory);
			base.PerformanceCounters.UpdateCacheSize(this.Count);
			this.UpdateDirectorySizeCounterIfRequired(true);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002D07 File Offset: 0x00000F07
		public long DirectorySize
		{
			get
			{
				return Directory.GetFiles(base.Settings.DatabaseDirectory).Sum((string file) => new FileInfo(file).Length);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002D3B File Offset: 0x00000F3B
		public override long Count
		{
			get
			{
				return (long)this.internalDictionary.Count;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D49 File Offset: 0x00000F49
		public override void Dispose()
		{
			base.Dispose();
			if (this.internalDictionary != null)
			{
				this.internalDictionary.Dispose();
				this.internalDictionary = null;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D74 File Offset: 0x00000F74
		internal override KeyValuePair<string, CacheEntry>[] InternalSearch(string keyWildcard)
		{
			string[] array = (from k in this.internalDictionary.Keys
			where k.IndexOf(keyWildcard, StringComparison.OrdinalIgnoreCase) >= 0
			select k).ToArray<string>();
			KeyValuePair<string, CacheEntry>[] array2 = new KeyValuePair<string, CacheEntry>[array.Length];
			int num = 0;
			foreach (string text in array)
			{
				CacheEntry value = null;
				try
				{
					value = new CacheEntry(this.internalDictionary[text].GetBytes());
				}
				catch (CorruptCacheEntryException ex)
				{
					base.PerformanceCounters.IncrementCorruptEntries();
					ex.Key = text;
					this.internalDictionary.Remove(text);
					throw;
				}
				array2[num++] = new KeyValuePair<string, CacheEntry>(text, value);
			}
			return array2;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002ED0 File Offset: 0x000010D0
		internal override CacheEntry InternalGet(string key)
		{
			this.UpdateDirectorySizeCounterIfRequired(false);
			PersistentBlob persistentBlob;
			if (this.internalDictionary.TryGetValue(key, ref persistentBlob))
			{
				return new CacheEntry(persistentBlob.GetBytes());
			}
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F01 File Offset: 0x00001101
		internal override void InternalInsert(string key, CacheEntry cacheEntryToInsert)
		{
			this.InternalDelete(key);
			this.internalDictionary.Add(key, new PersistentBlob(cacheEntryToInsert.ToByteArray()));
			this.UpdateDirectorySizeCounterIfRequired(false);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F2C File Offset: 0x0000112C
		internal override CacheEntry InternalDelete(string key)
		{
			CacheEntry result = null;
			try
			{
				result = this.InternalGet(key);
			}
			catch (CorruptCacheEntryException)
			{
				base.PerformanceCounters.IncrementCorruptEntries();
			}
			if (this.internalDictionary.ContainsKey(key))
			{
				this.internalDictionary.Remove(key);
			}
			this.UpdateDirectorySizeCounterIfRequired(false);
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F88 File Offset: 0x00001188
		private void UpdateDirectorySizeCounterIfRequired(bool force = false)
		{
			if (force || DateTime.UtcNow > this.nextDirectorySizeUpdate)
			{
				base.PerformanceCounters.UpdateDiskSpace(this.DirectorySize);
				this.nextDirectorySizeUpdate = DateTime.UtcNow + PersistentDatabase.directorySizeUpdateInterval;
			}
		}

		// Token: 0x0400001D RID: 29
		private static readonly TimeSpan directorySizeUpdateInterval = TimeSpan.FromSeconds(60.0);

		// Token: 0x0400001E RID: 30
		private PersistentDictionary<string, PersistentBlob> internalDictionary;

		// Token: 0x0400001F RID: 31
		private DateTime nextDirectorySizeUpdate;
	}
}
