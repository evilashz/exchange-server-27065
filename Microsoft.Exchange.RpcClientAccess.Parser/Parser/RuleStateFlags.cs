using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000304 RID: 772
	[Flags]
	internal enum RuleStateFlags
	{
		// Token: 0x040009C2 RID: 2498
		Enabled = 1,
		// Token: 0x040009C3 RID: 2499
		ProcessingError = 2,
		// Token: 0x040009C4 RID: 2500
		OnlyEnabledWhenOOF = 4,
		// Token: 0x040009C5 RID: 2501
		KeepOOFHistoryList = 8,
		// Token: 0x040009C6 RID: 2502
		TerminateAfterExecution = 16,
		// Token: 0x040009C7 RID: 2503
		SkipRuleIfNotSpam = 32,
		// Token: 0x040009C8 RID: 2504
		ParsingError = 64
	}
}
