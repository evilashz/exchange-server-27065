using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000010 RID: 16
	internal interface IConfigurationPolicy
	{
		// Token: 0x0600006C RID: 108
		ArbitrationMailboxStatus CheckArbitrationMailboxStatus(out Exception initialError);

		// Token: 0x0600006D RID: 109
		IAuditLog CreateLogger(ArbitrationMailboxStatus mailboxStatus);

		// Token: 0x0600006E RID: 110
		IAdminAuditLogConfig GetAdminAuditLogConfig();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111
		bool RunningOnDataCenter { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000070 RID: 112
		OrganizationId OrganizationId { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113
		IExchangePrincipal ExchangePrincipal { get; }
	}
}
