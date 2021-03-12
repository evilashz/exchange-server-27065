using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000012 RID: 18
	[Flags]
	public enum LogLevel
	{
		// Token: 0x0400003C RID: 60
		LogNone = 0,
		// Token: 0x0400003D RID: 61
		LogVerbose = 1,
		// Token: 0x0400003E RID: 62
		LogDebug = 2,
		// Token: 0x0400003F RID: 63
		LogTrace = 4,
		// Token: 0x04000040 RID: 64
		LogInfo = 8,
		// Token: 0x04000041 RID: 65
		LogWarn = 16,
		// Token: 0x04000042 RID: 66
		LogError = 32,
		// Token: 0x04000043 RID: 67
		LogFatal = 64,
		// Token: 0x04000044 RID: 68
		LogDefault = 126,
		// Token: 0x04000045 RID: 69
		LogAll = 127,
		// Token: 0x04000046 RID: 70
		LogSerious = 112
	}
}
