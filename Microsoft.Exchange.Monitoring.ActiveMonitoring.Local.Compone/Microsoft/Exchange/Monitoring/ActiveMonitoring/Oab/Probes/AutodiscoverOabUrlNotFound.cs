using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x02000238 RID: 568
	public class AutodiscoverOabUrlNotFound : Exception
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x0006A186 File Offset: 0x00068386
		public AutodiscoverOabUrlNotFound(string user) : base(string.Format("Autodiscover Service failed to return the ExternalOABUrl for user {0}", user))
		{
		}
	}
}
