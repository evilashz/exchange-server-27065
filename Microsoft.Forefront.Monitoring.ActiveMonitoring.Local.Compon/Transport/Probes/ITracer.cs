using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000279 RID: 633
	internal interface ITracer
	{
		// Token: 0x060014DF RID: 5343
		void TraceDebug(string debugInfo);

		// Token: 0x060014E0 RID: 5344
		void TraceError(string errorInfo);
	}
}
