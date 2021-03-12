using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public sealed class PublicFolderMoveRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x000270B2 File Offset: 0x000252B2
		public PublicFolderMoveRequestIdParameter()
		{
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000270BA File Offset: 0x000252BA
		public PublicFolderMoveRequestIdParameter(PublicFolderMoveRequest request) : base(request)
		{
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000270C3 File Offset: 0x000252C3
		public PublicFolderMoveRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000270CC File Offset: 0x000252CC
		public PublicFolderMoveRequestIdParameter(PublicFolderMoveRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000270D5 File Offset: 0x000252D5
		public PublicFolderMoveRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000270DE File Offset: 0x000252DE
		public PublicFolderMoveRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000270E7 File Offset: 0x000252E7
		public PublicFolderMoveRequestIdParameter(string request) : base(request)
		{
			if (base.MailboxName != null)
			{
				base.OrganizationName = base.MailboxName;
				base.MailboxName = null;
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0002710B File Offset: 0x0002530B
		public static PublicFolderMoveRequestIdParameter Parse(string request)
		{
			return new PublicFolderMoveRequestIdParameter(request);
		}
	}
}
