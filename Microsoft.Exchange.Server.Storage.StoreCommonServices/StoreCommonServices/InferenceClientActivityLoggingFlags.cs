using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000059 RID: 89
	[Flags]
	public enum InferenceClientActivityLoggingFlags : uint
	{
		// Token: 0x040002F6 RID: 758
		None = 0U,
		// Token: 0x040002F7 RID: 759
		EnabledOWALogging = 1U,
		// Token: 0x040002F8 RID: 760
		EnabledOLKLogging = 2U
	}
}
