using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002DF RID: 735
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SRestriction
	{
		// Token: 0x0400121D RID: 4637
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SRestriction));

		// Token: 0x0400121E RID: 4638
		internal int rt;

		// Token: 0x0400121F RID: 4639
		internal SUnionRestriction union;
	}
}
