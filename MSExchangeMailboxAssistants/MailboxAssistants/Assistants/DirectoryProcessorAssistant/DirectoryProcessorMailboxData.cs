using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x0200019C RID: 412
	internal class DirectoryProcessorMailboxData : AdminRpcMailboxData
	{
		// Token: 0x06001038 RID: 4152 RVA: 0x0005EB1F File Offset: 0x0005CD1F
		public DirectoryProcessorMailboxData(OrganizationId orgId, Guid databaseGuid, Guid mailboxGuid) : base(mailboxGuid, 0, databaseGuid)
		{
			this.OrgId = orgId;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0005EB31 File Offset: 0x0005CD31
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x0005EB39 File Offset: 0x0005CD39
		public OrganizationId OrgId { get; private set; }

		// Token: 0x0600103B RID: 4155 RVA: 0x0005EB42 File Offset: 0x0005CD42
		public override bool Equals(object other)
		{
			return this.Equals(other as DirectoryProcessorMailboxData);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005EB50 File Offset: 0x0005CD50
		public bool Equals(DirectoryProcessorMailboxData other)
		{
			return other != null && base.MailboxGuid.Equals(other.MailboxGuid) && base.Equals(other);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0005EB84 File Offset: 0x0005CD84
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ base.MailboxGuid.GetHashCode();
		}
	}
}
