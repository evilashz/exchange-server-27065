using System;

namespace System.Reflection
{
	// Token: 0x020005CA RID: 1482
	[Flags]
	[Serializable]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x04001C4A RID: 7242
		CallConvMask = 15,
		// Token: 0x04001C4B RID: 7243
		Default = 0,
		// Token: 0x04001C4C RID: 7244
		C = 1,
		// Token: 0x04001C4D RID: 7245
		StdCall = 2,
		// Token: 0x04001C4E RID: 7246
		ThisCall = 3,
		// Token: 0x04001C4F RID: 7247
		FastCall = 4,
		// Token: 0x04001C50 RID: 7248
		Vararg = 5,
		// Token: 0x04001C51 RID: 7249
		Field = 6,
		// Token: 0x04001C52 RID: 7250
		LocalSig = 7,
		// Token: 0x04001C53 RID: 7251
		Property = 8,
		// Token: 0x04001C54 RID: 7252
		Unmgd = 9,
		// Token: 0x04001C55 RID: 7253
		GenericInst = 10,
		// Token: 0x04001C56 RID: 7254
		Generic = 16,
		// Token: 0x04001C57 RID: 7255
		HasThis = 32,
		// Token: 0x04001C58 RID: 7256
		ExplicitThis = 64
	}
}
