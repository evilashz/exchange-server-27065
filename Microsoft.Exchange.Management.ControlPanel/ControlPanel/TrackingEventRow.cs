using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E0 RID: 736
	[DataContract]
	public class TrackingEventRow
	{
		// Token: 0x06002CE0 RID: 11488 RVA: 0x00089F30 File Offset: 0x00088130
		internal TrackingEventRow(TrackingEventType trackingEvent, DateTime eventDateTime, string eventTypeDescription, string eventDescription, string server, string[] eventData)
		{
			this.TrackingEvent = trackingEvent;
			this.EventDateTime = ((eventDateTime == DateTime.MinValue) ? string.Empty : eventDateTime.UtcToUserDateTimeString());
			this.EventTypeDescription = eventTypeDescription;
			this.EventDescription = eventDescription;
			this.Server = server;
			this.EventData = eventData;
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00089F89 File Offset: 0x00088189
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x00089F91 File Offset: 0x00088191
		[DataMember]
		public TrackingEventType TrackingEvent { get; private set; }

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x00089F9A File Offset: 0x0008819A
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x00089FA2 File Offset: 0x000881A2
		[DataMember]
		public string EventTypeDescription { get; private set; }

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x00089FAB File Offset: 0x000881AB
		// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x00089FB3 File Offset: 0x000881B3
		[DataMember]
		public string EventDescription { get; private set; }

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x00089FBC File Offset: 0x000881BC
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x00089FC4 File Offset: 0x000881C4
		[DataMember]
		public string EventDateTime { get; private set; }

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00089FCD File Offset: 0x000881CD
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00089FD5 File Offset: 0x000881D5
		[DataMember]
		public string[] EventData { get; private set; }

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00089FDE File Offset: 0x000881DE
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00089FE6 File Offset: 0x000881E6
		public string Server { get; private set; }
	}
}
