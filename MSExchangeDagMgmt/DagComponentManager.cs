using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;
using Microsoft.Exchange.DxStore.HA;

namespace Microsoft.Exchange.Cluster.DagService
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DagComponentManager : ComponentManager
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private DagComponentManager() : base(TimeSpan.FromMilliseconds((double)RegistryParameters.ConfigUpdaterTimerIntervalSlow))
		{
			Dependencies.Container.RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.IgnoreInvalidAdSession.ToString(), new ReplayAdObjectLookup());
			Dependencies.Container.RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.PartiallyConsistentAdSession.ToString(), new ReplayAdObjectLookupPartiallyConsistent());
			SharedDependencies.Container.RegisterInstance<IAmServerNameLookup>(new AmServerNameCacheLogEvent());
			this.ADLookupComponent = new ADConfigLookupComponent();
			Dependencies.Container.RegisterInstance<IADConfig>(new ADConfig(this.ADLookupComponent.ADConfigManager));
			this.CopyStatusLookupComponent = new CopyStatusLookupComponent();
			this.MonitoringComponent = new MonitoringComponent();
			this.MonitoringServiceManager = new MonitoringServiceManager(this.MonitoringComponent.DatabaseHealthTracker);
			if (!RegistryParameters.DisableDxStoreManager)
			{
				this.DxStoreManagerComponent = new DistributedStoreManagerComponent();
				this.m_components = new IServiceComponent[]
				{
					this.ADLookupComponent,
					this.DxStoreManagerComponent,
					this.CopyStatusLookupComponent,
					this.MonitoringServiceManager,
					this.MonitoringComponent
				};
				return;
			}
			this.m_components = new IServiceComponent[]
			{
				this.ADLookupComponent,
				this.CopyStatusLookupComponent,
				this.MonitoringServiceManager,
				this.MonitoringComponent
			};
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002206 File Offset: 0x00000406
		internal static DagComponentManager Instance
		{
			get
			{
				return DagComponentManager.dcm;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002215 File Offset: 0x00000415
		internal ADConfigLookupComponent ADLookupComponent { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002226 File Offset: 0x00000426
		internal CopyStatusLookupComponent CopyStatusLookupComponent { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000222F File Offset: 0x0000042F
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002237 File Offset: 0x00000437
		internal MonitoringComponent MonitoringComponent { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002248 File Offset: 0x00000448
		internal MonitoringServiceManager MonitoringServiceManager { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002251 File Offset: 0x00000451
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002259 File Offset: 0x00000459
		internal DistributedStoreManagerComponent DxStoreManagerComponent { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002262 File Offset: 0x00000462
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServiceTracer;
			}
		}

		// Token: 0x04000001 RID: 1
		private static DagComponentManager dcm = new DagComponentManager();
	}
}
