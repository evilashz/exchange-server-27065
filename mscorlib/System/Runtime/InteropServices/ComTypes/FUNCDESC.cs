using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A11 RID: 2577
	[__DynamicallyInvokable]
	public struct FUNCDESC
	{
		// Token: 0x04002CD9 RID: 11481
		[__DynamicallyInvokable]
		public int memid;

		// Token: 0x04002CDA RID: 11482
		public IntPtr lprgscode;

		// Token: 0x04002CDB RID: 11483
		public IntPtr lprgelemdescParam;

		// Token: 0x04002CDC RID: 11484
		[__DynamicallyInvokable]
		public FUNCKIND funckind;

		// Token: 0x04002CDD RID: 11485
		[__DynamicallyInvokable]
		public INVOKEKIND invkind;

		// Token: 0x04002CDE RID: 11486
		[__DynamicallyInvokable]
		public CALLCONV callconv;

		// Token: 0x04002CDF RID: 11487
		[__DynamicallyInvokable]
		public short cParams;

		// Token: 0x04002CE0 RID: 11488
		[__DynamicallyInvokable]
		public short cParamsOpt;

		// Token: 0x04002CE1 RID: 11489
		[__DynamicallyInvokable]
		public short oVft;

		// Token: 0x04002CE2 RID: 11490
		[__DynamicallyInvokable]
		public short cScodes;

		// Token: 0x04002CE3 RID: 11491
		[__DynamicallyInvokable]
		public ELEMDESC elemdescFunc;

		// Token: 0x04002CE4 RID: 11492
		[__DynamicallyInvokable]
		public short wFuncFlags;
	}
}
