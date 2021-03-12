using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000004 RID: 4
	internal static class AvailabilityGlobals
	{
		// Token: 0x0400000B RID: 11
		public static readonly Guid ComponentGuid = new Guid("{A12F4C36-83F1-4142-BE14-09FE7E782E16}");

		// Token: 0x0400000C RID: 12
		public static readonly ExEventLog Logger = new ExEventLog(AvailabilityGlobals.ComponentGuid, "MSExchange Availability");

		// Token: 0x0400000D RID: 13
		private static readonly Trace InitializeTracer = ExTraceGlobals.InitializeTracer;
	}
}
