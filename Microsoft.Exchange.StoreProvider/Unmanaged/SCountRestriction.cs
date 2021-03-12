using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002DB RID: 731
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SCountRestriction
	{
		// Token: 0x0400120B RID: 4619
		internal int ulCount;

		// Token: 0x0400120C RID: 4620
		internal unsafe SRestriction* lpRes;
	}
}
