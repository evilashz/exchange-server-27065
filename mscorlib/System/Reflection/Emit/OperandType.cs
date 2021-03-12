using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200062C RID: 1580
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum OperandType
	{
		// Token: 0x040020B3 RID: 8371
		[__DynamicallyInvokable]
		InlineBrTarget,
		// Token: 0x040020B4 RID: 8372
		[__DynamicallyInvokable]
		InlineField,
		// Token: 0x040020B5 RID: 8373
		[__DynamicallyInvokable]
		InlineI,
		// Token: 0x040020B6 RID: 8374
		[__DynamicallyInvokable]
		InlineI8,
		// Token: 0x040020B7 RID: 8375
		[__DynamicallyInvokable]
		InlineMethod,
		// Token: 0x040020B8 RID: 8376
		[__DynamicallyInvokable]
		InlineNone,
		// Token: 0x040020B9 RID: 8377
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		// Token: 0x040020BA RID: 8378
		[__DynamicallyInvokable]
		InlineR,
		// Token: 0x040020BB RID: 8379
		[__DynamicallyInvokable]
		InlineSig = 9,
		// Token: 0x040020BC RID: 8380
		[__DynamicallyInvokable]
		InlineString,
		// Token: 0x040020BD RID: 8381
		[__DynamicallyInvokable]
		InlineSwitch,
		// Token: 0x040020BE RID: 8382
		[__DynamicallyInvokable]
		InlineTok,
		// Token: 0x040020BF RID: 8383
		[__DynamicallyInvokable]
		InlineType,
		// Token: 0x040020C0 RID: 8384
		[__DynamicallyInvokable]
		InlineVar,
		// Token: 0x040020C1 RID: 8385
		[__DynamicallyInvokable]
		ShortInlineBrTarget,
		// Token: 0x040020C2 RID: 8386
		[__DynamicallyInvokable]
		ShortInlineI,
		// Token: 0x040020C3 RID: 8387
		[__DynamicallyInvokable]
		ShortInlineR,
		// Token: 0x040020C4 RID: 8388
		[__DynamicallyInvokable]
		ShortInlineVar
	}
}
