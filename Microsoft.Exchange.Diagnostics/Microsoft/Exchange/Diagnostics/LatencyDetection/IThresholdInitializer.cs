using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200017D RID: 381
	internal interface IThresholdInitializer
	{
		// Token: 0x06000AE0 RID: 2784
		void SetThresholdFromConfiguration(LatencyDetectionLocation location, LoggingType type);
	}
}
