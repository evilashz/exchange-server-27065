using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa.Probes
{
	// Token: 0x02000265 RID: 613
	public class OwaBackEndMailboxProbe : OwaLocalLogonProbe
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00073FF8 File Offset: 0x000721F8
		protected override string UserAgent
		{
			get
			{
				return "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; ACTIVEMONITORING; OWADEEPTEST)";
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00074000 File Offset: 0x00072200
		internal override ITestStep CreateScenario(Uri targetUri)
		{
			ITestStep testStep = OwaLogonProbe.CreateScenario(base.Definition, targetUri);
			OwaLogin owaLogin = testStep as OwaLogin;
			owaLogin.AuthenticationParameters = OwaUtils.ReadBackendAuthenticationParameters(base.Definition);
			return testStep;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00074034 File Offset: 0x00072234
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
			if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OwaHealthCheckProbe.PopulateDefinition: mailbox role not found on this server", null, "PopulateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaBackendMailboxProbe.cs", 70);
				throw new ArgumentException(Strings.OwaMailboxRoleNotInstalled);
			}
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.MailboxDatabaseEndpoint == null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OwaBackEndMailboxProbe.PopulateDefinition: endpointManager.MailboxDatabaseEndpoint is null", null, "PopulateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaBackendMailboxProbe.cs", 82);
				throw new ArgumentException(Strings.OwaNoMailboxesAvailable);
			}
			if (instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OwaBackEndMailboxProbe.PopulateDefinition: endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend is null or the list is empty", null, "PopulateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaBackendMailboxProbe.cs", 92);
				throw new ArgumentException(Strings.OwaNoMailboxesAvailable);
			}
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			MailboxDatabaseInfo mailboxDatabaseInfo = base.GetMailboxDatabaseInfo(probeDefinition, instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend);
			OwaDiscovery.PopulateOwaMailboxProbeDefinition(probeDefinition, mailboxDatabaseInfo, TimeSpan.Zero);
			definition.RecurrenceIntervalSeconds = 0;
		}

		// Token: 0x04000D17 RID: 3351
		public const string MailboxDatabaseGuidParameterName = "DatabaseGuid";
	}
}
