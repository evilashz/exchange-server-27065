using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001E RID: 30
	internal interface IMobileServiceSelector
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A4 RID: 164
		MobileServiceType Type { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A5 RID: 165
		int PersonToPersonMessagingPriority { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A6 RID: 166
		int MachineToPersonMessagingPriority { get; }
	}
}
