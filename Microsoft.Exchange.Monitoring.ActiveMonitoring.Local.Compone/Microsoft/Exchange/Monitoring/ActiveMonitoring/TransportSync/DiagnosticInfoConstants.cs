using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x02000115 RID: 277
	public static class DiagnosticInfoConstants
	{
		// Token: 0x040005AA RID: 1450
		public const string ComponentName = "syncmanager";

		// Token: 0x040005AB RID: 1451
		public const string Database = "Database";

		// Token: 0x040005AC RID: 1452
		public const string DatabaseId = "databaseId";

		// Token: 0x040005AD RID: 1453
		public const string DatabaseQueueManager = "DatabaseQueueManager";

		// Token: 0x040005AE RID: 1454
		public const string DefaultArgument = "basic";

		// Token: 0x040005AF RID: 1455
		public const string DispatchManager = "DispatchManager";

		// Token: 0x040005B0 RID: 1456
		public const string Enabled = "enabled";

		// Token: 0x040005B1 RID: 1457
		public const string GlobalDatabaseHandler = "GlobalDatabaseHandler";

		// Token: 0x040005B2 RID: 1458
		public const string InfoArgument = "info";

		// Token: 0x040005B3 RID: 1459
		public const string ItemsOutOfSla = "itemsOutOfSla";

		// Token: 0x040005B4 RID: 1460
		public const string ItemsOutOfSlaPercent = "itemsOutOfSlaPercent";

		// Token: 0x040005B5 RID: 1461
		public const string LastDatabaseDiscoveryStartTime = "LastDatabaseDiscoveryStartTime";

		// Token: 0x040005B6 RID: 1462
		public const string NextPollingTime = "nextPollingTime";

		// Token: 0x040005B7 RID: 1463
		public const string PollingQueue = "PollingQueue";

		// Token: 0x040005B8 RID: 1464
		public const string SubComponents = "databasemanager dispatchmanager";

		// Token: 0x040005B9 RID: 1465
		public const string WorkType = "workType";

		// Token: 0x040005BA RID: 1466
		public static readonly int AverageNumberOfMdbsPerServer = 200;

		// Token: 0x040005BB RID: 1467
		public static readonly TimeSpan DiagnosticInfoCacheTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040005BC RID: 1468
		public static readonly string WorkTypeName = "AggregationIncremental";
	}
}
