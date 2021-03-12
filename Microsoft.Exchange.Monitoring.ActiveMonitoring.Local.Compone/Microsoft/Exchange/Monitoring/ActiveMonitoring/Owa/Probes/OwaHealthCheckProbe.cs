using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa.Probes
{
	// Token: 0x02000268 RID: 616
	public class OwaHealthCheckProbe : OwaBaseProbe
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00075700 File Offset: 0x00073900
		protected override string UserAgent
		{
			get
			{
				return "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; ACTIVEMONITORING; OWASELFTEST)";
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00075708 File Offset: 0x00073908
		internal static ITestStep CreateScenario(ProbeDefinition probeDefinition, Uri targetUri)
		{
			ITestFactory testFactory = new TestFactory();
			return testFactory.CreateOwaHealthCheckScenario(targetUri, testFactory);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00075725 File Offset: 0x00073925
		internal override ITestStep CreateScenario(Uri targetUri)
		{
			return OwaHealthCheckProbe.CreateScenario(base.Definition, targetUri);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00075734 File Offset: 0x00073934
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
			if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OwaHealthCheckProbe.PopulateDefinition: mailbox role not found on this server", null, "PopulateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaHealthCheckProbe.cs", 74);
				throw new ArgumentException(Strings.OwaMailboxRoleNotInstalled);
			}
			OwaDiscovery.PopulateOwaProtocolProbeDefinition(definition as ProbeDefinition);
			definition.RecurrenceIntervalSeconds = 0;
		}
	}
}
