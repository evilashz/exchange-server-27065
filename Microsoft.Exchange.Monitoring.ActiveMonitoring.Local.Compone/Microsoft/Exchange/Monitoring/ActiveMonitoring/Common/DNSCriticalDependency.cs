using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Responders;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000592 RID: 1426
	internal sealed class DNSCriticalDependency : ICriticalDependency
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000D56BB File Offset: 0x000D38BB
		string ICriticalDependency.Name
		{
			get
			{
				return "DNSCriticalDependency";
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000D56C2 File Offset: 0x000D38C2
		TimeSpan ICriticalDependency.RetestDelay
		{
			get
			{
				return DNSCriticalDependency.RetestDelay;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000D56C9 File Offset: 0x000D38C9
		string ICriticalDependency.EscalationService
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000D56D0 File Offset: 0x000D38D0
		string ICriticalDependency.EscalationTeam
		{
			get
			{
				return "Networking";
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000D56D7 File Offset: 0x000D38D7
		public bool TestCriticalDependency()
		{
			return NetworkAdapterProbe.GetNetworkInterfaceSetting(TracingContext.Default, null, CancellationToken.None);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000D56E9 File Offset: 0x000D38E9
		public bool FixCriticalDependency()
		{
			return NetworkAdapterRecoveryResponder.FixAdapterSettings(NetworkAdapterProbe.MissingEntriesInNetworkAdapter.DnsAddresses, TracingContext.Default);
		}

		// Token: 0x04001984 RID: 6532
		private const string Name = "DNSCriticalDependency";

		// Token: 0x04001985 RID: 6533
		private const string escalationService = "Exchange";

		// Token: 0x04001986 RID: 6534
		private const string escalationTeam = "Networking";

		// Token: 0x04001987 RID: 6535
		private static TimeSpan RetestDelay = TimeSpan.FromSeconds(5.0);
	}
}
