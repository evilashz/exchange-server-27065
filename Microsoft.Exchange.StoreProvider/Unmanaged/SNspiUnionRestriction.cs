using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E1 RID: 737
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct SNspiUnionRestriction
	{
		// Token: 0x04001222 RID: 4642
		[FieldOffset(0)]
		internal SNspiAndOrNotRestriction resAnd;

		// Token: 0x04001223 RID: 4643
		[FieldOffset(0)]
		internal SContentRestriction resContent;

		// Token: 0x04001224 RID: 4644
		[FieldOffset(0)]
		internal SPropertyRestriction resProperty;

		// Token: 0x04001225 RID: 4645
		[FieldOffset(0)]
		internal SExistRestriction resExist;
	}
}
