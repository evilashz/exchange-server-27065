using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000278 RID: 632
	internal class MailboxMoveTargetResource : MailboxMoveResource
	{
		// Token: 0x06001F65 RID: 8037 RVA: 0x00041C35 File Offset: 0x0003FE35
		private MailboxMoveTargetResource(Guid mailboxGuid) : base(mailboxGuid)
		{
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x00041C3E File Offset: 0x0003FE3E
		public override string ResourceType
		{
			get
			{
				return "MailboxMoveTarget";
			}
		}

		// Token: 0x04000CAA RID: 3242
		public static readonly ResourceCache<MailboxMoveTargetResource> Cache = new ResourceCache<MailboxMoveTargetResource>((Guid id) => new MailboxMoveTargetResource(id));
	}
}
