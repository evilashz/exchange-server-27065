using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000058 RID: 88
	public interface IExportHandler : IDisposable
	{
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060006B5 RID: 1717
		// (remove) Token: 0x060006B6 RID: 1718
		event EventHandler<ExportStatusEventArgs> OnReportStatistics;

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006B7 RID: 1719
		IExportContext ExportContext { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006B8 RID: 1720
		ISearchResults SearchResults { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006B9 RID: 1721
		OperationStatus CurrentStatus { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006BA RID: 1722
		// (set) Token: 0x060006BB RID: 1723
		bool IsDocIdHintFlightingEnabled { get; set; }

		// Token: 0x060006BC RID: 1724
		void EnsureAuthentication(ICredentialHandler credentialHandler, Uri configurationEwsUrl = null);

		// Token: 0x060006BD RID: 1725
		void Prepare();

		// Token: 0x060006BE RID: 1726
		void Export();

		// Token: 0x060006BF RID: 1727
		void Stop();

		// Token: 0x060006C0 RID: 1728
		void Rollback();
	}
}
