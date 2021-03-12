using System;
using System.Net;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class AuthenticationParameters
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002A3B File Offset: 0x00000C3B
		public AuthenticationParameters(string userName, SecureString password)
		{
			this.NetworkCredential = new NetworkCredential(userName, password);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002A50 File Offset: 0x00000C50
		public AuthenticationParameters(NetworkCredential networkCredential)
		{
			this.NetworkCredential = networkCredential;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002A5F File Offset: 0x00000C5F
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002A67 File Offset: 0x00000C67
		internal NetworkCredential NetworkCredential { get; private set; }
	}
}
