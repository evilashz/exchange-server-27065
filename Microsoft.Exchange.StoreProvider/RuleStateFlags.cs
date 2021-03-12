using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000242 RID: 578
	[Flags]
	internal enum RuleStateFlags
	{
		// Token: 0x04000FEB RID: 4075
		Enabled = 1,
		// Token: 0x04000FEC RID: 4076
		Error = 2,
		// Token: 0x04000FED RID: 4077
		OnlyWhenOOF = 4,
		// Token: 0x04000FEE RID: 4078
		KeepOOFHistory = 8,
		// Token: 0x04000FEF RID: 4079
		ExitAfterExecution = 16,
		// Token: 0x04000FF0 RID: 4080
		SkipIfSCLIsSafe = 32,
		// Token: 0x04000FF1 RID: 4081
		RuleParseError = 64,
		// Token: 0x04000FF2 RID: 4082
		LegacyOofRule = 128,
		// Token: 0x04000FF3 RID: 4083
		OnlyWhenOOFEx = 256,
		// Token: 0x04000FF4 RID: 4084
		TempDisabled = 1073741824,
		// Token: 0x04000FF5 RID: 4085
		ClearOOFHistory = -2147483648
	}
}
