using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A3B RID: 2619
	[Serializable]
	public class MigrationStatisticsId : ObjectId
	{
		// Token: 0x0600600C RID: 24588 RVA: 0x0019540E File Offset: 0x0019360E
		internal MigrationStatisticsId(OrganizationId orgId)
		{
			this.OrganizationId = orgId;
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x0019541D File Offset: 0x0019361D
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x00195425 File Offset: 0x00193625
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x0600600F RID: 24591 RVA: 0x0019542E File Offset: 0x0019362E
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.OrganizationId.OrganizationalUnit.DistinguishedName);
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x0019544A File Offset: 0x0019364A
		public override string ToString()
		{
			if (OrganizationId.ForestWideOrgId == this.OrganizationId)
			{
				return null;
			}
			return this.OrganizationId.OrganizationalUnit.Name;
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00195470 File Offset: 0x00193670
		public override bool Equals(object obj)
		{
			MigrationStatisticsId migrationStatisticsId = obj as MigrationStatisticsId;
			return migrationStatisticsId != null && object.Equals(this.OrganizationId, migrationStatisticsId.OrganizationId);
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x0019549A File Offset: 0x0019369A
		public override int GetHashCode()
		{
			return this.OrganizationId.GetHashCode();
		}
	}
}
