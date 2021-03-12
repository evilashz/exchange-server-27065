using System;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200000F RID: 15
	internal interface IResourceMeter
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75
		ResourceIdentifier Resource { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76
		long Pressure { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77
		PressureTransitions PressureTransitions { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78
		ResourceUse ResourceUse { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79
		ResourceUse RawResourceUse { get; }

		// Token: 0x06000050 RID: 80
		void Refresh();
	}
}
