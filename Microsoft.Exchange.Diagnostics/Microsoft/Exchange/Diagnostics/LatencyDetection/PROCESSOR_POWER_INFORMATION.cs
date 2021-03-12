using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000185 RID: 389
	internal struct PROCESSOR_POWER_INFORMATION
	{
		// Token: 0x040007AF RID: 1967
		public uint Number;

		// Token: 0x040007B0 RID: 1968
		public uint MaxMhz;

		// Token: 0x040007B1 RID: 1969
		public uint CurrentMhz;

		// Token: 0x040007B2 RID: 1970
		public uint MhzLimit;

		// Token: 0x040007B3 RID: 1971
		public uint MaxIdleState;

		// Token: 0x040007B4 RID: 1972
		public uint CurrentIdleState;
	}
}
