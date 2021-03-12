using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002D7 RID: 727
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SPropertyRestriction
	{
		// Token: 0x040011FF RID: 4607
		internal int relop;

		// Token: 0x04001200 RID: 4608
		internal int ulPropTag;

		// Token: 0x04001201 RID: 4609
		internal unsafe SPropValue* lpProp;
	}
}
