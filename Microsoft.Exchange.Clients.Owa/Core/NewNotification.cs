using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000170 RID: 368
	[Flags]
	public enum NewNotification
	{
		// Token: 0x04000910 RID: 2320
		None = 0,
		// Token: 0x04000911 RID: 2321
		Sound = 1,
		// Token: 0x04000912 RID: 2322
		EMailToast = 2,
		// Token: 0x04000913 RID: 2323
		VoiceMailToast = 4,
		// Token: 0x04000914 RID: 2324
		FaxToast = 8
	}
}
