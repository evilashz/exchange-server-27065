using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public sealed class MailboxExportRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x00026D8B File Offset: 0x00024F8B
		public MailboxExportRequestIdParameter()
		{
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00026D93 File Offset: 0x00024F93
		public MailboxExportRequestIdParameter(MailboxExportRequest request) : base(request)
		{
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00026D9C File Offset: 0x00024F9C
		public MailboxExportRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00026DA5 File Offset: 0x00024FA5
		public MailboxExportRequestIdParameter(MailboxExportRequestStatistics requestStats) : base(requestStats)
		{
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00026DAE File Offset: 0x00024FAE
		public MailboxExportRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00026DB7 File Offset: 0x00024FB7
		public MailboxExportRequestIdParameter(Guid guid) : base(guid)
		{
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00026DC0 File Offset: 0x00024FC0
		public MailboxExportRequestIdParameter(string request) : base(request)
		{
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00026DC9 File Offset: 0x00024FC9
		public static MailboxExportRequestIdParameter Parse(string request)
		{
			return new MailboxExportRequestIdParameter(request);
		}
	}
}
