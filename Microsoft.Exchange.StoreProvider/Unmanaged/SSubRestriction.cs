using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002DC RID: 732
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SSubRestriction
	{
		// Token: 0x0400120D RID: 4621
		internal int ulSubObject;

		// Token: 0x0400120E RID: 4622
		internal unsafe SRestriction* lpRes;
	}
}
