using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005E0 RID: 1504
	[Cmdlet("Test", "PopConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestPopConnectivity : ProtocolConnectivity
	{
		// Token: 0x0600354A RID: 13642 RVA: 0x000DB0CE File Offset: 0x000D92CE
		public TestPopConnectivity()
		{
			base.CmdletNoun = "PopConnectivity";
			base.MailSubjectPrefix = "Test-PopConnectivity-";
			base.CurrentProtocol = "POP3";
			base.SendTestMessage = true;
			base.ConnectionType = ProtocolConnectionType.Ssl;
			this.trustAllCertificates = true;
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000DB10C File Offset: 0x000D930C
		protected override string PerformanceObject
		{
			get
			{
				return this.MonitoringEventSource;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000DB114 File Offset: 0x000D9314
		protected override string MonitoringEventSource
		{
			get
			{
				return base.CmdletMonitoringEventSource;
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000DB11C File Offset: 0x000D931C
		internal override TransientErrorCache GetTransientErrorCache()
		{
			if (!base.MonitoringContext)
			{
				return null;
			}
			return TransientErrorCache.POPTransientErrorCache;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000DB134 File Offset: 0x000D9334
		protected override void ExecuteTestConnectivity(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			int perConnectionTimeout = base.PerConnectionTimeout;
			CasTransactionOutcome casTransactionOutcome = new CasTransactionOutcome(base.CasFqdn, Strings.TestProtocolConnectivity("POP3"), Strings.ProtocolConnectivityScenario("POP3"), "POPConnectivity-Latency", base.LocalSiteName, false, instance.credentials.UserName);
			if (base.PortClientAccessServer == 0)
			{
				switch (base.ConnectionType)
				{
				case ProtocolConnectionType.Plaintext:
				case ProtocolConnectionType.Tls:
					base.PortClientAccessServer = 110;
					break;
				case ProtocolConnectionType.Ssl:
					base.PortClientAccessServer = 995;
					break;
				}
			}
			else
			{
				switch (base.ConnectionType)
				{
				case ProtocolConnectionType.Plaintext:
				case ProtocolConnectionType.Tls:
					if (110 != base.PortClientAccessServer)
					{
						instance.Outcomes.Enqueue(new Warning(Strings.PopImapTransactionWarning(base.PortClientAccessServer, base.ConnectionType.ToString(), 995, 110)));
					}
					break;
				case ProtocolConnectionType.Ssl:
					if (995 != base.PortClientAccessServer)
					{
						instance.Outcomes.Enqueue(new Warning(Strings.PopImapTransactionWarning(base.PortClientAccessServer, base.ConnectionType.ToString(), 995, 110)));
					}
					break;
				}
			}
			casTransactionOutcome.Port = base.PortClientAccessServer;
			casTransactionOutcome.ConnectionType = base.ConnectionType;
			instance.Port = base.PortClientAccessServer;
			instance.ConnectionType = base.ConnectionType;
			LocalizedString localizedString = Strings.ProtocolTransactionsDetails(base.CasFqdn, string.IsNullOrEmpty(base.MailboxFqdn) ? base.User.ServerName : base.MailboxFqdn, base.User.Database.Name, base.User.PrimarySmtpAddress.ToString(), base.PortClientAccessServer.ToString(CultureInfo.InvariantCulture), base.ConnectionType.ToString(), this.trustAllCertificates);
			instance.Outcomes.Enqueue(localizedString);
			this.transactionTarget = new PopTransaction(base.User, this.casToTest.Name, instance.credentials.Password, base.PortClientAccessServer);
			this.transactionTarget.ConnectionType = base.ConnectionType;
			this.transactionTarget.TrustAnySslCertificate = instance.trustAllCertificates;
			this.transactionTarget.MailSubject = base.MailSubject;
			this.transactionTarget.TransactionResult = casTransactionOutcome;
			this.transactionTarget.Instance = instance;
			this.transactionTarget.Execute();
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000DB3B7 File Offset: 0x000D95B7
		protected override string MonitoringLatencyPerformanceCounter()
		{
			return "POPConnectivity-Latency";
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000DB3BE File Offset: 0x000D95BE
		protected override uint GetDefaultTimeOut()
		{
			return 120U;
		}

		// Token: 0x040024A8 RID: 9384
		private const string PopProtocol = "POP3";

		// Token: 0x040024A9 RID: 9385
		private const string MonitoringPerformanceObject = "Exchange Monitoring";

		// Token: 0x040024AA RID: 9386
		private const string MonitoringLatencyPerfCounter = "POPConnectivity-Latency";

		// Token: 0x040024AB RID: 9387
		private const int MinimalTimeoutMSecForExtraTransaction = 12000;

		// Token: 0x040024AC RID: 9388
		private const int SecurePort = 995;

		// Token: 0x040024AD RID: 9389
		private const int NormalPort = 110;

		// Token: 0x040024AE RID: 9390
		private const uint TaskSessionTimeoutSeconds = 120U;

		// Token: 0x040024AF RID: 9391
		private PopTransaction transactionTarget;
	}
}
