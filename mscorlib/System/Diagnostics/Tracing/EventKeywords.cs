using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200040C RID: 1036
	[Flags]
	[__DynamicallyInvokable]
	public enum EventKeywords : long
	{
		// Token: 0x04001748 RID: 5960
		[__DynamicallyInvokable]
		None = 0L,
		// Token: 0x04001749 RID: 5961
		[__DynamicallyInvokable]
		All = -1L,
		// Token: 0x0400174A RID: 5962
		MicrosoftTelemetry = 562949953421312L,
		// Token: 0x0400174B RID: 5963
		[__DynamicallyInvokable]
		WdiContext = 562949953421312L,
		// Token: 0x0400174C RID: 5964
		[__DynamicallyInvokable]
		WdiDiagnostic = 1125899906842624L,
		// Token: 0x0400174D RID: 5965
		[__DynamicallyInvokable]
		Sqm = 2251799813685248L,
		// Token: 0x0400174E RID: 5966
		[__DynamicallyInvokable]
		AuditFailure = 4503599627370496L,
		// Token: 0x0400174F RID: 5967
		[__DynamicallyInvokable]
		AuditSuccess = 9007199254740992L,
		// Token: 0x04001750 RID: 5968
		[__DynamicallyInvokable]
		CorrelationHint = 4503599627370496L,
		// Token: 0x04001751 RID: 5969
		[__DynamicallyInvokable]
		EventLogClassic = 36028797018963968L
	}
}
