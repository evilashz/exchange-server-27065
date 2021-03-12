using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200004B RID: 75
	internal class CountingDictionary<T>
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0001C898 File Offset: 0x0001AA98
		public int Increment(T key, int incrementBy = 1)
		{
			int result;
			lock (this.instanceLock)
			{
				int num;
				if (this.map.TryGetValue(key, out num))
				{
					num += incrementBy;
				}
				else
				{
					num = incrementBy;
				}
				this.map[key] = num;
				result = num;
			}
			return result;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public int GetCount(T key)
		{
			int result;
			lock (this.instanceLock)
			{
				int num;
				if (!this.map.TryGetValue(key, out num))
				{
					result = 0;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001C950 File Offset: 0x0001AB50
		public void Clear()
		{
			lock (this.instanceLock)
			{
				this.map.Clear();
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001C998 File Offset: 0x0001AB98
		public override string ToString()
		{
			string result;
			lock (this.instanceLock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<T, int> keyValuePair in this.map)
				{
					stringBuilder.AppendFormat("{0}:{1} ", keyValuePair.Key, keyValuePair.Value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0400038E RID: 910
		private Dictionary<T, int> map = new Dictionary<T, int>();

		// Token: 0x0400038F RID: 911
		private object instanceLock = new object();
	}
}
