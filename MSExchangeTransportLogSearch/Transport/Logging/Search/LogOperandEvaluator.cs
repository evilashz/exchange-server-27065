using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000E RID: 14
	internal abstract class LogOperandEvaluator
	{
		// Token: 0x0600003D RID: 61
		public abstract object GetValue(LogCursor row);

		// Token: 0x0600003E RID: 62
		public abstract Type GetValueType(CsvTable table);

		// Token: 0x04000029 RID: 41
		public bool AssumeUppercase;
	}
}
