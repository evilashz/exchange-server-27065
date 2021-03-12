using System;

namespace System.Reflection
{
	// Token: 0x020005C9 RID: 1481
	[Serializable]
	internal enum CorElementType : byte
	{
		// Token: 0x04001C25 RID: 7205
		End,
		// Token: 0x04001C26 RID: 7206
		Void,
		// Token: 0x04001C27 RID: 7207
		Boolean,
		// Token: 0x04001C28 RID: 7208
		Char,
		// Token: 0x04001C29 RID: 7209
		I1,
		// Token: 0x04001C2A RID: 7210
		U1,
		// Token: 0x04001C2B RID: 7211
		I2,
		// Token: 0x04001C2C RID: 7212
		U2,
		// Token: 0x04001C2D RID: 7213
		I4,
		// Token: 0x04001C2E RID: 7214
		U4,
		// Token: 0x04001C2F RID: 7215
		I8,
		// Token: 0x04001C30 RID: 7216
		U8,
		// Token: 0x04001C31 RID: 7217
		R4,
		// Token: 0x04001C32 RID: 7218
		R8,
		// Token: 0x04001C33 RID: 7219
		String,
		// Token: 0x04001C34 RID: 7220
		Ptr,
		// Token: 0x04001C35 RID: 7221
		ByRef,
		// Token: 0x04001C36 RID: 7222
		ValueType,
		// Token: 0x04001C37 RID: 7223
		Class,
		// Token: 0x04001C38 RID: 7224
		Var,
		// Token: 0x04001C39 RID: 7225
		Array,
		// Token: 0x04001C3A RID: 7226
		GenericInst,
		// Token: 0x04001C3B RID: 7227
		TypedByRef,
		// Token: 0x04001C3C RID: 7228
		I = 24,
		// Token: 0x04001C3D RID: 7229
		U,
		// Token: 0x04001C3E RID: 7230
		FnPtr = 27,
		// Token: 0x04001C3F RID: 7231
		Object,
		// Token: 0x04001C40 RID: 7232
		SzArray,
		// Token: 0x04001C41 RID: 7233
		MVar,
		// Token: 0x04001C42 RID: 7234
		CModReqd,
		// Token: 0x04001C43 RID: 7235
		CModOpt,
		// Token: 0x04001C44 RID: 7236
		Internal,
		// Token: 0x04001C45 RID: 7237
		Max,
		// Token: 0x04001C46 RID: 7238
		Modifier = 64,
		// Token: 0x04001C47 RID: 7239
		Sentinel,
		// Token: 0x04001C48 RID: 7240
		Pinned = 69
	}
}
