using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200000C RID: 12
	internal abstract class BaseMrsMonitorCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003A52 File Offset: 0x00001C52
		protected BaseMrsMonitorCsvSchema(Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x0400002D RID: 45
		public const string RequestGuidColumn = "RequestGuid";
	}
}
