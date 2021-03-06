using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200021C RID: 540
	public class DatacenterHubOutboundProxyProbe : SmtpConnectionProbe
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000338DC File Offset: 0x00031ADC
		private string MailFromCommand
		{
			get
			{
				return string.Format("MAIL FROM:outboundproxy@outboundproxy.com{0}{1}", this.Client.IsXSysProbeAdvertised ? " XSYSPROBEID=" : string.Empty, this.Client.IsXSysProbeAdvertised ? base.GetProbeId() : string.Empty);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003391C File Offset: 0x00031B1C
		protected override void BeforeConnect()
		{
			if (!base.Broker.IsLocal())
			{
				throw new SmtpConnectionProbeException("DatacenterHubOutboundProbe is a local-only probe and should not be used outside in");
			}
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance == null || instance.MailboxDatabaseEndpoint == null)
			{
				throw new SmtpConnectionProbeException("No MailboxDatabaseEndpoint for Backend found on this server");
			}
			if (instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				base.Result.StateAttribute2 = "No mailboxes found, proceeding as success";
				base.CancelProbeWithSuccess = true;
				return;
			}
			this.mailboxCollectionForBackend = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			base.WorkDefinition.MailFrom = null;
			base.WorkDefinition.MailTo = null;
			base.WorkDefinition.Data = null;
			string smtpServer;
			if (!ProxyProbeCommon.TryGetSmtpServer(out smtpServer))
			{
				throw new SmtpConnectionProbeException("No outbound frontend servers configured");
			}
			base.TestCount = 1;
			base.WorkDefinition.Port = 25;
			base.WorkDefinition.SmtpServer = smtpServer;
			base.WorkDefinition.HeloDomain = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			string value;
			bool flag;
			if (!base.Definition.Attributes.TryGetValue("OutboundProxyConnectionOnly", out value) || string.IsNullOrEmpty(value) || !bool.TryParse(value, out flag))
			{
				flag = true;
			}
			if (!flag)
			{
				base.WorkDefinition.UseSsl = true;
				ProxyProbeCommon.ApplyCertificateCriteria(base.WorkDefinition, base.Definition.Attributes);
				this.Client.IgnoreCertificateNameMismatchPolicyError = true;
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00033A94 File Offset: 0x00031C94
		protected override void AfterHeloAfterStartTls()
		{
			if (base.ShouldCancelProbe())
			{
				return;
			}
			MailboxDatabaseInfo mailboxDatabaseInfo = this.mailboxCollectionForBackend.ElementAt(0);
			string monitoringAccountDomain = mailboxDatabaseInfo.MonitoringAccountDomain;
			if (string.IsNullOrEmpty(monitoringAccountDomain))
			{
				throw new SmtpConnectionProbeException("MonitoringAccountDomain is null or empty.");
			}
			string xproxyToCommand = string.Format("XPROXYTO DESTINATIONS={0} PORT=25 SESSIONID=OPP LAST=True", monitoringAccountDomain);
			base.MeasureLatency("XPROXYTO", delegate()
			{
				this.Client.Send(xproxyToCommand);
			});
			if (!this.Client.LastResponse.StartsWith("250"))
			{
				throw new SmtpConnectionProbeException("Response not as expected. Actual: " + this.Client.LastResponse);
			}
			base.MeasureLatency("MAILFROM", delegate()
			{
				this.Client.Send(this.MailFromCommand);
			});
			if (string.IsNullOrEmpty(this.Client.LastResponse))
			{
				throw new SmtpConnectionProbeException("No response from MAIL FROM. This means a proxy session was not established correctly.");
			}
		}

		// Token: 0x0400082D RID: 2093
		private const string OutboundProxyConnectionOnly = "OutboundProxyConnectionOnly";

		// Token: 0x0400082E RID: 2094
		private const string MessageFormat = "X-MS-Exchange-ActiveMonitoringProbeName:{0}\r\nSubject:Outbound proxy from {1} at {2}\r\n\r\nThis is an outbound proxy probe";

		// Token: 0x0400082F RID: 2095
		private const string XProxyToCommandFormat = "XPROXYTO DESTINATIONS={0} PORT=25 SESSIONID=OPP LAST=True";

		// Token: 0x04000830 RID: 2096
		private ICollection<MailboxDatabaseInfo> mailboxCollectionForBackend;
	}
}
