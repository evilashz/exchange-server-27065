using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILatencyDetectionLogger
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A85 RID: 2693
		LoggingType Type { get; }

		// Token: 0x06000A86 RID: 2694
		void Log(LatencyReportingThreshold threshold, LatencyDetectionContext trigger, ICollection<LatencyDetectionContext> context, LatencyDetectionException exception);
	}
}
