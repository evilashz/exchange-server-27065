using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000433 RID: 1075
	internal interface ILegacySmtpCommand
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003168 RID: 12648
		byte[] ProtocolCommand { get; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003169 RID: 12649
		int ProtocolCommandLength { get; }

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600316A RID: 12650
		int CurrentOffset { get; }
	}
}
