using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.ReplicaVssWriter;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.HA.FailureItem;
using Microsoft.Exchange.HA.ManagedAvailability;
using Microsoft.Exchange.HA.Services;
using Microsoft.Exchange.HA.SupportApi;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayComponentManager : ComponentManager
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000271D File Offset: 0x0000091D
		internal static ReplayComponentManager Instance
		{
			get
			{
				return ReplayComponentManager.s_rcm;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002724 File Offset: 0x00000924
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000272C File Offset: 0x0000092C
		internal ADConfigLookupComponent ADLookupComponent { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002735 File Offset: 0x00000935
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000273D File Offset: 0x0000093D
		internal CopyStatusLookupComponent CopyStatusLookupComponent { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002746 File Offset: 0x00000946
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000274E File Offset: 0x0000094E
		internal IReplayCoreManager ReplayCoreManager { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002757 File Offset: 0x00000957
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000275F File Offset: 0x0000095F
		internal SeedManager SeedManager { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002768 File Offset: 0x00000968
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002770 File Offset: 0x00000970
		internal AutoReseedManager AutoReseedManager { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002779 File Offset: 0x00000979
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002781 File Offset: 0x00000981
		internal ReplayRpcServerWrapper ReplayRpcServerWrapper { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000278A File Offset: 0x0000098A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002792 File Offset: 0x00000992
		internal RemoteDataProviderWrapper RemoteDataProviderWrapper { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000027A3 File Offset: 0x000009A3
		internal CReplicaVssWriterInterop VssWriter { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027AC File Offset: 0x000009AC
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000027B4 File Offset: 0x000009B4
		internal ActiveManagerCore ActiveManager { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000027BD File Offset: 0x000009BD
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000027C5 File Offset: 0x000009C5
		internal AmRpcServerWrapper ActiveManagerRpcServerWrapper { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027CE File Offset: 0x000009CE
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027D6 File Offset: 0x000009D6
		internal FailureItemManager FailureItemManager { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000027DF File Offset: 0x000009DF
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000027E7 File Offset: 0x000009E7
		internal ThirdPartyManager ThirdPartyManager { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000027F8 File Offset: 0x000009F8
		internal SupportApiManager SupportApiManager { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002801 File Offset: 0x00000A01
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002809 File Offset: 0x00000A09
		internal ServerLocatorManager ServerLocatorManager { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002812 File Offset: 0x00000A12
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000281A File Offset: 0x00000A1A
		internal HealthStateTracker HealthStateTracker { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002823 File Offset: 0x00000A23
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000282B File Offset: 0x00000A2B
		internal DiskReclaimerManager DiskReclaimerManager { get; private set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002834 File Offset: 0x00000A34
		private ReplayComponentManager() : base(TimeSpan.FromMilliseconds((double)RegistryParameters.ConfigUpdaterTimerIntervalSlow))
		{
			Dependencies.Container.RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.IgnoreInvalidAdSession.ToString(), new ReplayAdObjectLookup());
			Dependencies.Container.RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.PartiallyConsistentAdSession.ToString(), new ReplayAdObjectLookupPartiallyConsistent());
			SharedDependencies.Container.RegisterInstance<IAmServerNameLookup>(new AmServerNameCacheLogEvent());
			this.ReplayCoreManager = new ReplayCoreManager();
			Dependencies.Container.RegisterInstance<IReplayCoreManager>(this.ReplayCoreManager);
			ReplicaInstanceManager replicaInstanceManager = this.ReplayCoreManager.ReplicaInstanceManager;
			this.ADLookupComponent = new ADConfigLookupComponent();
			Dependencies.Container.RegisterInstance<IADConfig>(new ADConfig(this.ADLookupComponent.ADConfigManager));
			this.CopyStatusLookupComponent = new CopyStatusLookupComponent();
			this.SeedManager = new SeedManager(replicaInstanceManager);
			this.HealthStateTracker = new HealthStateTracker();
			this.AutoReseedManager = new AutoReseedManager(this.ADLookupComponent.ADConfigManager, this.CopyStatusLookupComponent.CopyStatusLookup, replicaInstanceManager);
			this.DiskReclaimerManager = new DiskReclaimerManager(this.ADLookupComponent.ADConfigManager, this.CopyStatusLookupComponent.CopyStatusLookup, replicaInstanceManager);
			this.ReplayRpcServerWrapper = new ReplayRpcServerWrapper(replicaInstanceManager, this.SeedManager, this.HealthStateTracker);
			this.RemoteDataProviderWrapper = new RemoteDataProviderWrapper();
			this.VssWriter = new CReplicaVssWriterInterop(replicaInstanceManager);
			this.ActiveManager = new ActiveManagerCore(replicaInstanceManager, replicaInstanceManager.AdLookup, Dependencies.ADConfig);
			this.ActiveManagerRpcServerWrapper = new AmRpcServerWrapper(this.ActiveManager);
			this.FailureItemManager = new FailureItemManager(Dependencies.ADConfig);
			this.ThirdPartyManager = ThirdPartyManager.Instance;
			this.SupportApiManager = SupportApiManager.Instance;
			this.ServerLocatorManager = ServerLocatorManager.Instance;
			replicaInstanceManager.SeedManagerInstance = this.SeedManager;
			this.m_components = new IServiceComponent[]
			{
				this.ADLookupComponent,
				this.ThirdPartyManager,
				this.RemoteDataProviderWrapper,
				this.HealthStateTracker,
				this.SeedManager,
				this.ReplayCoreManager,
				this.ReplayRpcServerWrapper,
				this.ActiveManager,
				this.ActiveManagerRpcServerWrapper,
				this.CopyStatusLookupComponent,
				this.ServerLocatorManager,
				this.VssWriter,
				this.FailureItemManager,
				this.AutoReseedManager,
				this.SupportApiManager,
				this.DiskReclaimerManager
			};
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A88 File Offset: 0x00000C88
		public override void Start()
		{
			this.LogServiceStarting();
			if (!RegistryParameters.DisablePriorityBoost)
			{
				try
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						int processorCount = Environment.ProcessorCount;
						int i = Math.Min(16, Math.Max(processorCount / 4, 1));
						long num = (long)currentProcess.ProcessorAffinity;
						long num2 = 1L;
						while (i > 0)
						{
							num &= ~num2;
							num2 <<= 1;
							i--;
						}
						currentProcess.ProcessorAffinity = (IntPtr)num;
						currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
						ExTraceGlobals.ReplayManagerTracer.TraceDebug<long>((long)this.GetHashCode(), "PriorityBoost AboveNormal. ProcessorMask=0x{0:X}", num);
					}
				}
				catch (Win32Exception arg)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceError<Win32Exception>((long)this.GetHashCode(), "PriorityBoost failed {0}", arg);
				}
			}
			base.Start();
			this.LogServiceStarted();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B68 File Offset: 0x00000D68
		public override void Stop()
		{
			ReplayEventLogConstants.Tuple_ServiceStopping.LogEvent(null, new object[0]);
			ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "Replay service is stopping...");
			base.Stop();
			ReplayEventLogConstants.Tuple_ServiceStopped.LogEvent(null, new object[0]);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BA8 File Offset: 0x00000DA8
		private void LogServiceStarting()
		{
			ReplayEventLogConstants.Tuple_ServiceStarting.LogEvent(null, new object[0]);
			ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "Replay service is starting...");
			ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} Replay service is starting...", 16669);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private void LogServiceStarted()
		{
			ReplayEventLogConstants.Tuple_ServiceStarted.LogEvent(null, new object[0]);
			ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "Replay service started.");
			ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} Exchange Replication service started", 22813);
		}

		// Token: 0x04000009 RID: 9
		private static ReplayComponentManager s_rcm = new ReplayComponentManager();
	}
}
