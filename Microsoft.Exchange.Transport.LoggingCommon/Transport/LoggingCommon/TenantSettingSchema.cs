using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000019 RID: 25
	internal static class TenantSettingSchema
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000312C File Offset: 0x0000132C
		static TenantSettingSchema()
		{
			Array values = Enum.GetValues(typeof(TenantSettingSchemaFields));
			if (values.Length > 0 && (int)values.GetValue(values.Length - 1) != values.Length - 1)
			{
				throw new InvalidOperationException("TenantSettingSchemaFields has invalid enum definitions");
			}
			CsvField[] array = new CsvField[values.Length];
			array[0] = new CsvField(TenantSettingSchemaFields.DateTime.ToString(), typeof(DateTime), TenantSettingSchema.E15Version);
			array[1] = new CsvField(TenantSettingSchemaFields.ChangeType.ToString(), typeof(int), TenantSettingSchema.E15Version);
			array[2] = new CsvField(TenantSettingSchemaFields.TenantID.ToString(), typeof(string), TenantSettingSchema.E15Version);
			array[3] = new CsvField(TenantSettingSchemaFields.SettingId.ToString(), typeof(string), TenantSettingSchema.E15Version);
			array[4] = new CsvField(TenantSettingSchemaFields.Name.ToString(), typeof(string), TenantSettingSchema.E15Version);
			array[5] = new CsvField(TenantSettingSchemaFields.CustomData.ToString(), typeof(KeyValuePair<string, object>[]), TenantSettingSchema.E15Version);
			TenantSettingSchema.schema = new CsvTable(array);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003283 File Offset: 0x00001483
		internal static Version E15FirstVersion
		{
			get
			{
				return TenantSettingSchema.E15Version;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000328A File Offset: 0x0000148A
		internal static CsvTable Schema
		{
			get
			{
				return TenantSettingSchema.schema;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003291 File Offset: 0x00001491
		internal static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return TenantSettingSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x040000F7 RID: 247
		private static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x040000F8 RID: 248
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			TenantSettingSchema.E15Version
		};

		// Token: 0x040000F9 RID: 249
		private static readonly CsvTable schema;
	}
}
