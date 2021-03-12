using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C03 RID: 3075
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RuleStatistics
	{
		// Token: 0x06006DCD RID: 28109 RVA: 0x001D6C7E File Offset: 0x001D4E7E
		public static void LogException(Exception e)
		{
			RuleStatistics.exceptionCounter.IncrementCounter(e.ToString());
		}

		// Token: 0x04003E62 RID: 15970
		private static CounterDictionary<string> exceptionCounter = new CounterDictionary<string>(20, 2000);
	}
}
