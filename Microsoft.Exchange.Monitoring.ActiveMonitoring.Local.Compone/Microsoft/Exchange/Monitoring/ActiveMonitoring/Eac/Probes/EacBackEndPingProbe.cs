using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Eac.Probes
{
	// Token: 0x02000165 RID: 357
	public class EacBackEndPingProbe : EacWebClientProbeBase
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x00040800 File Offset: 0x0003EA00
		internal override Task ExecuteScenario(IHttpSession session)
		{
			Uri uri = new Uri(base.Definition.Endpoint);
			ITestStep testStep = new EcpPing(uri);
			return testStep.CreateTask(session);
		}
	}
}
