using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x02000180 RID: 384
	internal class SafetyNetInfoHashTable : Dictionary<SafetyNetRequestKey, SafetyNetInfo>
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x000425ED File Offset: 0x000407ED
		public SafetyNetInfoHashTable() : base(5, SafetyNetRequestKeyComparer.Instance)
		{
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000425FC File Offset: 0x000407FC
		public bool RedeliveryRequired
		{
			get
			{
				foreach (SafetyNetInfo safetyNetInfo in base.Values)
				{
					if (safetyNetInfo.RedeliveryRequired)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0004265C File Offset: 0x0004085C
		public string RedeliveryServers
		{
			get
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (SafetyNetInfo safetyNetInfo in base.Values)
				{
					if (safetyNetInfo.RedeliveryRequired)
					{
						List<string> list = safetyNetInfo.PrimaryHubServers;
						if (safetyNetInfo.ShadowHubServers.Count > 0)
						{
							list = safetyNetInfo.ShadowHubServers;
						}
						foreach (string item in list)
						{
							if (!hashSet.Contains(item))
							{
								hashSet.Add(item);
							}
						}
					}
				}
				string[] value = (from server in hashSet
				select server).ToArray<string>();
				return string.Join(",", value);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00042758 File Offset: 0x00040958
		public DateTime RedeliveryStartTime
		{
			get
			{
				DateTime dateTime = DateTime.MinValue;
				foreach (SafetyNetInfo safetyNetInfo in base.Values)
				{
					if (safetyNetInfo.RedeliveryRequired)
					{
						if (dateTime.Equals(DateTime.MinValue))
						{
							dateTime = safetyNetInfo.StartTimeUtc;
						}
						else if (dateTime > safetyNetInfo.StartTimeUtc)
						{
							dateTime = safetyNetInfo.StartTimeUtc;
						}
					}
				}
				return dateTime;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000427E0 File Offset: 0x000409E0
		public DateTime RedeliveryEndTime
		{
			get
			{
				DateTime dateTime = DateTime.MinValue;
				foreach (SafetyNetInfo safetyNetInfo in base.Values)
				{
					if (safetyNetInfo.RedeliveryRequired)
					{
						if (dateTime.Equals(DateTime.MinValue))
						{
							dateTime = safetyNetInfo.EndTimeUtc;
						}
						if (dateTime < safetyNetInfo.EndTimeUtc)
						{
							dateTime = safetyNetInfo.EndTimeUtc;
						}
					}
				}
				return dateTime;
			}
		}
	}
}
