using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000E1 RID: 225
	[Flags]
	public enum NewNotification
	{
		// Token: 0x04000515 RID: 1301
		None = 0,
		// Token: 0x04000516 RID: 1302
		Sound = 1,
		// Token: 0x04000517 RID: 1303
		EMailToast = 2,
		// Token: 0x04000518 RID: 1304
		VoiceMailToast = 4,
		// Token: 0x04000519 RID: 1305
		FaxToast = 8
	}
}
