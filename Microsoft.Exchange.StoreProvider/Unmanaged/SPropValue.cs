using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002D2 RID: 722
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct SPropValue
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x00037FCA File Offset: 0x000361CA
		internal static PropValue Unmarshal(IntPtr item)
		{
			return SPropValue.Unmarshal(item, false);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00037FD3 File Offset: 0x000361D3
		internal static PropValue Unmarshal(IntPtr item, bool retainAnsiStrings)
		{
			return PropValue.Unmarshal(item, retainAnsiStrings);
		}

		// Token: 0x040011F1 RID: 4593
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SPropValue));

		// Token: 0x040011F2 RID: 4594
		[FieldOffset(0)]
		internal int ulPropTag;

		// Token: 0x040011F3 RID: 4595
		[FieldOffset(8)]
		internal _PV value;
	}
}
