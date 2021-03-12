using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200045D RID: 1117
	public struct ScriptSharpSymbol
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00088226 File Offset: 0x00086426
		// (set) Token: 0x0600257F RID: 9599 RVA: 0x0008822E File Offset: 0x0008642E
		public int ScriptStartPosition { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00088237 File Offset: 0x00086437
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x0008823F File Offset: 0x0008643F
		public int ScriptEndPosition { get; set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00088248 File Offset: 0x00086448
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x00088250 File Offset: 0x00086450
		public int SourceStartLine { get; set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00088259 File Offset: 0x00086459
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00088261 File Offset: 0x00086461
		public uint SourceFileId { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0008826A File Offset: 0x0008646A
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00088272 File Offset: 0x00086472
		public int FunctionNameIndex { get; set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x0008827B File Offset: 0x0008647B
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x00088283 File Offset: 0x00086483
		public int ParentSymbol { get; set; }
	}
}
