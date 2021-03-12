using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000555 RID: 1365
	internal interface ISubject
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060021F4 RID: 8692
		string ServerName { get; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060021F5 RID: 8693
		bool IsInMaintenance { get; }

		// Token: 0x060021F6 RID: 8694
		IEnumerable<IObserver> GetAllObservers();

		// Token: 0x060021F7 RID: 8695
		bool TryAddObserver(IObserver observer);

		// Token: 0x060021F8 RID: 8696
		void RemoveObserver(IObserver observer);

		// Token: 0x060021F9 RID: 8697
		bool SendRequest(IObserver observer);

		// Token: 0x060021FA RID: 8698
		void SendCancel(IObserver observer);

		// Token: 0x060021FB RID: 8699
		DateTime? GetLastResultTimestamp();

		// Token: 0x060021FC RID: 8700
		IPStatus Ping(TimeSpan timeout);

		// Token: 0x060021FD RID: 8701
		DateTime? GetLastObserverSelectionTimestamp();

		// Token: 0x060021FE RID: 8702
		DateTime? GetObserverHeartbeat(IObserver observer);
	}
}
