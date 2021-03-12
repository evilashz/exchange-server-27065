using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002DE RID: 734
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct SUnionRestriction
	{
		// Token: 0x04001212 RID: 4626
		[FieldOffset(0)]
		internal SComparePropsRestriction resCompareProps;

		// Token: 0x04001213 RID: 4627
		[FieldOffset(0)]
		internal SAndOrNotRestriction resAnd;

		// Token: 0x04001214 RID: 4628
		[FieldOffset(0)]
		internal SContentRestriction resContent;

		// Token: 0x04001215 RID: 4629
		[FieldOffset(0)]
		internal SPropertyRestriction resProperty;

		// Token: 0x04001216 RID: 4630
		[FieldOffset(0)]
		internal SBitMaskRestriction resBitMask;

		// Token: 0x04001217 RID: 4631
		[FieldOffset(0)]
		internal SSizeRestriction resSize;

		// Token: 0x04001218 RID: 4632
		[FieldOffset(0)]
		internal SExistRestriction resExist;

		// Token: 0x04001219 RID: 4633
		[FieldOffset(0)]
		internal SSubRestriction resSub;

		// Token: 0x0400121A RID: 4634
		[FieldOffset(0)]
		internal SCommentRestriction resComment;

		// Token: 0x0400121B RID: 4635
		[FieldOffset(0)]
		internal SCountRestriction resCount;

		// Token: 0x0400121C RID: 4636
		[FieldOffset(0)]
		internal SNearRestriction resNear;
	}
}
