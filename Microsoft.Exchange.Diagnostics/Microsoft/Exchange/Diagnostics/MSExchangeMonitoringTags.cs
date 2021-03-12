using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200027F RID: 639
	public struct MSExchangeMonitoringTags
	{
		// Token: 0x04001108 RID: 4360
		public const int MonitoringService = 0;

		// Token: 0x04001109 RID: 4361
		public const int MonitoringRpcServer = 1;

		// Token: 0x0400110A RID: 4362
		public const int MonitoringTasks = 2;

		// Token: 0x0400110B RID: 4363
		public const int MonitoringHelper = 3;

		// Token: 0x0400110C RID: 4364
		public const int MonitoringData = 4;

		// Token: 0x0400110D RID: 4365
		public const int CorrelationEngine = 5;

		// Token: 0x0400110E RID: 4366
		public const int FaultInjection = 6;

		// Token: 0x0400110F RID: 4367
		public static Guid guid = new Guid("170506F7-64BA-4C74-A2A3-0A5CC247DB58");
	}
}
