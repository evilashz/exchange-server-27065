using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000044 RID: 68
	[Flags]
	internal enum ComponentsState : ulong
	{
		// Token: 0x040000F6 RID: 246
		None = 0UL,
		// Token: 0x040000F7 RID: 247
		AllowInboundMailSubmissionFromHubs = 1UL,
		// Token: 0x040000F8 RID: 248
		AllowInboundMailSubmissionFromInternet = 2UL,
		// Token: 0x040000F9 RID: 249
		AllowInboundMailSubmissionFromPickupAndReplayDirectory = 4UL,
		// Token: 0x040000FA RID: 250
		AllowInboundMailSubmissionFromMailbox = 8UL,
		// Token: 0x040000FB RID: 251
		AllowOutboundMailDeliveryToRemoteDomains = 16UL,
		// Token: 0x040000FC RID: 252
		AllowBootScannerRunning = 64UL,
		// Token: 0x040000FD RID: 253
		AllowContentAggregation = 128UL,
		// Token: 0x040000FE RID: 254
		AllowMessageRepositoryResubmission = 256UL,
		// Token: 0x040000FF RID: 255
		AllowShadowRedundancyResubmission = 512UL,
		// Token: 0x04000100 RID: 256
		TransportServicePaused = 1024UL,
		// Token: 0x04000101 RID: 257
		AllowSmtpInComponentToRecvFromInternetAndHubs = 3UL,
		// Token: 0x04000102 RID: 258
		AllowAllComponents = 991UL,
		// Token: 0x04000103 RID: 259
		AllowIncomingTraffic = 143UL,
		// Token: 0x04000104 RID: 260
		HighResourcePressureState = 80UL,
		// Token: 0x04000105 RID: 261
		MediumResourcePressureState = 89UL
	}
}
