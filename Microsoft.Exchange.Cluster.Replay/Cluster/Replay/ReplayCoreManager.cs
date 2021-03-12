using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002F3 RID: 755
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReplayCoreManager : IReplayCoreManager, IServiceComponent
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0008A7D4 File Offset: 0x000889D4
		internal ReplayCoreManager()
		{
			this.SafetyNetVersionChecker = new SafetyNetVersionChecker();
			Dependencies.Container.RegisterInstance<ISafetyNetVersionCheck>(this.SafetyNetVersionChecker);
			this.DumpsterRedeliveryManager = new DumpsterRedeliveryManager();
			this.SkippedLogsDeleter = new SkippedLogsDeleter();
			this.ReplicaInstanceManager = new ReplicaInstanceManager(this.DumpsterRedeliveryManager, this.SkippedLogsDeleter);
			this.SystemQueue = new ReplaySystemQueue();
			this.SearchServiceMonitor = new AmSearchServiceMonitor();
			ConfigurationUpdater configurationUpdater = new ConfigurationUpdater(this.ReplicaInstanceManager, this.SystemQueue);
			Dependencies.Container.RegisterInstance<IRunConfigurationUpdater>(configurationUpdater);
			this.HealthThread = new HealthThread();
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x0008A86F File Offset: 0x00088A6F
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x0008A877 File Offset: 0x00088A77
		public ReplicaInstanceManager ReplicaInstanceManager { get; private set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0008A880 File Offset: 0x00088A80
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0008A888 File Offset: 0x00088A88
		public ReplaySystemQueue SystemQueue { get; private set; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0008A891 File Offset: 0x00088A91
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x0008A899 File Offset: 0x00088A99
		public DumpsterRedeliveryManager DumpsterRedeliveryManager { get; private set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0008A8A2 File Offset: 0x00088AA2
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0008A8AA File Offset: 0x00088AAA
		public SafetyNetVersionChecker SafetyNetVersionChecker { get; private set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0008A8B3 File Offset: 0x00088AB3
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0008A8BB File Offset: 0x00088ABB
		public SkippedLogsDeleter SkippedLogsDeleter { get; private set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0008A8C4 File Offset: 0x00088AC4
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0008A8CC File Offset: 0x00088ACC
		public AmSearchServiceMonitor SearchServiceMonitor { get; private set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0008A8D5 File Offset: 0x00088AD5
		public IRunConfigurationUpdater ConfigurationUpdater
		{
			get
			{
				return Dependencies.ConfigurationUpdater;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0008A8DC File Offset: 0x00088ADC
		public string Name
		{
			get
			{
				return "Replication Manager";
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0008A8E3 File Offset: 0x00088AE3
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ReplicationManager;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0008A8E6 File Offset: 0x00088AE6
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0008A8E9 File Offset: 0x00088AE9
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0008A8EC File Offset: 0x00088AEC
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0008A8EF File Offset: 0x00088AEF
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0008A8F8 File Offset: 0x00088AF8
		public bool Start()
		{
			this.SafetyNetVersionChecker.Start();
			this.ConfigurationUpdater.Start();
			this.DumpsterRedeliveryManager.Start();
			this.SkippedLogsDeleter.Start();
			this.SearchServiceMonitor.Start();
			EseHelper.GlobalInit();
			ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} EseHelper Initialization", 31901);
			if (this.HealthThread != null)
			{
				this.HealthThread.Start();
			}
			return true;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0008A970 File Offset: 0x00088B70
		public void Stop()
		{
			this.ReplicaInstanceManager.PrepareToStop();
			this.ConfigurationUpdater.PrepareToStop();
			this.SystemQueue.Stop();
			this.DumpsterRedeliveryManager.Stop();
			this.ConfigurationUpdater.Stop();
			this.SkippedLogsDeleter.Stop();
			this.SearchServiceMonitor.Stop();
			this.ReplicaInstanceManager.Stop();
			if (this.HealthThread != null)
			{
				this.HealthThread.Stop();
			}
			this.SafetyNetVersionChecker.Stop();
		}

		// Token: 0x04000CC9 RID: 3273
		internal HealthThread HealthThread;
	}
}
