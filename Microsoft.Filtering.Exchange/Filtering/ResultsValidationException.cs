using System;
using Microsoft.Filtering.Results;

namespace Microsoft.Filtering
{
	// Token: 0x0200000C RID: 12
	public class ResultsValidationException : FilteringException
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002903 File Offset: 0x00000B03
		public ResultsValidationException(string message, FilteringResults results) : base(string.Format("{0}\r\n{1}", message, ResultsFormatter.ConsoleFormatter.Format(results)), null)
		{
		}
	}
}
