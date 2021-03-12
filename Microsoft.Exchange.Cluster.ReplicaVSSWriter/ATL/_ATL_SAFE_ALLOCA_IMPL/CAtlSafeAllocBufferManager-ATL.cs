using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL._ATL_SAFE_ALLOCA_IMPL
{
	// Token: 0x020001A1 RID: 417
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	internal struct CAtlSafeAllocBufferManager<ATL::CCRTAllocator>
	{
		// Token: 0x04000328 RID: 808
		private long <alignment\u0020member>;

		// Token: 0x020001A2 RID: 418
		[UnsafeValueType]
		[NativeCppClass]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		internal struct CAtlSafeAllocBufferNode
		{
			// Token: 0x04000329 RID: 809
			private long <alignment\u0020member>;
		}
	}
}
