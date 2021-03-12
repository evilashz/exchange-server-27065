using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200059D RID: 1437
	public class LyncAutodiscoverResult
	{
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000CF43A File Offset: 0x000CD63A
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000CF442 File Offset: 0x000CD642
		public bool IsUcwaSupported { get; set; }

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000CF44B File Offset: 0x000CD64B
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000CF453 File Offset: 0x000CD653
		public string UcwaDiscoveryUrl { get; set; }

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000CF45C File Offset: 0x000CD65C
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x000CF464 File Offset: 0x000CD664
		public string Response { get; set; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000CF46D File Offset: 0x000CD66D
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x000CF475 File Offset: 0x000CD675
		public string DiagnosticInfo { get; set; }
	}
}
