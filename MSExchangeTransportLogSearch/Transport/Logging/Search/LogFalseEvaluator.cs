using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000008 RID: 8
	internal class LogFalseEvaluator : LogEvaluator
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public override bool Evaluate(LogCursor row)
		{
			return false;
		}
	}
}
