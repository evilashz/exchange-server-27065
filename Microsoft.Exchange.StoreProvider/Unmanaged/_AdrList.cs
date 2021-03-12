using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000259 RID: 601
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _AdrList
	{
		// Token: 0x040010A3 RID: 4259
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_AdrList));

		// Token: 0x040010A4 RID: 4260
		internal int cEntries;

		// Token: 0x040010A5 RID: 4261
		internal _AdrEntry adrEntry1;
	}
}
