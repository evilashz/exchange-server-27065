using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A23 RID: 2595
	[Serializable]
	public class MigrationConfigId : ObjectId
	{
		// Token: 0x06005F4B RID: 24395 RVA: 0x00192F5D File Offset: 0x0019115D
		internal MigrationConfigId(OrganizationId orgId)
		{
			this.OrganizationId = orgId;
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x00192F6C File Offset: 0x0019116C
		// (set) Token: 0x06005F4D RID: 24397 RVA: 0x00192F74 File Offset: 0x00191174
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x06005F4E RID: 24398 RVA: 0x00192F7D File Offset: 0x0019117D
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.OrganizationId.OrganizationalUnit.DistinguishedName);
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x00192F99 File Offset: 0x00191199
		public override string ToString()
		{
			if (OrganizationId.ForestWideOrgId == this.OrganizationId)
			{
				return null;
			}
			return this.OrganizationId.OrganizationalUnit.Name;
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x00192FC0 File Offset: 0x001911C0
		public override bool Equals(object obj)
		{
			MigrationConfigId migrationConfigId = obj as MigrationConfigId;
			return migrationConfigId != null && object.Equals(this.OrganizationId, migrationConfigId.OrganizationId);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x00192FEA File Offset: 0x001911EA
		public override int GetHashCode()
		{
			return this.OrganizationId.GetHashCode();
		}
	}
}
