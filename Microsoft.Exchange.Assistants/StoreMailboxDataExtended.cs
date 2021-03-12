using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007C RID: 124
	internal class StoreMailboxDataExtended : StoreMailboxData
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0001166C File Offset: 0x0000F86C
		public StoreMailboxDataExtended(Guid guid, Guid databaseGuid, string displayName, OrganizationId organizationId, TenantPartitionHint tenantPartitionHint, bool isArchiveMailbox, bool isGroupMailbox, bool isTeamSiteMailbox, bool isSharedMailbox) : base(guid, databaseGuid, displayName, organizationId, tenantPartitionHint)
		{
			this.IsArchiveMailbox = isArchiveMailbox;
			this.IsGroupMailbox = isGroupMailbox;
			this.IsTeamSiteMailbox = isTeamSiteMailbox;
			this.IsSharedMailbox = isSharedMailbox;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001169B File Offset: 0x0000F89B
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000116A3 File Offset: 0x0000F8A3
		public bool IsArchiveMailbox { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000116AC File Offset: 0x0000F8AC
		// (set) Token: 0x06000392 RID: 914 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public bool IsGroupMailbox { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000116BD File Offset: 0x0000F8BD
		// (set) Token: 0x06000394 RID: 916 RVA: 0x000116C5 File Offset: 0x0000F8C5
		public bool IsTeamSiteMailbox { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000116CE File Offset: 0x0000F8CE
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000116D6 File Offset: 0x0000F8D6
		public bool IsSharedMailbox { get; private set; }
	}
}
