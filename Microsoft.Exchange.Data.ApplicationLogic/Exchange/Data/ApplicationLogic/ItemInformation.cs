using System;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A1 RID: 161
	internal class ItemInformation
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001B09F File Offset: 0x0001929F
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001B0A7 File Offset: 0x000192A7
		public string Id { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001B0B0 File Offset: 0x000192B0
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public byte[] Data { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001B0C1 File Offset: 0x000192C1
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001B0C9 File Offset: 0x000192C9
		public Exception Error { get; set; }
	}
}
