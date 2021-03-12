using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000CB RID: 203
	internal enum WatsonIssueType
	{
		// Token: 0x04000419 RID: 1049
		GenericReport,
		// Token: 0x0400041A RID: 1050
		NativeCodeCrash,
		// Token: 0x0400041B RID: 1051
		ScriptError,
		// Token: 0x0400041C RID: 1052
		ManagedCodeIISException,
		// Token: 0x0400041D RID: 1053
		ManagedCodeException,
		// Token: 0x0400041E RID: 1054
		ManagedCodeDisposableLeak,
		// Token: 0x0400041F RID: 1055
		ManagedCodeLatencyIssue,
		// Token: 0x04000420 RID: 1056
		ManagedCodeTroubleshootingIssue
	}
}
