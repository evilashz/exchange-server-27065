using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CE RID: 718
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct SUnionNameId
	{
		// Token: 0x040011E6 RID: 4582
		[FieldOffset(0)]
		internal IntPtr lpStr;

		// Token: 0x040011E7 RID: 4583
		[FieldOffset(0)]
		internal int id;
	}
}
