using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HA.ManagedAvailability;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayRpcServerWrapper : IServiceComponent
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003487 File Offset: 0x00001687
		public ReplayRpcServerWrapper(IReplicaInstanceManager replicaInstanceManager, SeedManager seedManager, HealthStateTracker healthStateTracker)
		{
			this.m_replicaInstanceManager = replicaInstanceManager;
			this.m_seedmanager = seedManager;
			this.m_healthStateTracker = healthStateTracker;
			ReplayServerPerfmon.GetCopyStatusServerCalls.RawValue = 0L;
			ReplayServerPerfmon.GetCopyStatusServerCallsPerSec.RawValue = 0L;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000034BC File Offset: 0x000016BC
		public string Name
		{
			get
			{
				return "Tasks RPC Server";
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000034C3 File Offset: 0x000016C3
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.TasksRPCServer;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000034C6 File Offset: 0x000016C6
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000034C9 File Offset: 0x000016C9
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000034CC File Offset: 0x000016CC
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000034CF File Offset: 0x000016CF
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000034D7 File Offset: 0x000016D7
		public bool Start()
		{
			return ReplayRpcServer.TryStart(this.m_replicaInstanceManager, this.m_seedmanager, this.m_healthStateTracker);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000034F0 File Offset: 0x000016F0
		public void Stop()
		{
			ReplayRpcServer.Stop();
		}

		// Token: 0x0400001A RID: 26
		private SeedManager m_seedmanager;

		// Token: 0x0400001B RID: 27
		private IReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x0400001C RID: 28
		private HealthStateTracker m_healthStateTracker;
	}
}
