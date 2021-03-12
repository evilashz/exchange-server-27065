using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class MailboxServerIdParameter : RoleServerIdParameter
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x0002AF14 File Offset: 0x00029114
		public MailboxServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002AF1D File Offset: 0x0002911D
		public MailboxServerIdParameter(MailboxServer server) : base(server.Id)
		{
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002AF2B File Offset: 0x0002912B
		public MailboxServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002AF34 File Offset: 0x00029134
		public MailboxServerIdParameter()
		{
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002AF3C File Offset: 0x0002913C
		protected MailboxServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0002AF45 File Offset: 0x00029145
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.Mailbox;
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002AF48 File Offset: 0x00029148
		public new static MailboxServerIdParameter Parse(string identity)
		{
			return new MailboxServerIdParameter(identity);
		}
	}
}
