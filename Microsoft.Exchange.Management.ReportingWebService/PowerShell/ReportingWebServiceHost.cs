using System;
using System.Management.Automation.Host;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000012 RID: 18
	internal class ReportingWebServiceHost : RunspaceHost
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002ED2 File Offset: 0x000010D2
		public ReportingWebServiceHost() : base(true)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002EF0 File Offset: 0x000010F0
		public override string Name
		{
			get
			{
				return "Reporting Web Service";
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F14 File Offset: 0x00001114
		public override void Activate()
		{
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.ActivateReportingWebServiceHostLatency, delegate
			{
				ReportingWebServiceHost.activeRunspaceCounters.Increment();
				this.averageActiveRunspace.Start();
				this.<>n__FabricatedMethod1();
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F46 File Offset: 0x00001146
		public override void Deactivate()
		{
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.DeactivateReportingWebServiceHostLatency, delegate
			{
				this.averageActiveRunspace.Stop();
				ReportingWebServiceHost.activeRunspaceCounters.Decrement();
				this.<>n__FabricatedMethod3();
			});
		}

		// Token: 0x04000037 RID: 55
		private const string RunspaceName = "Reporting Web Service";

		// Token: 0x04000038 RID: 56
		public static readonly PSHostFactory Factory = new ReportingWebServiceHost.ReportingWebServiceHostFactory();

		// Token: 0x04000039 RID: 57
		private static readonly PerfCounterGroup activeRunspaceCounters = new PerfCounterGroup(RwsPerfCounters.ActiveRunspaces, RwsPerfCounters.ActiveRunspacesPeak, RwsPerfCounters.ActiveRunspacesTotal);

		// Token: 0x0400003A RID: 58
		private readonly AverageTimePerfCounter averageActiveRunspace = new AverageTimePerfCounter(RwsPerfCounters.AverageActiveRunspace, RwsPerfCounters.AverageActiveRunspaceBase);

		// Token: 0x02000013 RID: 19
		internal class ReportingWebServiceHostFactory : PSHostFactory
		{
			// Token: 0x0600005B RID: 91 RVA: 0x00002FA8 File Offset: 0x000011A8
			public override PSHost CreatePSHost()
			{
				ReportingWebServiceHost host = null;
				ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CreatePSHostLatency, delegate
				{
					host = new ReportingWebServiceHost();
				});
				return host;
			}
		}
	}
}
