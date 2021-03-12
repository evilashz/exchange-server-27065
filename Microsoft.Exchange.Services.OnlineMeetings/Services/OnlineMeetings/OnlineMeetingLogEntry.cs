using System;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000026 RID: 38
	internal class OnlineMeetingLogEntry : LogEntry
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005684 File Offset: 0x00003884
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000568C File Offset: 0x0000388C
		internal Guid UserGuid { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005695 File Offset: 0x00003895
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000569D File Offset: 0x0000389D
		internal string ItemId { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000056A6 File Offset: 0x000038A6
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000056AE File Offset: 0x000038AE
		internal string MeetingUrl { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000056B7 File Offset: 0x000038B7
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000056BF File Offset: 0x000038BF
		internal OnlineMeetingSettings MeetingSettings { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000056C8 File Offset: 0x000038C8
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000056D0 File Offset: 0x000038D0
		internal OnlineMeetingDefaultValuesResource DefaultValuesResource { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000056D9 File Offset: 0x000038D9
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000056E1 File Offset: 0x000038E1
		internal OnlineMeetingPoliciesResource PoliciesResource { get; set; }
	}
}
