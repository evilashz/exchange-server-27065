using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000193 RID: 403
	internal sealed class NotificationStatisticsManager
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x00036118 File Offset: 0x00034318
		static NotificationStatisticsManager()
		{
			bool incomingNotificationStatisticsEnabled;
			if (!bool.TryParse(ConfigurationManager.AppSettings["IncomingNotificationStatisticsEnabled"], out incomingNotificationStatisticsEnabled))
			{
				incomingNotificationStatisticsEnabled = true;
			}
			bool outgoingNotificationStatisticsEnabled;
			if (!bool.TryParse(ConfigurationManager.AppSettings["OutgoingNotificationStatisticsEnabled"], out outgoingNotificationStatisticsEnabled))
			{
				outgoingNotificationStatisticsEnabled = true;
			}
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["NotificationStatisticsLoggingIntervalSeconds"], out num))
			{
				num = 900;
			}
			NotificationStatisticsManager.IncomingNotificationStatisticsEnabled = incomingNotificationStatisticsEnabled;
			NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled = outgoingNotificationStatisticsEnabled;
			NotificationStatisticsManager.LoggingInterval = TimeSpan.FromSeconds((double)num);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000361E7 File Offset: 0x000343E7
		private NotificationStatisticsManager()
		{
			this.incomingNotifications = new NotificationStatistics();
			this.outgoingNotifications = new NotificationStatistics();
			this.isLogThreadActive = 0;
			this.lastLoggedTime = DateTime.UtcNow;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00036224 File Offset: 0x00034424
		public static NotificationStatisticsManager Instance
		{
			get
			{
				if (NotificationStatisticsManager.instance == null)
				{
					lock (NotificationStatisticsManager.InstanceLock)
					{
						if (NotificationStatisticsManager.instance == null)
						{
							NotificationStatisticsManager.instance = new NotificationStatisticsManager();
						}
					}
				}
				return NotificationStatisticsManager.instance;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003627C File Offset: 0x0003447C
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00036284 File Offset: 0x00034484
		internal Action<ILogEvent> TestLogEventCreated { get; set; }

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003628D File Offset: 0x0003448D
		public void NotificationCreated(NotificationPayloadBase payload)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && NotificationStatisticsManager.IsStatisticable(payload))
			{
				this.incomingNotifications.Update(payload.Source, payload, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationCreated));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000362C2 File Offset: 0x000344C2
		public void NotificationCreated(Guid mailboxGuid, IEnumerable<NotificationPayloadBase> payloads)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && mailboxGuid != Guid.Empty)
			{
				this.incomingNotifications.Update(new MailboxLocation(mailboxGuid), NotificationStatisticsManager.GetStatisticablePayloads(payloads), new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationCreated));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00036301 File Offset: 0x00034501
		public void NotificationReceived(NotificationPayloadBase payload)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && NotificationStatisticsManager.IsStatisticable(payload))
			{
				this.incomingNotifications.Update(payload.Source, payload, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationReceived));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00036336 File Offset: 0x00034536
		public void NotificationReceived(string serverName, IEnumerable<NotificationPayloadBase> payloads)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && !string.IsNullOrEmpty(serverName))
			{
				this.incomingNotifications.Update(new ServerLocation(serverName), NotificationStatisticsManager.GetStatisticablePayloads(payloads), new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationReceived));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00036370 File Offset: 0x00034570
		public void NotificationQueued(NotificationPayloadBase payload)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && NotificationStatisticsManager.IsStatisticable(payload))
			{
				this.incomingNotifications.Update(payload.Source, payload, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationQueued));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000363A5 File Offset: 0x000345A5
		public void NotificationQueued(IEnumerable<NotificationPayloadBase> payloads)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
			{
				this.BulkUpdateIncomingNotifications(payloads, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationQueued));
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000363C8 File Offset: 0x000345C8
		public void NotificationDispatched(string channelId, NotificationPayloadBase payload)
		{
			if ((NotificationStatisticsManager.IncomingNotificationStatisticsEnabled || NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled) && !string.IsNullOrEmpty(channelId) && NotificationStatisticsManager.IsStatisticable(payload))
			{
				if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
				{
					this.incomingNotifications.Update(payload.Source, payload, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationDispatched));
				}
				if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
				{
					this.outgoingNotifications.Update(new ChannelLocation(channelId), payload, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationDispatched));
				}
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00036444 File Offset: 0x00034644
		public void NotificationDispatched(string channelId, IEnumerable<NotificationPayloadBase> payloads)
		{
			if ((NotificationStatisticsManager.IncomingNotificationStatisticsEnabled || NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled) && !string.IsNullOrEmpty(channelId))
			{
				if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
				{
					this.BulkUpdateIncomingNotifications(payloads, new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationDispatched));
				}
				if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
				{
					this.outgoingNotifications.Update(new ChannelLocation(channelId), NotificationStatisticsManager.GetStatisticablePayloads(payloads), new Action<NotificationStatisticsValue, NotificationPayloadBase>(NotificationStatisticsManager.UpdateNotificationDispatched));
				}
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000364D8 File Offset: 0x000346D8
		public void NotificationPushed(string destinationUrl, NotificationPayloadBase payload, DateTime pushTime)
		{
			if ((NotificationStatisticsManager.IncomingNotificationStatisticsEnabled || NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled) && !string.IsNullOrEmpty(destinationUrl) && NotificationStatisticsManager.IsStatisticable(payload))
			{
				if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
				{
					this.incomingNotifications.Update(payload.Source, payload, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
					{
						NotificationStatisticsManager.UpdateNotificationPushed(v, p, pushTime);
					});
				}
				if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
				{
					this.outgoingNotifications.Update(new ServerLocation(destinationUrl), payload, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
					{
						NotificationStatisticsManager.UpdateNotificationPushed(v, p, pushTime);
					});
				}
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00036594 File Offset: 0x00034794
		public void NotificationPushed(string destinationUrl, IEnumerable<NotificationPayloadBase> payloads, DateTime pushTime)
		{
			if ((NotificationStatisticsManager.IncomingNotificationStatisticsEnabled || NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled) && !string.IsNullOrEmpty(destinationUrl))
			{
				if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
				{
					this.BulkUpdateIncomingNotifications(payloads, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
					{
						NotificationStatisticsManager.UpdateNotificationPushed(v, p, pushTime);
					});
				}
				if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
				{
					this.outgoingNotifications.Update(new ServerLocation(destinationUrl), NotificationStatisticsManager.GetStatisticablePayloads(payloads), delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
					{
						NotificationStatisticsManager.UpdateNotificationPushed(v, p, pushTime);
					});
				}
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00036644 File Offset: 0x00034844
		public void NotificationDropped(NotificationPayloadBase payload, NotificationState state)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled && NotificationStatisticsManager.IsStatisticable(payload))
			{
				this.incomingNotifications.Update(payload.Source, payload, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
				{
					NotificationStatisticsManager.UpdateNotificationDropped(v, p, state);
				});
				this.TriggerLogCheck();
			}
			if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
			{
				RemoteNotificationPayload remoteNotificationPayload = payload as RemoteNotificationPayload;
				if (remoteNotificationPayload != null && remoteNotificationPayload.ChannelIds != null)
				{
					foreach (string channelId in remoteNotificationPayload.ChannelIds)
					{
						this.outgoingNotifications.Update(new ChannelLocation(channelId), payload, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
						{
							NotificationStatisticsManager.UpdateNotificationDropped(v, p, state);
						});
					}
				}
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00036714 File Offset: 0x00034914
		public void NotificationDropped(IEnumerable<NotificationPayloadBase> payloads, NotificationState state)
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
			{
				this.BulkUpdateIncomingNotifications(payloads, delegate(NotificationStatisticsValue v, NotificationPayloadBase p)
				{
					NotificationStatisticsManager.UpdateNotificationDropped(v, p, state);
				});
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003674F File Offset: 0x0003494F
		internal void LogNotificationStatisticsDataInternal()
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled)
			{
				this.WriteNotificationStatisticsData(NotificationStatisticsEventType.Incoming, this.incomingNotifications);
			}
			if (NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
			{
				this.WriteNotificationStatisticsData(NotificationStatisticsEventType.Outgoing, this.outgoingNotifications);
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003677C File Offset: 0x0003497C
		private void BulkUpdateIncomingNotifications(IEnumerable<NotificationPayloadBase> payloads, Action<NotificationStatisticsValue, NotificationPayloadBase> doUpdate)
		{
			if (payloads != null && doUpdate != null)
			{
				foreach (NotificationPayloadBase notificationPayloadBase in payloads)
				{
					if (NotificationStatisticsManager.IsStatisticable(notificationPayloadBase))
					{
						this.incomingNotifications.Update(notificationPayloadBase.Source, notificationPayloadBase, doUpdate);
					}
				}
				this.TriggerLogCheck();
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000367E4 File Offset: 0x000349E4
		private static void UpdateNotificationCreated(NotificationStatisticsValue value, NotificationPayloadBase payload)
		{
			payload.CreatedTime = new DateTime?(DateTime.UtcNow);
			value.Created++;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00036804 File Offset: 0x00034A04
		private static void UpdateNotificationReceived(NotificationStatisticsValue value, NotificationPayloadBase payload)
		{
			payload.ReceivedTime = new DateTime?(DateTime.UtcNow);
			value.Received++;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00036824 File Offset: 0x00034A24
		private static void UpdateNotificationQueued(NotificationStatisticsValue value, NotificationPayloadBase payload)
		{
			payload.QueuedTime = new DateTime?(DateTime.UtcNow);
			value.Queued++;
			if (payload.CreatedTime != null)
			{
				value.CreatedAndQueued.Add((payload.QueuedTime.Value - payload.CreatedTime.Value).TotalMilliseconds);
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00036894 File Offset: 0x00034A94
		private static void UpdateNotificationDispatched(NotificationStatisticsValue value, NotificationPayloadBase payload)
		{
			value.Dispatched++;
			DateTime utcNow = DateTime.UtcNow;
			if (payload.CreatedTime != null)
			{
				value.CreatedAndDispatched.Add((utcNow - payload.CreatedTime.Value).TotalMilliseconds);
				return;
			}
			if (payload.ReceivedTime != null)
			{
				value.ReceivedAndDispatched.Add((utcNow - payload.ReceivedTime.Value).TotalMilliseconds);
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00036928 File Offset: 0x00034B28
		private static void UpdateNotificationPushed(NotificationStatisticsValue value, NotificationPayloadBase payload, DateTime pushTime)
		{
			value.Pushed++;
			if (payload.CreatedTime != null)
			{
				value.CreatedAndPushed.Add((pushTime - payload.CreatedTime.Value).TotalMilliseconds);
			}
			if (payload.QueuedTime != null)
			{
				value.QueuedAndPushed.Add((pushTime - payload.QueuedTime.Value).TotalMilliseconds);
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000369B4 File Offset: 0x00034BB4
		private static void UpdateNotificationDropped(NotificationStatisticsValue value, NotificationPayloadBase payload, NotificationState state)
		{
			value.Dropped++;
			switch (state)
			{
			case NotificationState.CreatedOrReceived:
				if (payload.ReceivedTime != null)
				{
					value.ReceivedAndDropped++;
					return;
				}
				if (payload.CreatedTime != null)
				{
					value.CreatedAndDropped++;
					return;
				}
				break;
			case NotificationState.Queued:
				value.QueuedAndDropped++;
				return;
			case NotificationState.Dispatching:
				value.DispatchingAndDropped++;
				break;
			default:
				return;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00036A3F File Offset: 0x00034C3F
		private static bool IsStatisticable(NotificationPayloadBase payload)
		{
			return payload != null && payload.Source != null && NotificationStatisticsManager.StatisticablePayloadTypes.Contains(payload.GetType());
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00036A66 File Offset: 0x00034C66
		private static IEnumerable<NotificationPayloadBase> GetStatisticablePayloads(IEnumerable<NotificationPayloadBase> payloads)
		{
			if (payloads == null)
			{
				return null;
			}
			return from payload in payloads
			where NotificationStatisticsManager.IsStatisticable(payload)
			select payload;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00036A90 File Offset: 0x00034C90
		private void TriggerLogCheck()
		{
			if (this.TestLogEventCreated != null)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow - this.lastLoggedTime > NotificationStatisticsManager.LoggingInterval)
			{
				lock (this.lastLoggedTimeLock)
				{
					if (utcNow - this.lastLoggedTime <= NotificationStatisticsManager.LoggingInterval)
					{
						return;
					}
					this.lastLoggedTime = utcNow;
				}
				this.StartLogThread();
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00036B18 File Offset: 0x00034D18
		private void StartLogThread()
		{
			if (NotificationStatisticsManager.IncomingNotificationStatisticsEnabled || NotificationStatisticsManager.OutgoingNotificationStatisticsEnabled)
			{
				if (this.isLogThreadActive == 0)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.LogNotificationStatisticsData));
					return;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Notification statistics log thread is already active. Not starting one.");
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00036B64 File Offset: 0x00034D64
		private void WriteNotificationStatisticsData(NotificationStatisticsEventType eventType, NotificationStatistics statistics)
		{
			IDictionary<NotificationStatisticsKey, NotificationStatisticsValue> dictionary;
			DateTime startTime;
			statistics.GetAndResetStatisticData(out dictionary, out startTime);
			foreach (KeyValuePair<NotificationStatisticsKey, NotificationStatisticsValue> keyValuePair in dictionary)
			{
				NotificationStatisticsLogEvent notificationStatisticsLogEvent = new NotificationStatisticsLogEvent(eventType, startTime, keyValuePair.Key, keyValuePair.Value);
				if (this.TestLogEventCreated != null)
				{
					this.TestLogEventCreated(notificationStatisticsLogEvent);
				}
				else
				{
					OwaServerLogger.AppendToLog(notificationStatisticsLogEvent);
				}
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00036BE8 File Offset: 0x00034DE8
		private void LogNotificationStatisticsData(object state)
		{
			int num = 0;
			try
			{
				num = Interlocked.CompareExchange(ref this.isLogThreadActive, 1, 0);
				if (num == 0)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "New notification statistics log thread started.");
					this.LogNotificationStatisticsDataInternal();
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Notification statistics log thread quit. Exception: {0}", arg);
			}
			finally
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Notification statistics log thread completed.");
				if (num == 0)
				{
					Interlocked.CompareExchange(ref this.isLogThreadActive, 0, 1);
				}
			}
		}

		// Token: 0x040008A7 RID: 2215
		private const string IncomingNotificationStatisticsEnabledAppSettingKey = "IncomingNotificationStatisticsEnabled";

		// Token: 0x040008A8 RID: 2216
		private const string OutgoingNotificationStatisticsEnabledAppSettingKey = "OutgoingNotificationStatisticsEnabled";

		// Token: 0x040008A9 RID: 2217
		private const string NotificationStatisticsLoggingIntervalSecondsAppSettingKey = "NotificationStatisticsLoggingIntervalSeconds";

		// Token: 0x040008AA RID: 2218
		private const int DefaultNotificationStatisticsLoggingIntervalSeconds = 900;

		// Token: 0x040008AB RID: 2219
		private static readonly Type[] StatisticablePayloadTypes = new Type[]
		{
			typeof(GroupAssociationNotificationPayload),
			typeof(ReloadAllNotificationPayload),
			typeof(RemoteNotificationPayload),
			typeof(RowNotificationPayload),
			typeof(UnseenItemNotificationPayload)
		};

		// Token: 0x040008AC RID: 2220
		private static readonly object InstanceLock = new object();

		// Token: 0x040008AD RID: 2221
		private static NotificationStatisticsManager instance;

		// Token: 0x040008AE RID: 2222
		private static readonly bool IncomingNotificationStatisticsEnabled;

		// Token: 0x040008AF RID: 2223
		private static readonly bool OutgoingNotificationStatisticsEnabled;

		// Token: 0x040008B0 RID: 2224
		private static readonly TimeSpan LoggingInterval;

		// Token: 0x040008B1 RID: 2225
		private readonly object lastLoggedTimeLock = new object();

		// Token: 0x040008B2 RID: 2226
		private DateTime lastLoggedTime;

		// Token: 0x040008B3 RID: 2227
		private int isLogThreadActive;

		// Token: 0x040008B4 RID: 2228
		private readonly NotificationStatistics incomingNotifications;

		// Token: 0x040008B5 RID: 2229
		private readonly NotificationStatistics outgoingNotifications;
	}
}
