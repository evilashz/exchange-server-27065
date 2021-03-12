using System;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x0200000B RID: 11
	internal static class MigrationServiceProcessorsCommonHelpers
	{
		// Token: 0x04000026 RID: 38
		public const string LocalizedErrorColumn = "LocalizedError";

		// Token: 0x04000027 RID: 39
		public const string InternalErrorColumn = "InternalError";

		// Token: 0x04000028 RID: 40
		public const string ObjectVersionColumn = "ObjectVersion";

		// Token: 0x04000029 RID: 41
		public static readonly ColumnDefinition<int> TenantName = new ColumnDefinition<int>("TenantName", "TenantNameId", KnownStringType.TenantName);

		// Token: 0x0400002A RID: 42
		public static readonly ColumnDefinition<int> MigrationType = new ColumnDefinition<int>("MigrationType", "MigrationTypeId", KnownStringType.MigrationType);

		// Token: 0x0400002B RID: 43
		public static readonly ColumnDefinition<int> Status = new ColumnDefinition<int>("Status", "MigrationStatusId", KnownStringType.MigrationStatus);

		// Token: 0x0400002C RID: 44
		public static readonly ColumnDefinition<int> WatsonHash = new ColumnDefinition<int>("WatsonHash", "WatsonHashId", KnownStringType.WatsonHash);
	}
}
