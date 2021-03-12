using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000352 RID: 850
	[Flags]
	public enum MobileClientFlagsType
	{
		// Token: 0x040017EC RID: 6124
		None = 0,
		// Token: 0x040017ED RID: 6125
		ClientCertProvisionEnabled = 1,
		// Token: 0x040017EE RID: 6126
		BadItemReportingEnabled = 2,
		// Token: 0x040017EF RID: 6127
		RemoteDocumentsActionForUnknownServers = 4,
		// Token: 0x040017F0 RID: 6128
		SendWatsonReport = 8,
		// Token: 0x040017F1 RID: 6129
		MailboxLoggingEnabled = 16
	}
}
