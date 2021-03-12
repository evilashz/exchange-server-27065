using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200001C RID: 28
	internal class ConfigurationDataTable : DataTable
	{
		// Token: 0x0400005C RID: 92
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int ConfigName = 0;

		// Token: 0x0400005D RID: 93
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, Required = true)]
		public const int ConfigValue = 1;
	}
}
