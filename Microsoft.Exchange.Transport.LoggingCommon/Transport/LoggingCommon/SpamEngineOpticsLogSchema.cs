using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000016 RID: 22
	internal static class SpamEngineOpticsLogSchema
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000303B File Offset: 0x0000123B
		public static CsvTable Schema
		{
			get
			{
				return SpamEngineOpticsLogSchema.schema;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003042 File Offset: 0x00001242
		public static CsvTable DefaultSchema
		{
			get
			{
				return SpamEngineOpticsLogSchema.schema;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003049 File Offset: 0x00001249
		public static Version DefaultVersion
		{
			get
			{
				return SpamEngineOpticsLogSchema.E15Version;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003050 File Offset: 0x00001250
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return SpamEngineOpticsLogSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x040000E6 RID: 230
		public const string Source = "source";

		// Token: 0x040000E7 RID: 231
		public const string EntityID = "entityt-id";

		// Token: 0x040000E8 RID: 232
		public const string Category = "category";

		// Token: 0x040000E9 RID: 233
		public const string CustomData = "custom-data";

		// Token: 0x040000EA RID: 234
		public static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x040000EB RID: 235
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			SpamEngineOpticsLogSchema.E15Version
		};

		// Token: 0x040000EC RID: 236
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("date-time", typeof(DateTime), SpamEngineOpticsLogSchema.E15Version),
			new CsvField("source", typeof(string), SpamEngineOpticsLogSchema.E15Version),
			new CsvField("entityt-id", typeof(string), SpamEngineOpticsLogSchema.E15Version),
			new CsvField("category", typeof(string), SpamEngineOpticsLogSchema.E15Version),
			new CsvField("custom-data", typeof(KeyValuePair<string, object>[]), SpamEngineOpticsLogSchema.E15Version)
		});
	}
}
