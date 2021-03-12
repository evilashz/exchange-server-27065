using System;
using System.Net;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200059C RID: 1436
	public class LyncAutodiscoverRequestState
	{
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000CF3F6 File Offset: 0x000CD5F6
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x000CF3FE File Offset: 0x000CD5FE
		public HttpWebRequest Request { get; set; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000CF407 File Offset: 0x000CD607
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x000CF40F File Offset: 0x000CD60F
		public HttpWebResponse Response { get; set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000CF418 File Offset: 0x000CD618
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x000CF420 File Offset: 0x000CD620
		public string TargetUrl { get; set; }

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000CF429 File Offset: 0x000CD629
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000CF431 File Offset: 0x000CD631
		public bool IsRedirect { get; set; }
	}
}
