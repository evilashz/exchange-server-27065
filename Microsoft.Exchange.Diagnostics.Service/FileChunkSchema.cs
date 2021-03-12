using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000006 RID: 6
	public class FileChunkSchema : LogCsvSchema
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000399E File Offset: 0x00001B9E
		public override bool IsHeader(LogSourceLine line)
		{
			return line.Text.Equals("date-time,analyzer-name,output-format,data");
		}
	}
}
