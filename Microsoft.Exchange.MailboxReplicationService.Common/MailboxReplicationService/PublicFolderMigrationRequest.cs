using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public sealed class PublicFolderMigrationRequest : RequestBase
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x0002EBD2 File Offset: 0x0002CDD2
		public PublicFolderMigrationRequest()
		{
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0002EBDA File Offset: 0x0002CDDA
		internal PublicFolderMigrationRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0002EBE3 File Offset: 0x0002CDE3
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0002EBEB File Offset: 0x0002CDEB
		public override string ToString()
		{
			if (base.Name != null && base.OrganizationId != null)
			{
				return string.Format("{0}\\{1}", base.OrganizationId.ToString(), base.Name);
			}
			return base.ToString();
		}
	}
}
