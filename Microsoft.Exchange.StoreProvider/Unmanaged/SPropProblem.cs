using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002D0 RID: 720
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SPropProblem
	{
		// Token: 0x040011EC RID: 4588
		internal int ulIndex;

		// Token: 0x040011ED RID: 4589
		internal int ulPropTag;

		// Token: 0x040011EE RID: 4590
		internal int scode;
	}
}
