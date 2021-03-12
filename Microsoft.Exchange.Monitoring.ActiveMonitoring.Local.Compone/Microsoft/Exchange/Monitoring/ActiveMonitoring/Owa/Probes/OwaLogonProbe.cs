using System;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa.Probes
{
	// Token: 0x02000263 RID: 611
	public abstract class OwaLogonProbe : OwaBaseProbe
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x00073E84 File Offset: 0x00072084
		internal static ITestStep CreateScenario(ProbeDefinition probeDefinition, Uri targetUri)
		{
			string userDomain = probeDefinition.Account.Substring(probeDefinition.Account.IndexOf('@') + 1);
			OwaLoginParameters owaLoginParameters = new OwaLoginParameters();
			owaLoginParameters.ShouldDownloadStaticFile = bool.Parse(probeDefinition.Attributes.GetValueOrDefault("DownloadStaticFile", "true"));
			owaLoginParameters.ShouldDownloadStaticFileOnLogonPage = bool.Parse(probeDefinition.Attributes.GetValueOrDefault("DownloadStaticFileOnLogon", "false"));
			owaLoginParameters.ShouldMeasureClientLatency = bool.Parse(probeDefinition.Attributes.GetValueOrDefault("MeasureClientLatency", "true"));
			ITestFactory testFactory = new TestFactory();
			return testFactory.CreateOwaLoginScenario(targetUri, probeDefinition.Account, userDomain, (probeDefinition.AccountPassword != null) ? probeDefinition.AccountPassword.ConvertToSecureString() : null, owaLoginParameters, testFactory);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00073F40 File Offset: 0x00072140
		internal override ITestStep CreateScenario(Uri targetUri)
		{
			return OwaLogonProbe.CreateScenario(base.Definition, targetUri);
		}

		// Token: 0x04000D14 RID: 3348
		public const string DownloadStaticFileParameterName = "DownloadStaticFile";

		// Token: 0x04000D15 RID: 3349
		public const string DownloadStaticFileOnLogonParameterName = "DownloadStaticFileOnLogon";

		// Token: 0x04000D16 RID: 3350
		public const string MeasureClientLatencyParameterName = "MeasureClientLatency";
	}
}
