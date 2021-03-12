using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B7 RID: 1463
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FieldAttributes
	{
		// Token: 0x04001BEA RID: 7146
		[__DynamicallyInvokable]
		FieldAccessMask = 7,
		// Token: 0x04001BEB RID: 7147
		[__DynamicallyInvokable]
		PrivateScope = 0,
		// Token: 0x04001BEC RID: 7148
		[__DynamicallyInvokable]
		Private = 1,
		// Token: 0x04001BED RID: 7149
		[__DynamicallyInvokable]
		FamANDAssem = 2,
		// Token: 0x04001BEE RID: 7150
		[__DynamicallyInvokable]
		Assembly = 3,
		// Token: 0x04001BEF RID: 7151
		[__DynamicallyInvokable]
		Family = 4,
		// Token: 0x04001BF0 RID: 7152
		[__DynamicallyInvokable]
		FamORAssem = 5,
		// Token: 0x04001BF1 RID: 7153
		[__DynamicallyInvokable]
		Public = 6,
		// Token: 0x04001BF2 RID: 7154
		[__DynamicallyInvokable]
		Static = 16,
		// Token: 0x04001BF3 RID: 7155
		[__DynamicallyInvokable]
		InitOnly = 32,
		// Token: 0x04001BF4 RID: 7156
		[__DynamicallyInvokable]
		Literal = 64,
		// Token: 0x04001BF5 RID: 7157
		[__DynamicallyInvokable]
		NotSerialized = 128,
		// Token: 0x04001BF6 RID: 7158
		[__DynamicallyInvokable]
		SpecialName = 512,
		// Token: 0x04001BF7 RID: 7159
		[__DynamicallyInvokable]
		PinvokeImpl = 8192,
		// Token: 0x04001BF8 RID: 7160
		ReservedMask = 38144,
		// Token: 0x04001BF9 RID: 7161
		[__DynamicallyInvokable]
		RTSpecialName = 1024,
		// Token: 0x04001BFA RID: 7162
		[__DynamicallyInvokable]
		HasFieldMarshal = 4096,
		// Token: 0x04001BFB RID: 7163
		[__DynamicallyInvokable]
		HasDefault = 32768,
		// Token: 0x04001BFC RID: 7164
		[__DynamicallyInvokable]
		HasFieldRVA = 256
	}
}
