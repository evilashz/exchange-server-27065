using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002BE RID: 702
	internal class PiiMap
	{
		// Token: 0x0600193F RID: 6463 RVA: 0x0004F794 File Offset: 0x0004D994
		public PiiMap()
		{
			for (int i = 0; i < this.maps.Length; i++)
			{
				this.maps[i] = new ConcurrentDictionary<string, string>(2, 10000, StringComparer.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000784 RID: 1924
		public string this[string key]
		{
			get
			{
				string result = null;
				foreach (ConcurrentDictionary<string, string> concurrentDictionary in this.maps)
				{
					if (concurrentDictionary.TryGetValue(key, out result))
					{
						break;
					}
				}
				return result;
			}
			set
			{
				this.CleanUpOldDataIfNeeded();
				if (!string.IsNullOrEmpty(key))
				{
					this.maps[this.currentMap][key] = value;
				}
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0004F838 File Offset: 0x0004DA38
		private void CleanUpOldDataIfNeeded()
		{
			int num = (this.currentMap == 0) ? 1 : 0;
			if (this.maps[this.currentMap].Count >= 10000)
			{
				if (this.maps[num].Count >= 10000)
				{
					this.maps[num].Clear();
				}
				this.currentMap = num;
			}
		}

		// Token: 0x04000F0C RID: 3852
		private const int Capacity = 10000;

		// Token: 0x04000F0D RID: 3853
		private ConcurrentDictionary<string, string>[] maps = new ConcurrentDictionary<string, string>[2];

		// Token: 0x04000F0E RID: 3854
		private int currentMap;
	}
}
