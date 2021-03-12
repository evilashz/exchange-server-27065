using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200018C RID: 396
	internal class NotificationStatistics
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x000359F6 File Offset: 0x00033BF6
		internal NotificationStatistics()
		{
			this.data = new ConcurrentDictionary<NotificationStatisticsKey, NotificationStatisticsValue>();
			this.startTime = DateTime.UtcNow;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00035A14 File Offset: 0x00033C14
		internal void Update(NotificationLocation location, NotificationPayloadBase payload, Action<NotificationStatisticsValue, NotificationPayloadBase> doUpdate)
		{
			if (location != null && payload != null && doUpdate != null)
			{
				this.UpdateInternal(location, payload, doUpdate);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00035A28 File Offset: 0x00033C28
		internal void Update(NotificationLocation location, IEnumerable<NotificationPayloadBase> payloads, Action<NotificationStatisticsValue, NotificationPayloadBase> doUpdate)
		{
			if (location != null && payloads != null && doUpdate != null)
			{
				foreach (NotificationPayloadBase notificationPayloadBase in payloads)
				{
					if (notificationPayloadBase != null)
					{
						this.UpdateInternal(location, notificationPayloadBase, doUpdate);
					}
				}
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00035A80 File Offset: 0x00033C80
		internal void GetAndResetStatisticData(out IDictionary<NotificationStatisticsKey, NotificationStatisticsValue> outputData, out DateTime outputStartTime)
		{
			outputData = new Dictionary<NotificationStatisticsKey, NotificationStatisticsValue>(this.data);
			outputStartTime = this.startTime;
			this.data.Clear();
			this.startTime = DateTime.UtcNow;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00035AB4 File Offset: 0x00033CB4
		private void UpdateInternal(NotificationLocation location, NotificationPayloadBase payload, Action<NotificationStatisticsValue, NotificationPayloadBase> doUpdate)
		{
			NotificationStatisticsKey key = new NotificationStatisticsKey(location, payload.GetType(), payload.EventType == QueryNotificationType.Reload);
			NotificationStatisticsValue orAdd = this.data.GetOrAdd(key, new NotificationStatisticsValue());
			doUpdate(orAdd, payload);
		}

		// Token: 0x04000881 RID: 2177
		private readonly ConcurrentDictionary<NotificationStatisticsKey, NotificationStatisticsValue> data;

		// Token: 0x04000882 RID: 2178
		private DateTime startTime;
	}
}
