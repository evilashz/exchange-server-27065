using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A6 RID: 166
	internal class TenantDataCsvSchema : CsvSchema
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00008199 File Offset: 0x00006399
		private TenantDataCsvSchema() : base(int.MaxValue, TenantDataCsvSchema.requiredColumns, null, null)
		{
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000081AD File Offset: 0x000063AD
		public static TenantDataCsvSchema TenantDataCsvSchemaInstance
		{
			get
			{
				return TenantDataCsvSchema.tenantDataCsvSchemaInstance;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000081B4 File Offset: 0x000063B4
		internal static ProviderPropertyDefinition GetDefaultPropertyDefinition(string propertyName, PropertyDefinitionConstraint[] constraints)
		{
			if (constraints == null)
			{
				constraints = PropertyDefinitionConstraint.None;
			}
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, constraints, constraints);
		}

		// Token: 0x040001F9 RID: 505
		public const string TenantColumn = "TenantName";

		// Token: 0x040001FA RID: 506
		public const string PrimaryMBXCountColumn = "PrimaryMBXNum";

		// Token: 0x040001FB RID: 507
		public const string PrimaryMBXSizeColumn = "PrimaryMBXSize";

		// Token: 0x040001FC RID: 508
		public const string ArchiveMBXCountColumn = "ArchiveMBXNum";

		// Token: 0x040001FD RID: 509
		public const string ArchiveMBXSizeColumn = "ArchiveMBXSize";

		// Token: 0x040001FE RID: 510
		private const int InternalMaximumRowCount = 2147483647;

		// Token: 0x040001FF RID: 511
		private static readonly TenantDataCsvSchema tenantDataCsvSchemaInstance = new TenantDataCsvSchema();

		// Token: 0x04000200 RID: 512
		private static Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"TenantName",
				TenantDataCsvSchema.GetDefaultPropertyDefinition("TenantName", null)
			},
			{
				"PrimaryMBXNum",
				TenantDataCsvSchema.GetDefaultPropertyDefinition("PrimaryMBXNum", null)
			},
			{
				"PrimaryMBXSize",
				TenantDataCsvSchema.GetDefaultPropertyDefinition("PrimaryMBXSize", null)
			},
			{
				"ArchiveMBXNum",
				TenantDataCsvSchema.GetDefaultPropertyDefinition("ArchiveMBXNum", null)
			},
			{
				"ArchiveMBXSize",
				TenantDataCsvSchema.GetDefaultPropertyDefinition("ArchiveMBXSize", null)
			}
		};
	}
}
