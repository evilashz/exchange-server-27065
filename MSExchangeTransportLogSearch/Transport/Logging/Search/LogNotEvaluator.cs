using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000D RID: 13
	internal class LogNotEvaluator : LogUnaryEvaluator
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003B62 File Offset: 0x00001D62
		public override bool Evaluate(LogCursor row)
		{
			return !this.Condition.Evaluate(row);
		}
	}
}
