using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HealthMonitoringEventsStrings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002210 File Offset: 0x00000410
		public static Dictionary<HealthMonitoringEvents, string> StringMap
		{
			get
			{
				if (HealthMonitoringEventsStrings.stringMap == null)
				{
					lock (HealthMonitoringEventsStrings.initLock)
					{
						if (HealthMonitoringEventsStrings.stringMap == null)
						{
							Type typeFromHandle = typeof(HealthMonitoringEvents);
							Array values = Enum.GetValues(typeFromHandle);
							HealthMonitoringEventsStrings.stringMap = new Dictionary<HealthMonitoringEvents, string>(values.Length);
							for (int i = 0; i < values.Length; i++)
							{
								HealthMonitoringEvents healthMonitoringEvents = (HealthMonitoringEvents)values.GetValue(i);
								string name = Enum.GetName(typeFromHandle, healthMonitoringEvents);
								HealthMonitoringEventsStrings.stringMap.Add(healthMonitoringEvents, name);
							}
						}
					}
				}
				return HealthMonitoringEventsStrings.stringMap;
			}
		}

		// Token: 0x04000012 RID: 18
		private static object initLock = new object();

		// Token: 0x04000013 RID: 19
		private static Dictionary<HealthMonitoringEvents, string> stringMap;
	}
}
