using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A0 RID: 2464
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TeamMailboxSyncId : IEquatable<TeamMailboxSyncId>, IComparable<TeamMailboxSyncId>, IComparable
	{
		// Token: 0x06005AF5 RID: 23285 RVA: 0x0017CC88 File Offset: 0x0017AE88
		public override int GetHashCode()
		{
			return this.MailboxGuid.GetHashCode();
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x0017CCA9 File Offset: 0x0017AEA9
		public bool Equals(TeamMailboxSyncId other)
		{
			return other != null && this.MailboxGuid == other.MailboxGuid;
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x0017CCC4 File Offset: 0x0017AEC4
		public int CompareTo(object other)
		{
			TeamMailboxSyncId teamMailboxSyncId = other as TeamMailboxSyncId;
			if (teamMailboxSyncId != null)
			{
				return this.MailboxGuid.CompareTo(teamMailboxSyncId.MailboxGuid);
			}
			return 1;
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x0017CCF4 File Offset: 0x0017AEF4
		public int CompareTo(TeamMailboxSyncId other)
		{
			return this.MailboxGuid.CompareTo(other.MailboxGuid);
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0017CD15 File Offset: 0x0017AF15
		// (set) Token: 0x06005AFA RID: 23290 RVA: 0x0017CD1D File Offset: 0x0017AF1D
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x0017CD26 File Offset: 0x0017AF26
		// (set) Token: 0x06005AFC RID: 23292 RVA: 0x0017CD2E File Offset: 0x0017AF2E
		public OrganizationId OrgId { get; private set; }

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x0017CD37 File Offset: 0x0017AF37
		// (set) Token: 0x06005AFE RID: 23294 RVA: 0x0017CD3F File Offset: 0x0017AF3F
		public string DomainController { get; private set; }

		// Token: 0x06005AFF RID: 23295 RVA: 0x0017CD48 File Offset: 0x0017AF48
		public TeamMailboxSyncId(Guid mailboxGuid, OrganizationId orgId, string domainController)
		{
			this.MailboxGuid = mailboxGuid;
			this.OrgId = orgId;
			this.DomainController = domainController;
		}
	}
}
