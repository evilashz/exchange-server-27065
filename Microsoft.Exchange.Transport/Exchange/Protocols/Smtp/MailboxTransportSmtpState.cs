using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004FF RID: 1279
	internal enum MailboxTransportSmtpState
	{
		// Token: 0x04001DD4 RID: 7636
		WaitingForGreeting,
		// Token: 0x04001DD5 RID: 7637
		GreetingReceived,
		// Token: 0x04001DD6 RID: 7638
		WaitingForSecureGreeting,
		// Token: 0x04001DD7 RID: 7639
		SecureGreetingReceived,
		// Token: 0x04001DD8 RID: 7640
		MailTransactionStarted,
		// Token: 0x04001DD9 RID: 7641
		WaitingForMoreRecipientsOrData,
		// Token: 0x04001DDA RID: 7642
		ReceivingBdatChunks,
		// Token: 0x04001DDB RID: 7643
		End
	}
}
