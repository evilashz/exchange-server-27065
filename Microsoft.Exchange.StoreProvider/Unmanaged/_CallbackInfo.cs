using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025A RID: 602
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _CallbackInfo
	{
		// Token: 0x040010A6 RID: 4262
		internal int cb;

		// Token: 0x040010A7 RID: 4263
		internal unsafe byte* pb;

		// Token: 0x040010A8 RID: 4264
		internal long id;
	}
}
