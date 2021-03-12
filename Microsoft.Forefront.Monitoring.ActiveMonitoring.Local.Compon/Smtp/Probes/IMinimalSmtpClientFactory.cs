using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200020E RID: 526
	public interface IMinimalSmtpClientFactory
	{
		// Token: 0x0600100D RID: 4109
		IMinimalSmtpClient CreateSmtpClient(string host, SmtpProbeWorkDefinition workDefinition, DelTraceDebug traceDebug);
	}
}
