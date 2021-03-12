using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public sealed class MailboxImportRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00026DD1 File Offset: 0x00024FD1
		public MailboxImportRequestIdParameter()
		{
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00026DD9 File Offset: 0x00024FD9
		public MailboxImportRequestIdParameter(MailboxImportRequest request) : base(request)
		{
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00026DE2 File Offset: 0x00024FE2
		public MailboxImportRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00026DEB File Offset: 0x00024FEB
		public MailboxImportRequestIdParameter(MailboxImportRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00026DF4 File Offset: 0x00024FF4
		public MailboxImportRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00026DFD File Offset: 0x00024FFD
		public MailboxImportRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00026E06 File Offset: 0x00025006
		public MailboxImportRequestIdParameter(string request) : base(request)
		{
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00026E0F File Offset: 0x0002500F
		public static MailboxImportRequestIdParameter Parse(string request)
		{
			return new MailboxImportRequestIdParameter(request);
		}
	}
}
