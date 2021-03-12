using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public class HighAvailabilityMAProbeException : Exception
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0004A0F6 File Offset: 0x000482F6
		public HighAvailabilityMAProbeException(string message) : base(message)
		{
		}
	}
}
