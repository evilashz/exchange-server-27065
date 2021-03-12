using System;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B14 RID: 2836
	internal class AuthenticateWebSession : WebSession
	{
		// Token: 0x06003D40 RID: 15680 RVA: 0x0009F7E8 File Offset: 0x0009D9E8
		public AuthenticateWebSession(Uri loginUrl, NetworkCredential credentials) : base(loginUrl, credentials)
		{
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x0009F7F2 File Offset: 0x0009D9F2
		public override void Initialize()
		{
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x0009F7F4 File Offset: 0x0009D9F4
		protected override void Authenticate(HttpWebRequest request)
		{
			request.Credentials = base.Credentials;
		}
	}
}
