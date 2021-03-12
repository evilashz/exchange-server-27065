using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	public class MailboxTransportServerIdParameter : ExchangeTransportServerIdParameter
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x0002AF50 File Offset: 0x00029150
		public MailboxTransportServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002AF59 File Offset: 0x00029159
		public MailboxTransportServerIdParameter(MailboxServer server) : base(server.Id)
		{
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002AF67 File Offset: 0x00029167
		public MailboxTransportServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002AF70 File Offset: 0x00029170
		public MailboxTransportServerIdParameter()
		{
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002AF78 File Offset: 0x00029178
		protected MailboxTransportServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002AF81 File Offset: 0x00029181
		public static MailboxTransportServerIdParameter Parse(string identity)
		{
			return new MailboxTransportServerIdParameter(identity);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002AF8C File Offset: 0x0002918C
		public static MailboxTransportServerIdParameter CreateIdentity(MailboxTransportServerIdParameter identityPassedIn)
		{
			return new MailboxTransportServerIdParameter("Mailbox")
			{
				identityPassedIn = identityPassedIn
			};
		}
	}
}
