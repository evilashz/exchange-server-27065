using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A7 RID: 679
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct STATUS_OBJECT_NOTIFICATION
	{
		// Token: 0x04001174 RID: 4468
		internal int cbEntryID;

		// Token: 0x04001175 RID: 4469
		internal IntPtr lpEntryID;

		// Token: 0x04001176 RID: 4470
		internal int cValues;

		// Token: 0x04001177 RID: 4471
		internal unsafe SPropValue* lpPropVals;
	}
}
