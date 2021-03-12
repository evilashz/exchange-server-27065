using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000558 RID: 1368
	internal class ObserverFactory : IObserverFactory
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x000CD9A8 File Offset: 0x000CBBA8
		public IObserver CreateObserver(string serverName)
		{
			return new Observer(serverName);
		}
	}
}
