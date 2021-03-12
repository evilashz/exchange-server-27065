using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000009 RID: 9
	internal abstract class LogCompoundEvaluator : LogEvaluator
	{
		// Token: 0x04000027 RID: 39
		public readonly List<LogEvaluator> Conditions = new List<LogEvaluator>();
	}
}
