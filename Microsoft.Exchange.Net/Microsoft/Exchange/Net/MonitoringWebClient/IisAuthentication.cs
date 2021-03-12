using System;
using System.Net;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079A RID: 1946
	internal class IisAuthentication : BaseTestStep
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x00052C30 File Offset: 0x00050E30
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x00052C38 File Offset: 0x00050E38
		public Uri Uri { get; private set; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x00052C41 File Offset: 0x00050E41
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x00052C49 File Offset: 0x00050E49
		public string UserName { get; private set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x00052C52 File Offset: 0x00050E52
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x00052C5A File Offset: 0x00050E5A
		public string UserDomain { get; private set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x00052C63 File Offset: 0x00050E63
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x00052C6B File Offset: 0x00050E6B
		public SecureString Password { get; private set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00052C74 File Offset: 0x00050E74
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x00052C7C File Offset: 0x00050E7C
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00052C85 File Offset: 0x00050E85
		protected override TestId Id
		{
			get
			{
				return TestId.IisAuthentication;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00052C89 File Offset: 0x00050E89
		public IisAuthentication(Uri uri, string userName, string userDomain, SecureString password)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00052CB0 File Offset: 0x00050EB0
		protected override void StartTest()
		{
			this.session.AuthenticationData = new AuthenticationData?(new AuthenticationData
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(this.UserName, this.Password)
			});
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x0400235A RID: 9050
		private const TestId ID = TestId.IisAuthentication;
	}
}
