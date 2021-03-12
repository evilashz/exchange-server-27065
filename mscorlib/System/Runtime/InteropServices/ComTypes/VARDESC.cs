using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A19 RID: 2585
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04002D01 RID: 11521
		[__DynamicallyInvokable]
		public int memid;

		// Token: 0x04002D02 RID: 11522
		[__DynamicallyInvokable]
		public string lpstrSchema;

		// Token: 0x04002D03 RID: 11523
		[__DynamicallyInvokable]
		public VARDESC.DESCUNION desc;

		// Token: 0x04002D04 RID: 11524
		[__DynamicallyInvokable]
		public ELEMDESC elemdescVar;

		// Token: 0x04002D05 RID: 11525
		[__DynamicallyInvokable]
		public short wVarFlags;

		// Token: 0x04002D06 RID: 11526
		[__DynamicallyInvokable]
		public VARKIND varkind;

		// Token: 0x02000C76 RID: 3190
		[__DynamicallyInvokable]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x040037A8 RID: 14248
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x040037A9 RID: 14249
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
