using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	internal sealed class MultiMailboxSearchResult : MultiMailboxSearchResultItem
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x0000A140 File Offset: 0x00009540
		internal MultiMailboxSearchResult(int version, Guid mailboxGuid, int documentId, long referenceId) : base(version)
		{
			this.mailboxGuid = mailboxGuid;
			this.documentId = documentId;
			this.referenceId = referenceId;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000A110 File Offset: 0x00009510
		internal MultiMailboxSearchResult(Guid mailboxGuid, int documentId, long referenceId) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.mailboxGuid = mailboxGuid;
			this.documentId = documentId;
			this.referenceId = referenceId;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0000A16C File Offset: 0x0000956C
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000A184 File Offset: 0x00009584
		internal int DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0000A198 File Offset: 0x00009598
		internal long ReferenceId
		{
			get
			{
				return this.referenceId;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private readonly Guid mailboxGuid;

		// Token: 0x04000B15 RID: 2837
		private readonly int documentId;

		// Token: 0x04000B16 RID: 2838
		private readonly long referenceId;
	}
}
