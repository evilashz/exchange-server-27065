using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000267 RID: 615
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct CHECKPOINTSTATUSRAW
	{
		// Token: 0x040010E0 RID: 4320
		internal Guid guidMdb;

		// Token: 0x040010E1 RID: 4321
		internal uint ulBeginCheckpointDepth;

		// Token: 0x040010E2 RID: 4322
		internal uint ulEndCheckpointDepth;
	}
}
