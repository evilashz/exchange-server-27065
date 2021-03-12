using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000FA RID: 250
	internal class QueueTable : DataTable
	{
		// Token: 0x0400045B RID: 1115
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int QueueRowId = 0;

		// Token: 0x0400045C RID: 1116
		[DataColumnDefinition(typeof(Guid), ColumnAccess.CachedProp, Required = true)]
		public const int NextHopConnector = 1;

		// Token: 0x0400045D RID: 1117
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, Required = true)]
		public const int NextHopDomain = 2;

		// Token: 0x0400045E RID: 1118
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int State = 3;

		// Token: 0x0400045F RID: 1119
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int NextHopType = 4;
	}
}
