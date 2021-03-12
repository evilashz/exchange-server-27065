using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D0 RID: 1488
	[Serializable]
	public class TestEcpConnectivityOutcome : CasTransactionOutcome
	{
		// Token: 0x06003454 RID: 13396 RVA: 0x000D3F84 File Offset: 0x000D2184
		public TestEcpConnectivityOutcome(string virtualDirectoryName, string casServerName, string mailboxServerName, string localSite, Uri targetUri, VirtualDirectoryUriScope targetUriScope, string performanceCounterName, string userName) : base(casServerName, Strings.CasHealthEcpScenarioTestWebService, Strings.CasHealthEcpScenarioTestWebServiceDescription, performanceCounterName, localSite, targetUri != null && targetUri.AbsoluteUri != null && targetUri.AbsoluteUri.IndexOf("https", StringComparison.OrdinalIgnoreCase) == 0, userName, virtualDirectoryName, targetUri, targetUriScope)
		{
			this.MailboxServer = ((!string.IsNullOrEmpty(mailboxServerName)) ? ServerIdParameter.Parse(mailboxServerName) : null);
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000D4004 File Offset: 0x000D2204
		public TestEcpConnectivityOutcome(string casServerName, string mailboxServerName, string scenarioName, string scenarioDescription, string performanceCounterName, string localSite, bool secureAccess, string userName, string virtualDirectoryName, Uri targetUri, VirtualDirectoryUriScope targetUriScope) : base(casServerName, scenarioName, scenarioDescription, performanceCounterName, localSite, secureAccess, userName, virtualDirectoryName, targetUri, targetUriScope)
		{
			this.MailboxServer = ((!string.IsNullOrEmpty(mailboxServerName)) ? ServerIdParameter.Parse(mailboxServerName) : null);
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000D404B File Offset: 0x000D224B
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000D4053 File Offset: 0x000D2253
		public string AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
			internal set
			{
				this.authenticationMethod = value;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000D405C File Offset: 0x000D225C
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x000D4064 File Offset: 0x000D2264
		public ServerIdParameter MailboxServer { get; private set; }

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000D406D File Offset: 0x000D226D
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000D4075 File Offset: 0x000D2275
		public string FailureReason { get; internal set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000D407E File Offset: 0x000D227E
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x000D4086 File Offset: 0x000D2286
		public string FailureSource { get; internal set; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000D408F File Offset: 0x000D228F
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x000D4097 File Offset: 0x000D2297
		public string FailingComponent { get; internal set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000D40A0 File Offset: 0x000D22A0
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000D40A8 File Offset: 0x000D22A8
		public List<ResponseTrackerItem> HttpData { get; internal set; }

		// Token: 0x06003462 RID: 13410 RVA: 0x000D40B4 File Offset: 0x000D22B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append(Strings.CasHealthOwaAuthMethodHeader);
			stringBuilder.Append(": ");
			stringBuilder.AppendLine((!string.IsNullOrEmpty(this.authenticationMethod)) ? this.authenticationMethod : "");
			stringBuilder.AppendLine(Strings.CasHealthOwaMailboxServerHeader);
			stringBuilder.AppendLine((this.MailboxServer != null) ? this.MailboxServer.ToString() : string.Empty);
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04002430 RID: 9264
		private string authenticationMethod = string.Empty;
	}
}
