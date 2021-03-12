using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200001B RID: 27
	internal class OpenProxyStatusTable : DataTable
	{
		// Token: 0x04000056 RID: 86
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int SenderAddress = 0;

		// Token: 0x04000057 RID: 87
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int LastAccessTime = 1;

		// Token: 0x04000058 RID: 88
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp)]
		public const int LastDetectionTime = 2;

		// Token: 0x04000059 RID: 89
		[DataColumnDefinition(typeof(bool), ColumnAccess.CachedProp, Required = true)]
		public const int Processing = 3;

		// Token: 0x0400005A RID: 90
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp)]
		public const int Message = 4;

		// Token: 0x0400005B RID: 91
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int OpenProxyStatus = 5;
	}
}
