using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph.Probes
{
	// Token: 0x02000253 RID: 595
	public class OfficeGraphProbeFailureException : Exception
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x0006F13C File Offset: 0x0006D33C
		public OfficeGraphProbeFailureException(string message) : base(message)
		{
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0006F145 File Offset: 0x0006D345
		public OfficeGraphProbeFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
