using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002DD RID: 733
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SCommentRestriction
	{
		// Token: 0x0400120F RID: 4623
		internal int cValues;

		// Token: 0x04001210 RID: 4624
		internal unsafe SRestriction* lpRes;

		// Token: 0x04001211 RID: 4625
		internal unsafe SPropValue* lpProp;
	}
}
