using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public sealed class MailboxRestoreRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x00026E55 File Offset: 0x00025055
		public MailboxRestoreRequestIdParameter()
		{
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00026E5D File Offset: 0x0002505D
		public MailboxRestoreRequestIdParameter(MailboxRestoreRequest request) : base(request)
		{
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00026E66 File Offset: 0x00025066
		public MailboxRestoreRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00026E6F File Offset: 0x0002506F
		public MailboxRestoreRequestIdParameter(MailboxRestoreRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00026E78 File Offset: 0x00025078
		public MailboxRestoreRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00026E81 File Offset: 0x00025081
		public MailboxRestoreRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00026E8A File Offset: 0x0002508A
		public MailboxRestoreRequestIdParameter(string request) : base(request)
		{
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00026E93 File Offset: 0x00025093
		public static MailboxRestoreRequestIdParameter Parse(string request)
		{
			return new MailboxRestoreRequestIdParameter(request);
		}
	}
}
