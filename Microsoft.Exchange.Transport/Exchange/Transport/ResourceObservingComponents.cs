using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000050 RID: 80
	[Flags]
	internal enum ResourceObservingComponents
	{
		// Token: 0x04000132 RID: 306
		None = 0,
		// Token: 0x04000133 RID: 307
		BootScanner = 1,
		// Token: 0x04000134 RID: 308
		ContentAggregator = 2,
		// Token: 0x04000135 RID: 309
		EnhancedDns = 4,
		// Token: 0x04000136 RID: 310
		IsMemberOfResolver = 8,
		// Token: 0x04000137 RID: 311
		MessageResubmission = 16,
		// Token: 0x04000138 RID: 312
		PickUp = 32,
		// Token: 0x04000139 RID: 313
		RemoteDelivery = 64,
		// Token: 0x0400013A RID: 314
		ShadowRedundancy = 128,
		// Token: 0x0400013B RID: 315
		SmtpIn = 256,
		// Token: 0x0400013C RID: 316
		All = 4095
	}
}
