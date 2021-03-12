using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000006 RID: 6
	public class AuditUploaderAction
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003238 File Offset: 0x00001438
		public AuditUploaderAction(Actions action, TimeSpan? interval)
		{
			this.ActionToPerform = action;
			this.ActionThrottlingInterval = interval;
			this.LastTriggerDate = DateTime.MinValue;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003259 File Offset: 0x00001459
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003261 File Offset: 0x00001461
		public Actions ActionToPerform { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000326A File Offset: 0x0000146A
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003272 File Offset: 0x00001472
		public TimeSpan? ActionThrottlingInterval { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000327B File Offset: 0x0000147B
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003283 File Offset: 0x00001483
		public DateTime LastTriggerDate { get; set; }
	}
}
