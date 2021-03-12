using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000016 RID: 22
	internal class MeetingPolicies
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000283C File Offset: 0x00000A3C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002844 File Offset: 0x00000A44
		public AttendanceAnnouncementsStatus AttendanceAnnouncementsStatus { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000284D File Offset: 0x00000A4D
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002855 File Offset: 0x00000A55
		public Policy PstnUserAdmission { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000285E File Offset: 0x00000A5E
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002866 File Offset: 0x00000A66
		public Policy MeetingRecording { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000286F File Offset: 0x00000A6F
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002877 File Offset: 0x00000A77
		public Policy ExternalUserRecording { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002880 File Offset: 0x00000A80
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002888 File Offset: 0x00000A88
		public int MeetingSize { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002891 File Offset: 0x00000A91
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002899 File Offset: 0x00000A99
		public Policy VoipAudio { get; set; }
	}
}
