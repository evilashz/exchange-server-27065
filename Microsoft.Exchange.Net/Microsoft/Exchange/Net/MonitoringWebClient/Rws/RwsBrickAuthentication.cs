using System;
using System.Net;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007F3 RID: 2035
	internal class RwsBrickAuthentication : BaseTestStep
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x0005CFCA File Offset: 0x0005B1CA
		// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x0005CFD2 File Offset: 0x0005B1D2
		public CommonAccessToken Token { get; private set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x0005CFDB File Offset: 0x0005B1DB
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x0005CFE3 File Offset: 0x0005B1E3
		public Uri Uri { get; private set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x0005CFEC File Offset: 0x0005B1EC
		protected override TestId Id
		{
			get
			{
				return TestId.RwsBrickAuthentication;
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
		public RwsBrickAuthentication(CommonAccessToken token, Uri uri)
		{
			this.Token = token;
			this.Uri = uri;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0005D008 File Offset: 0x0005B208
		protected override void StartTest()
		{
			this.session.AuthenticationData = new AuthenticationData?(new AuthenticationData
			{
				UseDefaultCredentials = false,
				Credentials = CredentialCache.DefaultNetworkCredentials.GetCredential(this.Uri, "Kerberos")
			});
			this.session.PersistentHeaders.Add("X-CommonAccessToken", this.Token.Serialize());
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x0400254E RID: 9550
		private const TestId ID = TestId.RwsBrickAuthentication;
	}
}
