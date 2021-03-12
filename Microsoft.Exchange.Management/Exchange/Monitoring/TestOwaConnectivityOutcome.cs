using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005DF RID: 1503
	[Serializable]
	public class TestOwaConnectivityOutcome : CasTransactionOutcome
	{
		// Token: 0x0600353B RID: 13627 RVA: 0x000DAEE4 File Offset: 0x000D90E4
		public TestOwaConnectivityOutcome(string virtualDirectoryName, string casServerName, string mailboxServerName, string localSite, Uri targetUri, VirtualDirectoryUriScope targetUriScope, string performanceCounterName, string userName) : base(casServerName, Strings.CasHealthOwaLogonScenarioName, Strings.CasHealthOwaLogonScenarioDescription, performanceCounterName, localSite, targetUri != null && targetUri.AbsoluteUri != null && targetUri.AbsoluteUri.IndexOf("https", StringComparison.OrdinalIgnoreCase) == 0, userName, virtualDirectoryName, targetUri, targetUriScope)
		{
			this.MailboxServer = ((!string.IsNullOrEmpty(mailboxServerName)) ? ServerIdParameter.Parse(mailboxServerName) : null);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000DAF64 File Offset: 0x000D9164
		public TestOwaConnectivityOutcome(string casServerName, string mailboxServerName, string scenarioName, string scenarioDescription, string performanceCounterName, string localSite, bool secureAccess, string userName, string virtualDirectoryName, Uri targetUri, VirtualDirectoryUriScope targetUriScope) : base(casServerName, scenarioName, scenarioDescription, performanceCounterName, localSite, secureAccess, userName, virtualDirectoryName, targetUri, targetUriScope)
		{
			this.MailboxServer = ((!string.IsNullOrEmpty(mailboxServerName)) ? ServerIdParameter.Parse(mailboxServerName) : null);
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000DAFAB File Offset: 0x000D91AB
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x000DAFB3 File Offset: 0x000D91B3
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

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000DAFBC File Offset: 0x000D91BC
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000DAFC4 File Offset: 0x000D91C4
		public ServerIdParameter MailboxServer { get; private set; }

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000DAFCD File Offset: 0x000D91CD
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000DAFD5 File Offset: 0x000D91D5
		public string FailureReason { get; internal set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000DAFDE File Offset: 0x000D91DE
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x000DAFE6 File Offset: 0x000D91E6
		public string FailureSource { get; internal set; }

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000DAFEF File Offset: 0x000D91EF
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000DAFF7 File Offset: 0x000D91F7
		public string FailingComponent { get; internal set; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000DB000 File Offset: 0x000D9200
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x000DB008 File Offset: 0x000D9208
		public List<ResponseTrackerItem> HttpData { get; internal set; }

		// Token: 0x06003549 RID: 13641 RVA: 0x000DB014 File Offset: 0x000D9214
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Strings.CasHealthOwaAuthMethodHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append((!string.IsNullOrEmpty(this.authenticationMethod)) ? this.authenticationMethod : "");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthOwaMailboxServerHeader);
			stringBuilder.Append(": ");
			stringBuilder.Append((this.MailboxServer != null) ? this.MailboxServer.ToString() : string.Empty);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040024A2 RID: 9378
		private string authenticationMethod = string.Empty;
	}
}
