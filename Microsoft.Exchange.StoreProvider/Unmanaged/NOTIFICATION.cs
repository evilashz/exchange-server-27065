using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AA RID: 682
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct NOTIFICATION
	{
		// Token: 0x04001182 RID: 4482
		internal int ulEventType;

		// Token: 0x04001183 RID: 4483
		internal int ulAlignPad;

		// Token: 0x04001184 RID: 4484
		internal NOTIFICATION_UNION info;
	}
}
