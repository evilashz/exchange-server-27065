using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000019 RID: 25
	internal class ProtocolAnalysisTable : DataTable
	{
		// Token: 0x0400004C RID: 76
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int SenderAddress = 0;

		// Token: 0x0400004D RID: 77
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int LastUpdateTime = 1;

		// Token: 0x0400004E RID: 78
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.CachedProp)]
		public const int DataBlob = 2;

		// Token: 0x0400004F RID: 79
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp)]
		public const int ReverseDns = 3;

		// Token: 0x04000050 RID: 80
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp)]
		public const int LastQueryTime = 4;

		// Token: 0x04000051 RID: 81
		[DataColumnDefinition(typeof(bool), ColumnAccess.CachedProp, Required = true)]
		public const int Processing = 5;
	}
}
