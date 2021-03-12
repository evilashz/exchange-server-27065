using System;
using System.Net;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class EasAuthenticationParameters : AuthenticationParameters
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00005C04 File Offset: 0x00003E04
		public EasAuthenticationParameters(NetworkCredential networkCredential, string local, string domain, string endpointOverride = null) : base(networkCredential)
		{
			this.UserSmtpAddress = new UserSmtpAddress(local, domain);
			this.EndpointOverride = endpointOverride;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005C22 File Offset: 0x00003E22
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00005C2A File Offset: 0x00003E2A
		internal UserSmtpAddress UserSmtpAddress { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005C33 File Offset: 0x00003E33
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00005C3B File Offset: 0x00003E3B
		internal string EndpointOverride { get; private set; }
	}
}
