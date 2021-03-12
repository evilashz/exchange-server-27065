using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000191 RID: 401
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NotificationStatisticsLogEvent : ILogEvent
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00035FD8 File Offset: 0x000341D8
		internal NotificationStatisticsLogEvent(NotificationStatisticsEventType eventType, DateTime startTime, NotificationStatisticsKey key, NotificationStatisticsValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.eventType = eventType;
			this.startTime = startTime;
			this.key = key;
			this.value = value;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00036025 File Offset: 0x00034225
		public string EventId
		{
			get
			{
				if (this.eventType != NotificationStatisticsEventType.Incoming)
				{
					return "OutgoingNotifications";
				}
				return "IncomingNotifications";
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003603C File Offset: 0x0003423C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>();
			collection.Add(new KeyValuePair<string, object>("StartTime", this.startTime.ToString("o")));
			collection.Add(new KeyValuePair<string, object>("PayloadType", this.key.PayloadType.Name));
			collection.Add(new KeyValuePair<string, object>("IsReload", this.key.IsReload.ToString()));
			collection.Add(this.key.Location.GetEventData());
			foreach (KeyValuePair<string, object> item in this.value.GetEventData())
			{
				collection.Add(item);
			}
			return collection;
		}

		// Token: 0x0400089A RID: 2202
		private const string IncomingNotificationEventId = "IncomingNotifications";

		// Token: 0x0400089B RID: 2203
		private const string OutgoingNotificationEventId = "OutgoingNotifications";

		// Token: 0x0400089C RID: 2204
		private const string PayloadTypeKey = "PayloadType";

		// Token: 0x0400089D RID: 2205
		private const string StartTimeKey = "StartTime";

		// Token: 0x0400089E RID: 2206
		private const string IsReloadKey = "IsReload";

		// Token: 0x0400089F RID: 2207
		private readonly NotificationStatisticsEventType eventType;

		// Token: 0x040008A0 RID: 2208
		private readonly DateTime startTime;

		// Token: 0x040008A1 RID: 2209
		private readonly NotificationStatisticsKey key;

		// Token: 0x040008A2 RID: 2210
		private readonly NotificationStatisticsValue value;
	}
}
