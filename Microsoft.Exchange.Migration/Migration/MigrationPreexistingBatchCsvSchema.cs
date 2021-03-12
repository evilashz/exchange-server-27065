using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F5 RID: 245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationPreexistingBatchCsvSchema : MigrationBatchCsvSchema
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0003544F File Offset: 0x0003364F
		public MigrationPreexistingBatchCsvSchema() : base(int.MaxValue, MigrationPreexistingBatchCsvSchema.requiredColumns, null, null)
		{
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00035463 File Offset: 0x00033663
		public void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(MigrationPreexistingBatchCsvSchema.requiredColumns.Keys);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00035478 File Offset: 0x00033678
		public void Write(StreamWriter streamWriter, Guid userId)
		{
			streamWriter.WriteCsvLine(new List<string>(1)
			{
				userId.ToString()
			});
		}

		// Token: 0x040004A8 RID: 1192
		public const string JobItemGuidColumnName = "JobItemGuid";

		// Token: 0x040004A9 RID: 1193
		internal static readonly ProviderPropertyDefinition JobItemGuid = new SimpleProviderPropertyDefinition("JobItemGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.TaskPopulated, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040004AA RID: 1194
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"JobItemGuid",
				MigrationPreexistingBatchCsvSchema.JobItemGuid
			}
		};
	}
}
