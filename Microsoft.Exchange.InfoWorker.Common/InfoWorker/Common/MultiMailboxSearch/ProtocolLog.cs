using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200020C RID: 524
	internal class ProtocolLog : Dictionary<string, object>, IProtocolLog, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x0003DA44 File Offset: 0x0003BC44
		public new void Add(string key, object value)
		{
			if (!Array.Exists<string>(ProtocolLog.ValidLogNames, (string validLog) => string.Equals(validLog, key)))
			{
				throw new ArgumentException(string.Format("Key name '{0}' for protocol log is invalid", key));
			}
			this.MergeProtocolLogKey(key, value);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003DA9C File Offset: 0x0003BC9C
		public void Merge(IProtocolLog other)
		{
			if (other == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> keyValuePair in other)
			{
				this.MergeProtocolLogKey(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
		private void MergeProtocolLogKey(string key, object otherValue)
		{
			object obj = null;
			base.TryGetValue(key, out obj);
			object obj2;
			if (string.Equals(key, "NumberOfLocalSearch"))
			{
				int val = (obj == null) ? 0 : ((int)obj);
				int val2 = (otherValue == null) ? 0 : ((int)otherValue);
				obj2 = Math.Max(val, val2);
			}
			else if (string.Equals(key, "NumberOfRemoteSearch"))
			{
				int val3 = (obj == null) ? 0 : ((int)obj);
				int val4 = (otherValue == null) ? 0 : ((int)otherValue);
				obj2 = Math.Max(val3, val4);
			}
			else if (string.Equals(key, "LongestLocalSearchTaskTime"))
			{
				long val5 = (obj == null) ? 0L : ((long)obj);
				long val6 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val5, val6);
			}
			else if (string.Equals(key, "LongestLocalSearchTaskFastTime"))
			{
				long val7 = (obj == null) ? 0L : ((long)obj);
				long val8 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val7, val8);
			}
			else if (string.Equals(key, "LongestLocalSearchTaskStoreTime"))
			{
				long val9 = (obj == null) ? 0L : ((long)obj);
				long val10 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val9, val10);
			}
			else if (string.Equals(key, "LongestMailboxSearchTime"))
			{
				long val11 = (obj == null) ? 0L : ((long)obj);
				long val12 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val11, val12);
			}
			else if (string.Equals(key, "LongestMailboxFastSearchTime"))
			{
				long val13 = (obj == null) ? 0L : ((long)obj);
				long val14 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val13, val14);
			}
			else if (string.Equals(key, "LongestMailboxStoreTime"))
			{
				long val15 = (obj == null) ? 0L : ((long)obj);
				long val16 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val15, val16);
			}
			else if (string.Equals(key, "LongestFanoutTime"))
			{
				long val17 = (obj == null) ? 0L : ((long)obj);
				long val18 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val17, val18);
			}
			else if (string.Equals(key, "LocalMailboxMappingTime"))
			{
				long val19 = (obj == null) ? 0L : ((long)obj);
				long val20 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val19, val20);
			}
			else if (string.Equals(key, "AutodiscoverTime"))
			{
				long val21 = (obj == null) ? 0L : ((long)obj);
				long val22 = (otherValue == null) ? 0L : ((long)otherValue);
				obj2 = Math.Max(val21, val22);
			}
			else
			{
				obj2 = null;
			}
			if (obj2 == null)
			{
				base.Remove(key);
				return;
			}
			base[key] = obj2;
		}

		// Token: 0x040009B2 RID: 2482
		internal const string NumberOfLocalSearch = "NumberOfLocalSearch";

		// Token: 0x040009B3 RID: 2483
		internal const string NumberOfRemoteSearch = "NumberOfRemoteSearch";

		// Token: 0x040009B4 RID: 2484
		internal const string LongestMailboxSearchTime = "LongestMailboxSearchTime";

		// Token: 0x040009B5 RID: 2485
		internal const string LongestMailboxFastSearchTime = "LongestMailboxFastSearchTime";

		// Token: 0x040009B6 RID: 2486
		internal const string LongestMailboxStoreTime = "LongestMailboxStoreTime";

		// Token: 0x040009B7 RID: 2487
		internal const string LongestLocalSearchTaskTime = "LongestLocalSearchTaskTime";

		// Token: 0x040009B8 RID: 2488
		internal const string LongestLocalSearchTaskFastTime = "LongestLocalSearchTaskFastTime";

		// Token: 0x040009B9 RID: 2489
		internal const string LongestLocalSearchTaskStoreTime = "LongestLocalSearchTaskStoreTime";

		// Token: 0x040009BA RID: 2490
		internal const string LongestFanoutTime = "LongestFanoutTime";

		// Token: 0x040009BB RID: 2491
		internal const string LocalMailboxMappingTime = "LocalMailboxMappingTime";

		// Token: 0x040009BC RID: 2492
		internal const string AutodiscoverTime = "AutodiscoverTime";

		// Token: 0x040009BD RID: 2493
		private static readonly string[] ValidLogNames = new string[]
		{
			"NumberOfLocalSearch",
			"NumberOfRemoteSearch",
			"LongestMailboxSearchTime",
			"LongestMailboxFastSearchTime",
			"LongestMailboxStoreTime",
			"LongestLocalSearchTaskTime",
			"LongestLocalSearchTaskFastTime",
			"LongestLocalSearchTaskStoreTime",
			"LongestFanoutTime",
			"LocalMailboxMappingTime",
			"AutodiscoverTime"
		};
	}
}
