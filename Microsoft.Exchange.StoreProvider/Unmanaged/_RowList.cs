using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025E RID: 606
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _RowList
	{
		// Token: 0x040010B8 RID: 4280
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_RowList));

		// Token: 0x040010B9 RID: 4281
		internal int cEntries;

		// Token: 0x040010BA RID: 4282
		internal _RowEntry aEntries;
	}
}
