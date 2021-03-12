using System;
using System.Text;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200006D RID: 109
	public class EventLogSubscription
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000183C5 File Offset: 0x000165C5
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x000183CD File Offset: 0x000165CD
		public string Name { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000183D6 File Offset: 0x000165D6
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x000183DE File Offset: 0x000165DE
		public EventMatchingRule GreenEvents { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000183E7 File Offset: 0x000165E7
		// (set) Token: 0x060003AB RID: 939 RVA: 0x000183EF File Offset: 0x000165EF
		public EventMatchingRule RedEvents { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000183F8 File Offset: 0x000165F8
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00018400 File Offset: 0x00016600
		public TimeSpan AutoResetInterval { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00018409 File Offset: 0x00016609
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00018411 File Offset: 0x00016611
		public EventLogSubscription.CustomAction OnGreenEvents { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001841A File Offset: 0x0001661A
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00018422 File Offset: 0x00016622
		public EventLogSubscription.CustomAction OnRedEvents { get; set; }

		// Token: 0x060003B2 RID: 946 RVA: 0x0001842B File Offset: 0x0001662B
		public EventLogSubscription(string name, EventMatchingRule redEvents, EventMatchingRule greenEvents = null, EventLogSubscription.CustomAction onGreen = null, EventLogSubscription.CustomAction onRed = null) : this(name, EventLogSubscription.NoAutoReset, redEvents, greenEvents, onGreen, onRed)
		{
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001843F File Offset: 0x0001663F
		public EventLogSubscription(string name, TimeSpan autoReset, EventMatchingRule redEvents, EventMatchingRule greenEvents = null, EventLogSubscription.CustomAction onGreen = null, EventLogSubscription.CustomAction onRed = null)
		{
			this.Name = name;
			this.GreenEvents = greenEvents;
			this.RedEvents = redEvents;
			this.AutoResetInterval = autoReset;
			this.OnGreenEvents = onGreen;
			this.OnRedEvents = onRed;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00018474 File Offset: 0x00016674
		public string GetContentHash()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Name={0}", this.Name);
			stringBuilder.AppendFormat("redEvents={0}", (this.RedEvents == null) ? "NULL" : this.RedEvents.ToString());
			stringBuilder.AppendFormat("greenEvents={0}", (this.GreenEvents == null) ? "NULL" : this.GreenEvents.ToString());
			stringBuilder.AppendFormat("AutoReset={0}s", this.AutoResetInterval.TotalSeconds);
			return stringBuilder.ToString();
		}

		// Token: 0x040002AB RID: 683
		public static TimeSpan NoAutoReset = TimeSpan.MinValue;

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x060003B7 RID: 951
		public delegate void CustomAction(EventLogNotification.EventRecordInternal record, EventLogNotification.EventNotificationMetadata enm);
	}
}
