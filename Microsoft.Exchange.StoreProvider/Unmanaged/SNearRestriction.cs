using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002D4 RID: 724
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SNearRestriction
	{
		// Token: 0x040011F6 RID: 4598
		internal int ulDistance;

		// Token: 0x040011F7 RID: 4599
		internal int ulOrdered;

		// Token: 0x040011F8 RID: 4600
		internal unsafe SRestriction* lpRes;
	}
}
