using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x0200022B RID: 555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MonitoringComponent : IServiceComponent
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00053DB9 File Offset: 0x00051FB9
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00053DC0 File Offset: 0x00051FC0
		internal MonitoringComponent()
		{
			IMonitoringADConfigProvider monitoringADConfigProvider = Dependencies.MonitoringADConfigProvider;
			ICopyStatusClientLookup monitoringCopyStatusClientLookup = Dependencies.MonitoringCopyStatusClientLookup;
			if (!RegistryParameters.DatabaseHealthMonitorDisabled)
			{
				this.DatabaseHealthMonitor = new DatabaseHealthMonitor(monitoringADConfigProvider, monitoringCopyStatusClientLookup);
			}
			if (!RegistryParameters.DatabaseHealthTrackerDisabled)
			{
				this.DatabaseHealthTracker = new DatabaseHealthTracker(monitoringADConfigProvider, monitoringCopyStatusClientLookup);
			}
			if (!RegistryParameters.ReplayLagManagerDisabled)
			{
				this.ReplayLagManager = new ReplayLagManager(monitoringADConfigProvider, monitoringCopyStatusClientLookup);
			}
			if (!RegistryParameters.SpaceMonitorDisabled)
			{
				this.SpaceMonitor = new SpaceMonitor(monitoringADConfigProvider, monitoringCopyStatusClientLookup);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00053E2F File Offset: 0x0005202F
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x00053E37 File Offset: 0x00052037
		public ReplayLagManager ReplayLagManager { get; private set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00053E40 File Offset: 0x00052040
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x00053E48 File Offset: 0x00052048
		public SpaceMonitor SpaceMonitor { get; private set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00053E51 File Offset: 0x00052051
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00053E59 File Offset: 0x00052059
		public DatabaseHealthMonitor DatabaseHealthMonitor { get; private set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00053E62 File Offset: 0x00052062
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00053E6A File Offset: 0x0005206A
		public DatabaseHealthTracker DatabaseHealthTracker { get; private set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00053E73 File Offset: 0x00052073
		public string Name
		{
			get
			{
				return "Monitoring Component";
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00053E7A File Offset: 0x0005207A
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.MonitoringComponent;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00053E7E File Offset: 0x0005207E
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00053E81 File Offset: 0x00052081
		public bool IsEnabled
		{
			get
			{
				return !RegistryParameters.MonitoringComponentDisabled;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00053E8B File Offset: 0x0005208B
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00053E8E File Offset: 0x0005208E
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00053E98 File Offset: 0x00052098
		public bool Start()
		{
			if (this.DatabaseHealthMonitor != null)
			{
				this.DatabaseHealthMonitor.Start();
			}
			if (this.DatabaseHealthTracker != null)
			{
				this.DatabaseHealthTracker.Start();
			}
			if (this.ReplayLagManager != null)
			{
				this.ReplayLagManager.Start();
			}
			if (this.SpaceMonitor != null)
			{
				this.SpaceMonitor.Start();
			}
			return true;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00053EF4 File Offset: 0x000520F4
		public void Stop()
		{
			if (this.SpaceMonitor != null)
			{
				this.SpaceMonitor.Stop();
			}
			if (this.ReplayLagManager != null)
			{
				this.ReplayLagManager.Stop();
			}
			if (this.DatabaseHealthTracker != null)
			{
				this.DatabaseHealthTracker.Stop();
			}
			if (this.DatabaseHealthMonitor != null)
			{
				this.DatabaseHealthMonitor.Stop();
			}
		}
	}
}
