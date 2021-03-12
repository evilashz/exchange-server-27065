using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000059 RID: 89
	public interface IExportContext
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006C1 RID: 1729
		bool IsResume { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006C2 RID: 1730
		IExportMetadata ExportMetadata { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060006C3 RID: 1731
		IList<ISource> Sources { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006C4 RID: 1732
		ITargetLocation TargetLocation { get; }

		// Token: 0x060006C5 RID: 1733
		void WriteResultManifest(IEnumerable<ExportRecord> records);

		// Token: 0x060006C6 RID: 1734
		void WriteErrorLog(IEnumerable<ErrorRecord> errorRecords);
	}
}
