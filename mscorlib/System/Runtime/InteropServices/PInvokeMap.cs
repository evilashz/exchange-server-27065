using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000926 RID: 2342
	[Serializable]
	internal enum PInvokeMap
	{
		// Token: 0x04002AAC RID: 10924
		NoMangle = 1,
		// Token: 0x04002AAD RID: 10925
		CharSetMask = 6,
		// Token: 0x04002AAE RID: 10926
		CharSetNotSpec = 0,
		// Token: 0x04002AAF RID: 10927
		CharSetAnsi = 2,
		// Token: 0x04002AB0 RID: 10928
		CharSetUnicode = 4,
		// Token: 0x04002AB1 RID: 10929
		CharSetAuto = 6,
		// Token: 0x04002AB2 RID: 10930
		PinvokeOLE = 32,
		// Token: 0x04002AB3 RID: 10931
		SupportsLastError = 64,
		// Token: 0x04002AB4 RID: 10932
		BestFitMask = 48,
		// Token: 0x04002AB5 RID: 10933
		BestFitEnabled = 16,
		// Token: 0x04002AB6 RID: 10934
		BestFitDisabled = 32,
		// Token: 0x04002AB7 RID: 10935
		BestFitUseAsm = 48,
		// Token: 0x04002AB8 RID: 10936
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04002AB9 RID: 10937
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04002ABA RID: 10938
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04002ABB RID: 10939
		ThrowOnUnmappableCharUseAsm = 12288,
		// Token: 0x04002ABC RID: 10940
		CallConvMask = 1792,
		// Token: 0x04002ABD RID: 10941
		CallConvWinapi = 256,
		// Token: 0x04002ABE RID: 10942
		CallConvCdecl = 512,
		// Token: 0x04002ABF RID: 10943
		CallConvStdcall = 768,
		// Token: 0x04002AC0 RID: 10944
		CallConvThiscall = 1024,
		// Token: 0x04002AC1 RID: 10945
		CallConvFastcall = 1280
	}
}
