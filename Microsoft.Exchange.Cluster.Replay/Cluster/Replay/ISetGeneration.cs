using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetGeneration : IGetStatus
	{
		// Token: 0x06000A74 RID: 2676
		void SetCopyGeneration(long gen, DateTime writeTime);

		// Token: 0x06000A75 RID: 2677
		void SetInspectGeneration(long gen, DateTime writeTime);

		// Token: 0x06000A76 RID: 2678
		void SetCopyNotificationGeneration(long gen, DateTime writeTime);

		// Token: 0x06000A77 RID: 2679
		void SetLogStreamStartGeneration(long generation);

		// Token: 0x06000A78 RID: 2680
		void ClearLogStreamStartGeneration();

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A79 RID: 2681
		bool IsLogStreamStartGenerationResetPending { get; }
	}
}
