using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004D0 RID: 1232
	internal class DiagnosticsTracker
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000B8500 File Offset: 0x000B6700
		public IList<DiagnosticsItemBase> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000B8508 File Offset: 0x000B6708
		public DiagnosticsItemBase TrackDiagnostics(string header)
		{
			DiagnosticsItemBase diagnosticsItemBase = DiagnosticsItemFactory.Create(header);
			if (diagnosticsItemBase.IsValid)
			{
				this.items.Add(diagnosticsItemBase);
			}
			return diagnosticsItemBase;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000B8534 File Offset: 0x000B6734
		public DiagnosticsItemBase TrackLocalDiagnostics(int errorid, string reason, params string[] additional)
		{
			string header = DiagnosticsItemFactory.FormatDiagnostics(errorid, reason, additional);
			return this.TrackDiagnostics(header);
		}

		// Token: 0x040015D2 RID: 5586
		private List<DiagnosticsItemBase> items = new List<DiagnosticsItemBase>(3);
	}
}
