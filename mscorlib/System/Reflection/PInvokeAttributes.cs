using System;

namespace System.Reflection
{
	// Token: 0x020005CB RID: 1483
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04001C5A RID: 7258
		NoMangle = 1,
		// Token: 0x04001C5B RID: 7259
		CharSetMask = 6,
		// Token: 0x04001C5C RID: 7260
		CharSetNotSpec = 0,
		// Token: 0x04001C5D RID: 7261
		CharSetAnsi = 2,
		// Token: 0x04001C5E RID: 7262
		CharSetUnicode = 4,
		// Token: 0x04001C5F RID: 7263
		CharSetAuto = 6,
		// Token: 0x04001C60 RID: 7264
		BestFitUseAssem = 0,
		// Token: 0x04001C61 RID: 7265
		BestFitEnabled = 16,
		// Token: 0x04001C62 RID: 7266
		BestFitDisabled = 32,
		// Token: 0x04001C63 RID: 7267
		BestFitMask = 48,
		// Token: 0x04001C64 RID: 7268
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04001C65 RID: 7269
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04001C66 RID: 7270
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04001C67 RID: 7271
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04001C68 RID: 7272
		SupportsLastError = 64,
		// Token: 0x04001C69 RID: 7273
		CallConvMask = 1792,
		// Token: 0x04001C6A RID: 7274
		CallConvWinapi = 256,
		// Token: 0x04001C6B RID: 7275
		CallConvCdecl = 512,
		// Token: 0x04001C6C RID: 7276
		CallConvStdcall = 768,
		// Token: 0x04001C6D RID: 7277
		CallConvThiscall = 1024,
		// Token: 0x04001C6E RID: 7278
		CallConvFastcall = 1280,
		// Token: 0x04001C6F RID: 7279
		MaxValue = 65535
	}
}
