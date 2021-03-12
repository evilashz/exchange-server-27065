using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200004F RID: 79
	public class ErrorRecord
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00017549 File Offset: 0x00015749
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00017551 File Offset: 0x00015751
		public ExportErrorType ErrorType { get; internal set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001755A File Offset: 0x0001575A
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00017562 File Offset: 0x00015762
		public DateTime Time { get; internal set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001756B File Offset: 0x0001576B
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00017573 File Offset: 0x00015773
		public ExportRecord Item { get; internal set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001757C File Offset: 0x0001577C
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00017584 File Offset: 0x00015784
		public string DiagnosticMessage { get; internal set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001758D File Offset: 0x0001578D
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00017595 File Offset: 0x00015795
		public string SourceId { get; internal set; }
	}
}
