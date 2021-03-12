using System;

namespace Microsoft.Exchange.Hygiene.Data.SystemProbe
{
	// Token: 0x02000230 RID: 560
	[Serializable]
	public class SystemProbeEvent
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00045E85 File Offset: 0x00044085
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00045E8D File Offset: 0x0004408D
		public Guid MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00045E96 File Offset: 0x00044096
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x00045E9E File Offset: 0x0004409E
		public Guid EventId
		{
			get
			{
				return this.eventId;
			}
			set
			{
				this.eventId = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00045EA7 File Offset: 0x000440A7
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x00045EAF File Offset: 0x000440AF
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
			set
			{
				this.timeStamp = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00045EB8 File Offset: 0x000440B8
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00045EC0 File Offset: 0x000440C0
		public string ServerHostName
		{
			get
			{
				return this.serverHostName;
			}
			set
			{
				this.serverHostName = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00045EC9 File Offset: 0x000440C9
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x00045ED1 File Offset: 0x000440D1
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
			set
			{
				this.componentName = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00045EDA File Offset: 0x000440DA
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x00045EE2 File Offset: 0x000440E2
		public SystemProbeEvent.Status EventStatus
		{
			get
			{
				return this.eventStatus;
			}
			set
			{
				this.eventStatus = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00045EEB File Offset: 0x000440EB
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x00045EF3 File Offset: 0x000440F3
		public string EventMessage
		{
			get
			{
				return this.eventMessage;
			}
			set
			{
				this.eventMessage = value;
			}
		}

		// Token: 0x04000B6B RID: 2923
		private Guid messageId;

		// Token: 0x04000B6C RID: 2924
		private Guid eventId;

		// Token: 0x04000B6D RID: 2925
		private DateTime timeStamp;

		// Token: 0x04000B6E RID: 2926
		private string serverHostName;

		// Token: 0x04000B6F RID: 2927
		private string componentName;

		// Token: 0x04000B70 RID: 2928
		private SystemProbeEvent.Status eventStatus;

		// Token: 0x04000B71 RID: 2929
		private string eventMessage;

		// Token: 0x02000231 RID: 561
		public enum Status
		{
			// Token: 0x04000B73 RID: 2931
			Pass,
			// Token: 0x04000B74 RID: 2932
			Fail
		}
	}
}
