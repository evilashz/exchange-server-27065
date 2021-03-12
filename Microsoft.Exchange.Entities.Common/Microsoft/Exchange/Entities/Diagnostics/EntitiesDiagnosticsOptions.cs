using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.Diagnostics
{
	// Token: 0x02000015 RID: 21
	internal class EntitiesDiagnosticsOptions
	{
		// Token: 0x0400002F RID: 47
		public const ReportOptions WatsonReportOptions = ReportOptions.DoNotCollectDumps | ReportOptions.DoNotLogProcessAndThreadIds | ReportOptions.DoNotFreezeThreads;
	}
}
