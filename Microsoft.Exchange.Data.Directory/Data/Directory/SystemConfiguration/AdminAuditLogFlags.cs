using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200034A RID: 842
	[Flags]
	internal enum AdminAuditLogFlags
	{
		// Token: 0x040017C6 RID: 6086
		None = 0,
		// Token: 0x040017C7 RID: 6087
		AdminAuditLogEnabled = 1,
		// Token: 0x040017C8 RID: 6088
		TestCmdletLoggingEnabled = 2,
		// Token: 0x040017C9 RID: 6089
		CaptureDetailsEnabled = 4
	}
}
