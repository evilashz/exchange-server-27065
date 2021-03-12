using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000277 RID: 631
	internal class MailboxMoveSourceResource : MailboxMoveResource
	{
		// Token: 0x06001F61 RID: 8033 RVA: 0x00041BF4 File Offset: 0x0003FDF4
		private MailboxMoveSourceResource(Guid mailboxGuid) : base(mailboxGuid)
		{
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00041BFD File Offset: 0x0003FDFD
		public override string ResourceType
		{
			get
			{
				return "MailboxMoveSource";
			}
		}

		// Token: 0x04000CA8 RID: 3240
		public static readonly ResourceCache<MailboxMoveSourceResource> Cache = new ResourceCache<MailboxMoveSourceResource>((Guid id) => new MailboxMoveSourceResource(id));
	}
}
