using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000011 RID: 17
	internal interface IAdminAuditLogConfig
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000072 RID: 114
		MultiValuedProperty<string> AdminAuditLogParameters { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115
		MultiValuedProperty<string> AdminAuditLogCmdlets { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000074 RID: 116
		MultiValuedProperty<string> AdminAuditLogExcludedCmdlets { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000075 RID: 117
		bool AdminAuditLogEnabled { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118
		bool IsValidAuditLogMailboxAddress { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119
		bool TestCmdletLoggingEnabled { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120
		AuditLogLevel LogLevel { get; }
	}
}
