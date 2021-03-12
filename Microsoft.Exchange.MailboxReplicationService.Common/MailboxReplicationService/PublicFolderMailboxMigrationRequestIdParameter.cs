using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public sealed class PublicFolderMailboxMigrationRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x00026FF0 File Offset: 0x000251F0
		public PublicFolderMailboxMigrationRequestIdParameter()
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00026FF8 File Offset: 0x000251F8
		public PublicFolderMailboxMigrationRequestIdParameter(PublicFolderMailboxMigrationRequest request) : base(request)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00027001 File Offset: 0x00025201
		public PublicFolderMailboxMigrationRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002700A File Offset: 0x0002520A
		public PublicFolderMailboxMigrationRequestIdParameter(PublicFolderMailboxMigrationRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00027013 File Offset: 0x00025213
		public PublicFolderMailboxMigrationRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002701C File Offset: 0x0002521C
		public PublicFolderMailboxMigrationRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00027025 File Offset: 0x00025225
		public PublicFolderMailboxMigrationRequestIdParameter(string request) : base(request)
		{
			if (base.MailboxName != null)
			{
				base.OrganizationName = base.MailboxName;
				base.MailboxName = null;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00027049 File Offset: 0x00025249
		public static PublicFolderMailboxMigrationRequestIdParameter Parse(string request)
		{
			return new PublicFolderMailboxMigrationRequestIdParameter(request);
		}
	}
}
