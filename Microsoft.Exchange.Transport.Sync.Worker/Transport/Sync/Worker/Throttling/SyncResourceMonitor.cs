using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncResourceMonitor
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public SyncResourceMonitor(SyncLogSession syncLogSession, ResourceKey resourceKey, SyncResourceMonitorType syncResourceMonitorType)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.SyncLogSession = syncLogSession;
			this.resourceKey = resourceKey;
			this.syncResourceMonitorType = syncResourceMonitorType;
			this.resourceHealthMonitor = this.CreateResourceHealthMonitor(resourceKey);
			this.disabled = AggregationConfiguration.Instance.SyncResourceMonitorsDisabled.Contains(syncResourceMonitorType);
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		internal IResourceLoadMonitor ResourceHealthMonitor
		{
			get
			{
				return this.resourceHealthMonitor;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000CACA File Offset: 0x0000ACCA
		internal ResourceKey ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		internal SyncResourceMonitorType SyncResourceMonitorType
		{
			get
			{
				return this.syncResourceMonitorType;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000CADA File Offset: 0x0000ACDA
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		private protected SyncLogSession SyncLogSession { protected get; private set; }

		// Token: 0x060002AE RID: 686 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		internal static SyncResourceMonitorType IsAnyResourceUnhealthyOrUnknown(AggregationWorkItem workItem, IEnumerable syncResourceMonitors, out bool isAnyResourceUnhealthy, out bool isAnyResourceUnknown)
		{
			isAnyResourceUnhealthy = false;
			isAnyResourceUnknown = false;
			SyncResourceMonitorType result = SyncResourceMonitorType.Unknown;
			foreach (object obj in syncResourceMonitors)
			{
				SyncResourceMonitor syncResourceMonitor = (SyncResourceMonitor)obj;
				ResourceLoad resourceLoadIfEnabled = syncResourceMonitor.GetResourceLoadIfEnabled(workItem);
				isAnyResourceUnhealthy |= (resourceLoadIfEnabled.State == ResourceLoadState.Critical);
				if (resourceLoadIfEnabled.State == ResourceLoadState.Critical)
				{
					result = syncResourceMonitor.syncResourceMonitorType;
				}
				isAnyResourceUnknown |= (resourceLoadIfEnabled.State == ResourceLoadState.Unknown);
			}
			return result;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		protected virtual ResourceLoad GetResourceLoad(AggregationWorkItem workItem)
		{
			ResourceLoadDelayInfo.Initialize();
			return this.ResourceHealthMonitor.GetResourceLoad(WorkloadType.TransportSync, false, null);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CB92 File Offset: 0x0000AD92
		protected virtual IResourceLoadMonitor CreateResourceHealthMonitor(ResourceKey resourceKey)
		{
			SyncUtilities.ThrowIfArgumentNull("resourceKey", resourceKey);
			return ResourceHealthMonitorManager.Singleton.Get(resourceKey);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		private ResourceLoad GetResourceLoadIfEnabled(AggregationWorkItem workItem)
		{
			if (this.disabled)
			{
				return ResourceLoad.Zero;
			}
			return this.GetResourceLoad(workItem);
		}

		// Token: 0x04000178 RID: 376
		private readonly bool disabled;

		// Token: 0x04000179 RID: 377
		private ResourceKey resourceKey;

		// Token: 0x0400017A RID: 378
		private SyncResourceMonitorType syncResourceMonitorType;

		// Token: 0x0400017B RID: 379
		private IResourceLoadMonitor resourceHealthMonitor;
	}
}
