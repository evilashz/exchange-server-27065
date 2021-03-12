using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D1 RID: 1489
	[Cmdlet("Test", "ImapConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestImapConnectivity : ProtocolConnectivity
	{
		// Token: 0x06003463 RID: 13411 RVA: 0x000D414F File Offset: 0x000D234F
		public TestImapConnectivity()
		{
			base.CmdletNoun = "ImapConnectivity";
			base.MailSubjectPrefix = "Test-ImapConnectivity-";
			base.CurrentProtocol = "IMAP4";
			base.SendTestMessage = true;
			base.ConnectionType = ProtocolConnectionType.Ssl;
			this.trustAllCertificates = true;
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000D418D File Offset: 0x000D238D
		protected override string PerformanceObject
		{
			get
			{
				return this.MonitoringEventSource;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000D4195 File Offset: 0x000D2395
		protected override string MonitoringEventSource
		{
			get
			{
				return base.CmdletMonitoringEventSource;
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000D419D File Offset: 0x000D239D
		internal override TransientErrorCache GetTransientErrorCache()
		{
			if (!base.MonitoringContext)
			{
				return null;
			}
			return TransientErrorCache.IMAPTransientErrorCache;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000D41B4 File Offset: 0x000D23B4
		protected override void ExecuteTestConnectivity(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			int perConnectionTimeout = base.PerConnectionTimeout;
			CasTransactionOutcome casTransactionOutcome = new CasTransactionOutcome(base.CasFqdn, Strings.TestProtocolConnectivity("IMAP4"), Strings.ProtocolConnectivityScenario("IMAP4"), "ImapConnectivity-Latency", base.LocalSiteName, false, instance.credentials.UserName);
			if (base.PortClientAccessServer == 0)
			{
				if (ProtocolConnectionType.Ssl == base.ConnectionType)
				{
					base.PortClientAccessServer = 993;
				}
				else
				{
					base.PortClientAccessServer = 143;
				}
			}
			else if (ProtocolConnectionType.Ssl == base.ConnectionType)
			{
				if (993 != base.PortClientAccessServer)
				{
					instance.Outcomes.Enqueue(new Warning(Strings.PopImapTransactionWarning(base.PortClientAccessServer, base.ConnectionType.ToString(), 993, 143)));
				}
			}
			else if (143 != base.PortClientAccessServer)
			{
				instance.Outcomes.Enqueue(new Warning(Strings.PopImapTransactionWarning(base.PortClientAccessServer, base.ConnectionType.ToString(), 993, 143)));
			}
			casTransactionOutcome.Port = base.PortClientAccessServer;
			casTransactionOutcome.ConnectionType = base.ConnectionType;
			instance.Port = base.PortClientAccessServer;
			instance.ConnectionType = base.ConnectionType;
			LocalizedString localizedString = Strings.ProtocolTransactionsDetails(base.CasFqdn, string.IsNullOrEmpty(base.MailboxFqdn) ? base.User.ServerName : base.MailboxFqdn, base.User.Database.Name, base.User.PrimarySmtpAddress.ToString(), base.PortClientAccessServer.ToString(CultureInfo.InvariantCulture), base.ConnectionType.ToString(), this.trustAllCertificates);
			instance.Outcomes.Enqueue(localizedString);
			this.transactionTarget = new ImapTransaction(base.User, this.casToTest.Name, instance.credentials.Password, base.PortClientAccessServer);
			this.transactionTarget.ConnectionType = base.ConnectionType;
			this.transactionTarget.TrustAnySslCertificate = instance.trustAllCertificates;
			this.transactionTarget.MailSubject = base.MailSubject;
			this.transactionTarget.TransactionResult = casTransactionOutcome;
			this.transactionTarget.Instance = instance;
			this.transactionTarget.Execute();
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000D441A File Offset: 0x000D261A
		protected override string MonitoringLatencyPerformanceCounter()
		{
			return "ImapConnectivity-Latency";
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000D4421 File Offset: 0x000D2621
		protected override uint GetDefaultTimeOut()
		{
			return 120U;
		}

		// Token: 0x04002436 RID: 9270
		private const string ImapProtocol = "IMAP4";

		// Token: 0x04002437 RID: 9271
		private const int DefaultConnectionTimeoutInSeconds = 180;

		// Token: 0x04002438 RID: 9272
		private const string MonitoringPerformanceObject = "Exchange Monitoring";

		// Token: 0x04002439 RID: 9273
		private const string MonitoringLatencyPerfCounter = "ImapConnectivity-Latency";

		// Token: 0x0400243A RID: 9274
		private const int MinimalTimeoutMSecForExtraTransaction = 12000;

		// Token: 0x0400243B RID: 9275
		private const int SecurePort = 993;

		// Token: 0x0400243C RID: 9276
		private const int NormalPort = 143;

		// Token: 0x0400243D RID: 9277
		private const uint TaskSessionTimeoutSeconds = 120U;

		// Token: 0x0400243E RID: 9278
		private ImapTransaction transactionTarget;
	}
}
