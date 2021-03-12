using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Filtering
{
	// Token: 0x02000017 RID: 23
	internal static class TextExtractionSchema
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000301F File Offset: 0x0000121F
		public static int TimeStampFieldIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003022 File Offset: 0x00001222
		public static CsvTable DefaultSchema
		{
			get
			{
				return TextExtractionSchema.schema;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003029 File Offset: 0x00001229
		public static Version DefaultVersion
		{
			get
			{
				return TextExtractionSchema.E15Version;
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x0400003A RID: 58
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField(TextExtractionLogLineFields.Timestamp.ToString(), typeof(DateTime), TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.ExMessageId.ToString(), typeof(string), TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.StreamId.ToString(), typeof(int), TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.StreamSize.ToString(), typeof(long), TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.ParentId.ToString(), typeof(int), TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeTypes.ToString(), typeof(string), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeModuleUsed.ToString(), typeof(string), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeResult.ToString(), typeof(int), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeSkippedModules.ToString(), typeof(string), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeFailedModules.ToString(), typeof(string), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.TeDisabledModules.ToString(), typeof(string), true, TextExtractionSchema.E15Version),
			new CsvField(TextExtractionLogLineFields.AdditionalInformation.ToString(), typeof(string), true, TextExtractionSchema.E15Version)
		});
	}
}
