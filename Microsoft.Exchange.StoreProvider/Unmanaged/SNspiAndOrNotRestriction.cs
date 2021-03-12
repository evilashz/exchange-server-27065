using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E0 RID: 736
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SNspiAndOrNotRestriction
	{
		// Token: 0x04001220 RID: 4640
		internal int cRes;

		// Token: 0x04001221 RID: 4641
		internal unsafe SNspiRestriction* lpRes;
	}
}
