using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000007 RID: 7
	internal class LogTrueEvaluator : LogEvaluator
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003AA9 File Offset: 0x00001CA9
		public override bool Evaluate(LogCursor row)
		{
			return true;
		}
	}
}
