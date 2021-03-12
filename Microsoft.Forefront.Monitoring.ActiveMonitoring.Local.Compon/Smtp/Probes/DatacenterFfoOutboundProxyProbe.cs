using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200021B RID: 539
	public class DatacenterFfoOutboundProxyProbe : SmtpConnectionProbe
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x00033718 File Offset: 0x00031918
		protected override void BeforeConnect()
		{
			if (!base.Broker.IsLocal())
			{
				throw new SmtpConnectionProbeException("DatacenterFfoOutboundProxyProbe is a local-only probe and should not be used outside in");
			}
			if (base.WorkDefinition.MailFrom == null)
			{
				throw new SmtpConnectionProbeException("MailFrom is null");
			}
			if (base.WorkDefinition.MailTo == null)
			{
				throw new SmtpConnectionProbeException("MailTo is null");
			}
			if (base.WorkDefinition.MailTo.Count == 0)
			{
				throw new SmtpConnectionProbeException("MailTo has no recipients");
			}
			string smtpServer;
			if (!ProxyProbeCommon.TryGetSmtpServer(out smtpServer))
			{
				throw new SmtpConnectionProbeException("No outbound frontend servers configured");
			}
			base.TestCount = 1;
			base.WorkDefinition.Port = 25;
			base.WorkDefinition.SmtpServer = smtpServer;
			base.WorkDefinition.HeloDomain = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			base.WorkDefinition.Data = null;
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

		// Token: 0x0600118E RID: 4494 RVA: 0x00033838 File Offset: 0x00031A38
		protected override void AfterHeloAfterStartTls()
		{
			new StringBuilder();
			string username = base.WorkDefinition.MailTo.ElementAt(0).Username;
			RoutingAddress routingAddress = new RoutingAddress(username);
			if (!routingAddress.IsValid)
			{
				throw new SmtpConnectionProbeException("Invalid monitoring recipient address: " + username);
			}
			string text = string.Format("XPROXYTO DESTINATIONS={0} PORT=25 SESSIONID=OPP PROBE=1 LAST=True", routingAddress.DomainPart);
			this.Client.Send(text);
			if (!this.Client.LastResponse.StartsWith("250"))
			{
				throw new SmtpConnectionProbeException("Response not as expected. Actual: " + this.Client.LastResponse);
			}
		}

		// Token: 0x0400082B RID: 2091
		private const string OutboundProxyConnectionOnly = "OutboundProxyConnectionOnly";

		// Token: 0x0400082C RID: 2092
		private const string XProxyToCommandFormat = "XPROXYTO DESTINATIONS={0} PORT=25 SESSIONID=OPP PROBE=1 LAST=True";
	}
}
