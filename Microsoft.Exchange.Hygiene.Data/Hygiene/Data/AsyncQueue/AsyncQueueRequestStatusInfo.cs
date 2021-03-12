using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000022 RID: 34
	internal class AsyncQueueRequestStatusInfo
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000046FB File Offset: 0x000028FB
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004703 File Offset: 0x00002903
		public AsyncQueueStatus Status { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000470C File Offset: 0x0000290C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004714 File Offset: 0x00002914
		public DateTime? StartDatetime { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000471D File Offset: 0x0000291D
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004725 File Offset: 0x00002925
		public DateTime? EndDatetime { get; set; }
	}
}
