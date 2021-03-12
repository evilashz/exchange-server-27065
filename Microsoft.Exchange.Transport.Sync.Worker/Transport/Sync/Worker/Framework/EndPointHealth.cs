using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EndPointHealth
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0001027C File Offset: 0x0000E47C
		internal static void UpdateDeltaSyncEndPointStatus(string host, bool endPointReachable, SyncLogSession syncLogSession)
		{
			EndPointHealth.UpdateEndPointStatus(host, endPointReachable, syncLogSession, EndPointHealth.deltaSyncEndPointName, TransportSyncWorkerEventLogConstants.Tuple_DeltaSyncEndpointUnreachable, FrameworkAggregationConfiguration.Instance.DeltaSyncEndPointUnreachableThreshold, EndPointHealth.deltaSyncEndPointHealthData);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000102A0 File Offset: 0x0000E4A0
		private static void UpdateEndPointStatus(string host, bool endPointReachable, SyncLogSession syncLogSession, string endPointType, ExEventLog.EventTuple eventTuple, TimeSpan endPointUnreachableThreshold, Dictionary<string, EndPointHealth.EndPointHealthData> endPointHealthDataDict)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("host", host);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("endPointType", endPointType);
			SyncUtilities.ThrowIfArgumentNull("endPointHealthDataDict", endPointHealthDataDict);
			if (!endPointReachable)
			{
				ExDateTime now = ExDateTime.Now;
				EndPointHealth.EndPointHealthData endPointHealthData = null;
				lock (EndPointHealth.healthDataLock)
				{
					if (endPointHealthDataDict.TryGetValue(host, out endPointHealthData))
					{
						syncLogSession.LogDebugging((TSLID)568UL, "{0} endpoint {1} was unreachable before, loading previous health entry.", new object[]
						{
							endPointType,
							host
						});
					}
					else
					{
						syncLogSession.LogDebugging((TSLID)569UL, "{0} endpoint {1} is unreachable unreachable for the first time, creating health entry.", new object[]
						{
							endPointType,
							host
						});
						endPointHealthDataDict[host] = new EndPointHealth.EndPointHealthData(now, now);
					}
				}
				if (endPointHealthData != null)
				{
					ExDateTime lastUpdated = endPointHealthData.LastUpdated;
					endPointHealthData.LastUpdated = now;
					TimeSpan t = now - lastUpdated;
					if (t >= endPointUnreachableThreshold)
					{
						syncLogSession.LogVerbose((TSLID)570UL, "A connection to the {0} endpoint {1} hasn't been made for {2} minutes, ignoring previous unreachable case.", new object[]
						{
							endPointType,
							host,
							t.TotalMinutes
						});
						endPointHealthData.TimeEndPointNotReachable = now;
						return;
					}
					TimeSpan t2 = now - endPointHealthData.TimeEndPointNotReachable;
					if (t2 >= endPointUnreachableThreshold)
					{
						syncLogSession.LogError((TSLID)571UL, "A {0} endpoint ( {1} ) on hub server {2} has been unreachable for {3} minutes, resulting in no syncing on the hub. This may be due to a communication error or endpoint downtime.", new object[]
						{
							endPointType,
							host,
							Environment.MachineName,
							t2.TotalMinutes
						});
						FrameworkAggregationConfiguration.EventLogger.LogEvent(eventTuple, string.Empty, new object[]
						{
							host,
							Environment.MachineName,
							t2.TotalMinutes
						});
						EndPointHealth.PublishEventNotificationItemEndPointUnreachable(endPointType);
						return;
					}
				}
			}
			else
			{
				lock (EndPointHealth.healthDataLock)
				{
					bool flag3 = endPointHealthDataDict.Remove(host);
					if (flag3)
					{
						syncLogSession.LogVerbose((TSLID)572UL, "The {0} endpoint {1} on hub became reachable again. Removing health entry.", new object[]
						{
							endPointType,
							host
						});
					}
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000104F8 File Offset: 0x0000E6F8
		private static void PublishEventNotificationItemEndPointUnreachable(string endPointType)
		{
			if (endPointType.Equals(EndPointHealth.deltaSyncEndPointName))
			{
				EventNotificationHelper.PublishTransportEventNotificationItem(TransportSyncNotificationEvent.DeltaSyncEndpointUnreachable.ToString());
			}
		}

		// Token: 0x040001E5 RID: 485
		private const int DeltaSyncEndPointHealthDataCapacity = 10;

		// Token: 0x040001E6 RID: 486
		private static readonly string deltaSyncEndPointName = "DeltaSync";

		// Token: 0x040001E7 RID: 487
		private static readonly object healthDataLock = new object();

		// Token: 0x040001E8 RID: 488
		private static Dictionary<string, EndPointHealth.EndPointHealthData> deltaSyncEndPointHealthData = new Dictionary<string, EndPointHealth.EndPointHealthData>(10, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200004C RID: 76
		private sealed class EndPointHealthData
		{
			// Token: 0x0600036D RID: 877 RVA: 0x00010546 File Offset: 0x0000E746
			public EndPointHealthData(ExDateTime timeEndPointNotReachable, ExDateTime lastUpdated)
			{
				SyncUtilities.ThrowIfArgumentNull("timeEndPointNotReachable", timeEndPointNotReachable);
				SyncUtilities.ThrowIfArgumentNull("lastUpdated", lastUpdated);
				this.timeEndPointNotReachable = timeEndPointNotReachable;
				this.lastUpdated = lastUpdated;
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600036E RID: 878 RVA: 0x0001057C File Offset: 0x0000E77C
			// (set) Token: 0x0600036F RID: 879 RVA: 0x00010584 File Offset: 0x0000E784
			public ExDateTime TimeEndPointNotReachable
			{
				get
				{
					return this.timeEndPointNotReachable;
				}
				set
				{
					this.timeEndPointNotReachable = value;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0001058D File Offset: 0x0000E78D
			// (set) Token: 0x06000371 RID: 881 RVA: 0x00010595 File Offset: 0x0000E795
			public ExDateTime LastUpdated
			{
				get
				{
					return this.lastUpdated;
				}
				set
				{
					this.lastUpdated = value;
				}
			}

			// Token: 0x040001E9 RID: 489
			private ExDateTime timeEndPointNotReachable;

			// Token: 0x040001EA RID: 490
			private ExDateTime lastUpdated;
		}
	}
}
