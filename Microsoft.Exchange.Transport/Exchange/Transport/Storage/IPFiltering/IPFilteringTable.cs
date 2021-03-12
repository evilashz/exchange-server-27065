using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x02000121 RID: 289
	internal class IPFilteringTable : DataTable
	{
		// Token: 0x0400059E RID: 1438
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, PrimaryKey = true, Required = true, AutoIncrement = true)]
		public const int Identity = 0;

		// Token: 0x0400059F RID: 1439
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Type = 1;

		// Token: 0x040005A0 RID: 1440
		[DataColumnDefinition(typeof(IPvxAddress), ColumnAccess.CachedProp, Required = true)]
		public const int LowAddress = 2;

		// Token: 0x040005A1 RID: 1441
		[DataColumnDefinition(typeof(IPvxAddress), ColumnAccess.CachedProp, Required = true)]
		public const int HighAddress = 3;

		// Token: 0x040005A2 RID: 1442
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int DateExpired = 4;

		// Token: 0x040005A3 RID: 1443
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp)]
		public const int Comment = 5;
	}
}
