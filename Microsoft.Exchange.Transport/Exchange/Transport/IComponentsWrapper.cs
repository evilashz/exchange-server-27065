using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000010 RID: 16
	internal interface IComponentsWrapper
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000055 RID: 85
		bool IsPaused { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86
		bool IsActive { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87
		bool IsShuttingDown { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000058 RID: 88
		bool IsBridgeHead { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000059 RID: 89
		object SyncRoot { get; }
	}
}
