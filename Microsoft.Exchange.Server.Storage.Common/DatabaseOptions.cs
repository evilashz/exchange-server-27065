using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200002A RID: 42
	public class DatabaseOptions
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00007E28 File Offset: 0x00006028
		public DatabaseOptions Clone()
		{
			return new DatabaseOptions
			{
				MinCachePages = this.MinCachePages,
				MaxCachePages = this.MaxCachePages,
				EnableOnlineDefragmentation = this.EnableOnlineDefragmentation,
				BackgroundDatabaseMaintenance = this.BackgroundDatabaseMaintenance,
				ReplayBackgroundDatabaseMaintenance = this.ReplayBackgroundDatabaseMaintenance,
				BackgroundDatabaseMaintenanceSerialization = this.BackgroundDatabaseMaintenanceSerialization,
				BackgroundDatabaseMaintenanceDelay = this.BackgroundDatabaseMaintenanceDelay,
				ReplayBackgroundDatabaseMaintenanceDelay = this.ReplayBackgroundDatabaseMaintenanceDelay,
				MimimumBackgroundDatabaseMaintenanceInterval = this.MimimumBackgroundDatabaseMaintenanceInterval,
				MaximumBackgroundDatabaseMaintenanceInterval = this.MaximumBackgroundDatabaseMaintenanceInterval,
				TemporaryDataFolderPath = this.TemporaryDataFolderPath,
				LogBuffers = this.LogBuffers,
				MaximumOpenTables = this.MaximumOpenTables,
				MaximumTemporaryTables = this.MaximumTemporaryTables,
				MaximumCursors = this.MaximumCursors,
				MaximumSessions = this.MaximumSessions,
				MaximumVersionStorePages = this.MaximumVersionStorePages,
				PreferredVersionStorePages = this.PreferredVersionStorePages,
				DatabaseExtensionSize = this.DatabaseExtensionSize,
				LogCheckpointDepth = this.LogCheckpointDepth,
				ReplayCheckpointDepth = this.ReplayCheckpointDepth,
				CachedClosedTables = this.CachedClosedTables,
				CachePriority = this.CachePriority,
				ReplayCachePriority = this.ReplayCachePriority,
				MaximumPreReadPages = this.MaximumPreReadPages,
				MaximumReplayPreReadPages = this.MaximumReplayPreReadPages,
				LogFilePrefix = this.LogFilePrefix,
				TotalDatabasesOnServer = this.TotalDatabasesOnServer,
				MaxActiveDatabases = this.MaxActiveDatabases
			};
		}

		// Token: 0x0400040A RID: 1034
		public int? MinCachePages;

		// Token: 0x0400040B RID: 1035
		public int? MaxCachePages;

		// Token: 0x0400040C RID: 1036
		public bool? EnableOnlineDefragmentation;

		// Token: 0x0400040D RID: 1037
		public bool BackgroundDatabaseMaintenance = true;

		// Token: 0x0400040E RID: 1038
		public bool? ReplayBackgroundDatabaseMaintenance = new bool?(true);

		// Token: 0x0400040F RID: 1039
		public bool? BackgroundDatabaseMaintenanceSerialization;

		// Token: 0x04000410 RID: 1040
		public int? BackgroundDatabaseMaintenanceDelay;

		// Token: 0x04000411 RID: 1041
		public int? ReplayBackgroundDatabaseMaintenanceDelay;

		// Token: 0x04000412 RID: 1042
		public int? MimimumBackgroundDatabaseMaintenanceInterval;

		// Token: 0x04000413 RID: 1043
		public int? MaximumBackgroundDatabaseMaintenanceInterval;

		// Token: 0x04000414 RID: 1044
		public string TemporaryDataFolderPath;

		// Token: 0x04000415 RID: 1045
		public string LogFilePrefix;

		// Token: 0x04000416 RID: 1046
		public int? LogBuffers;

		// Token: 0x04000417 RID: 1047
		public int? MaximumOpenTables;

		// Token: 0x04000418 RID: 1048
		public int? MaximumTemporaryTables;

		// Token: 0x04000419 RID: 1049
		public int? MaximumCursors;

		// Token: 0x0400041A RID: 1050
		public int? MaximumSessions;

		// Token: 0x0400041B RID: 1051
		public int? MaximumVersionStorePages;

		// Token: 0x0400041C RID: 1052
		public int? PreferredVersionStorePages;

		// Token: 0x0400041D RID: 1053
		public int? DatabaseExtensionSize;

		// Token: 0x0400041E RID: 1054
		public int? LogCheckpointDepth;

		// Token: 0x0400041F RID: 1055
		public int? ReplayCheckpointDepth;

		// Token: 0x04000420 RID: 1056
		public int? CachedClosedTables;

		// Token: 0x04000421 RID: 1057
		public int? CachePriority;

		// Token: 0x04000422 RID: 1058
		public int? ReplayCachePriority;

		// Token: 0x04000423 RID: 1059
		public int? MaximumPreReadPages;

		// Token: 0x04000424 RID: 1060
		public int? MaximumReplayPreReadPages;

		// Token: 0x04000425 RID: 1061
		public int? TotalDatabasesOnServer;

		// Token: 0x04000426 RID: 1062
		public int? MaxActiveDatabases;
	}
}
