using System;
using System.Net;
using System.Security;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Pop3AuthenticationParameters : AuthenticationParameters
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00006916 File Offset: 0x00004B16
		public Pop3AuthenticationParameters(string userName, SecureString userPassword, Pop3AuthenticationMechanism pop3AuthenticationMechanism, Pop3SecurityMechanism pop3SecurityMechanism) : base(userName, userPassword)
		{
			this.Pop3AuthenticationMechanism = pop3AuthenticationMechanism;
			this.Pop3SecurityMechanism = pop3SecurityMechanism;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000692F File Offset: 0x00004B2F
		public Pop3AuthenticationParameters(NetworkCredential networkCredential, Pop3AuthenticationMechanism pop3AuthenticationMechanism, Pop3SecurityMechanism pop3SecurityMechanism) : base(networkCredential)
		{
			this.Pop3AuthenticationMechanism = pop3AuthenticationMechanism;
			this.Pop3SecurityMechanism = pop3SecurityMechanism;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006946 File Offset: 0x00004B46
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000694E File Offset: 0x00004B4E
		public Pop3AuthenticationMechanism Pop3AuthenticationMechanism { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006957 File Offset: 0x00004B57
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000695F File Offset: 0x00004B5F
		public Pop3SecurityMechanism Pop3SecurityMechanism { get; private set; }
	}
}
