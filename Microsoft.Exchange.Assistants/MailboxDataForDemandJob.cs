using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000069 RID: 105
	internal sealed class MailboxDataForDemandJob : StoreMailboxData
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000F18C File Offset: 0x0000D38C
		public MailboxDataForDemandJob(Guid mailboxGuid, Guid databaseGuid, OrganizationId organizationId, string parameters) : this(mailboxGuid, databaseGuid, organizationId, parameters, null)
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F19A File Offset: 0x0000D39A
		public MailboxDataForDemandJob(Guid mailboxGuid, Guid databaseGuid, OrganizationId organizationId, string parameters, TenantPartitionHint tenantPartitionHint) : base(mailboxGuid, databaseGuid, "(unknown)", organizationId, tenantPartitionHint)
		{
			this.Parameters = parameters;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public string Parameters { get; private set; }
	}
}
