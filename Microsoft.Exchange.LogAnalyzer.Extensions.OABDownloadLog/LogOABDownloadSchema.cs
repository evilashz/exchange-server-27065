using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog
{
	// Token: 0x02000005 RID: 5
	public class LogOABDownloadSchema : LogCsvSchema
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023BC File Offset: 0x000005BC
		public override bool IsHeader(LogSourceLine line)
		{
			return line.Text.StartsWith("DateTime", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
