using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab
{
	// Token: 0x02000240 RID: 576
	internal class OabEdsAlertDefinitions
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x0006BB60 File Offset: 0x00069D60
		public OabEdsAlertDefinitions()
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0006BB68 File Offset: 0x00069D68
		public OabEdsAlertDefinitions(string redEvent, string subject, string body, NotificationServiceClass responderType, bool recycleAppPool)
		{
			this.RedEvent = redEvent;
			this.MessageSubject = subject;
			this.MessageBody = body;
			this.NotificationClass = responderType;
			this.RecycleAppPool = recycleAppPool;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0006BB95 File Offset: 0x00069D95
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x0006BB9D File Offset: 0x00069D9D
		public string RedEvent { get; protected set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0006BBA6 File Offset: 0x00069DA6
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x0006BBAE File Offset: 0x00069DAE
		public string MessageSubject { get; protected set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0006BBB7 File Offset: 0x00069DB7
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0006BBBF File Offset: 0x00069DBF
		public string MessageBody { get; protected set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0006BBC8 File Offset: 0x00069DC8
		public string MonitorName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Monitor");
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0006BBDF File Offset: 0x00069DDF
		public string EscalateResponderName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Escalate");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0006BBF6 File Offset: 0x00069DF6
		public string RecycleResponderName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Recycle");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0006BC0D File Offset: 0x00069E0D
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x0006BC15 File Offset: 0x00069E15
		public NotificationServiceClass NotificationClass { get; protected set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0006BC1E File Offset: 0x00069E1E
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x0006BC26 File Offset: 0x00069E26
		public bool RecycleAppPool { get; protected set; }

		// Token: 0x04000C12 RID: 3090
		internal const string MonitorString = "Monitor";

		// Token: 0x04000C13 RID: 3091
		internal const string EscalateString = "Escalate";

		// Token: 0x04000C14 RID: 3092
		internal const string RecycleString = "Recycle";
	}
}
