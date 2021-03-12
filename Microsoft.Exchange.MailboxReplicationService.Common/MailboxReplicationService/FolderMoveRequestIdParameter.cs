using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public sealed class FolderMoveRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x00026D2A File Offset: 0x00024F2A
		public FolderMoveRequestIdParameter()
		{
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00026D32 File Offset: 0x00024F32
		public FolderMoveRequestIdParameter(FolderMoveRequest request) : base(request)
		{
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00026D3B File Offset: 0x00024F3B
		public FolderMoveRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00026D44 File Offset: 0x00024F44
		public FolderMoveRequestIdParameter(FolderMoveRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00026D4D File Offset: 0x00024F4D
		public FolderMoveRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00026D56 File Offset: 0x00024F56
		public FolderMoveRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00026D5F File Offset: 0x00024F5F
		public FolderMoveRequestIdParameter(string request) : base(request)
		{
			if (base.MailboxName != null)
			{
				base.OrganizationName = base.MailboxName;
				base.MailboxName = null;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00026D83 File Offset: 0x00024F83
		public static FolderMoveRequestIdParameter Parse(string request)
		{
			return new FolderMoveRequestIdParameter(request);
		}
	}
}
