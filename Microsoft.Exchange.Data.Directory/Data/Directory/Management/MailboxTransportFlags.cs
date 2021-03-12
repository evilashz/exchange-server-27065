using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200071C RID: 1820
	[Flags]
	internal enum MailboxTransportFlags
	{
		// Token: 0x040039FA RID: 14842
		None = 0,
		// Token: 0x040039FB RID: 14843
		ConnectivityLogEnabled = 1,
		// Token: 0x040039FC RID: 14844
		ContentConversionTracingEnabled = 2,
		// Token: 0x040039FD RID: 14845
		PipelineTracingEnabled = 4,
		// Token: 0x040039FE RID: 14846
		MailboxDeliverySmtpUtf8Enabled = 8
	}
}
