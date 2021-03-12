using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000005 RID: 5
	internal static class AvEngineUpdateSchema
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002495 File Offset: 0x00000695
		public static int TimeStampFieldIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002498 File Offset: 0x00000698
		public static CsvTable DefaultSchema
		{
			get
			{
				return AvEngineUpdateSchema.schema;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000249F File Offset: 0x0000069F
		public static Version DefaultVersion
		{
			get
			{
				return AvEngineUpdateSchema.E15Version;
			}
		}

		// Token: 0x04000017 RID: 23
		private static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x04000018 RID: 24
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField(AvEngineUpdateLogLineFields.Timestamp.ToString(), typeof(DateTime), AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.EngineCategory.ToString(), typeof(string), true, AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.EngineName.ToString(), typeof(string), true, AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.EngineVersion.ToString(), typeof(string), true, AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.SignatureVersion.ToString(), typeof(string), true, AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.SignatureDateTime.ToString(), typeof(DateTime), true, AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.RUSId.ToString(), typeof(string), AvEngineUpdateSchema.E15Version),
			new CsvField(AvEngineUpdateLogLineFields.UpdatedDateTime.ToString(), typeof(DateTime), AvEngineUpdateSchema.E15Version)
		});
	}
}
