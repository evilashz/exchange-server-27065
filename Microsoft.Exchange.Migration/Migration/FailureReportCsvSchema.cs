using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000129 RID: 297
	internal abstract class FailureReportCsvSchema : ReportCsvSchema
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x00041659 File Offset: 0x0003F859
		public FailureReportCsvSchema(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(maximumRowCount, requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x06000F39 RID: 3897
		public abstract void WriteRow(StreamWriter streamWriter, MigrationBatchError batchError);
	}
}
