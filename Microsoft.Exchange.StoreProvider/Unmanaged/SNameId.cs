using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CF RID: 719
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SNameId
	{
		// Token: 0x040011E8 RID: 4584
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SNameId));

		// Token: 0x040011E9 RID: 4585
		internal IntPtr lpGuid;

		// Token: 0x040011EA RID: 4586
		internal int ulKind;

		// Token: 0x040011EB RID: 4587
		internal SUnionNameId union;
	}
}
