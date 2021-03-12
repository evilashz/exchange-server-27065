using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public sealed class PublicFolderMigrationRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x00027051 File Offset: 0x00025251
		public PublicFolderMigrationRequestIdParameter()
		{
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00027059 File Offset: 0x00025259
		public PublicFolderMigrationRequestIdParameter(PublicFolderMigrationRequest request) : base(request)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00027062 File Offset: 0x00025262
		public PublicFolderMigrationRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0002706B File Offset: 0x0002526B
		public PublicFolderMigrationRequestIdParameter(PublicFolderMigrationRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00027074 File Offset: 0x00025274
		public PublicFolderMigrationRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002707D File Offset: 0x0002527D
		public PublicFolderMigrationRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00027086 File Offset: 0x00025286
		public PublicFolderMigrationRequestIdParameter(string request) : base(request)
		{
			if (base.MailboxName != null)
			{
				base.OrganizationName = base.MailboxName;
				base.MailboxName = null;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000270AA File Offset: 0x000252AA
		public static PublicFolderMigrationRequestIdParameter Parse(string request)
		{
			return new PublicFolderMigrationRequestIdParameter(request);
		}
	}
}
