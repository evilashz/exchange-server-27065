using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000012 RID: 18
	internal abstract class LogQuantifierEvaluator : LogUnaryEvaluator
	{
		// Token: 0x0400002E RID: 46
		public LogVariableEvaluator Variable;

		// Token: 0x0400002F RID: 47
		public int FieldIndex = -1;
	}
}
