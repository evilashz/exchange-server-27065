using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008D RID: 141
	internal class EventEntity
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A4A6 File Offset: 0x000086A6
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000A4AE File Offset: 0x000086AE
		public Link Link { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000A4B7 File Offset: 0x000086B7
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000A4BF File Offset: 0x000086BF
		public Link In { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000A4C8 File Offset: 0x000086C8
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public EventOperation Relationship { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A4D9 File Offset: 0x000086D9
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000A4E1 File Offset: 0x000086E1
		public EventStatus Status { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000A4EA File Offset: 0x000086EA
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000A4F2 File Offset: 0x000086F2
		public Resource EmbeddedResource { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000A4FB File Offset: 0x000086FB
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000A503 File Offset: 0x00008703
		public ErrorInformation Error { get; set; }
	}
}
