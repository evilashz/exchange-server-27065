using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000553 RID: 1363
	internal interface IObserver
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060021ED RID: 8685
		string ServerName { get; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060021EE RID: 8686
		bool IsInMaintenance { get; }

		// Token: 0x060021EF RID: 8687
		IEnumerable<ISubject> GetAllSubjects();

		// Token: 0x060021F0 RID: 8688
		bool TryAddSubject(ISubject subject);

		// Token: 0x060021F1 RID: 8689
		void RemoveSubject(ISubject subject);

		// Token: 0x060021F2 RID: 8690
		ObserverHeartbeatResponse SendHeartbeat(ISubject subject);
	}
}
