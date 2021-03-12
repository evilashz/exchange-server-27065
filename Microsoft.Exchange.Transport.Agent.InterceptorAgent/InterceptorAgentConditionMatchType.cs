using System;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000E RID: 14
	public enum InterceptorAgentConditionMatchType
	{
		// Token: 0x04000044 RID: 68
		CaseInsensitive,
		// Token: 0x04000045 RID: 69
		CaseSensitive,
		// Token: 0x04000046 RID: 70
		CaseSensitiveEqual,
		// Token: 0x04000047 RID: 71
		CaseInsensitiveEqual,
		// Token: 0x04000048 RID: 72
		CaseSensitiveNotEqual,
		// Token: 0x04000049 RID: 73
		CaseInsensitiveNotEqual,
		// Token: 0x0400004A RID: 74
		Regex,
		// Token: 0x0400004B RID: 75
		PatternMatch,
		// Token: 0x0400004C RID: 76
		GreaterThan,
		// Token: 0x0400004D RID: 77
		GreaterThanOrEqual,
		// Token: 0x0400004E RID: 78
		LessThan,
		// Token: 0x0400004F RID: 79
		LessThanOrEqual
	}
}
