using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000018 RID: 24
	public class LogHeaderFormatter
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public LogHeaderFormatter(LogSchema schema) : this(schema, LogHeaderCsvOption.NotCsvCompatible)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public LogHeaderFormatter(LogSchema schema, bool csvCompatible) : this(schema, csvCompatible ? LogHeaderCsvOption.CsvCompatible : LogHeaderCsvOption.NotCsvCompatible)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public LogHeaderFormatter(LogSchema schema, LogHeaderCsvOption csvOption)
		{
			this.logHeaderFormatterImpl = new LogHeaderFormatter(schema.LogSchemaImpl, (LogHeaderCsvOption)csvOption);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002DDA File Offset: 0x00000FDA
		public LogHeaderCsvOption CsvOption
		{
			get
			{
				return (LogHeaderCsvOption)this.logHeaderFormatterImpl.CsvOption;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002DE7 File Offset: 0x00000FE7
		internal LogHeaderFormatter LogHeaderFormatterImpl
		{
			get
			{
				return this.logHeaderFormatterImpl;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002DEF File Offset: 0x00000FEF
		internal void Write(Stream output, DateTime date)
		{
			this.logHeaderFormatterImpl.Write(output, date);
		}

		// Token: 0x0400003D RID: 61
		public const string DateTimeFormatSpecifier = "yyyy-MM-ddTHH\\:mm\\:ss.fffZ";

		// Token: 0x0400003E RID: 62
		public const string TimeSpanFormatSpecifier = "g";

		// Token: 0x0400003F RID: 63
		private LogHeaderFormatter logHeaderFormatterImpl;
	}
}
