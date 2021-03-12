using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200001C RID: 28
	[Flags]
	internal enum ExRpcConnectionCreateFlag
	{
		// Token: 0x0400009D RID: 157
		None = 0,
		// Token: 0x0400009E RID: 158
		UseConMod = 1,
		// Token: 0x0400009F RID: 159
		UseLcidString = 2,
		// Token: 0x040000A0 RID: 160
		UseLcidSort = 4,
		// Token: 0x040000A1 RID: 161
		UseCpid = 8,
		// Token: 0x040000A2 RID: 162
		UseReconnectInterval = 16,
		// Token: 0x040000A3 RID: 163
		UseRpcBufferSize = 32,
		// Token: 0x040000A4 RID: 164
		UseAuxBufferSize = 64,
		// Token: 0x040000A5 RID: 165
		LegacyCall = 65536,
		// Token: 0x040000A6 RID: 166
		CompressUp = 131072,
		// Token: 0x040000A7 RID: 167
		CompressDown = 262144,
		// Token: 0x040000A8 RID: 168
		PackedUp = 524288,
		// Token: 0x040000A9 RID: 169
		PackedDown = 1048576,
		// Token: 0x040000AA RID: 170
		XorMagicUp = 2097152,
		// Token: 0x040000AB RID: 171
		XorMagicDown = 4194304,
		// Token: 0x040000AC RID: 172
		WebServices = 8388608
	}
}
