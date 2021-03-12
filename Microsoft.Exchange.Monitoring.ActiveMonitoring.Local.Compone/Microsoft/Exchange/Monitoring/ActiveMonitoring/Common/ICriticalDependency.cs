using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000591 RID: 1425
	public interface ICriticalDependency
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060023A6 RID: 9126
		string Name { get; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060023A7 RID: 9127
		TimeSpan RetestDelay { get; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060023A8 RID: 9128
		string EscalationService { get; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060023A9 RID: 9129
		string EscalationTeam { get; }

		// Token: 0x060023AA RID: 9130
		bool TestCriticalDependency();

		// Token: 0x060023AB RID: 9131
		bool FixCriticalDependency();
	}
}
