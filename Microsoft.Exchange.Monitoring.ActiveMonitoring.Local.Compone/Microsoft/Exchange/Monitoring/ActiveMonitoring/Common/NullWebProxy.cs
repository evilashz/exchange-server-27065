using System;
using System.Net;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200012D RID: 301
	public class NullWebProxy : IWebProxy
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00033EA9 File Offset: 0x000320A9
		public static NullWebProxy Instance
		{
			get
			{
				return NullWebProxy.instance;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00033EB0 File Offset: 0x000320B0
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00033EB8 File Offset: 0x000320B8
		public ICredentials Credentials { get; set; }

		// Token: 0x060008E7 RID: 2279 RVA: 0x00033EC1 File Offset: 0x000320C1
		public Uri GetProxy(Uri destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00033EC8 File Offset: 0x000320C8
		public bool IsBypassed(Uri host)
		{
			return true;
		}

		// Token: 0x04000612 RID: 1554
		private static NullWebProxy instance = new NullWebProxy();
	}
}
