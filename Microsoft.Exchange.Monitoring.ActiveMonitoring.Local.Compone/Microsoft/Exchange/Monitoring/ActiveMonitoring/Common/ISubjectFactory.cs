using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000556 RID: 1366
	internal interface ISubjectFactory
	{
		// Token: 0x060021FF RID: 8703
		ISubject CreateSubject(string serverName);
	}
}
