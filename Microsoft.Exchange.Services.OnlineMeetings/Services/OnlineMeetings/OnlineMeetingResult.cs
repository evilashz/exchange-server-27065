using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001C RID: 28
	internal class OnlineMeetingResult
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000321A File Offset: 0x0000141A
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003222 File Offset: 0x00001422
		public OnlineMeeting OnlineMeeting { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000322B File Offset: 0x0000142B
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003233 File Offset: 0x00001433
		public CustomizationValues CustomizationValues { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000323C File Offset: 0x0000143C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003244 File Offset: 0x00001444
		public MeetingPolicies MeetingPolicies { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000324D File Offset: 0x0000144D
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003255 File Offset: 0x00001455
		public DialInInformation DialIn { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000325E File Offset: 0x0000145E
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003266 File Offset: 0x00001466
		public DefaultValues DefaultValues { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000326F File Offset: 0x0000146F
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003277 File Offset: 0x00001477
		public OnlineMeetingLogEntry LogEntry { get; set; }
	}
}
