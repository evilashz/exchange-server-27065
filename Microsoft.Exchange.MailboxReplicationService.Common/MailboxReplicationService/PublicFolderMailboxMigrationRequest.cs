using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public sealed class PublicFolderMailboxMigrationRequest : RequestBase
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x0002EC25 File Offset: 0x0002CE25
		public PublicFolderMailboxMigrationRequest()
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0002EC2D File Offset: 0x0002CE2D
		internal PublicFolderMailboxMigrationRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0002EC36 File Offset: 0x0002CE36
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0002EC3E File Offset: 0x0002CE3E
		public new ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0002EC46 File Offset: 0x0002CE46
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
