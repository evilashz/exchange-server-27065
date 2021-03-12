using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000105 RID: 261
	internal class KillBitList
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002D026 File Offset: 0x0002B226
		public static KillBitList Singleton
		{
			get
			{
				return KillBitList.killBitList;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002D030 File Offset: 0x0002B230
		public void Clear()
		{
			lock (this.lockObject)
			{
				this.list.Clear();
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002D078 File Offset: 0x0002B278
		public void Add(KilledExtensionEntry entry)
		{
			lock (this.lockObject)
			{
				this.list.Add(entry);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002D0C0 File Offset: 0x0002B2C0
		public void Remove(string extensionId)
		{
			lock (this.lockObject)
			{
				List<KilledExtensionEntry> list = new List<KilledExtensionEntry>();
				foreach (KilledExtensionEntry killedExtensionEntry in this.list)
				{
					if (string.Equals(killedExtensionEntry.ExtensionId, ExtensionDataHelper.FormatExtensionId(extensionId), StringComparison.OrdinalIgnoreCase))
					{
						list.Add(killedExtensionEntry);
					}
				}
				foreach (KilledExtensionEntry item in list)
				{
					this.list.Remove(item);
				}
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
		public KilledExtensionEntry[] GetList()
		{
			KilledExtensionEntry[] result;
			lock (this.lockObject)
			{
				result = this.list.ToArray();
			}
			return result;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		public bool IsExtensionKilled(string extensionId)
		{
			lock (this.lockObject)
			{
				foreach (KilledExtensionEntry killedExtensionEntry in this.list)
				{
					if (string.Equals(killedExtensionEntry.ExtensionId, ExtensionDataHelper.FormatExtensionId(extensionId), StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002D27C File Offset: 0x0002B47C
		public int Count
		{
			get
			{
				int count;
				lock (this.lockObject)
				{
					count = this.list.Count;
				}
				return count;
			}
		}

		// Token: 0x0400059D RID: 1437
		private static KillBitList killBitList = new KillBitList();

		// Token: 0x0400059E RID: 1438
		private List<KilledExtensionEntry> list = new List<KilledExtensionEntry>();

		// Token: 0x0400059F RID: 1439
		private object lockObject = new object();
	}
}
