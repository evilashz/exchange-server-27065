using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class EventCategoryIdentity : ObjectId
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0003A32A File Offset: 0x0003852A
		public string Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0003A332 File Offset: 0x00038532
		public string EventSource
		{
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0003A33A File Offset: 0x0003853A
		public string Category
		{
			get
			{
				return this.m_category;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0003A342 File Offset: 0x00038542
		public string SubEventSource
		{
			get
			{
				return this.m_subEventSource;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0003A34A File Offset: 0x0003854A
		public EventCategoryIdentity()
		{
			this.m_server = null;
			this.m_eventSource = null;
			this.m_category = null;
			this.m_subEventSource = null;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003A36E File Offset: 0x0003856E
		private EventCategoryIdentity(string server, string source, string subEventSource, string category)
		{
			this.m_server = server;
			this.m_eventSource = source;
			this.m_category = category;
			this.m_subEventSource = subEventSource;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003A393 File Offset: 0x00038593
		public override byte[] GetBytes()
		{
			return null;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0003A398 File Offset: 0x00038598
		public static EventCategoryIdentity Parse(string input)
		{
			string server = null;
			string source = null;
			string subEventSource = null;
			string category = null;
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (!input.Contains("\\"))
			{
				if (input.StartsWith("MSExchange", StringComparison.OrdinalIgnoreCase))
				{
					source = input;
				}
				else
				{
					category = input;
				}
			}
			else
			{
				int num = input.IndexOf('\\');
				int num2 = input.LastIndexOf('\\');
				string text = input.Substring(0, num);
				if (num == num2)
				{
					if (text.StartsWith("MSExchange", StringComparison.OrdinalIgnoreCase) || text.StartsWith("*") || text.EndsWith("*"))
					{
						source = text;
						category = input.Substring(num + 1);
					}
					else
					{
						server = text;
						source = input.Substring(num + 1);
					}
				}
				else if (text.StartsWith("MSExchange", StringComparison.OrdinalIgnoreCase) || text.StartsWith("*") || text.EndsWith("*"))
				{
					source = text;
					subEventSource = input.Substring(num + 1, num2 - (num + 1));
					if (num2 + 1 != input.Length)
					{
						category = input.Substring(num2 + 1);
					}
				}
				else
				{
					server = text;
					text = input.Substring(num + 1, num2 - (num + 1));
					if (text.Contains("\\"))
					{
						int num3 = text.IndexOf('\\');
						source = text.Substring(0, num3);
						subEventSource = text.Substring(num3 + 1);
					}
					else
					{
						source = text;
					}
					if (num2 + 1 != input.Length)
					{
						category = input.Substring(num2 + 1);
					}
				}
			}
			return new EventCategoryIdentity(server, source, subEventSource, category);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0003A514 File Offset: 0x00038714
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			if (this.m_server != null)
			{
				stringBuilder.Append(this.m_server);
				stringBuilder.Append("\\");
			}
			stringBuilder.Append(this.m_eventSource);
			if (this.m_subEventSource != null)
			{
				stringBuilder.Append("\\");
				stringBuilder.Append(this.m_subEventSource);
			}
			if (this.m_category != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\\");
				}
				stringBuilder.Append(this.m_category);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000B41 RID: 2881
		internal static string[] EventSources = new string[]
		{
			"MSExchange ActiveSync",
			"MSExchange ActiveSync Schema Converter",
			"MSExchange Antispam",
			"MSExchange Assistants",
			"MSExchange Autodiscover",
			"MSExchange Availability",
			"MSExchange Cluster",
			"MSExchange Common",
			"MSExchange RBAC",
			"MSExchange CmdletLogs",
			"MSExchange Configuration Cmdlet - Management Console",
			"MSExchange Delegated Authentication Module",
			"MSExchange LiveId Redirection Module",
			"MSExchange Organization Redirection Module",
			"MSExchange Certificate Authentication Module",
			"MSExchange Control Panel",
			"MSExchange Crash Service",
			"MSExchange Diagnostics Service",
			"MSExchange DsaHelper",
			"MSExchange Extensibility",
			"MSExchange EdgeSync",
			"MSExchange TransportService",
			"MSExchange Web Services",
			"MSExchange IMAP4",
			"MSExchange IMAP4 BE",
			"MSExchange Messaging Policies",
			"MSExchange Anti-spam Update",
			"MSExchange Mailbox Replication",
			"MSExchange MailTips",
			"MSExchange Message Security",
			"MSExchange Message Tracking",
			"MSExchange Mid-Tier Storage",
			"MSExchange Management Application",
			"MSExchange New-ClusteredMailboxServer",
			"MSExchange OWA",
			"MSExchange POP3",
			"MSExchange POP3 BE",
			"MSExchange Process Manager",
			"MSExchange Provisioning Web Service",
			"MSExchange Remove-ClusteredMailboxServer",
			"MSExchange Repl",
			"MSExchange ReportingWebService",
			"MSExchange Search Indexer",
			"MSExchange Security",
			"MSExchange Store Driver",
			"MSExchange System Attendant Mailbox",
			"MSExchange Topology",
			"MSExchange Transcoding",
			"MSExchange Unified Messaging",
			"MSExchange ADAccess",
			"MSExchangeADTopology",
			"MSExchangeApplicationLogic",
			"MSExchangeAL",
			"MSExchangeCAMomConnector",
			"MSExchangeCentralAdmin",
			"MSExchangeFBPublish",
			"MSExchangeIS",
			"MSExchangeMailboxAssistants",
			"MSExchangeGlobalLocatorCache",
			"MSExchangeMU",
			"MSExchangeSA",
			"MSExchangeTransport",
			"MSExchangeIMAP4",
			"MSExchangePOP3",
			"MSExchangeTransportLogSearch",
			"MSExchangeTransportSyncCommon",
			"MSExchangeTransportSyncManager",
			"MSExchangeTransportSyncWorker",
			"MSExchangeTransportSyncWorkerFramework",
			"MSExchange Address Book Service",
			"MSExchange OutlookProtectionRules",
			"MSExchange Provisioning MailboxAssistant",
			"MSExchangeThrottling",
			"MSExchangeThrottlingClient",
			"MSExchange Discovery",
			"MSExchange Bulk User Provisioning",
			"MSExchangeFastSearch",
			"MSExchangeInference",
			"MSExchangeDiagnostics",
			"MSExchange FailFast Module",
			"MSExchange Store Driver Delivery",
			"MSExchangeDelivery",
			"MSExchange Store Driver Submission",
			"MSExchangeSubmission",
			"MSExchange IMAP4 backend service",
			"MSExchange POP3 backend service",
			"MSExchangeIMAP4BE",
			"MSExchangePOP3BE",
			"MSExchange Antimalware",
			"MSExchange OAuth",
			"MSExchange BackEndRehydration",
			"MSExchange Front End HTTP Proxy",
			"MSExchange RemotePowershell BackendCmdletProxy Module",
			"MSExchange Error Logging Module",
			"MSExchange Client Diagnostics Module",
			"MSExchangeFrontEndTransport",
			"MSExchange OSP",
			"MSExchange SmartCard Authentication for OAB",
			"MSExchangeProcessUtilizationManager"
		};

		// Token: 0x04000B42 RID: 2882
		private string m_server;

		// Token: 0x04000B43 RID: 2883
		private string m_eventSource;

		// Token: 0x04000B44 RID: 2884
		private string m_category;

		// Token: 0x04000B45 RID: 2885
		private string m_subEventSource;
	}
}
