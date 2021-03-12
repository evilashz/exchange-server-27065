using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000554 RID: 1364
	internal interface IObserverFactory
	{
		// Token: 0x060021F3 RID: 8691
		IObserver CreateObserver(string serverName);
	}
}
