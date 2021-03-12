using System;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200000D RID: 13
	public enum TraceType
	{
		// Token: 0x04000017 RID: 23
		None,
		// Token: 0x04000018 RID: 24
		DebugTrace,
		// Token: 0x04000019 RID: 25
		Debug = 1,
		// Token: 0x0400001A RID: 26
		Warning,
		// Token: 0x0400001B RID: 27
		WarningTrace = 2,
		// Token: 0x0400001C RID: 28
		Error,
		// Token: 0x0400001D RID: 29
		ErrorTrace = 3,
		// Token: 0x0400001E RID: 30
		FatalTrace,
		// Token: 0x0400001F RID: 31
		Fatal = 4,
		// Token: 0x04000020 RID: 32
		Info,
		// Token: 0x04000021 RID: 33
		InfoTrace = 5,
		// Token: 0x04000022 RID: 34
		Performance,
		// Token: 0x04000023 RID: 35
		PerformanceTrace = 6,
		// Token: 0x04000024 RID: 36
		Function,
		// Token: 0x04000025 RID: 37
		FunctionTrace = 7,
		// Token: 0x04000026 RID: 38
		PfdTrace,
		// Token: 0x04000027 RID: 39
		Pfd = 8,
		// Token: 0x04000028 RID: 40
		FaultI,
		// Token: 0x04000029 RID: 41
		FaultInjection = 9
	}
}
