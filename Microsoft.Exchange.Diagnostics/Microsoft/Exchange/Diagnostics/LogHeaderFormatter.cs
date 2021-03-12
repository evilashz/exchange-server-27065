using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C3 RID: 451
	public class LogHeaderFormatter
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x0002E8AD File Offset: 0x0002CAAD
		public LogHeaderFormatter(LogSchema schema) : this(schema, LogHeaderCsvOption.NotCsvCompatible)
		{
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002E8B7 File Offset: 0x0002CAB7
		public LogHeaderFormatter(LogSchema schema, bool csvCompatible) : this(schema, csvCompatible ? LogHeaderCsvOption.CsvCompatible : LogHeaderCsvOption.NotCsvCompatible)
		{
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002E8C7 File Offset: 0x0002CAC7
		public LogHeaderFormatter(LogSchema schema, LogHeaderCsvOption csvOption)
		{
			this.schema = schema;
			this.CsvOption = csvOption;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002E8DD File Offset: 0x0002CADD
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0002E8E5 File Offset: 0x0002CAE5
		public LogHeaderCsvOption CsvOption { get; private set; }

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		internal void Write(Stream output, DateTime date)
		{
			if (output.Position == 0L)
			{
				if (this.CsvOption != LogHeaderCsvOption.NotCsvCompatible)
				{
					Utf8Csv.WriteHeaderRow(output, this.schema.Fields);
				}
				else
				{
					Utf8Csv.WriteBom(output);
				}
			}
			if (this.CsvOption != LogHeaderCsvOption.CsvStrict)
			{
				Utf8Csv.EncodeAndWrite(output, "#Software: ");
				Utf8Csv.EncodeAndWriteLine(output, this.schema.Software);
				Utf8Csv.EncodeAndWrite(output, "#Version: ");
				Utf8Csv.EncodeAndWriteLine(output, this.schema.Version);
				Utf8Csv.EncodeAndWrite(output, "#Log-type: ");
				Utf8Csv.EncodeAndWriteLine(output, this.schema.LogType);
				Utf8Csv.EncodeAndWrite(output, "#Date: ");
				Utf8Csv.EncodeAndWriteLine(output, date.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo));
				Utf8Csv.EncodeAndWrite(output, "#Fields: ");
				Utf8Csv.WriteHeaderRow(output, this.schema.Fields);
			}
		}

		// Token: 0x04000964 RID: 2404
		public const string DateTimeFormatSpecifier = "yyyy-MM-ddTHH\\:mm\\:ss.fffZ";

		// Token: 0x04000965 RID: 2405
		public const string TimeSpanFormatSpecifier = "g";

		// Token: 0x04000966 RID: 2406
		private LogSchema schema;
	}
}
