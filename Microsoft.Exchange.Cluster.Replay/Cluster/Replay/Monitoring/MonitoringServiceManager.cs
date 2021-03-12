using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001DA RID: 474
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonitoringServiceManager : IServiceComponent
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004CD05 File Offset: 0x0004AF05
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringWcfServiceTracer;
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004CD0C File Offset: 0x0004AF0C
		public MonitoringServiceManager(IDatabaseHealthTracker healthTracker)
		{
			this.m_healthTracker = healthTracker;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004CD1B File Offset: 0x0004AF1B
		public string Name
		{
			get
			{
				return "Monitoring WCF Service";
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004CD22 File Offset: 0x0004AF22
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.MonitoringWcfService;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0004CD26 File Offset: 0x0004AF26
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0004CD29 File Offset: 0x0004AF29
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004CD2C File Offset: 0x0004AF2C
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004CD2F File Offset: 0x0004AF2F
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004CD38 File Offset: 0x0004AF38
		public bool Start()
		{
			Exception ex;
			this.m_service = MonitoringService.StartListening(this.m_healthTracker, out ex);
			return ex == null;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004CD5C File Offset: 0x0004AF5C
		public void Stop()
		{
			MonitoringServiceManager.Tracer.TraceDebug((long)this.GetHashCode(), "MonitoringServiceManager Stop() called.");
			if (this.m_service != null)
			{
				this.m_service.StopListening();
				this.m_service = null;
			}
			MonitoringServiceManager.Tracer.TraceDebug((long)this.GetHashCode(), "MonitoringServiceManager stopped!");
		}

		// Token: 0x0400073B RID: 1851
		private MonitoringService m_service;

		// Token: 0x0400073C RID: 1852
		private IDatabaseHealthTracker m_healthTracker;
	}
}
