using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000565 RID: 1381
	internal static class ActiveMonitoringRpcErrorCode
	{
		// Token: 0x040018EB RID: 6379
		public const int Success = 0;

		// Token: 0x040018EC RID: 6380
		public const int CafeServerOverloaded = -2147417343;

		// Token: 0x040018ED RID: 6381
		public const int CafeServerNotOwner = -2147417342;

		// Token: 0x040018EE RID: 6382
		public const int DatabaseCopyNotActive = -2147417341;

		// Token: 0x040018EF RID: 6383
		public const int ServerVersionNotMatch = -2147417340;

		// Token: 0x040018F0 RID: 6384
		public const int DatabaseBlacklisted = -2147417339;

		// Token: 0x040018F1 RID: 6385
		public const int MonitoringStateOff = -2147417338;

		// Token: 0x040018F2 RID: 6386
		public const int WLIDException = -2147417337;

		// Token: 0x040018F3 RID: 6387
		public const int OtherManagedException = -2147417336;

		// Token: 0x040018F4 RID: 6388
		public const int InvalidArgument = -2147024809;
	}
}
