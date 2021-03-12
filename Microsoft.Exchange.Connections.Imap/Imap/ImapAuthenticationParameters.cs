using System;
using System.Net;
using System.Security;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ImapAuthenticationParameters : AuthenticationParameters
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000A821 File Offset: 0x00008A21
		public ImapAuthenticationParameters(string userName, SecureString userPassword, ImapAuthenticationMechanism imapAuthenticationMechanism, ImapSecurityMechanism imapSecurityMechanism) : base(userName, userPassword)
		{
			this.ImapAuthenticationMechanism = imapAuthenticationMechanism;
			this.ImapSecurityMechanism = imapSecurityMechanism;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A83A File Offset: 0x00008A3A
		public ImapAuthenticationParameters(NetworkCredential networkCredential, ImapAuthenticationMechanism imapAuthenticationMechanism, ImapSecurityMechanism imapSecurityMechanism) : base(networkCredential)
		{
			this.ImapAuthenticationMechanism = imapAuthenticationMechanism;
			this.ImapSecurityMechanism = imapSecurityMechanism;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A851 File Offset: 0x00008A51
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A859 File Offset: 0x00008A59
		public ImapAuthenticationMechanism ImapAuthenticationMechanism { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A862 File Offset: 0x00008A62
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A86A File Offset: 0x00008A6A
		public ImapSecurityMechanism ImapSecurityMechanism { get; private set; }
	}
}
