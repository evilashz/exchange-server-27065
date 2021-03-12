using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200050B RID: 1291
	internal enum SmtpInStateMachineEvents
	{
		// Token: 0x04001E44 RID: 7748
		CommandFailed,
		// Token: 0x04001E45 RID: 7749
		DataFailed,
		// Token: 0x04001E46 RID: 7750
		BdatFailed,
		// Token: 0x04001E47 RID: 7751
		NetworkError,
		// Token: 0x04001E48 RID: 7752
		SendResponseAndDisconnectClient,
		// Token: 0x04001E49 RID: 7753
		AuthProcessed,
		// Token: 0x04001E4A RID: 7754
		BdatProcessed,
		// Token: 0x04001E4B RID: 7755
		BdatLastProcessed,
		// Token: 0x04001E4C RID: 7756
		DataProcessed,
		// Token: 0x04001E4D RID: 7757
		EhloProcessed,
		// Token: 0x04001E4E RID: 7758
		ExpnProcessed,
		// Token: 0x04001E4F RID: 7759
		HeloProcessed,
		// Token: 0x04001E50 RID: 7760
		HelpProcessed,
		// Token: 0x04001E51 RID: 7761
		MailProcessed,
		// Token: 0x04001E52 RID: 7762
		NoopProcessed,
		// Token: 0x04001E53 RID: 7763
		QuitProcessed,
		// Token: 0x04001E54 RID: 7764
		RcptProcessed,
		// Token: 0x04001E55 RID: 7765
		RsetProcessed,
		// Token: 0x04001E56 RID: 7766
		StartTlsProcessed,
		// Token: 0x04001E57 RID: 7767
		VrfyProcessed,
		// Token: 0x04001E58 RID: 7768
		XExpsProcessed,
		// Token: 0x04001E59 RID: 7769
		XAnonymousTlsProcessed,
		// Token: 0x04001E5A RID: 7770
		XSessionParamsProcessed
	}
}
