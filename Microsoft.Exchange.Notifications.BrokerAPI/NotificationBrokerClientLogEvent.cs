using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000006 RID: 6
	internal class NotificationBrokerClientLogEvent : ILogEvent
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002D23 File Offset: 0x00000F23
		public NotificationBrokerClientLogEvent(ConsumerId consumerId, string action)
		{
			this.consumerId = consumerId;
			this.eventId = action;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D39 File Offset: 0x00000F39
		public string EventId
		{
			get
			{
				return this.eventId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002D41 File Offset: 0x00000F41
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002D49 File Offset: 0x00000F49
		public Guid? SubscriptionId { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002D52 File Offset: 0x00000F52
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002D5A File Offset: 0x00000F5A
		public BrokerStatus? Status { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002D6B File Offset: 0x00000F6B
		public Guid? NotificationId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D74 File Offset: 0x00000F74
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002D7C File Offset: 0x00000F7C
		public int? SequenceId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D85 File Offset: 0x00000F85
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002D8D File Offset: 0x00000F8D
		public long Latency { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002D96 File Offset: 0x00000F96
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002D9E File Offset: 0x00000F9E
		public Exception Exception { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("ConsumerId", this.consumerId.ToString());
			if (this.SubscriptionId != null)
			{
				dictionary.Add("SubId", this.SubscriptionId.Value.ToString());
			}
			if (this.Status != null)
			{
				dictionary.Add("Status", this.Status.ToString());
			}
			if (this.NotificationId != null)
			{
				dictionary.Add("NtfId", this.NotificationId.Value.ToString());
			}
			if (this.SequenceId != null)
			{
				dictionary.Add("SeqId", this.SequenceId.ToString());
			}
			dictionary.Add("Latency", this.Latency.ToString());
			if (this.Exception != null)
			{
				dictionary.Add("Ex", this.Exception.ToString());
			}
			return dictionary;
		}

		// Token: 0x04000018 RID: 24
		private readonly ConsumerId consumerId;

		// Token: 0x04000019 RID: 25
		private readonly string eventId;
	}
}
