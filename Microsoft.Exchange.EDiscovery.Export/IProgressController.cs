using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000060 RID: 96
	internal interface IProgressController
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000708 RID: 1800
		bool IsStopRequested { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000709 RID: 1801
		bool IsDocumentIdHintFlightingEnabled { get; }

		// Token: 0x0600070A RID: 1802
		void ReportProgress(ProgressRecord progressRecord);
	}
}
