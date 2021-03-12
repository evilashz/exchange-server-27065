using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000015 RID: 21
	internal abstract class LogBinaryOperatorEvaluator : LogEvaluator
	{
		// Token: 0x04000030 RID: 48
		public LogOperandEvaluator Left;

		// Token: 0x04000031 RID: 49
		public LogOperandEvaluator Right;
	}
}
