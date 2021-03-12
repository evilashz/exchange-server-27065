using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200001A RID: 26
	internal class SenderReputationTable : DataTable
	{
		// Token: 0x04000052 RID: 82
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int SenderAddrHash = 0;

		// Token: 0x04000053 RID: 83
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Srl = 1;

		// Token: 0x04000054 RID: 84
		[DataColumnDefinition(typeof(bool), ColumnAccess.CachedProp, Required = true)]
		public const int OpenProxy = 2;

		// Token: 0x04000055 RID: 85
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int ExpirationTime = 3;
	}
}
