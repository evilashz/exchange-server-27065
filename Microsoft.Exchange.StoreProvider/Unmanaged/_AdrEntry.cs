using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000258 RID: 600
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _AdrEntry
	{
		// Token: 0x0400109F RID: 4255
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_AdrEntry));

		// Token: 0x040010A0 RID: 4256
		internal int ulReserved1;

		// Token: 0x040010A1 RID: 4257
		internal int cValues;

		// Token: 0x040010A2 RID: 4258
		internal unsafe SPropValue* pspva;
	}
}
