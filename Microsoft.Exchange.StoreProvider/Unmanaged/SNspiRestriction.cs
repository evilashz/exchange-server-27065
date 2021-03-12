using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E2 RID: 738
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SNspiRestriction
	{
		// Token: 0x04001226 RID: 4646
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SNspiRestriction));

		// Token: 0x04001227 RID: 4647
		internal int rt;

		// Token: 0x04001228 RID: 4648
		internal SNspiUnionRestriction union;
	}
}
