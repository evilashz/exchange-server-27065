using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000468 RID: 1128
	internal class UserContextStatistics
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00088897 File Offset: 0x00086A97
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x0008889F File Offset: 0x00086A9F
		public bool CookieCreated { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000888A8 File Offset: 0x00086AA8
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x000888B0 File Offset: 0x00086AB0
		public bool Created { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000888B9 File Offset: 0x00086AB9
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x000888C1 File Offset: 0x00086AC1
		public UserContextCreationError Error { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x000888CA File Offset: 0x00086ACA
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x000888D2 File Offset: 0x00086AD2
		public int AcquireLatency { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x000888DB File Offset: 0x00086ADB
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x000888E3 File Offset: 0x00086AE3
		public int LoadTime { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000888EC File Offset: 0x00086AEC
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x000888F4 File Offset: 0x00086AF4
		public int ExchangePrincipalCreationTime { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000888FD File Offset: 0x00086AFD
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x00088905 File Offset: 0x00086B05
		public int MiniRecipientCreationTime { get; set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0008890E File Offset: 0x00086B0E
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x00088916 File Offset: 0x00086B16
		public int SKUCapabilityTestTime { get; set; }
	}
}
