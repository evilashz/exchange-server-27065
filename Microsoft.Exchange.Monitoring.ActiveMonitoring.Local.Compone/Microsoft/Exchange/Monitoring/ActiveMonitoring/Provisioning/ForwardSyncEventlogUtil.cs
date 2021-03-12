using System;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x02000403 RID: 1027
	public class ForwardSyncEventlogUtil
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x0008DDF8 File Offset: 0x0008BFF8
		public static FowardSyncEventRecord GetArbitrationEventLog()
		{
			string query = string.Format("<QueryList>  <Query Id=\"0\" Path=\"{0}\">    <Select Path=\"{0}\">        *[System[Provider[@Name='{1}'] and        (EventID={2}) and        TimeCreated[timediff(@SystemTime) &lt;= {3}]]]    </Select>  </Query></QueryList>", new object[]
			{
				"ForwardSync",
				"MSExchangeForwardSync",
				5015,
				2400000
			});
			FowardSyncEventRecord fowardSyncEventRecord = null;
			using (EventLogReader eventLogReader = new EventLogReader(new EventLogQuery("ForwardSync", PathType.LogName, query)
			{
				ReverseDirection = true
			}))
			{
				using (EventRecord eventRecord = eventLogReader.ReadEvent())
				{
					if (eventRecord != null)
					{
						fowardSyncEventRecord = new FowardSyncEventRecord();
						fowardSyncEventRecord.ServiceInstanceName = eventRecord.Properties[0].Value.ToString();
						fowardSyncEventRecord.Status = "Active";
						fowardSyncEventRecord.TimeCreated = eventRecord.TimeCreated;
					}
				}
			}
			return fowardSyncEventRecord;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0008DEE8 File Offset: 0x0008C0E8
		public static string GetServiceInstancename()
		{
			string result = string.Empty;
			FowardSyncEventRecord arbitrationEventLog = ForwardSyncEventlogUtil.GetArbitrationEventLog();
			if (arbitrationEventLog != null)
			{
				result = arbitrationEventLog.ServiceInstanceName;
			}
			return result;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0008DF0C File Offset: 0x0008C10C
		public static bool IsForwardSyncActiveServer()
		{
			return !string.IsNullOrEmpty(ForwardSyncEventlogUtil.GetServiceInstancename());
		}

		// Token: 0x040011BE RID: 4542
		private const string LogName = "ForwardSync";

		// Token: 0x040011BF RID: 4543
		private const string EventSource = "MSExchangeForwardSync";

		// Token: 0x040011C0 RID: 4544
		private const int EventId = 5015;

		// Token: 0x040011C1 RID: 4545
		private const int EventIntervalMilliSeconds = 2400000;

		// Token: 0x040011C2 RID: 4546
		private const string QueryStringFormat = "<QueryList>  <Query Id=\"0\" Path=\"{0}\">    <Select Path=\"{0}\">        *[System[Provider[@Name='{1}'] and        (EventID={2}) and        TimeCreated[timediff(@SystemTime) &lt;= {3}]]]    </Select>  </Query></QueryList>";
	}
}
