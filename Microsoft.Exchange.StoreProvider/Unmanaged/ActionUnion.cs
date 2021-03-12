using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000264 RID: 612
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct ActionUnion
	{
		// Token: 0x040010CE RID: 4302
		[FieldOffset(0)]
		internal _MoveCopyAction actMoveCopy;

		// Token: 0x040010CF RID: 4303
		[FieldOffset(0)]
		internal _ReplyAction actReply;

		// Token: 0x040010D0 RID: 4304
		[FieldOffset(0)]
		internal _DeferAction actDeferAction;

		// Token: 0x040010D1 RID: 4305
		[FieldOffset(0)]
		internal uint scBounceCode;

		// Token: 0x040010D2 RID: 4306
		[FieldOffset(0)]
		internal unsafe _AdrList* lpadrlist;

		// Token: 0x040010D3 RID: 4307
		[FieldOffset(0)]
		internal SPropValue propTag;
	}
}
