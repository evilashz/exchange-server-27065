using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010B RID: 267
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetViable
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A64 RID: 2660
		bool Viable { get; }

		// Token: 0x06000A65 RID: 2661
		void SetViable();

		// Token: 0x06000A66 RID: 2662
		void ClearViable();
	}
}
