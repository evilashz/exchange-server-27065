using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E5 RID: 741
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SSortOrder
	{
		// Token: 0x04001233 RID: 4659
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SSortOrder));

		// Token: 0x04001234 RID: 4660
		internal int ulPropTag;

		// Token: 0x04001235 RID: 4661
		internal int ulOrder;
	}
}
