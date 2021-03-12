using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000FB RID: 251
	internal sealed class ServerInfoTable : DataTable
	{
		// Token: 0x04000460 RID: 1120
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, PrimaryKey = true, Required = true, AutoIncrement = true)]
		public const int RowId = 0;

		// Token: 0x04000461 RID: 1121
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, Required = true)]
		public const int DatabaseState = 1;

		// Token: 0x04000462 RID: 1122
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, Required = true)]
		public const int ServerFqdn = 2;

		// Token: 0x04000463 RID: 1123
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int StartTime = 3;

		// Token: 0x04000464 RID: 1124
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int EndTime = 4;

		// Token: 0x04000465 RID: 1125
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Version = 5;
	}
}
