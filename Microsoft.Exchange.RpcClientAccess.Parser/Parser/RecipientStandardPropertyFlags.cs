using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000080 RID: 128
	[Flags]
	internal enum RecipientStandardPropertyFlags : ushort
	{
		// Token: 0x040001A7 RID: 423
		EmailAddressPresent = 8,
		// Token: 0x040001A8 RID: 424
		DisplayNamePresent = 16,
		// Token: 0x040001A9 RID: 425
		TransmittableDisplayNamePresent = 32,
		// Token: 0x040001AA RID: 426
		StandardPropertiesInUnicode = 512,
		// Token: 0x040001AB RID: 427
		SimpleDisplayNamePresent = 1024
	}
}
