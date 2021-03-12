using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000405 RID: 1029
	[Flags]
	public enum RecipientJunkEmailContextMenuType
	{
		// Token: 0x040019CA RID: 6602
		None = 0,
		// Token: 0x040019CB RID: 6603
		Sender = 2,
		// Token: 0x040019CC RID: 6604
		Recipient = 4,
		// Token: 0x040019CD RID: 6605
		SenderAndRecipient = 6
	}
}
