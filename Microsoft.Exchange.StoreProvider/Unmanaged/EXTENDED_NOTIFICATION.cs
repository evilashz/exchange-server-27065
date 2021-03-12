using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A6 RID: 678
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct EXTENDED_NOTIFICATION
	{
		// Token: 0x04001171 RID: 4465
		internal int ulEvent;

		// Token: 0x04001172 RID: 4466
		internal int cb;

		// Token: 0x04001173 RID: 4467
		internal IntPtr pbEventParameters;
	}
}
