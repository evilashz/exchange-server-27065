using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000128 RID: 296
	internal abstract class ReportCsvSchema : CsvSchema
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x0004164C File Offset: 0x0003F84C
		public ReportCsvSchema(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(maximumRowCount, requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x06000F36 RID: 3894
		public abstract void WriteHeader(StreamWriter streamWriter);

		// Token: 0x06000F37 RID: 3895
		public abstract void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem);
	}
}
