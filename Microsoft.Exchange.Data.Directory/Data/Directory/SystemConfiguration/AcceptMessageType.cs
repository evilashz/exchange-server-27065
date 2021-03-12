using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003FD RID: 1021
	[Flags]
	internal enum AcceptMessageType
	{
		// Token: 0x04001F3D RID: 7997
		Default = 0,
		// Token: 0x04001F3E RID: 7998
		LegacyOOF = 1,
		// Token: 0x04001F3F RID: 7999
		AutoReply = 2,
		// Token: 0x04001F40 RID: 8000
		AutoForward = 4,
		// Token: 0x04001F41 RID: 8001
		DR = 8,
		// Token: 0x04001F42 RID: 8002
		NDR = 16,
		// Token: 0x04001F43 RID: 8003
		BlockOOF = 32,
		// Token: 0x04001F44 RID: 8004
		InternalDomain = 64,
		// Token: 0x04001F45 RID: 8005
		MFN = 128,
		// Token: 0x04001F46 RID: 8006
		TargetDeliveryDomain = 256,
		// Token: 0x04001F47 RID: 8007
		UseSimpleDisplayName = 512,
		// Token: 0x04001F48 RID: 8008
		NDRDiagnosticInfoDisabled = 1024
	}
}
