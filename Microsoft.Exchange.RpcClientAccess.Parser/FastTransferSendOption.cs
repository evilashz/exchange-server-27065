using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004D RID: 77
	[Flags]
	internal enum FastTransferSendOption : byte
	{
		// Token: 0x040000F1 RID: 241
		UseMAPI = 0,
		// Token: 0x040000F2 RID: 242
		Unicode = 1,
		// Token: 0x040000F3 RID: 243
		UseCpId = 2,
		// Token: 0x040000F4 RID: 244
		Upload = 3,
		// Token: 0x040000F5 RID: 245
		RecoverMode = 4,
		// Token: 0x040000F6 RID: 246
		ForceUnicode = 8,
		// Token: 0x040000F7 RID: 247
		PartialItem = 16,
		// Token: 0x040000F8 RID: 248
		SendPropErrors = 32,
		// Token: 0x040000F9 RID: 249
		StripLargeRules = 64
	}
}
