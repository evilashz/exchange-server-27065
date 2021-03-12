using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200008C RID: 140
	internal class CacheEntry
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x00025BD6 File Offset: 0x00023DD6
		public CacheEntry(List<ObjectTuple> simpleADObjectList)
		{
			this.SimpleADObjectList = simpleADObjectList;
			this.keys = new List<string>[CacheEntry.KeyLength];
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00025BF5 File Offset: 0x00023DF5
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00025BFD File Offset: 0x00023DFD
		public List<ObjectTuple> SimpleADObjectList { get; internal set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00025C06 File Offset: 0x00023E06
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00025C0E File Offset: 0x00023E0E
		public bool Invalid { get; internal set; }

		// Token: 0x17000170 RID: 368
		public List<string> this[KeyType keyType]
		{
			get
			{
				if (this.keys[this.GetKeyIndex(keyType)] == null)
				{
					this.keys[this.GetKeyIndex(keyType)] = new List<string>(5);
				}
				return this.keys[this.GetKeyIndex(keyType)];
			}
			set
			{
				this.keys[this.GetKeyIndex(keyType)] = value;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00025C5C File Offset: 0x00023E5C
		public void ClearKeys()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (this.keys[i] != null)
				{
					List<string> list = null;
					list = Interlocked.Exchange<List<string>>(ref this.keys[i], list);
					if (list != null)
					{
						foreach (string key in list)
						{
							CacheManager.Instance.KeyTable.Remove(key, null);
						}
						list.Clear();
					}
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00025CF0 File Offset: 0x00023EF0
		public void ClearKey(KeyType keyType)
		{
			int keyIndex = this.GetKeyIndex(keyType);
			if (this.keys[keyIndex] != null)
			{
				List<string> list = null;
				list = Interlocked.Exchange<List<string>>(ref this.keys[keyIndex], list);
				if (list != null)
				{
					foreach (string key in list)
					{
						CacheManager.Instance.KeyTable.Remove(key, null);
					}
					list.Clear();
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00025D7C File Offset: 0x00023F7C
		private int GetKeyIndex(KeyType keyType)
		{
			if (keyType == KeyType.None)
			{
				return 0;
			}
			int num = 1;
			for (int i = 0; i < 32; i++)
			{
				if ((keyType & (KeyType)num) != KeyType.None)
				{
					return i + 1;
				}
				num <<= 1;
			}
			return 0;
		}

		// Token: 0x040002AA RID: 682
		private List<string>[] keys;

		// Token: 0x040002AB RID: 683
		private static int KeyLength = Enum.GetValues(typeof(KeyType)).Length;
	}
}
